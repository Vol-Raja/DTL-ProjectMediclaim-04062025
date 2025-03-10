$(document).ready(function () {
    var WithdrawId = '';
    var ApplicationStatusId = '';
    var RejectReason = '';
    var ApprovedAmount = '';

    $('#btnApprove').click(function () {
        var request = {
            WithdrawId: $('#hdnWithdrawalId').val(),
            ApplicationStatusId: 2,
            RejectReason: null,
            ApprovedAmount: $('#txtApprovedAmount').val()
        }
        $.ajax({
            type: 'POST',
            url: "/GPF/LoanProcessing/ApproveReject",
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    if (response == "Success") {
                        //$('#lblApproveClaimId').text($('#hdnWithdrawalId').val());
                        $('#approvemsg').modal('show');

                        setTimeout(function () {
                            window.location.href = "../../LoanProcessing";
                        }, 2000);
                    }
                    else {
                    }
                }
            },
            error: function (xhr, status, error) {

            }
        });

    });

    $('#btnReject').click(function () {
        var request = {
            WithdrawId: $('#hdnWithdrawalId').val(),
            ApplicationStatusId: 3,
            RejectReason: $('#txtRejectReason').val(),
            ApprovedAmount: null
        }

        $.ajax({
            type: 'POST',
            url: "/GPF/LoanProcessing/ApproveReject",
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    if (response == "Success") {
                        //$('#lblRejectClaimId').text($('#hdnWithdrawalId').val());
                        $('#rejectmsg').modal('show');
                        $('#txtRejectReason').val('');

                        setTimeout(function () {
                            window.location.href = "../../LoanProcessing";
                        }, 2000);
                    }
                    else {

                    }
                }
            },
            error: function (xhr, status, error) {

            }
        });
    });

});