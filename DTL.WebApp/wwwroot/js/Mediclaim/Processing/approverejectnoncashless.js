$(document).ready(function () {
    var ClaimId = '';
    var ClaimStausId = '';
    var RejectReason = '';

    $('#btnApprove').click(function () {
       
        var request = {
            ClaimId: $('#hdnClaimId').val(),
            ClaimStatusId: 2,
            RejectReason: null,
            ApprovedAmount: $('#txtApprovedAmount').val(),
            Remark: $('#txtRemark').val(),
            DealingAssistanceRemark: $('#txtDealingAssistantRemark').val(),
            //Remark: $('#txtRemark').val(), ASORemark: $('#txtASORemark').val(),
            //SORemark: $('#txtSORemark').val()
        }
        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Processing/Noncashless/ApproveReject",
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#approveModal').modal('hide');
                if (xhr.status == '200') {
                    if (response == "Success") {
                        //$('#approvemsg').modal('show');
                        //let message = `Claim ${$('#hdnClaimId').val()} is approved successfully`                                             
                        //$('#lblApproveMessage').text(message);
                        
                        setTimeout(function () {
                            window.location.href = "/Mediclaim/Processing/NonCashlessClaims";
                        }, 2000);
                    }
                    else {
                        //$('#approvemsg').modal('show');
                        //let message = `Something went wrong`;
                        //$('#lblReturnMessage').text(message);
                        

                    }
                }
            },
            error: function (xhr, status, error) {
                $('#approveModal').modal('hide');
            }
        });

    });

    $('#btnReject').click(function () { 
        if ($('#txtRejectReason').val() !== '') {
            $('#txtRejectReason').removeClass('is-invalid');
            var request = {
                ClaimId: $('#hdnClaimId').val(),
                ClaimStatusId: 3,
                RejectReason: $('#txtRejectReason').val(),
                DealingAssistanceRemark: $('#txtDealingAssistantRemark').val(),
                ApprovedAmount: 0
            }

            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Processing/Noncashless/ApproveReject",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    $('#rejectModal').modal('hide');
                    if (xhr.status == '200') {
                        if (response == "Success") {
                            //$('#rejectmsg').modal('show');
                            //let message = `Claim ${$('#hdnClaimId').val()} is rejected `;
                            //$('#lblRejectMessage').text(message);                             
                            $('#txtRejectReason').val('');
                            setTimeout(function () {
                                window.location.href = "/Mediclaim/Processing/NonCashlessClaims";
                            }, 2000);
                        }
                        else {
                            //$('#rejectmsg').modal('show');
                            //let message = `Something went wrong`;
                            //$('#lblRejectMessage').text(message);
                        }
                    }
                },
                error: function (xhr, status, error) {
                    $('#rejectModal').modal('hide');
                }
            });
        }
        else {
            $('#txtRejectReason').addClass('is-invalid');
        }
    });

    $('#btnModalApprove').click(function () { 
        var ApprovedAmount = $('#txtApprovedAmount').val();
        if (parseFloat(ApprovedAmount) > 0) {
            //$('input[type="text"]').remove('is-invalid');
            $('#approveModal').modal('show');
        }
        else {
            $('#validationModal').modal('show');
            //$('input[type="text"]').addClass('is-invalid');
        }
    });

    $('#btnModalReject').click(function () { 
        $('#rejectModal').modal('show');
    });
});