$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddForm").valid() == false)
            return false;

        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/Form/Save';
            var formData = BindAddFormFormData();

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
                        window.location.href = '/WebsiteCMS/Form';
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


function BindAddFormFormData() {
    var frm = $('#AddForm');
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

$('#AddForm').validate({
    rules: {
        TitleInEnglish: {
            required: true,
        },
        TitleInHindi: {
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
        TitleInEnglish: {
            required: "Please provide name In English.",
        },
        TitleInHindi: {
            required: "Please provide name In Hindi.",

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