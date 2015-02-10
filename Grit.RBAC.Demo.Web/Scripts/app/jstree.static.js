define('jstree-static-js', ['jquery', 'jstree'], function ($, jstree) {
    'use strict';
    var $treeControl = null;
    var $tree = null;

    var App = {
        init: function (treeControl, treeJson) {
            $treeControl = treeControl;
            $treeControl.jstree({
                'core': {
                    'themes': {
                        'name': 'proton',
                        'responsive': true
                    },
                    'check_callback': true,
                    'data': treeJson || []
                },
                'plugins': ['dnd', 'unique']
            });
            $tree = $treeControl.jstree(true);
        },
        data: function () {
            var json_data = $tree.get_json('#', { 'no_state': true });
            pick(json_data, ['id', 'data', 'children']);
            return json_data;
        }
    };
    return App;
});
