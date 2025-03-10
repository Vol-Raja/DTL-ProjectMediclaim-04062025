$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        var ishelpline = $('#IsHelpline').val() == "True";
        if ($("#AddRuleActForm").valid() == false)
            return false;

        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/RuleActHelpline/Save';
            var formData = BindAddFormFormData();

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
                        window.location.href = '/WebsiteCMS/RuleActHelpline?IsHelpline=' + ishelpline;
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    showErrorSwal("Error! " + error);
                  
                }
            });
        });
    });

});


function BindAddFormFormData() {
    var frm = $('#AddRuleActForm');
    var formData = new FormData(frm[0]);
    return formData;
}


$('#AddRuleActForm').validate({
    rules: {
        TitleInEnglish: {
            required: true,
        },
        TitleInHindi: {
            required: true,

        },
        HelplineDescription: {
            required: true,
        },
        HelplineDescriptionInHindi: {
            required: true,

        },
        ContactNumber: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 10
        },
        ContactNumberInHindi: {
            required: true,
         
            minlength: 10,
            maxlength: 10
        },

        AttachmentFileInEnglish: {
            required: function (element) {
                var status = $("#isEnglishFileRequired").val();
                return status == 1;
            }
        },
        AttachmentFileInHindi: {
            required: function (element) {
                var status = $("#isHindiFileRequired").val();
                return status == 1;
            }
        }
    },
    messages: {
        TitleInEnglish: {
            required: "Please provide name In English.",
        },
        TitleInHindi: {
            required: "Please provide name In Hindi.",
        }
        ,
        ContactNumber: {
            required: "Please provide Contact Number In English.",
        },
        ContactNumberInHindi: {
            required: "Please provide Contact Number In Hindi.",
        },
        AttachmentFileInEnglish: {
            required: "Please provide Attachment File In English.",
        },
        AttachmentFileInHindi: {
            required: "Please provide Attachment File InHindi.",
        }
        ,
        HelplineDescriptionInHindi: {
            required: "Please provide Helpline Description In Hindi.",
        }
        , HelplineDescription: {
            required: "Please provide Helpline Description.",
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