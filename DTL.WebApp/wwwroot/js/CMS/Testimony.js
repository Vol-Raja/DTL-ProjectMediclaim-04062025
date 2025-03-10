
$(document).ready(function () {

    $('#btnSubmit').click(function (e) {
        e.preventDefault();

        if ($("#AddEditTestimonyForm").valid() == false)
            return false;
        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {
            var url = '/WebsiteCMS/Testimony/Save';
            var formData = BindAddEditTestimonyFormData();
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
                        console.log(response)
                        showSuccessSwal(successMsg);

                    }
                    setTimeout(function () {
                        window.location.href = '/WebsiteCMS/Testimony';
                    }, 4000);
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



function BindAddEditTestimonyFormData() {
    var frm = $('#AddEditTestimonyForm');
    var formData = new FormData(frm[0]);
    return formData;
}

$('#AddEditTestimonyForm').validate({
    rules: {
        Image: {
            required: function (element) {
                var status = $(".file-required").val();
                return status == 1;
            }
        },
        NameEnglish: {
            required: true

        },
        NameHindi: {
            required: true,
        },
        PositionHindi: {
            required: true,
        },
        PositionEnglish: {
            required: true,
        },
      
    },
    messages: {
        Image: {
            required: "Please provide Image.",
        },
        NameEnglish: {
            required: "Please provide Name .",

        },
        NameHindi: {
            required: "Please Provide Name In Hindi .",

        }, 
        PositionHindi: {
            required: "Please Provide Position In Hindi.",

        },
        PositionEnglish: {
            required: "Please Provide Position.",

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
    let url = '/WebsiteCMS/Testimony/DownloadFile?TestimonyId=' + id;

    var IsNew = $("#IsNew").val();
    if (IsNew == 'False') {

        addFiles("Image", $("#ImageName").val(), $("#ImageContentType").val(), url);


    }
});