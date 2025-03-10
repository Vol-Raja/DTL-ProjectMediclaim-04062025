
$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddEditAboutForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';
        showConfirmSwal(msg, function () {
      
            var url = '/WebsiteCMS/About/Save';
            var formData = BindAddEditAboutFormData();
            showLoader()
            $.ajax({

                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                url: url,

                success: function (response, status, xhr) {
                    if (xhr.status == 200) {
                        hideLoader(response);
                        showSuccessSwal(successMsg);
                        setTimeout(function () {
                            window.location.href = '/WebsiteCMS/About';
                        }, 4000);
                    }
                 
                },
                error: function (xhr, status, error) {
                    hideLoader();
                   
                    showErrorSwal("Error! " + error);
                  
                }
            });
        });
    });

});


var showLoader = function () {
    //$('.preloader').css()
    $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
    $(".preloader img").css({ "display": "block" });
};

var hideLoader = function () {
    $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
    $(".preloader img").css({ "display": "none" });
};
function BindAddEditAboutFormData() {
    var frm = $('#AddEditAboutForm');
    var formData = new FormData(frm[0]);
    return formData;
}
function download(base64Element, type, fileName, isDownload) {
    var data = $("input[name=" + base64Element + "]").val();
    //var pdf = $("#" + base64Element).val();
    if (isDownload) {
        const linkSource = 'data:' + type + ';base64,' + data;
        const downloadLink = document.createElement("a");


        // var fileName = Date.now() + ".pdf";

        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
        return;
    }

    let newWindow = window.open("")
    newWindow.document.write("<iframe width='100%' height='100%' src='data:" + type + ";base64, " + encodeURI(data) + "'></iframe>")

}
function isNew() {
    return $("#IsNew").val() == "true";
}
$('#AddEditAboutForm').validate({
    rules: {
        Image: {
            required: true,
        },
        FileName: {
            required: true

        }
        , TextContent: {
            required: true

        }, TextContentHindi: {
            required: true

        }

    },
    messages: {
        Title: {
            required: "Please provide Description.",
        },
        DocumentFileName: {
            required: "Please provide File.",

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

$(document).ready(function () {
    let id = $("#ID").val();
    let url = '/WebsiteCMS/About/DownloadFile?AboutId=' + id;

    var IsNew = $("#IsNew").val();
    if (IsNew == 'False') {

        addFiles("Image", $("#ImageName").val(), $("#ImageContentType").val(), url);

      
    }
});