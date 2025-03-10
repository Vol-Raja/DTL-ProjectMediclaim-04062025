$(function () {
    $("#txtPensionerName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Disbursement/FetchPensioner/',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#txtEmployeeID").val(i.item.val);
            $("#txtMonthlyPension").val(i.item.monthlyPension);
            $('#Employer').val(i.item.employerName).change();
            $('#ChangeType').val(i.item.changeType).change();
            $('#Reason').val(i.item.reason).change();
            var str = i.item.typeOfRecovery;
            if (str != null) {
                var arr = [];
                arr = str.split(",");
                if (arr.length > 0)
                    $('#TypeOfRecovery').val(arr);
            } else { $('#TypeOfRecovery').val("") }
            $('#txtRecoveryAmount').val(i.item.recoveryAmount);
            $('#RecoveryOption').val(i.item.recoveryOption).change();
            $("#txtMonthlyPension").val(i.item.monthlyPension);
            $("#txtApplicableAmount").val(i.item.applicable);
            $("#txtMonthlyPensionAfter").val(i.item.updatedPension);
            if (moment(i.item.froms).format("yyyy-MM-DD") != "0001-01-01" && moment(i.item.froms).format("yyyy-MM-DD") != "1900-01-01") {
                $("#dtFrom").val(moment(i.item.froms).format("yyyy-MM-DD")).change();
            }
            else { $("#dtFrom").val(""); }

            if (moment(i.item.to).format("yyyy-MM-DD") != "0001-01-01" && moment(i.item.to).format("yyyy-MM-DD") != "1900-01-01") {
                $("#dtTo").val(moment(i.item.to).format("yyyy-MM-DD")).change();
            } else { $("#dtTo").val(""); }

            $("#id").val(i.item.id);
            $("#eid").val(i.item.eid);

        },
        minLength: 1
    });
});


$("#txtApplicableAmount").change(function () {
    Calculation();
});

$(function () {
    $("#ChangeType").change("option", function () {
        Calculation();
    });
});

function Calculation() {
    var monthlyPension = $("#txtMonthlyPension").val();
    var updatedAmount;
    if ($("#ChangeType").val() == 'Allowance')
        updatedAmount = parseInt($("#txtMonthlyPension").val()) + parseInt($("#txtApplicableAmount").val());
    else
        updatedAmount = parseInt($("#txtMonthlyPension").val()) - parseInt($("#txtApplicableAmount").val());

    $("#txtMonthlyPensionAfter").val(updatedAmount);
}
