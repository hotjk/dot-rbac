define('jstree-static-js', ['jquery', 'jstree'], function ($, jstree) {
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
            treeControl: $treeControl,
            data: function () {
                var json_data = $tree.get_json('#', { 'no_state': true });
                pick(json_data, ['id', 'data', 'children']);
                return json_data;
            },
            destroy: function () {
                if ($tree != null) {
                    $tree.destroy();
                    $tree = null;
                }
            }
        }
    };
    return App;
});
