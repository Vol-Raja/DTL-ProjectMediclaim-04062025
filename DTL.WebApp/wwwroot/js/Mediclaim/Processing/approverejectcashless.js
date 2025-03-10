$(document).ready(function () {
    var ClaimId = '';
    var ClaimStausId = '';
    var RejectReason = '';
    var ApprovedAmount = 0;

    $('#btnApprove').click(function () {
        //if ($('#txtRemark').val() !== '') {
            var request = {
                ClaimId: $('#hdnClaimId').val(),
                ClaimStatusId: 2,
                RejectReason: null,
                ApprovedAmount: $('#txtApprovedAmount').val(),
                Remark: $('#txtRemark').val(),
                CMORemark: $('#txtCMORemark').val(),
                DealingAssistanceRemark: $('#txtDealingAssistantRemark').val(),
            }
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Processing/Cashless/ApproveReject",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    
                    if (xhr.status == '200') {

                        setTimeout(function () {
                            window.location.href = "/Mediclaim/IPD/ProcessingList";
                        }, 1000);
                    }
                },
                error: function (xhr, status, error) {

                }
            });
        //}
        //else {
        //    $('#txtRemark').addClass('is-invalid');
        //}
    });

    $('#btnReject').click(function () {
        var request = {
            ClaimId: $('#hdnClaimId').val(),
            ClaimStatusId: 3,
            RejectReason: $('#txtRejectReason').val(),
            ApprovedAmount: 0,
            ApproveRemark: null,
            CMORemark: $('#txtCMORemark').val(),
            DealingAssistanceRemark: $('#txtDealingAssistantRemark').val(),
            ASORemark: $('#txtASORemark').val(),
            SORemark: $('#txtSORemark').val()
        }

        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Processing/Cashless/ApproveReject",
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
               
                if (xhr.status == '200') {
                    if (response == "Success") {
                        $('#lblRejectClaimId').text($('#hdnClaimId').val());                        
                        $('#txtRejectReason').val('');

                        setTimeout(function () {
                            window.location.href = "/Mediclaim/IPD/ProcessingList";
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

    $('#btnModalApprove').click(function () {
        validateEntryForm();
        if (isValid) {
            ApprovedAmount = $('#txtApprovedAmount').val();

            if (parseFloat(ApprovedAmount) > 0) {
                $('#approveModal').modal('show');
            }
            else {
                $('#validationModal').modal('show');
            }
        }
    });
    var validateEntryForm = function () {
        var errorCount = 0;

        if ($('#txtDealingAssistantRemark').val() === '') {
            $('#txtDealingAssistantRemark').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtDealingAssistantRemark').removeClass('is-invalid');

        }
        isValid = (errorCount > 0) ? false : true;
    }

    $('#btnModalReject').click(function () {
        $('#rejectModal').modal('show');
    });
});