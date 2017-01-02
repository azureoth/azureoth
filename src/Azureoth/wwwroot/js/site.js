(function () {
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