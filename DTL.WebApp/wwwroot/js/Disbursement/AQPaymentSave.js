AddEditForm();

$("#btnSubmit").click(function () {
    if (confirm("Are you sure?"))
        $("#AQPaymentForm").submit();
    else
        alert("Canceled Saving Data...!!!");
});
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var formData = BindAQFormData();
            $.ajax({
                type: "POST",
                url: "/Disbursement/SaveAQPayment",
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
    $('#AQPaymentForm').validate({
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
    var frm = $('#AQPaymentForm');
    var formData = new FormData(frm[0]);
    formData.append('ID', $('#id').val());
    formData.append('EmployeeRegistrationId', $('#eid').val());
    formData.append('PensionerName', $('#txtPensionerName').val());
    formData.append('DOB', $('#txtDOB').val());
    formData.append('EmployeeID', $('#txtEmployeeID').val());
    formData.append('EmployerName', $('#Employer').val());
    formData.append('CurrentAge', $('#txtCurrentAge').val());
    formData.append('AgeGroup', $('#AgeGroup').val());
    formData.append('MonthlyPension', $('#txtMonthlyPension').val());
    formData.append('IncrementPercentage', $('#txtIncrementPercentage').val());
    formData.append('IncrementAmount', $('#txtIncrementAmount').val());
    formData.append('AQPMonthlyPension', $('#txtMonthlyPensionAQP').val());
    formData.append('FromDate', $('#dtFrom').val());
    formData.append('ToDate', $('#dtTo').val());
    return formData;
}

$("#btnClear").click(function () {
    $('#dtFrom').val('').change();
    $('#dtTo').val('').change();
    $('#txtDOB').val('').change();
});