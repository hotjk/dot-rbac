// singleton
define('jstree-lookup-js', ['jquery'], function ($) {
    'use strict';
    var DUMMY_OPTION = "<option value=''></option>";
    var SEPERATPR = "&nbsp;&nbsp;&nbsp;&nbsp;";
    var STEP_JOIN_WITH = ".";
    var INDEX_ID = 0, INDEX_NAME = 1, INDEX_OBSOLETE = 2, INDEX_CHILDREN = 3;
    var INDEX_AUTO_ID = 0, INDEX_AUTO_NAME = 1, INDEX_AUTO_DEPTH = 2, INDEX_AUTO_OBSOLETE = 3;
    var _trees = {};

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

    // remove all options from all select.
    var _clearSelects = function (select_array, from) {
        for (var i = from; i < select_array.length; i++) {
            select_array[i].find('option').remove().end();
        }
    }

    // hide not value option from all select.
    var _hideSelects = function (select_array) {
        $.each(select_array, function (i, v) {
            v.toggle(v.find('option').length > 0);
        });
    }

    var _bindSelect = function (tree, textbox, select_array, depth, parent, orignalValuePath, onlyLeafCanBeSelect, hideUnusedSelect) {
        var select = select_array[depth];
        if (parent == null || parent[INDEX_CHILDREN] == null) {
            _clearSelects(select_array, depth);
            if (hideUnusedSelect) {
                _hideSelects(select_array);
            }
            return;
        }
        _clearSelects(select_array, depth);
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
            _bindSelect(tree, textbox, select_array, depth + 1, node, orignalValuePath, onlyLeafCanBeSelect, hideUnusedSelect);
            if (!onlyLeafCanBeSelect || (node != null && node[INDEX_CHILDREN] == null)) {
                if (val == "" && depth != 0) {
                    val = select_array[depth - 1].val();
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
                select.append('<option value="' + v[INDEX_ID] + '">' + Array(v[INDEX_AUTO_DEPTH]).join(seperator) + v[INDEX_NAME] + '</option>');
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


    var _flotNode = function (node, list, depth) {
        list.push([node[INDEX_ID], node[INDEX_NAME], depth, node[INDEX_OBSOLETE]]);
        if (node[INDEX_CHILDREN] != null) {
            $.each(node[INDEX_CHILDREN], function (i, v) {
                _flotNode(v, list, depth + 1);
            });
        }
    }

    return {
        // MUST add tree before invoke bind functions
        AddTree: function (key, tree) {
            _trees[key] = tree;
        },

        // Build one drop down list, all tree node will be flat and indentation.
        BindFlatLookup: function (textbox, key, seperator) {
            var tree = _trees[key];
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
        BindLookup: function (textbox, key, onlyLeafCanBeSelect, hideUnusedSelect) {
            var tree = _trees[key];
            if (tree == null) {
                return;
            }

            var select_array = [];
            var container = $("<div class='lookup_container'></div>");
            textbox.before(container);
            for (var i = 0; i < tree.depth; i++) {
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

        Contain: function (treeID, nodeValue) {
            var tree = _trees[key];
            if (tree == null) {
                return true;
            }
            var path = [];
            return _findValuePath(tree.root, nodeValue, path);
        }
    };
});