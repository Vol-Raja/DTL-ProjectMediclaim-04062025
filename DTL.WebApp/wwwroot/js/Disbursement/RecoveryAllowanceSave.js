AddEditForm();

$("#btnSubmit").click(function () {
    if (confirm("Are you sure?"))
        $("#RecoveryForm").submit();
    else
        alert("Canceled Saving Data...!!!");
});
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var formData = BindAQFormData();
            $.ajax({
                type: "POST",
                url: "/Disbursement/SaveRecoveryAllowanceData",
                data: formData,
                processData: false,
                contentType: false,
                cache: false,
                dataType: "json",
                success: function (data) {
                    toastrSuccess("Data saved successfully!");
                    setTimeout(function () {
                        window.location.href = "/Disbursement";
                    }, 5000)

                },
                error: function (err) {
                    toastrError("Something went wrong!!!");
                }

            });
        }
    });
    $('#RecoveryForm').validate({
        rules: {
            PensionerName: {
                required: true
            }
        },
        messages: {
            PensionerName: {
                required: "Please provide Pensioner Name."
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

function BindAQFormData() {
    var frm = $('#RecoveryForm');
    var formData = new FormData(frm[0]);
    formData.append('ID', $('#id').val());
    formData.append('EmployeeRegistrationId', $('#eid').val());
    formData.append('PensionerName', $('#txtPensionerName').val());
    formData.append('EmployeeID', $('#txtEmployeeID').val());
    formData.append('EmployerName', $('#Employer').val());
    formData.append('ChangeType', $('#ChangeType').val());
    formData.append('Reason', $('#Reason').val());
   
    var allsel = "", res;
    var str = [],strFinal="";
    var x = document.getElementById('TypeOfRecovery');
    for (var i = 0; i < x.options.length; i++) {
        if (x.options[i].selected) {
            str.push(x.options[i].value);
        }
    }
    res =str.join(", ");
    
    formData.append('TypeOfRecovery', res);
    formData.append('RecoveryAmount', $("#txtRecoveryAmount").val());
    formData.append('RecoveryOption', $("#RecoveryOption").val());
    formData.append('MonthlyPension', $('#txtMonthlyPension').val());
    formData.append('ApplicableAmount', $('#txtApplicableAmount').val());
    formData.append('MonthlyPensionAfter', $('#txtMonthlyPensionAfter').val());
    formData.append('FromDate', $('#dtFrom').val());
    formData.append('ToDate', $('#dtTo').val());
    return formData;
}

$("#btnClear").click(function () {
    $('#dtFrom').val('').change();
    $('#dtTo').val('').change();
});

