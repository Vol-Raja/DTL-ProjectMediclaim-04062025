$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddWhatsNewForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/WhatsNew/Save';
            var formData = BindAddWhatsNewFormData();

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
                        window.location.href = '/WebsiteCMS/WhatsNew';
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    showErrorSwal("Error! " + error);
                }
            });
        });
    });

});


function BindAddWhatsNewFormData() {
    var frm = $('#AddWhatsNewForm');
    var formData = new FormData(frm[0]);
    return formData;
}
function downloadPDF(base64Element, isDownload) {
    var pdf = $("input[name=" + base64Element + "]").val();
    //var pdf = $("#" + base64Element).val();
    if (isDownload) {
        const linkSource = 'data:application/pdf;base64,' + pdf;
        const downloadLink = document.createElement("a");


        var fileName = Date.now() + ".pdf";

        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
        return;
    }

    let pdfWindow = window.open("")
    pdfWindow.document.write("<iframe width='100%' height='100%' src='data:application/pdf;base64, " + encodeURI(pdf) + "'></iframe>")

}

$('#AddWhatsNewForm').validate({
    rules: {
        TitleInEnglish: {
            required: true,
        },
        TitleInHindi: {
            required: true,

        },
        WhatsNewDate: {
            required: true,
        },
        DescriptionHindi: {
            required: true,
        },
        Description: {
            required: true,
        },
        Image: {
            required: function (element) {
                var status = $(".file-required").val();
                return status == 1;
            }
        }
        ,
        AttachmentFileInEnglish: {
            required: function (element) {
                var status = $("#isEnglishFileRequired").val();
                return status == 1;
            }
        },
        AttachmentFileInHindi: {
            required: function (element) {
                var status = $("#isHindiFileRequired").val();
                return status == 1;
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


        WhatsNewDate: {
            required: "Please provide Date.",
        },
        Description: {
            required: "Please provide News Description.",
        },
         DescriptionHindi: {
            required: "Please provide News Description.",
        }
        ,
        AttachmentFileInEnglish: {
            required: "Please provide Attachment File .",
        },
        AttachmentFileInHindi: {
            required: "Please provide Attachment File In Hindi.",
        },
        Image: {
            required: "Please Upload Image",
        },
      


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


$("#upload_documentEnglish").on("change", function () {
    $("#upload_documentHindi-error").css('display', 'None');
    $(this).removeClass('is-invalid');

});
$("#upload_documentHindi").on("change", function () {
    $(this).removeClass('is-invalid');
    $("#upload_documentHindi-error").css('display', 'None');
});
$("#date").on("change", function () {
    $(this).removeClass('is-invalid');
    $("#date-error").css('display', 'None');
});



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
    let url = '/WebsiteCMS/WhatsNew/DownloadFile/' + id;



    addFiles("AttachmentFileInEnglish", $("#EnglishFileName").val(), $("#EnglishContentType").val(), url + '?lang=English');

    addFiles("AttachmentFileInHindi", $("#HindiFileName").val(), $("#HindiContentType").val(), url + '?lang=Hindi');
});