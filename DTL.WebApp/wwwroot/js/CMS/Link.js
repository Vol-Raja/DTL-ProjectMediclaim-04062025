
$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddLinkForm").valid() == false)
            return false;

        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {

            var url = '/WebsiteCMS/Link/Save';
            var formData = BindAddLinkFormData();
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
                        showSuccessSwal(successMsg);
                        console.log(response)

                    }
                    setTimeout(function () {
                        window.location.href = '/WebsiteCMS/Link';
                    }, 4000);
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



function BindAddLinkFormData() {
    var frm = $('#AddLinkForm');
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
function isNew() {
    return $("#IsNew").val() == "true";
}
$('#AddLinkForm').validate({
    rules: {
        Title: {
            required: true,
        },
        TitleHindi: {
            required: true,
        },
        Link: {
            required: true,
        },
        FileContent: {
            required: function (element) {
                var status = $(".file-required").val();
                return status == 1;
            }

        }


    },
    messages: {
        Title: {
            required: "Please provide Title.",
        },
        TitleHindi: {
            required: "Please provide Title In Hindi.",
        },
        Link: {
            required: "Please provide Important Link.",
        },
        FileContent: {
            required: "Please provide  upload image",

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