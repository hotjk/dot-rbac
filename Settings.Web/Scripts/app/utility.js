define('utility-js', ['jquery'], function ($) {
    'use strict';

    var App = {
        hideMessage: function () {
            var alert = $('div.alert:hidden').fadeIn(500);
            setTimeout(function () { alert.fadeOut(500); }, 2000);
        },
        showMessage: function (message, css) {
            $('div.alert').remove();
            var alert = $('<div class="alert ' + css + '" role="alert" id="div_message"></div>')
            .text(message).hide().appendTo($('#panel_message')).fadeIn(500);
            setTimeout(function () { alert.fadeOut(500); }, 2000);
        }
    };
    $(document).ready(function () {
        App.hideMessage();
    });
    return App;
});