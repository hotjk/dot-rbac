define('jstree-select-js', ['jquery', 'jstree'], function ($, jstree) {
    'use strict';

    var App = function (treeControl, treeJson) {
        var $treeControl = treeControl;
        $treeControl.jstree({
            'core': {
                'themes': {
                    'name': 'proton',
                    'responsive': true
                },
                'data': treeJson || []
            },
            'checkbox': {
                'three_state': false,
                'visible': false
            },
            'plugins': ['unique', 'checkbox']
        });
        var $tree = $treeControl.jstree(true);

        return {
            tree: $tree,
            treeControl: $treeControl,
            data: function () {
                var checked = $tree.get_checked();
                var ids = [];
                $.each(checked, function (i, v) {
                    ids.push($tree.get_node(v).data.content);
                });
                return ids;
            }
        }
    };
    return App;
});
