$(document).ready(function () {
    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#HospitalUserForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit ?' : 'Are you sure you want update ?';

        showConfirmSwal(msg, function () {
            var formData = BindHospitalUserFormData();
            var url = '/UserManagement/HospitalUser/Create';
            $.ajax({

                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                url: url,


                success: function (response, status, xhr) {
                    if (xhr.status == 200) {
                        if (response == 'userExists') {
                            hideLoader();
                            showErrorSwal("Email Id is already used");
                            return;
                        }
                        if (response == 'DuplicateUserName') {
                            hideLoader();
                            showErrorSwal("Username is already used");
                            return;
                        }
                        else {
                            showSuccessSwal(successMsg);
                        }
                    }

                    setTimeout(function () {
                        window.location.href = '/UserManagement/HospitalUser/HospitalUserList';
                    }, 900);
                },
                error: function (xhr, status, error) {
                    hideLoader();
                    showErrorSwal('Error-' + error);
                    //toastr.error("Something went wrong!!")
                }
            });
        });
    })
});

//Self Invoking function to populate the table when page loads
(function () {
    //$(".showDependent").hide();
    var mode = $('#hdnhospitalUserFormMode').val();
    if (mode === 'Edit') {
        LoadHospitalUser($('#hdnhospitaluserid').val());
    }
})();


$(".generateHospitalUserCredential").click(function () {
    showLoader();
    if (confirm("Are you sure you want to generate credential?")) {
        $.ajax('/UserManagement/HospitalUser/GenerateHospitalUserCredentialById', {
            type: 'POST',  // http method
            data: { hospitaluserid: $(this).attr("data-hospitaluserid") },  // data to submit
            success: function (data, status, xhr) {
                hideLoader();
                if (data == "success") {
                    toastrSuccess("DVBUser credential has generated and sent to registerd email id and mobile number.");
                    setTimeout(function () { window.location.reload(); }, 5000)

                }
                else {
                    toastrWarning("Please try again. somehthing went wrong")
                }
            },
            error: function (jqXhr, textStatus, errorMessage) {
                hideLoader();
                toastrError("Something went wrong!!!")
            }
        });
    }
})

function BindHospitalUserFormData() {
    var frm = $('#HospitalUserForm');
    var formData = new FormData(frm[0]);
    return formData;
}

function DeleteHospitalUser(Id, rw) {


    $.ajax('/UserManagement/HospitalUser/Delete/' + Id, {
        type: 'DELETE',  // http method
        data: '',  // data to submit
        success: function (data, status, xhr) {

            if (xhr.status == "200") {
                showSuccessSwal("Hospital User delete successfully!")
                $('#deleteModal').modal('hide');

                $("#" + Id).remove();
            }

        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('#deleteModal').modal('hide');
            showErrorSwal('Error:' + errorMessage)
        }
    });

}

function showLoader() {
    //$('.preloader').css()
    $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
    $(".preloader img").css({ "display": "block" });
};

function hideLoader() {
    $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
    $(".preloader img").css({ "display": "none" });
};


$('#HospitalUserForm').validate({
    rules: {
        NameOfHospital: {
            required: true,
        },
        EmailAddress: {
            required: true,
            email: true

        }
        , PhoneNumber: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 10

        }, AuthorizedPerson: {
            required: true

        }
        , Address: {
            required: true

        },
        Username: {
            required: true,
            minlength: 6,
            regex: /^[a-zA-Z0-9]+$/,
        },
    },
    messages: {
        NameOfHospital: {
            required: "Please provide Name Of Hospital.",
        },
        EmailAddress: {
            required: "Please provide EmailAddress.",

        }
        , PhoneNumber: {
            required: "Please provide Phone Number.",

        }, AuthorizedPerson: {
            required: "Please provide Name Authorized Person.",

        }
        , Address: {
            required: "Please provide Address.",

        }, Username: {
            required: "Please provide Username.",

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
$('#example1').DataTable({
    "ordering": false,
    "sScrollX": "100%",
    "sScrollXInner": "100%",
    "bAutoWidth": false,
    "bJQueryUI": true,
    "bRetrieve": true,
    "bAutoWidth": true
});
