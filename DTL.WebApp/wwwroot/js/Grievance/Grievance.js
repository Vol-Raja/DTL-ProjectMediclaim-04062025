AddEditForm();
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            let IsNew = $("#IsNew").val();
            let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
            let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';
            showConfirmSwal(msg, function () {
                var formData = BindGrievanceData();
                $.ajax({
                    url: "/Grievance/AddEditGrievance",
                    type: "POST",
                    dataType: "json",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (isNaN(data) == false) {
                            // showSuccessSwal("Your Grievance Id is " + data);

                            showSuccessDataSwal("Your Grievance Id is " + data, function () { location = '/Grievance/GrievanceList' });
                            //  toastr.success("Data saved successfully!");
                            //  $("#GrievanceIDSpan").text(data);
                            //  $("#GrievanceIDModal").modal('show');
                        }
                        else if ("update" == data) {
                            showSuccessSwal(successMsg);
                            setTimeout(function () {
                                window.location.href = '/Grievance/GrievanceList';
                            }, 4000);
                        }
                        else {
                            showErrorSwal("Error! " + data);
                        }
                    },
                    error: function (err) {
                        showErrorSwal("Error! " + err);
                        //toastrError("Something went wrong!!!" + err);
                    }
                });
            });

        }
    });
    $('#CreateGrievanceForm').validate({
        rules: {

            EmployeeId: {
                required: true,
               
            },
            MobileNo: {
                required: true,
                number: true,
                minlength: 10,
                maxlength: 10
            },
            Subject: {
                required: true,
            },
            UserType: {
                required: true,
            }
            ,
            Name: {
                required: true,
            }
            ,
            Gender: {
                required: true,
            }
            ,
            Office: {
                required: true,
            }
            ,
            Remarks: {
                required: true,
            }
            ,
            Status: {
                required: true,
            }
            ,
            EmailID: {
                required: true,
                email: true
            }
            ,
            Description: {
                required: true,

            }
            ,
            Address: {
                required: true,
            },
            AttachmentFileInEnglish: {
                // required: true,
                extension: "pdf"

            }


        },
        messages: {
            UserType: {
                required: "Please provide User Type.",
            }
            ,
            Name: {
                required: "Please provide Name.",
            }
            ,
            Gender: {
                required: "Please provide Gender.",
            }
            ,
            Office: {
                required: "Please provide Office.",
            },

            EmailID: {
                required: "Please provide EmailID.",

            },
            EmployeeId: {
                required: "Please provide Employee Id.",
            },
            MobileNo: {
                required: "Please Provide Mobile No. ex.9812345678"
            },
            Subject: {
                required: "Please provide Subject."
            },
            Description: {
                required: "Please provide Description.",
            }
            ,
            Address: {
                required: "Please provide Address.",
            },
            AttachmentFileInEnglish: {
                required: "Please provide pdf file.",
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
    var frm = $('#CreateGrievanceForm');
    var formData = new FormData(frm[0]);
    //formData.append('Id', $('#Id').val());
    //formData.append('Department', $('#Department').val());
    //formData.append('PPONo', $('#PPONo').val());
    //formData.append('EmployeeId', $('#EmployeeId').val());
    //formData.append('MobileNo', $('#MobileNo').val());
    //formData.append('Subject', $('#Subject').val());
    //formData.append('GrievanceDetails', $('#GrievanceDetails').val());
    return formData;
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

function ShowPreview(event) {
    var output = document.getElementById('pdf_Preview');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
}

$(document).ready(function () {
    $('#UserType').on('change', function () {
        var selected = $(this).find(":selected").val();
        if (selected == "Public User") {
            $('#EmployeeId').prop('disabled', 'disabled');
            $('#Office').prop('disabled', 'disabled');
            $('#PPONoDiv').hide()
            $('#OfficeDiv').hide()

        } else {
            $('#EmployeeId').removeAttr('disabled');
            $('#Office').removeAttr('disabled');
            $('#PPONoDiv').show()
            $('#OfficeDiv').show()
        }
    });

   
    $('#UserType').trigger('change');
    addFiles();

});

async function addFiles() {
    const fileInput = document.querySelector('input[type="file"]');
    var grievanceId = $("#GrievanceID").val();
    if (grievanceId > 0) {
        var url = $("#pdf_Preview").attr('src');

        var englishFileName = $("#EnglishFileName").val();
        var englishContentType = $("#EnglishContentType").val();

        let file = await fetch(url).then(async r => await r.blob()).then(blobFile => new File([blobFile], englishFileName, { type: englishContentType }))
        // Create a new File object
        //const myFile = new File(['Hello World!'], 'myFile.pdf', {
        //    type: 'text/plain',
        //    lastModified: new Date(),
        //});

        // Now let's create a DataTransfer to get a FileList
        const dataTransfer = new DataTransfer();
        dataTransfer.items.add(file);
        fileInput.files = dataTransfer.files;
    }
}
