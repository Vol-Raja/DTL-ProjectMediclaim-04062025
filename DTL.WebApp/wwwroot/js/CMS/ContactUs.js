$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddContactUsForm").valid() == false)
            return false;
        var IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';
        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/ContactUs/Save';
            var formData = BindAddContactUsFormData();

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
                        window.location.href = '/WebsiteCMS/ContactUs';
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    showErrorSwal("Error - " + error);
                  
                }
            });
        });
    });

});


function BindAddContactUsFormData() {
    var frm = $('#AddContactUsForm');
    var formData = new FormData(frm[0]);
    return formData;
}

$('#AddContactUsForm').validate({
    rules: {
        Name: {
            required: true,
        },
        NameHindi: {
            required: true,

        },
        Designation: {
            required: true,
        },
        DesignationHindi: {
            required: true
        },
        EmailAddress: {
            required: true,
            email: true
        },
        PhoneNumber: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 10
        },
        Telephone: {
            required: true,
            number: true,
            minlength: 8,
            maxlength: 8
        }
    },
    messages: {
        Name: {
            required: "Please provide Name In English.",
        },
        NameInHindi: {
            required: "Please provide Name In Hindi.",

        },
        Designation: {
            required: "Please provide Designation.",
        },
        DesignationHindi: {
            required: "Please provide Designation in Hindi.",
        },
        EmailAddress: {
            required: "Please provide email.",
        },
        PhoneNumber: {
            required: "Please provide mobile number.",
        },
        Telephone: {
            required: "Please provide telephone.",
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