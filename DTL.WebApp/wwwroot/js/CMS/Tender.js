$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddTenderForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {

            var url = '/WebsiteCMS/Tender/Save';
            var formData = BindAddTenderFormData();
            showLoader()
            $.ajax({

                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                url: url,

                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        hideLoader(response);
                        console.log(response)

                        showSuccessSwal(successMsg);

                    }
                    setTimeout(function () {
                        window.location.href = '/WebsiteCMS/Tender';
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    hideLoader();
                    console.log(xhr);
                    showErrorSwal("Error! " + error);
                }
            });
        });
    });

});


function BindAddTenderFormData() {
    var frm = $('#AddTenderForm');
    var formData = new FormData(frm[0]);
    return formData;
}
function downloadPDF(lang) {

    let id = $("#ID").val();
    let url = '/WebsiteCMS/Tender/DownloadFile?Id=' + id + '&lang=' + lang;
    let pdfWindow = window.open("")
    pdfWindow.document.write("<iframe width='100%' height='100%' src='" + url + "'></iframe>")

}

$('#AddTenderForm').validate({
    rules: {
        TitleInEnglish: {
            required: true,
        },
        TitleInHindi: {
            required: true,

        },
        TenderIdInHindi: {
            required: true,
        },
        TenderIdEnglish: {
            required: true,
        }
        ,
        OpeningDate: {
            required: true,
        }

        ,
        OpeningTime: {
            required: true,
        }

        ,
        PublishDate: {
            required: true,
        }

        ,
        PublishTime: {
            required: true,
        }

        ,
        ClosingDate: {
            required: true,
        }

        ,
        ClosingTime: {
            required: true,
        }

        ,
        DiscriptionInEnglish: {
            required: true,
        }
        ,
        DiscriptionInHindi: {
            required: true,
        }
        ,
        AttachmentTitleInEnglish: {
            required: true,
        }
        ,
        AttachmentTitleInHindi: {
            required: true,
        }
        ,
        AttachmentFileInEnglish: {
            required: function (element) {
                var value = $("#AttachmentFileInEnglishBase64").val();
                if (value)
                    return false
                else
                    return true;
            }
        },
        AttachmentFileInHindi: {
            required: function (element) {
                var value = $("#AttachmentFileInHindiBase64").val();
                if (value)
                    return false
                else
                    return true;

            }
        }
    },
    messages: {
        TitleInEnglish: {
            required: "Please provide Title In English.",
        },
        TitleInHindi: {
            required: "Please provide Title In Hindi.",

        },
        TenderIdInHindi: {
            required: "Please provide Tender Id In Hindi.",
        },
        TenderIdEnglish: {
            required: "Please provide Tender Id English.",
        }
        ,
        OpeningDate: {
            required: "Please provide Opening Date.",
        }
        ,
        OpeningTime: {
            required: "Please provide Opening Time.",
        }

        ,
        PublishDate: {
            required: "Please provide Publish Date.",
        }

        ,
        PublishTime: {
            required: "Please provide Publish Time.",
        }

        ,
        ClosingDate: {
            required: "Please provide Closing Date.",
        }

        ,
        ClosingTime: {
            required: "Please provide Closing Time.",
        }

        ,
        DiscriptionInEnglish: {
            required: "Please provide Discription In English.",
        }
        ,
        DiscriptionInHindi: {
            required: "Please provide Discription In Hindi.",
        }
        ,
        AttachmentTitleInEnglish: {
            required: "Please provide Title In English.",
        }
        ,
        AttachmentTitleInHindi: {
            required: "Please provide Attachment Title In English.",
        }
        ,
        AttachmentFileInEnglish: {
            required: "Please provide Attachment File In English.",
        },
        AttachmentFileInHindi: {
            required: "Please provide Attachment File InHindi.",
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


function Delete(Id, rw) {

    $.ajax('/WebsiteCMS/Tender/Delete', {
        type: 'POST',  // http method
        data: JSON.stringify(Id),  // data to submit
        contentType: 'application/json; charset=utf-8',
        success: function (data, status, xhr) {

            if (xhr.status == 200) {

                $('#deleteModal').modal('hide')
                showSuccessSwal("deleted successfully!")

                setTimeout(function () {
                    window.location.reload();
                }, 4000);

            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('#deleteModal').modal('hide')
            showErrorSwal("Error - " + errorMessage);


        }
    });

}



async function addFiles(elementName, fileName, contentType, downloadUrl) {
    const fileInput = document.querySelector('input[name="' + elementName + '"]');
    var IsNew = $("#IsNew").val();
    if (IsNew == 'False') {
        var url = downloadUrl;

        var englishFileName = fileName;
        var englishContentType = contentType;

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

$(document).ready(function () {
    let id = $("#ID").val();
    let url = '/WebsiteCMS/Tender/DownloadFile/' + id;
    addFiles("AttachmentFileInEnglish", $("#AttachmentTitleInEnglish").val(), 'application/pdf', url + '?lang=en');
    addFiles("AttachmentFileInHindi", $("#AttachmentTitleInHindi").val(), 'application/pdf', url + '?lang=hn');
});