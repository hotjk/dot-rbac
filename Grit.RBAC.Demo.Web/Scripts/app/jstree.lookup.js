define('jstree-lookup-js', ['jquery', 'jstree'], function ($, jstree) {
    'use strict';
    var DUMMY_OPTION = "<option value=''></option>";
    var SEPERATPR = "&nbsp;&nbsp;&nbsp;&nbsp;";
    var STEP_JOIN_WITH = ".";
    var INDEX_ID = 0, INDEX_NAME = 1, INDEX_OBSOLETE = 2, INDEX_CHILDREN = 3;
    var INDEX_AUTO_ID = 0, INDEX_AUTO_NAME = 1, INDEX_AUTO_DEEPTH = 2, INDEX_AUTO_OBSOLETE = 3;
    var _lookups = [];

    // find node in node tree.
    var _findChild = function (node, id) {
        for (var i = 0; i < node[INDEX_CHILDREN].length; i++) {
            if (node[INDEX_CHILDREN][i][INDEX_ID] == id) {
                return node[INDEX_CHILDREN][i];
            }
        }
        return null;
    }

    // path is a reference parameter.
    var _findValuePath = function (node, id, path) {
        if (node[INDEX_ID] == id) {
            path.push(node);
            return true;
        }
        if (node[INDEX_CHILDREN] != null) {
            for (var i = 0; i < node[INDEX_CHILDREN].length; i++) {
                if (_findValuePath(node[INDEX_CHILDREN][i], id, path)) {
                    path.push(node);
                    return true;
                }
            }
        }
        return false;
    }

    // get string value with full path.
    var _getFullValue = function (tree, id) {
        var path = [];
        _findValuePath(tree.root, id, path);
        path = path.slice(0, path.length - 1).reverse();
        return $.map(path, function (v, i) { return v[INDEX_AUTO_NAME]; }).join(STEP_JOIN_WITH);
    }

    // remove all options from all select.
    var _clearSelects = function (select_array, from) {
        for (var i = from; i < select_array.length; i++) {
            select_array[i].find('option').remove().end();
        }
    }

    // hide not value option from all select.
    var _hideSelects = function (select_array) {
        $.each(select_array, function (i, v) {
            if (v.find('option').length <= 1) {
                v.hide();
            }
            else {
                v.show();
            }
        });
    }

    var _bindSelect = function (tree, textbox, select_array, deepth, parent, orignalValuePath, onlyLeafCanBeSelect, hideUnusedSelect) {
        var select = select_array[deepth];
        if (parent == null || parent[INDEX_CHILDREN] == null) {
            _clearSelects(select_array, deepth);
            if (hideUnusedSelect) {
                _hideSelects(select_array);
            }
            return;
        }
        _clearSelects(select_array, deepth);
        select.append(DUMMY_OPTION);
        $.each(parent[INDEX_CHILDREN], function (k, v) {
            if (v[INDEX_OBSOLETE] == 0 || $.grep(orignalValuePath, function (e) { return e[INDEX_ID] == v[INDEX_ID]; }).length != 0) {
                select.append('<option value="' + v[INDEX_ID] + '">' + v[INDEX_NAME] + '</option>');
            }
        });
        select.unbind().bind('change', function () {
            var val = $(this).val();
            var node = null;
            if (val != "") {
                node = _findChild(parent, parseInt(val, 10));
            }
            _bindSelect(tree, textbox, select_array, deepth + 1, node, orignalValuePath, onlyLeafCanBeSelect, hideUnusedSelect);
            if (!onlyLeafCanBeSelect || (node != null && node[INDEX_CHILDREN] == null)) {
                if (val == "" && deepth != 0) {
                    val = select_array[deepth - 1].val();
                }
                textbox.val(val).blur();
            }
            else {
                textbox.val('').blur();
            }
        });
        if (hideUnusedSelect) {
            _hideSelects(select_array);
        }
    }

    var _bindFlatSelect = function (tree, textbox, select, seperator) {
        if (seperator == null) {
            seperator = SEPERATPR;
        }
        var dataSource = [];
        _flotNode(tree.root, dataSource, 0);
        dataSource = dataSource.slice(1); // remove root node
        select.append(DUMMY_OPTION);
        var val = parseInt(textbox.val(), 10);
        $.each(dataSource, function (k, v) {
            if (v[INDEX_AUTO_OBSOLETE] == 0 || v[INDEX_ID] == val) {
                select.append('<option value="' + v[INDEX_ID] + '">' + Array(v[INDEX_AUTO_DEEPTH]).join(seperator) + v[INDEX_NAME] + '</option>');
            }
        });
        select.unbind().bind('change', function () {
            var val = $(this).val();
            textbox.val(val).blur();
        });
    }

    var _setValues = function (tree, select_array, value) {
        var path = [];
        _findValuePath(tree.root, value, path);
        path = path.slice(0, path.length - 1).reverse();
        if (path.length == 0) {
            select_array[INDEX_ID].val('').change();
        }
        else {
            for (var i = 0; i < path.length; i++) {
                select_array[i].val(path[i][INDEX_ID]).change();
            }
        }
    }

    var _findTree = function (treeID) {
        var tree = $.grep(_lookups, function (v, i) { return v.root[INDEX_ID] === treeID })[0];
        if (tree == null) {
            alert("Oops! tree not found, the id is " + treeID);
            return null;
        }
        return tree;
    }

    var _flotNode = function (node, list, deepth) {
        list.push([node[INDEX_ID], node[INDEX_NAME], deepth, node[INDEX_OBSOLETE]]);
        if (node[INDEX_CHILDREN] != null) {
            $.each(node[INDEX_CHILDREN], function (i, v) {
                _flotNode(v, list, deepth + 1);
            });
        }
    }

    return {
        // Must add tree before invoke below functions
        AddTree: function (tree) {
            _lookups.push(tree)
        },

        // Build one select, all tree node will be flot with indentation and show in the select.
        BindFlatLookup: function (textbox, treeID, seperator) {
            var tree = _findTree(treeID);
            if (tree == null) {
                return;
            }

            var container = $("<div class='lookup_container'></div>");
            textbox.before(container);
            var select = $("<select class='form-control'></select>");
            container.append(select);
            _bindFlatSelect(tree, textbox, select, seperator);

            textbox.unbind('change').bind('change', function () {
                select.val(textbox.val());
            }).change().hide();
        },

        // Build multiple select.
        BindLookup: function (textbox, treeID, onlyLeafCanBeSelect, hideUnusedSelect) {
            var tree = _findTree(treeID);
            if (tree == null) {
                return;
            }

            var select_array = [];
            var container = $("<div class='lookup_container'></div>");
            textbox.before(container);
            for (var i = 0; i < tree.deepth; i++) {
                var select = $("<select class='form-control'></select>");
                select_array.push(select);
                container.append(select);
            }
            _clearSelects(select_array, 0);

            var orignalValuePath = [];
            _findValuePath(tree.root, parseInt(textbox.val()), orignalValuePath);
            _bindSelect(tree, textbox, select_array, 0, tree.root, orignalValuePath, onlyLeafCanBeSelect, hideUnusedSelect);

            _setValues(tree, select_array, textbox.val());
            textbox.unbind('change').bind('change', function () {
                _setValues(tree, select_array, textbox.val());
            });
            textbox.hide();
        },

        // build auto complete for orignal textbox, jQuery UI autocomplete is required.
        BindAutoComplete: function (textbox, treeID, onlyShowFirstLevelNodes, fullValue, seperator) {
            var tree = _findTree(treeID);
            if (tree == null) {
                return;
            }
            if (seperator == null) {
                seperator = SEPERATPR;
            }
            var dataSource = [];
            if (onlyShowFirstLevelNodes == true) {
                $.each(tree.root[INDEX_CHILDREN], function (i, v) {
                    dataSource.push([v[INDEX_ID], v[INDEX_NAME], 1, v[INDEX_OBSOLETE]]);
                });
            }
            else {
                _flotNode(tree.root, dataSource, 0);
                dataSource = dataSource.slice(1); // remove root node
            }
            dataSource = $.grep(dataSource, function (v, i) { return v[INDEX_AUTO_OBSOLETE] == 0; });
            textbox.autocomplete({
                minLength: 0,
                source: dataSource,
                focus: function (event, ui) {
                    textbox.val(ui.item[INDEX_AUTO_NAME]);
                    return false;
                },
                select: function (event, ui) {
                    if (fullValue == true) {
                        textbox.val(_getFullValue(tree, ui.item[INDEX_AUTO_ID]));
                    }
                    else {
                        textbox.val(ui.item[INDEX_AUTO_NAME]);
                    }
                    return false;
                }
            })
            .data("ui-autocomplete")._renderItem = function (ul, item) {
                return $("<li>")
                  .append("<a>" + Array(item[INDEX_AUTO_DEEPTH]).join(seperator) + item[INDEX_AUTO_NAME] + "</a>")
                  .appendTo(ul);
            };
            $.each($(".ui-autocomplete"), function (i, v) {
                if (!$(v).hasClass("dropdown-menu")) {
                    $(v).addClass("dropdown-menu");
                }
            });
        },

        Contain: function (treeID, nodeValue) {
            var tree = _findTree(treeID);
            if (tree == null) {
                return true;
            }
            var path = [];
            return _findValuePath(tree.root, nodeValue, path);
        }
    };
});