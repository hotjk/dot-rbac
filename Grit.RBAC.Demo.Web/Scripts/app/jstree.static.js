define('jstree-static-js', ['jquery', 'jstree'], function ($, jstree) {
    'use strict';
    var App = function (treeControl, treeJson) {
        var $treeControl = treeControl;
        $treeControl.jstree({
            'core': {
                'themes': {
                    'name': 'proton',
                    'responsive': true
                },
                "multiple": false,
                'check_callback': true,
                'data': treeJson || []
            },
            'plugins': ['unique']
        });
        var $tree = $treeControl.jstree(true);

        return {
            tree: $tree,
            treeControl : $treeControl
        }
    };
    return App;
});
