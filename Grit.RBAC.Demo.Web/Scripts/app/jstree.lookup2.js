/*
## tree structure
{
  "depth": 3, // max depth of tree
  "root": [   // tree node, the root array should only have one node
    8,        // node id
    null,     // node name(text)
    0,        // obsolete?
    [         // children
      ...
    ]
  ]
}
## tree demo
{"depth":3,"root":[8,null,0,[[5,"Order",0,[[4,"Invoice",0],[2,"Pay Order",0,[[21,"Close Order",0]]],[3,"Shipment",0,[[22,"Copy",0]]]]],[1,"Create Order",0],[10,"Print",0],[20,"Query",0]]]}

## jsTree structure
{
  "text": "Order",
  "icon": null,
  "data": {
    "content": 5,
    "elements": null
  },
  "children": [
    ...
  ]
}
## jsTree demo
{"text":null,"icon":null,"data":null,"children":[{"text":"Order","icon":null,"data":{"content":5,"elements":null},"children":[{"text":"Invoice","icon":null,"data":{"content":4,"elements":null},"children":null,"state":{"opened":true,"disabled":false,"selected":false}},{"text":"Pay Order","icon":null,"data":{"content":2,"elements":null},"children":[{"text":"Close Order","icon":null,"data":{"content":21,"elements":null},"children":null,"state":{"opened":true,"disabled":false,"selected":false}}],"state":{"opened":true,"disabled":false,"selected":false}},{"text":"Shipment","icon":null,"data":{"content":3,"elements":null},"children":[{"text":"Copy","icon":null,"data":{"content":22,"elements":null},"children":null,"state":{"opened":true,"disabled":false,"selected":false}}],"state":{"opened":true,"disabled":false,"selected":false}}],"state":{"opened":true,"disabled":false,"selected":false}},{"text":"Create Order","icon":null,"data":{"content":1,"elements":null},"children":null,"state":{"opened":true,"disabled":false,"selected":false}},{"text":"Print","icon":null,"data":{"content":10,"elements":null},"children":null,"state":{"opened":true,"disabled":false,"selected":false}},{"text":"Query","icon":null,"data":{"content":20,"elements":null},"children":null,"state":{"opened":true,"disabled":false,"selected":false}}],"state":null}
*/

define('jstree-lookup2-js', ['jquery', 'jstree-static-js'], function ($, treeStatic) {
    'use strict';
    var DUMMY_OPTION = "<option value=''></option>";
    var SEPERATPR = "&nbsp;&nbsp;&nbsp;&nbsp;";
    var STEP_JOIN_WITH = ".";
    var NODE_ID = 0, NODE_NAME = 1, NODE_OBSOLETE = 2, NODE_CHILDREN = 3;
    var FLAT_ID = 0, FLAT_NAME = 1, FLAT_OBSOLETE = 2, FLAT_DEPTH = 3;
    var _trees = {};
    var _jsTrees = {};

    // find node in node tree.
    var _findChild = function (node, id) {
        for (var i = 0; i < node[NODE_CHILDREN].length; i++) {
            if (node[NODE_CHILDREN][i][NODE_ID] == id) {
                return node[NODE_CHILDREN][i];
            }
        }
        return null;
    }

    // find a node and return the node path.
    // path is a reference parameter.
    var _findValuePath = function (node, id, path) {
        if (node[NODE_ID] == id) {
            path.push(node);
            return true;
        }
        if (node[NODE_CHILDREN] != null) {
            for (var i = 0; i < node[NODE_CHILDREN].length; i++) {
                if (_findValuePath(node[NODE_CHILDREN][i], id, path)) {
                    path.push(node);
                    return true;
                }
            }
        }
        return false;
    }

    // find js tree node
    var _findTreeNode = function (node, value) {
        if (node.data != null && node.data.content == value) {
            return node;
        }
        if (node.children != null) {
            for (var i = 0; i < node.children.length; i++) {
                var found = _findTreeNode(node.children[i], value);
                if (found != null) return found;
            }
        }
        return null;
    }

    // remove all options from all select.
    var _clearSelects = function (select_array, from) {
        for (var i = from; i < select_array.length; i++) {
            select_array[i].find('option').remove().end();
        }
    }

    // hide no value option from all select.
    var _hideSelects = function (select_array) {
        $.each(select_array, function (i, v) {
            v.toggle(v.find('option').length > 0);
        });
    }

    var _bindSelect = function (tree, textbox, select_array, depth, parent, orignalValuePath, onlyLeafCanBeSelect, hideUnusedSelect) {
        var select = select_array[depth];
        if (parent == null || parent[NODE_CHILDREN] == null) {
            _clearSelects(select_array, depth);
            if (hideUnusedSelect) {
                _hideSelects(select_array);
            }
            return;
        }
        _clearSelects(select_array, depth);
        select.append(DUMMY_OPTION);
        $.each(parent[NODE_CHILDREN], function (k, v) {
            if (v[NODE_OBSOLETE] == 0 || $.grep(orignalValuePath, function (e) { return e[NODE_ID] == v[NODE_ID]; }).length != 0) {
                select.append('<option value="' + v[NODE_ID] + '">' + v[NODE_NAME] + '</option>');
            }
        });
        select.unbind().bind('change', function () {
            var val = $(this).val();
            var node = null;
            if (val != "") {
                node = _findChild(parent, parseInt(val, 10));
            }
            _bindSelect(tree, textbox, select_array, depth + 1, node, orignalValuePath, onlyLeafCanBeSelect, hideUnusedSelect);
            if (!onlyLeafCanBeSelect || (node != null && node[NODE_CHILDREN] == null)) {
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
            if (v[FLAT_OBSOLETE] == 0 || v[FLAT_ID] == val) {
                select.append('<option value="' + v[FLAT_ID] + '">' + Array(v[FLAT_DEPTH]).join(seperator) + v[FLAT_NAME] + '</option>');
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
            select_array[NODE_ID].val('').change();
        }
        else {
            for (var i = 0; i < path.length; i++) {
                select_array[i].val(path[i][NODE_ID]).change();
            }
        }
    }

    /*
    Convert a node tree to flot node list
    ## flot node list structure
    [
      [       // node
        8,    // node id
        null, // node text
        0,    // depth
        0     // obsolete?
      ]
    ...
    ]    
    ##flot node list demo
    [[8, null, 0, 0], [5, "Order", 1, 0], [4, "Invoice", 2, 0], [2, "Pay Order", 2, 0], [21, "Close Order", 3, 0], [3, "Shipment", 2, 0], [22, "Copy", 3, 0], [1, "Create Order", 1, 0], [10, "Print", 1, 0], [20, "Query", 1, 0]]
*/
    var _flotNode = function (node, list, depth) {
        list.push([node[NODE_ID], node[NODE_NAME], node[NODE_OBSOLETE], depth]);
        if (node[NODE_CHILDREN] != null) {
            $.each(node[NODE_CHILDREN], function (i, v) {
                _flotNode(v, list, depth + 1);
            });
        }
    }

    return {
        // MUST add tree before invoke bind functions
        AddTree: function (key, tree) {
            _trees[key] = tree;
        },

        AddJsTree: function(key, tree) {
            _jsTrees[key] = tree;
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

        BindJsTree: function (textbox, key, onlyLeafCanBeSelect) {
            var tree = _jsTrees[key];
            if (tree == null) {
                return true;
            }

            var container = $("<div class='lookup_container'></div>");
            textbox.before(container);

            var theTree = treeStatic(container, tree.children);
            theTree.treeControl.on('select_node.jstree', function (e, data) {
                var node = theTree.tree.get_node(data.selected[0]);
                if (onlyLeafCanBeSelect && node.children.length > 0) {
                    theTree.deselect();
                    textbox.val('').blur();
                    return;
                }
                var found = node.data.content;
                textbox.val(found).blur();
            }).on('deselect_node.jstree', function (e, data) {
                textbox.val('').blur();
            }).on('ready.jstree', function () {
                theTree.select(textbox.val());
            });

            textbox.unbind('change').bind('change', function () {
                theTree.select(textbox.val());
            }).hide();
        },

        BindJsTreePicker: function (textbox, key, modal, title, onlyLeafCanBeSelect) {
            var tree = _jsTrees[key];
            if (tree == null) {
                return true;
            }

            var container = $("<div class='lookup_container'></div>");
            textbox.before(container);
            var input = $("<input class='form-control' readonly></input>");
            container.append(input);

            textbox.unbind('change').bind('change', function () {
                var node = _findTreeNode(tree, parseInt(textbox.val()));
                input.val(node != null ? node.text : '');
            }).change().hide();
            
            input.unbind('focus').bind('focus', function () {
                modal.find('.modal-title').text(title);
                var theTree = treeStatic(modal.find('.modal-body'), tree.children);
                theTree.treeControl.on('select_node.jstree', function (e, data) {
                    var node = theTree.tree.get_node(data.selected[0]);
                    if (onlyLeafCanBeSelect && node.children.length > 0) {
                        theTree.deselect();
                        textbox.val('').change().blur();
                        return;
                    }
                    var found = node.data.content;
                    textbox.val(found).change().blur();
                }).on('deselect_node.jstree', function (e, data) {
                    textbox.val('').change().blur();
                }).on('ready.jstree', function () {
                    theTree.select(textbox.val());
                });
                
                modal.modal().on('hidden.bs.modal', function (e) {
                    theTree.destroy();
                });
            });
        }
    };
});