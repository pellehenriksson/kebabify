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
        var text = $('#kebab-textbox').val();

        var temp = document.createElement("textarea");
        document.body.appendChild(temp);

        temp.value = text;
        temp.select();

        document.execCommand("copy");
        document.body.removeChild(temp);
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
