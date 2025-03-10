
$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#AddBannerImageForm").valid() == false)
            return false;
      
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
     
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';
        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/BannerImage/Save';
            var formData = BindAddBannerImageFormData();
            var IsGallery = $("#IsGallery").val() || false;

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
                        
                        showSuccessSwal(successMsg);
                    }
                    setTimeout(function () {
                        window.location.href = '/WebsiteCMS/BannerImage?IsGallery=' + IsGallery;
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    hideLoader();
                    console.error(xhr);
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
function BindAddBannerImageFormData() {
    var frm = $('#AddBannerImageForm');
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
$('#AddBannerImageForm').validate({
    rules: {
        Description: {
            required: true,
        },
        Image: {
            required: function (element) {
                var status = $(".file-required").val();
                return status == 1;
            }
        },
        DescriptionHindi: {
            required: true,
        },
    },
    messages: {
        Description: {
            required: "Please provide Description.",
        },
        DescriptionHindi: {
            required: "Please provide Description in Hindi.",
        },
        Image: {
            required: "Please provide image.",

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