﻿define('jstree-group-js', ['jquery', 'jstree'], function ($, jstree) {
    'use strict';
    function pick(collection, keys) {
        for (var i = 0; i < collection.length; i++) {
            var obj = collection[i];
            var copy = {};
            $.each(keys, function (i, key) {
                if (key in obj) {
                    copy[key] = obj[key];
                    if ($.isArray(copy[key])) {
                        if (copy[key].length == 0) {
                            delete copy[key];
                        }
                        else {
                            pick(copy[key], keys);
                        }
                    }
                }
            });
            collection[i] = copy;
        }
    };

    var App = function (treeControl, treeJson) {
        var $treeControl = null;
        var $tree = null;
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
        return {
            treeControl: $treeControl,
            tree: $tree,
            data: function () {
                var json_data = $tree.get_json('#', { 'no_state': true });
                pick(json_data, ['id', 'data', 'children']);
                return json_data;
            }
        };
    };
    return App;
});
