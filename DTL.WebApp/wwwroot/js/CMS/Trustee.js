
$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();

        if ($("#AddEditTrusteeForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/Trustee/Save';
            var formData = BindAddEditTrusteeFormData();
            showLoader()
            $.ajax({

                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                url: url,

                success: function (response, status, xhr) {
                    if (xhr.status == 200) {
                        hideLoader(response);
                        console.log(response)
                        showSuccessSwal(successMsg);
                     
                    }
                    setTimeout(function () {
                        window.location.href = '/WebsiteCMS/Trustee';
                    }, 4000);
                },
                error: function (xhr, status, error) {
                    hideLoader();
                    console.error(xhr);
                    showErrorSwal("Error! " + error);
                   
                }
            });
        });
    });

});



function BindAddEditTrusteeFormData() {
    var frm = $('#AddEditTrusteeForm');
    var formData = new FormData(frm[0]);
    return formData;
}

$('#AddEditTrusteeForm').validate({
    rules: {
        Image: {
            required: function (element) {
                var status = $(".file-required").val();
                return status == 1;
            }
        },
        NameEnglish: {
            required: true

        },
        NameHindi: {
            required: true,
        },
        PositionHindi: {
            required: true,
        },
        PositionEnglish: {
            required: true,
        },
        Phone: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 10

        }

    },
    messages: {
        Title: {
            required: "Please provide Description.",
        },
        NameEnglish: {
            required: "Please provide Name In English.",

        },
           NameHindi: {
               required: "Please provide Name  In Hindi.",
        },
        PositionEnglish: {
            required: "Please provide Trustee Position.",

        },
        PositionHindi: {
            required: "Please provide Trustee Position In Hindi.",

        },
        PositionHindi: {
            required: "Please provide Title In Hindi.",
        },
        Phone: {
            required: "Please provide Phone Number.",
        },
        Image: {
            required: "Please provide Image.",
        }
    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        error.insertAfter($(element));
        element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }

});