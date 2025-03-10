$(document).ready(function () {

    var UploadFile = function () {
        debugger;
        var form = new FormData();
        var files = $('#fileUpload')[0].files;

        for (var i = 0; i != files.length; i++) {
            form.append("Files", files[i]);
        }
        var claimId = 1;
        var settings = {
            "url": "/Mediclaim/Raise/Upload?v=" + claimId,
            "method": "POST",
            "timeout": 0,
            "processData": false,
            //mimeType": "multipart/form-data",
            "contentType": false,
            "data": form
        };
        $.ajax(settings).done(function (response) {  });

    }


    $('#btnUpload').click(function () {
        //AddMediclaim();
        UploadFile()
    });

    var AddMediclaim = function () {
        var request = {
            Id: "",
            HospitalName: "",
            HospitalLocation: "",
            PatientName: "",
            EmployeeId: "",
            Gender: "",
            Amount: 0.0,
            ClaimType: "0",
        };

        jQuery.ajax({
            type: 'POST',
            url: "/Mediclaim/Raise/AddNewMediclaim",
            data: JSON.stringify(request),
            contentType: 'application/json',
            dataType: 'json',
            success: function (response) { },
            error: function (xhr, status, error) { }
        });
    }

})
