$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddFooterLegalSectionForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';
        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/SocialMediaAccount/SaveFooterLegalSection';
            var formData = BindAddSocialMediaAccountFormData();

            $.ajax({

                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                url: url,

                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        showSuccessSwal(successMsg);
                    }
                    setTimeout(function () {
                        window.location.href = '/WebsiteCMS/SocialMediaAccount/LegalList';
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    showErrorSwal(error);
                  
                }
            });
        });
    });

});


function BindAddSocialMediaAccountFormData() {
    var frm = $('#AddFooterLegalSectionForm');
    var formData = new FormData(frm[0]);
    return formData;
}


$('#AddSocialMediaAccountForm').validate({
    rules: {
        Youtube: {
            required: true,
        },
        Twitter: {
            required: true,

        },

        Facebook: {
            required: true
        },
        Instagram: {
            required: true
        }
    },
    messages: {
        Youtube: {
            required: "Please provide Youtube url.",
        },
        Twitter: {
            required: "Please provide Twitter url.",

        },

        Facebook: {
            required: "Please provide Facebook url.",
        },
        Instagram: {
            required: "Please provide Instagram url.",
        }


    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        error.insertAfter($(element));
        //element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }

});