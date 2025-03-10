AddEditForm();

$("#btnSaveForm5").click(function () {
    $("#form5").submit();
});

i = 0;
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var formData = BindForm5Data();
            $.ajax({
                url: "/Pensioner/CreateForm5",
                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    $("#custom-tabs-three-familyNominee-tab").trigger("click");
                    toastrSuccess("Data saved successfully!");
                },
                error: function (err) {
                    toastrError("Something went wrong!!!");
                }
            });
        }
    });
    $('#form5').validate({
        rules: {
            Aadhar: {
                required: true,
                aadharCard: true,

            },
            PAN: {
                required: true,
                panCard: true,
            },
            Bank: {
                required: true
            },
            BankAddress: {
                required: true
            },
            IFSCCode: {
                required: true
            },
            BICCode: {
                required: true
            },
            IdentificationMark: {
                required: true
            },
            BankAccountNumber: {
                required: true
            }
        },
        messages: {
            AadharCardNo: {
                required: "Please enter aadhar card number.",

            },
            PAN: {
                required: "Please provide PAN",
            },
            Bank: {
                required: "Please provide a bank",
            },
            BankAddress: {
                required: "Please provide a bank address",
            },
            IFSCCode: {
                required: "Please provide IFSCCode",
            }, BICCode: {
                required: "Please provide BICCode",
            },
            IdentificationMark: {
                required: "Please provide Identification Mark",
            },
            BankAccountNumber: {
                required: "Please provide Bank Account Number",
            }
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
}

jQuery.validator.addMethod("aadharCard", function (value, element) {
    return this.optional(element) || /^[2-9]{1}[0-9]{3}\s{1}[0-9]{4}\s{1}[0-9]{4}$/.test(value);
}, "Please enter aadhar card in proper format. E.g XXXX XXXX XXXX");

jQuery.validator.addMethod("panCard", function (value, element) {
    return this.optional(element) || /([A-Z]){5}([0-9]){4}([A-Z]){1}$/.test(value);
}, "Please enter pancard in proper format. E.g AAAAA1111A");

$("#Aadhar").keypress(function () {
    if ($("#Aadhar").val().length < 14) {
        if ($("#Aadhar").val().length == 0) {
            i = 0;
        }
        var aadhar = $("#Aadhar").val();
        if (i == 4) {
            $("#Aadhar").val(aadhar += " ");
            i = 0;
        }
        i++;
    }
});
$("#lblSpecimenSignature1").click(function () {
    $("#SpecimenSignature1").trigger("click");
})
$("#lblSpecimenSignature2").click(function () {
    $("#SpecimenSignature2").trigger("click");
})
$("#lblSpecimenSignature3").click(function () {
    $("#SpecimenSignature3").trigger("click");
})
$("#lblThumbImpression1").click(function () {
    $("#ThumbImpression1").trigger("click");
})
$("#lblThumbImpression2").click(function () {
    $("#ThumbImpression2").trigger("click");
})
$(document).on("change", "#SpecimenSignature1,#SpecimenSignature2,#SpecimenSignature3,#ThumbImpression1,#ThumbImpression2", function () {
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


$("#btnAadharCard").click(function () {
    $("#AadharCard").trigger("click");
});

$("#btnPanCard").click(function () {
    $("#PanCard").trigger("click");
});

function BindForm5Data() {
    var frm = $('#form5');
    var formData = new FormData(frm[0]);
    //formData.append('AadharCard', $('input[name=AadharCard]')[0].files[0]);
    //formData.append('PanCard', $('input[name=PanCard]')[0].files[0]);
    //formData.append('SpecimenSignature', $('input[name=profile_pic]')[0].files[0]);
    formData.append('Id', $('#Id').val());
    formData.append('EmployeeRegistrationId', $('#EmployeeRegistrationId').val());
    formData.append('Aadhar', $('#Aadhar').val());
    formData.append('PAN', $('#PAN').val());
    formData.append('Bank', $('#Bank').val());
    formData.append('BankAddress', $('#BankAddress').val());
    formData.append('IFSCCode', $('#IFSCCode').val());
    formData.append('BICCode', $('#BICCode').val());
    formData.append('IdentificationMark', $('#IdentificationMark').val());


    return formData;
}

$("#closeAadharCardModal").click(function () {
    DeleteTempFile($("#ifrmAadhar").attr("src"));
});

$("#closePanCardModal").click(function () {
    DeleteTempFile($("#ifrmPan").attr("src"));
});


$("#viewAadharDoc").click(function () {   
    var employeeId = $("#EmployeeRegistrationId").val();
    var fileName = "aadhar"
    $.ajax({
        url: "/Pensioner/GetFile",
        type: "POST",
        dataType: "json",
        data: { fileName: fileName, employeeId: employeeId },
        success: function (data) {
            switch (fileName) {
                case "aadhar":
                    if (data != "NoRecord") {
                        $("#ifrmAadhar").attr('src', data);
                    } else {
                        $("#modal-aadhar-body").html("<div class='text - center'>No Document Found!!!</div>")
                    }
                    $("#modal-aadhar").modal("show");
                    break;
                case "pan":
                    if (data != "NoRecord") {
                        $("#ifrmPan").attr('src', data);
                    } else {
                        $("#modal-pan-body").html("<div class='text - center'>No Document Found!!!</div>")
                    }
                    $("#modal-pan").modal("show");
                    break;
            }
        },
        error: function (err) {
            toastrError("Something went wrong!!!")
        }

    })
});

$("#viewPanDoc").click(function () {   
    var employeeId = $("#EmployeeRegistrationId").val();
    var fileName = "pan"
    $.ajax({
        url: "/Pensioner/GetFile",
        type: "POST",
        dataType: "json",
        data: { fileName: fileName, employeeId: employeeId },
        success: function (data) {
            switch (fileName) {
                case "aadhar":
                    if (data != "NoRecord") {
                        $("#ifrmAadhar").attr('src', data);
                    } else {
                        $("#modal-aadhar-body").html("<div class='text - center'>No Document Found!!!</div>")
                    }
                    $("#modal-aadhar").modal("show");
                    break;
                case "pan":
                    if (data != "NoRecord") {
                        $("#ifrmPan").attr('src', data);
                    } else {
                        $("#modal-pan-body").html("<div class='text - center'>No Document Found!!!</div>")
                    }
                    $("#modal-pan").modal("show");
                    break;
            }
        },
        error: function (err) {
            toastrError("Something went wrong!!!")
        }

    })
});
