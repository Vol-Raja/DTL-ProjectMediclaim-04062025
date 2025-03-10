$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddCMSHospitalForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/CMSHospital/Save';
            var formData = BindAddCMSHospitalFormData();

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
                        window.location.href = '/WebsiteCMS/CMSHospital';
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    showErrorSwal(error);
                }
            });
        });
    });

});


function BindAddCMSHospitalFormData() {
    var frm = $('#AddCMSHospitalForm');
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

$('#AddCMSHospitalForm').validate({
    rules: {
        NameInEnglish: {
            required: true,
        },
        NameInHindi: {
            required: true,

        },
      
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
        NameInEnglish: {
            required: "Please provide Name In English.",
        },
        NameInHindi: {
            required: "Please provide Name In Hindi.",

        },
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
    let url = '/WebsiteCMS/CMSHospital/DownloadFile/' + id;



    addFiles("AttachmentFileInEnglish", $("#EnglishFileName").val(), $("#EnglishContentType").val(), url + '?lang=English');

    addFiles("AttachmentFileInHindi", $("#HindiFileName").val(), $("#HindiContentType").val(), url + '?lang=Hindi');
});