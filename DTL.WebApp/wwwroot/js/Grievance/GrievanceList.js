$(function () {
    //$("#example1").DataTable({
    //    "responsive": true, "lengthChange": false, "autoWidth": false,
    //    "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    //}).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#grvTbl').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});

function DeleteGrievance(grvId, rw) {

    $.ajax('/Grievance/DeleteGrievance', {
        type: 'POST',  // http method
        data: { Id: grvId },  // data to submit
        success: function (data, status, xhr) {

            if (data == "Deleted") {
                showSuccessSwal("Grievance info delete successfully!");

                $('#' + grvId).remove();
            }

        },
        error: function (jqXhr, textStatus, errorMessage) {
            showErrorSwal(errorMessage);
        }
    });

}

function ReplyToGrievance(grvId) {
    $('#replyGrievanceModel').modal('show');
    $('#hdnGrievanceId').val(grvId);
    //$('#txtRejectReason').val('');
}


function PostReplyMessage() {
    var replyMessage = $('#txtReply').val();

    var request = {
        Reply: replyMessage,
        Id: $('#hdnGrievanceId').val()
    }
    $.ajax({
        type: 'POST',
        url: "/Grievance/GrievanceReplyMessage",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {
                if (response == "Success") {
                    $('#replyGrievanceModel').modal('hide');
                    $('#hdnGrievanceId').val('');
                    $('#txtReply').val('');

                    setTimeout(function () {
                        window.location.href = "/Grievance";
                    }, 4000);
                }
                else {

                }
            }
        },
        error: function (xhr, status, error) {

        }
    });
}