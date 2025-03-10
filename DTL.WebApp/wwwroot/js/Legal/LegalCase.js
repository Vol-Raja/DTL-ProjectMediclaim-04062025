AddEditForm();

$(function () {
    $('#tblCase').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });

});

$("#btnSubmit").click(function () {
    showConfirmSwal('Are you sure you want submit?', function () { $("#LegalCaseForm").submit(); });
    return false;

});
function ShowPreview(event) {
    var output = document.getElementById('pdf_Preview');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
}
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            let IsNew = $("#IsNew").val();
            let successMsg = IsNew ? "Data saved successfully" : "Data updated successfully";
            let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

            showConfirmSwal(msg, function () {
                var formData = BindCaseData();
                $.ajax({
                    url: "/LegalSection/AddEditLegalCaseDetails",
                    type: "POST",
                    dataType: "json",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        //alert(data);
                        if (data == "add" || data == "update") {
                            showSuccessSwal(successMsg);
                            ClearData();
                            setTimeout(function () {
                                window.location.href = "/LegalSection";
                            }, 4000);
                        }
                        else {

                            if (data.indexOf("UC_CaseNo") > -1) {
                                showErrorSwal("Case number already exists.");
                            } else
                                showErrorSwal("Error-" + data);

                        }
                    },
                    error: function (err) {
                        showErrorSwal("Error-" + err);
                    }
                });
            });
            return false;
        }
    });
    $('#LegalCaseForm').validate({
        rules: {
            CaseNo: {
                required: true,
            },
            CourtType: {
                required: true,
            },
            CaseInitialDate: {
                required: true,
            },
            NextHearingDate: {
                required: true,
            },
            PartiesInvolved: {
                required: true,
            },
            NameOfCouncil: {
                required: true,
            },
            PetitionerName: {
                required: true,
            },
            Email: {
                required: true,
                email: true
            },
            Mobile: {
                required: true,

                number: true,
                minlength: 10,
                maxlength: 10
            },
            StatusType: {
                required: true,
            },
            Department: {
                required: true,
            },
            AttachmentFileInEnglish: {
                required: true,
                extension: "pdf"
            },
            CaseEndDate: {
                required: function (element) {
                    var status = $("#StatusType").val();
                    if (status == "Resolved")
                        return true;
                    else
                        return false;
                }
            },
            SummaryOfCase: {
                required: true,

            },
            SubjectOfCase: {
                required: true
            },

        },
        messages: {
            CaseNo: {
                required: "Please provide Case Number.",
            },
            CourtType: {
                required: "Please Select Court Type.",

            },
            NextHearingDate: {
                required: "Please provide Next Hearing date.",
            },
            CaseInitialDate: {
                required: "Please provide Case initial date.",
            },
            PartiesInvolved: {
                required: "Please Provide Parties Involved."
            },
            NameOfCouncil: {
                required: "Please provide Name of Advisor ."
            }
            ,
            Email: {
                required: "Please Provide Email."
            },
            Mobile: {
                required: "Please Provide Mobile Number."
            },
            SubjectOfCase: {
                required: "Please Provide Subject Of Case."
            },
            StatusType: {
                required: "Please Provide Status."
            },
            Department: {
                required: "Please Provide Department."
            },

            CaseEndDate: {
                required: "Please Provide Case End Date."
            },
            SummaryOfCase: {
                required: "Please Provide Case Desc."
            },
            AttachmentFileInEnglish: {
                required: "Please Provide PDF file."
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

function ClearData() {
    $("#CaseNo").val("");
    $("#CourtTypes").val("").change();
    $("#CaseInitialDate").val("").change();
    $("#CaseEndDate").val("").change();
    $("#NextHearingDate").val("").change();
    $("#PartiesInvolved").val("");
    $("#NatureOfCase").val("");
    $("#SummaryOfCase").val("");
    $("#NameOfCouncil").val("");
    $("#SubjectOfCase").val("");
    $("#Email").val("");
    $("#Mobile").val("");
    $("#StatusType").val("");
    $("#Department").val("");



}

function BindCaseData() {
    var frm = $('#LegalCaseForm');

    var formData = new FormData(frm[0]);


    //var formData = new FormData(frm[0]);
    //formData.append('Id', $('#Id').val());
    //formData.append('CaseNo', $('#CaseNo').val());
    //formData.append('CourtType', $("#CourtTypes").val());
    //formData.append('CaseInitialDate', $('#CaseInitialDate').val());
    //formData.append('CaseEndDate', $('#CaseEndDate').val());
    //formData.append('NextHearingDate', $('#NextHearingDate').val());
    //formData.append('PartiesInvolved', $('#PartiesInvolved').val());
    //formData.append('NatureOfCase', $('#NatureOfCase').val());
    //formData.append('SummaryOfCase', $('#SummaryOfCase').val());
    //formData.append('NameOfCouncil', $('#NameOfCouncil').val());
    //formData.append('SubjectOfCase', $('#SubjectOfCase').val());
    //formData.append('Email', $('#Email').val());
    //formData.append('Mobile', $('#Mobile').val());
    //formData.append('StatusType', $('#StatusType').val());
    //formData.append('Department', $('#Department').val());


    return formData;
}

function ApproveLegalCases(ID) {
    $.ajax({
        type: 'POST',
        url: "/LegalSection/ApproveLegalCase",
        data: JSON.stringify(ID),
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {
                if (response == "Success") {
                    showSuccessSwal("Legal case Approved !");
                    setTimeout(function () {
                        window.location.href = "/LegalSection";
                    }, 4000);
                }
                else {
                    showErrorSwal(data);
                }
            }
        },
        error: function (xhr, status, error) {
            showErrorSwal(error);
        }
    });
}


function DeleteLegalCase(Id, rw) {

    $.ajax('/LegalSection/DeleteLegalCase', {
        type: 'POST',  // http method
        data: JSON.stringify(Id),  // data to submit
        contentType: 'application/json; charset=utf-8',
        success: function (data, status, xhr) {
            debugger;
            if (xhr.status == '200') {
                if (data == "Success") {

                    showSuccessSwal("Legal case deleted successfully!")

                    setTimeout(function () {
                        //window.location.href = "/LegalSection";
                        $(rw).parents("tr").remove();
                    }, 4000);
                }
                else {
                    showErrorSwal(data);
                }
            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            showErrorSwal(errorMessage);
        }
    });

}

async function addFiles() {
    const fileInput = document.querySelector('input[type="file"]');
    var IsNew = $("#IsNew").val();
    if (IsNew == 'False') {
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


$(document).ready(function () { addFiles() });