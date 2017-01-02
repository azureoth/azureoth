(function () {
    var jsEditor;
    $(document).ready(activate);

    function activate() {
        $('#new-app').click(function () {
            $('#new-app-form').toggleClass('hidden');
            return false;
        });

        $('#new-app-form').submit(function () {
            submitForm(this);
            return false;
        });

        $('#submit-schema').click(function () {
            var self = $(this),
                success = $('.success'),
                error = $('.error'),
                appId = self.attr('data-app-id');

            self.attr('disabled', 'disabled');
            success.addClass('hidden');
            error.addClass('hidden');

            console.log(jsEditor.get());

            $.ajax({
                type: 'POST',
                url: '/apps/' + appId + '/schema',
                data: JSON.stringify(jsEditor.get()),
                contentType: 'application/json'
            }).done(function () {
                success.removeClass('hidden');
            }).fail(function () {
                error.removeClass('hidden');
            }).always(function () {
                self.removeAttr('disabled');
            });

            return false;
        });

        var jsContainer = document.getElementById("jsoneditor");

        if (jsContainer) {
            var options = {
                mode: 'text'
            };

            jsEditor = new JSONEditor(jsContainer, options);
        }
    }

    function serializeForm(form) {
        var obj = {};
        $.each($(form).serializeArray(), function (k, v) {
            obj[v.name] = v.value;
        });
        return JSON.stringify(obj);
    }

    function submitForm(form) {
        var error = $('.alert-danger', form),
            submit = $('.submit', form),
            postUrl = $(form).attr('data-action'),
            data = serializeForm(form);

        submit.attr('disabled', 'disabled');
        error.addClass('hidden');

        $.ajax({
            type: 'POST',
            url: postUrl,
            data: data,
            contentType: 'application/json'
        }).done(function () {
            location.reload();
        }).fail(function () {
            error.removeClass('hidden');
        }).always(function () {
            submit.removeAttr('disabled');
        });
    }
})();