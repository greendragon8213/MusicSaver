$(function() {
    $("#contact-me-form input").jqBootstrapValidation({
        preventSubmit: true,
        submitError: function($form, event, errors) {
        },
        submitSuccess: function($form, event) {
            $('#contact-me-form').block({
                message: '<i class="fa fa-spinner fa-spin fa-5x fa-fw" style="color: white"></i>',
                css: { 'background-color': 'transparent', 'border': 'none' }
            });
            event.preventDefault();
            var contactMeBody = {
                "Name": $("#contact-me-form #name").val(),
                "Email": $('#contact-me-form #email').val(),
                "Message": $('#contact-me-form #message').val()
            }

            $.ajax({
                url: urls.sendContactMeUrl,
                data: contactMeBody,
                dataType: "json",
                type: "POST",
                success: function() {
                    $("#contact-me-form input[type='submit']").attr("disabled", true);
                    $('#contact-me-form .request-result').html("<div class='alert alert-success'>");
                    $('#contact-me-form .request-result > .alert-success').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
                        .append("</button>");
                    $('#contact-me-form .request-result > .alert-success')
                        .append("<strong>Success! </strong>Your message has been sent.");
                    $('#contact-me-form .request-result > .alert-success')
                        .append('</div>');
                    $('#contact-me-form').trigger("reset");
                },
                error: function() {
                    $('#contact-me-form .request-result').html("<div class='alert alert-danger'>");
                    $('#contact-me-form .request-result > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
                        .append("</button>");
                    $('#contact-me-form .request-result > .alert-danger').append("<strong>Error! </strong>Sorry, something went wrong. Please try again later!");
                    $('#contact-me-form .request-result > .alert-danger').append('</div>');
                    $('#contact-me-form').trigger("reset");
                },
                complete: function() {
                    $('#contact-me-form').unblock();
                }
            });
        },
        filter: function() {
            return $(this).is(":visible");
        }
    });

    $('#contact-me-form #name, #contact-me-form #email, #contact-me-form #message').focus(function () {
        $('#contact-me-form .request-result').html('');
    });
});