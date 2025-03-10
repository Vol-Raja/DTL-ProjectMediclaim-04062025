AddEditForm();




$("#btnDeclareInvestment").click(function () {
    if (confirm("Please check the details before Submission!")) {
        $("#frmInvestmentDeclaration").submit();
    }
    else {
        alert("Canceled Investment Declaration...!!!");
    }

});
$("#btnModifyInvestment").click(function () {
    if (confirm("Please check the details before Submission!")) {
        $("#frmInvestmentDeclaration").submit();
    }
    else {
        alert("Canceled Investment Declaration...!!!");
    }

});
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var formData = BindInvestmentData();
            $.ajax({
                url: "/FinanceManagement/AddEditInvestmentDeclaration",
                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //alert(data);
                    toastrSuccess("Data saved successfully!");
                    window.location.href = "/FinanceManagement/InvestmentManagement";
                    ClearData();
                    //if (data == "add" || data == "update") {

                    //}
                },
                error: function (err) {
                    toastrError("Something went wrong!!!");
                }
            });
        }
    });
    $('#frmInvestmentDeclaration').validate({
        rules: {
            FinancialYear: {
                required: true,
            },
            InvestmentTitle: {
                required: true,
            },
            ReferenceNo: {
                required: true,
            },
            InvestmentAmount: {
                required: true,
            },
            FromDate: {
                required: true,
            },
            ToDate: {
                required: true,
            }

        },
        messages: {
            FinancialYear: {
                required: "Please provide Financial Year.",
            },
            InvestmentTitle: {
                required: "Please provide Investment Title.",

            },
            ReferenceNo: {
                required: "Please provide Reference No.",
            },
            InvestmentAmount: {
                required: "Please Provide Investment Amount."
            },
            FromDate: {
                required: "Please provide From Date."
            },
            ToDate: {
                required: "Please Provide To Date."
            }


        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
}

function isNumberKey(evt, element) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
        return false;
    else {
        var len = $(element).val().length;
        var index = $(element).val().indexOf('.');
        if (index > 0 && charCode == 46) {
            return false;
        }
        if (index > 0) {
            var CharAfterdot = (len + 1) - index;
            if (CharAfterdot > 3) {
                return false;
            }
        }

    }
    return true;
}

function ClearData() {
    $("#InvestmentType").val("").change();
    $("#Others").val("");
    $("#InvestmentTitle").val("");
    $("#ReferenceNo").val("");
    $("#InvestedAmount").val("");
    $("#ClosingPeriod").val("");
    $("#FromDate").val("").change();
    $("#ToDate").val("").change();
    $("#ExpectedProfitMargin").val("");
    $("#ExpectedROI").val("");
}

function BindInvestmentData() {
    var frm = $('#frmInvestmentDeclaration');
    var formData = new FormData(frm[0]);
    formData.append('Id', $('#Id').val());
    formData.append('FinancialYear', $('#FinancialYear').val());
    
    var investmenttype = "";
    if ($("#InvestmentType").val() != "")
        investmenttype = $("#InvestmentType").val();
    if ($("#InvestmentType").val() == "" && $("#Others").val() !== "")
        investmenttype = $("#Others").val();
    formData.append('InvestmentType', investmenttype);
    formData.append('InvestmentTitle', $('#InvestmentTitle').val());
    formData.append('ReferenceNo', $('#ReferenceNo').val());
    formData.append('InvestedAmount', $('#InvestedAmount').val());
    formData.append('ClosingPeriod', $('#ClosingPeriod').val());
    formData.append('FromDate', $('#FromDate').val());
    formData.append('ToDate', $('#ToDate').val());
    formData.append('ExpectedProfitMargin', $('#ExpectedProfitMargin').val());
    formData.append('ExpectedROI', $('#ExpectedROI').val());
    return formData;
}
/*
$("#InvestmentType").on('change', function () {
    if ($("#InvestmentType").val() == $("#Others").val()) {
        $("#Others").val("");
    }
});
*/
function CalculateExptectedROI() {    
    var exproi, invamt, expprofitmargin, closingper;
    if (isNaN($("#InvestedAmount").val()) || $("#InvestedAmount").val() == '') {
        invamt = 0
    }
    else
        invamt = $("#InvestedAmount").val();
    if (isNaN($("#ExpectedProfitMargin").val()) || $("#ExpectedProfitMargin").val() == '') {
        expprofitmargin = 0;
    }
    else
        expprofitmargin = $("#ExpectedProfitMargin").val();
    exproi = (invamt * expprofitmargin) / 100;
    $("#ExpectedROI").val(exproi);
    var closingPeriod = $("#ClosingPeriod").val()
    var profit = (parseInt(invamt) +( parseInt(exproi) * closingPeriod));
    $("#ExpectedProfitAmount").val(profit);
}

$("#ExpectedProfitMargin,#InvestedAmount,#ClosingPeriod").change(function () {
    CalculateExptectedROI();
});

$("#ActualReturnOnInvestment").change(function () {
    CalculateProfitLossOnActualReturn();
});

function CalculateProfitLossOnActualReturn() {
    var exproi, actret,res, profitloss;
    if (isNaN($("#ExpectedROI").val()) || $("#ExpectedROI").val() == '') {
        exproi = 0;
    }
    else
        exproi = $("#ExpectedROI").val();
    
    if (isNaN($("#ActualReturnOnInvestment").val()) || $("#ActualReturnOnInvestment").val() == '') {
        actret = 0;
    }
    else
        actret = $("#ActualReturnOnInvestment").val();
    res = parseFloat(actret) - parseFloat(exproi);
    if (res > 0) {
        profitloss = "Profit of Rs.  " + res;
    }
    else
        profitloss = "Loss of Rs.  "+ Math.abs(res) ;
    $("#ActualProfitLossAmt").val(profitloss);

}

$("#btnCloseInvestmentOpt").click(function () {
    var investmentId = $("#Id").val();
    var actualReturnOnInvestment = $("#ActualReturnOnInvestment").val();
    $.ajax('/FinanceManagement/CloseInvestmentOpt', {
        type: 'POST',  // http method
        data: { InvestmentId: investmentId, ActualReturnOnInvestment: actualReturnOnInvestment },  // data to submit
        success: function (data, status, xhr) {
            if (data == "Closed") {
                alert("Investment Closed!!")
                setTimeout(function () {
                    var href = $('#btnCancel').attr('href');
                    window.location.href = href;
                }, 2000);
            }
        },
        error: function (data) {
        }
    });

});

