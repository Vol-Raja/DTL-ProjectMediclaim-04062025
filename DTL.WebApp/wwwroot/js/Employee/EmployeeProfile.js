
$(document).on("change", ".uploadProfileInput", function () {
    var triggerInput = this;
    var currentImg = $(this).closest(".pic-holder").find(".pic").attr("src");
    var holder = $(this).closest(".pic-holder");
    var wrapper = $(this).closest(".profile-pic-wrapper");
    $(wrapper).find('[role="alert"]').remove();
    var files = !!this.files ? this.files : [];
    if (!files.length || !window.FileReader) {
        return;
    }
    if (/^image/.test(files[0].type)) {
        // only image file
        var reader = new FileReader(); // instance of the FileReader
        reader.readAsDataURL(files[0]); // read the local file

        reader.onloadend = function () {
            $(holder).addClass("uploadInProgress");
            $(holder).find(".pic").attr("src", this.result);
            $(holder).append(
                '<div class="upload-loader"><div class="spinner-border text-primary" role="status"><span class="sr-only">Loading...</span></div></div>'
            );

            // Dummy timeout; call API or AJAX below
            setTimeout(() => {
                $(holder).removeClass("uploadInProgress");
                $(holder).find(".upload-loader").remove();
                // If upload successful
                if (Math.random() < 0.9) {
                    $(wrapper).append(
                        //'<div class="snackbar show" role="alert"><i class="fa fa-check-circle text-success"></i> Profile image updated successfully</div>'
                    );

                    // Clear input after upload
                    //$(triggerInput).val("");

                    setTimeout(() => {
                        $(wrapper).find('[role="alert"]').remove();
                    }, 3000);
                } else {
                    $(holder).find(".pic").attr("src", currentImg);
                    $(wrapper).append(
                        //'<div class="snackbar show" role="alert"><i class="fa fa-times-circle text-danger"></i> There is an error while uploading! Please try again later.</div>'
                    );

                    // Clear input after upload
                    $(triggerInput).val("");
                    setTimeout(() => {
                        $(wrapper).find('[role="alert"]').remove();
                    }, 3000);
                }
            }, 1500);
        };
    } else {
        $(wrapper).append(
            '<div class="alert alert-danger d-inline-block p-2 small" role="alert">Please choose the valid image.</div>'
        );
        setTimeout(() => {
            $(wrapper).find('role="alert"').remove();
        }, 3000);
    }
});

$(function () {
    $("#btnEmployeeRegistration").click(function () {
        $("#addEmployeeForm").submit();
    });

    $.validator.setDefaults({
        submitHandler: function () {
            //debugger;
            //var employeeRegistrationModel = $("#addEmployeeForm").serialize();
            var warningMsg = ($("#usermode").val() == "Edit" ? "update" : "save");
            if (confirm("Do you really want " + warningMsg + " the details?")) {
                var formData = BindProfileFormData();

                $.ajax({
                    url: "/EmployeeRegistration/AddEditEmployee",
                    type: "POST",
                    dataType: "json",
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (data) {
                        //debugger;
                        if (data == "add") {
                            toastrSuccess("Employee profile added!");
                            setTimeout(function () {
                                var href = $('#btnCnl').attr('href');
                                window.location.href = href;
                            }, 5000)
                        } else if (data == "update") {
                            toastrSuccess("Employee profile updated!");
                            setTimeout(function () {
                                var href = $('#btnCnl').attr('href');
                                window.location.href = href;
                            }, 5000)
                        } else if (data == "userExists") {
                            toastrWarning("User email address already has been used!!!")
                        }
                        else {
                            toastrWarning("Something went wrong!!!")
                        }

                    },
                    error: function (err) {
                        console.log(err);
                        toastrError("Not able to add/update employee profile!!!");
                    }
                });
            }
        }
    });
    $('#addEmployeeForm').validate({
        rules: {

            EmployeeName: {
                required: true,
            },
            EmployeeId: {
                required: true,
            }, DOB: {
                required: true,
            },
            Gender: {
                required: true,
            },
            DTLOffice: {
                required: true,
            },
            Designation: {
                required: true,
            },
            ServiceStartDate: {
                required: true,
            },
            ServiceEndDate: {
                required: true,
            },
            RAddress: {
                required: true,
            },
            RState: {
                required: true,
            },
            RDistrict: {
                required: true,
            },
            RZipcode: {
                required: true,
            },
            EmailAddress: {
                required: true,
                email: true
            },
            Mobile: {
                required: true,
                minlength: 10,
                maxlength: 10
            },
            Phone: {
                required: false,
                maxlength: 10
            },
        },
        messages: {
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
})

function BindProfileFormData() {
    var frm = $('#addEmployeeForm');
    var formData = new FormData(frm[0]);
    //formData.append('file', $('input[type=file]')[0].files[0]);
    formData.append('Prefix', $('#Prefix').val());
    formData.append('EmployeeName', $('#EmployeeName').val());
    formData.append('EmployeeId', $('#EmployeeId').val());
    formData.append('DOB', $('#DOB').val());
    formData.append('Gender', $('#Gender').val());
    formData.append('DTLOffice', $('#DTLOffice').val());
    formData.append('Designation', $('#Designation').val());
    formData.append('ServiceStartDate', $('#ServiceStartDate').val());
    formData.append('ServiceEndDate', $('#ServiceEndDate').val());
    formData.append('ResidentialAddress', $('#RAddress').val());
    formData.append('PermanentAddress', $('#PAddress').val());
    formData.append('RState', $('#RState').val());
    formData.append('PState', $('#PState').val());
    formData.append('RDistrict', $('#RDistrict').val());
    formData.append('PDistrict', $('#PDistrict').val());
    formData.append('RZipcode', $('#RZipcode').val());
    formData.append('PZipcode', $('#PZipcode').val());
    formData.append('EmailAddress', $('#EmailAddress').val());
    formData.append('Mobile', $('#Mobile').val());
    formData.append('Phone', $('#Phone').val());
    formData.append('SpouseName', $('#SpouseName').val());

    return formData;
}

$("#isAddressSame").click(function () {
    if ($("#isAddressSame").is(":checked")) {
        $("#PAddress").val($("#RAddress").val())
        $("#PState").val($("#RState").val())
        $("#PDistrict").val($("#RDistrict").val())
        $("#PZipcode").val($("#RZipcode").val())
    } else {
        $("#PAddress").val("")
        $("#PState").val("")
        $("#PDistrict").val("")
        $("#PZipcode").val("")
    }
});

$("#newProfilePhoto").change(function (e) {
    if (this.files[0].size > 300000) {
        toastrWarning("File size should not greater than 300KB.")
        return false;
    }
});