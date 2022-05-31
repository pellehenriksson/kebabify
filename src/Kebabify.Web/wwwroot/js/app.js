var Kebabify = (function ($, window, app) {

    app.Make = app.Make || {};

    function makeKebab() {
        $.ajax({
            url: '/api/kebab/',
            type: 'post',
            data: JSON.stringify({ input: $('#input-textbox').val() }),
            contentType: 'application/json',
            success: function (response) {
                $('#kebab-textbox').val(response.kebab);
            }
        });
    }

    function copy() {
        $('#kebab').select();
        document.execCommand('copy');
    }

    function initEventHandlers() {
        $('#make-kebab-button').click(function () {
            makeKebab();
        });

        $('#copy-button').click(function () {
            copy();
        });
    }

    app.Make.init = function () {
        initEventHandlers();
    };

    return app;

})(jQuery, window, Kebabify || {});
