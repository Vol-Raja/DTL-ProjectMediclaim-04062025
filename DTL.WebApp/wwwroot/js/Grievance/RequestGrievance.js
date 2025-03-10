AddEditForm();
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            showConfirmSwal("Are you sure you want to update?", function () {
                var formData = BindGrievanceData();
                $.ajax({
                    url: "/Grievance/SetStatus",
                    type: "POST",
                    dataType: "json",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        //alert(data);

                        if (data == "add" || data == "update") {

                            showSuccessSwal();
                            window.location.href = "/Grievance/RequestGrievanceList";
                        }
                        else {
                            showErrorSwal("Error! " + data)

                        }
                    },
                    error: function (err) {
                        showErrorSwal("Error! " + err)

                    }
                });
            });

        }
    });
    $('#GrievanceRequestForm').validate({
        rules: {

            Remarks: {
                required: function (element) {
                    var status = $("#Status").val();
                    if (status == "Resolved" || status == "Withdrawn by Complaint")
                        return true;
                    else
                        return false;
                }

            }
            ,
            Status: {
                required: true,
            }
            ,
            GrievanceHeadAttachmentFileInEnglish: {
                required: false,
                extension: "pdf"
            }

        },
        messages: {
            Remarks: {
                required: "Please provide Remarks.",
            }
            ,
            Status: {
                required: "Please provide Status.",
            },
            GrievanceHeadAttachmentFileInEnglish: {
                required: "Please provide File.",
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
function BindGrievanceData() {
    var frm = $('#GrievanceRequestForm');
    var formData = new FormData(frm[0]);

    return formData;
}
function isRemarksRequired() {
    var status = $("#Status").val();
    if (status == "Resolved" || status == "Withdrawn by Complaint")
        return true;
    else
        return false;
}

function ClearData() {
    $("#Id").val("");
    $('#Department').val("");
    $('#PPONo').val("");
    $('#EmployeeId').val("");
    $('#MobileNo').val("");
    $('#Subject').val("");
    $('#GrievanceDetails').val("");

}
$("#btnSubmit").click(function () {
    $("#CreateGrievanceForm").submit();
});
$("#Status").change(function () {
    var status = $("#Status").val();
    if (status == "Resolved" || status == "Withdrawn by Complaint")
        $("#RemarksDiv").show();
    else
        $("#RemarksDiv").hide();
});
function ShowDocHeadPreview(event) {
    var output = document.getElementById('pdfHeadPreview');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
}
