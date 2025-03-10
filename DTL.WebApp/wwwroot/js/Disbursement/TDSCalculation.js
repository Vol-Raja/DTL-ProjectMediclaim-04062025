AddEditForm();

//$("#btnSaveTDS").click(function () {
//    $("#TDSCalculationForm").submit();
//});

function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var formData = BindTDSData();
            $.ajax({
                url: "/Disbursement/AddEditTDSCalculation",
                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //alert(data);
                    toastrSuccess("Data saved successfully!");
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
    $('#TDSCalculationForm').validate({
        rules: {
            FinancialYear: {
                required: true,
            },
            Pensioner: {
                required: true,

            },
            EmployeeId: {
                required: true,
            },
            EmployeeName: {
                required: true,
            }

        },
        messages: {
            FinancialYear: {
                required: "Please provide Financial Year.",
            },
            Pensioner: {
                required: "Please provide Pensioner.",

            },
            EmployeeId: {
                required: "Please provide Employee ID.",
            },
            EmployeeName: {
                required: "Please Provide Employee Name."
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

$(document).ready(function () {
    $("#EmployeeId").autocomplete({

        source: function (request, response) {
            $.ajax({

                url: '/Disbursement/GetEmployee/',
                data: { DtlOffice: $("#Pensioner").val(), SearchType: "EmployeeId", SearchVal: $("#EmployeeId").val() },
                type: "POST",

                success: function (data) {
                    response($.map(data, function (item) {

                        return { label: item.empid, value: item.empid, item };

                        // return item;
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
            $(this).val(i.item.item.empid);

            $("#EmployeeName").val(i.item.item.empName);
            $("#EmpRegId").val(i.item.item.regId);
            $("#DOB").val(i.item.item.dob);
            $("#age").val(i.item.item.age);
            $("#Id").val(i.item.item.tdsId);
            
            if ($("#age").val() < 60) {

                document.getElementById("txt80TTB").setAttribute('readonly', 'true');
                document.getElementById("file80TTB").disabled = true;
                document.getElementById("InvFilePath80TTB").src = "";
            }
            else {
                document.getElementById("txt80TTB").removeAttribute("readonly");
                document.getElementById("file80TTB").disabled = false;
            }

        },
        //minLength: 1
    });

    $("#EmployeeName").autocomplete({
        source: function (request, response) {

            $.ajax({
                url: '/Disbursement/GetEmployee/',

                data: { DtlOffice: $("#Pensioner").val(), SearchType: "EmployeeName", SearchVal: $("#EmployeeName").val() },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        //alert($("#Pensioner").val());
                        return { label: item.label, value: item.label, item };
                        //return item;
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
         
            $("#EmployeeId").val(i.item.item.val);

            $("#EmployeeName").val(i.item.item.label);
            $("#EmpRegId").val(i.item.item.regId);
            $("#DOB").val(i.item.item.dob);
            $("#age").val(i.item.item.age);
            $("#Id").val(i.item.item.tdsId);
            
            if ($("#age").val() < 60) {

                document.getElementById("txt80TTB").setAttribute('readonly', 'true');
                document.getElementById("file80TTB").disabled = true;
                document.getElementById("InvFilePath80TTB").src = "";
            }
            else {
                document.getElementById("txt80TTB").removeAttribute("readonly");
                document.getElementById("file80TTB").disabled = false;
        }

            
        },
        minLength: 1,
    });

});

function BindTDSData() {
    var frm = $('#TDSCalculationForm');
    var formData = new FormData(frm[0]);
    formData.append('Id', $('#Id').val());
    formData.append('EmployeeRegistrationId', $('#EmpRegId').val());
    formData.append('FinancialYear', $('#FinancialYear').val());
    formData.append('Pensioner', $('#Pensioner').val());
    formData.append('Pension', $('#Pension').val());
    formData.append('Gratuity', $('#Gratuity').val());
    formData.append('Commutation', $('#Commutation').val());
    formData.append('LeaveEncashment', $('#LeaveEncashment').val());
    formData.append('AQP', $('#AQP').val());
    formData.append('DA', $('#DA').val());
    formData.append('HRA', $('#HRA').val());
    formData.append('Travel_Allowance', $('#LTA').val());
    formData.append('Medical_Allowance', $('#MA').val());
    formData.append('OtherIncome', $('#OtherIncome').val());
    formData.append('ExemptedAmount', $('#ExemptedAmount').val());
    formData.append('Ex80DD', $('#80DD').val());
    formData.append('Ex80C', $('#80C').val());
    formData.append('Mediclaim', $('#Mediclaim').val());
    formData.append('HomeLoan', $('#HomeLoan').val());
    formData.append('IntOnEduLoan', $('#IntEducationLoan').val());
    formData.append('Donation', $('#Donation').val());
    formData.append('TaxableAmount', $('#TaxableAmount').val());
    formData.append('StandardDeduction', $('#StandardDeduction').val());
    formData.append('NetTaxableAmount', $('#NetTaxableAmount').val());
    formData.append('TaxPayable', $('#TaxPayableAmount').val());
    formData.append('Cess', $('#Cess').val());
    formData.append('MonthlyTDSAmount', $('#MonthlyTDS').val());
    var Ragime = "";
    if ($("#Regime1")[0].checked)
        Ragime = "Regime 1";
    else if ($("#Regime2")[0].checked)
        Ragime = "Regime 2";
    formData.append('Regime', Ragime);
    formData.append('TDSInvestmentId', $("#TDSInvestmentId").val());
    formData.append('TDSCalculationId', $("#Id").val());
    formData.append('Inv80D', $('#txt80D').val());
    formData.append('Inv80DD', $('#txt80DD').val());
    formData.append('Inv80DDB', $('#txt80DDB').val());
    formData.append('Inv5YrsTermDepositPostoffice', $('#txt5YrsTermDepositPostoffice').val());
    formData.append('InvLIC_Pension_Plan', $('#txtLIC_Pension_Plan').val());
    formData.append('InvNSC', $('#txtNSC').val());
    formData.append('InvPPF', $('#txtPPF').val());
    formData.append('InvStampDuty', $('#txtStampDuty').val());
    formData.append('InvSukanyaSmriddhiYojana', $('#txtSukanyaSmriddhiYojana').val());
    formData.append('InvTermDepositBank', $('#txtTermDepositBank').val());
    formData.append('InvTF', $('#txtTF').val());
    formData.append('InvULIP', $('#txtULIP').val());
    formData.append('InvMF', $('#txtMF').val());
    formData.append('InvHousingLoanInt', $('#txtHousingLoanInt').val());
    formData.append('InvHousingLoanInt1617', $('#txtHousingLoanInt1617').val());
    formData.append('InvHousingLoanInt1920', $('#txtHousingLoanInt1920').val());
    formData.append('Inv80E', $('#txt80E').val());
    formData.append('Inv80G', $('#txt80G').val());
    formData.append('Inv80GGB', $('#txt80GGB').val());
    formData.append('Inv80GGC', $('#txt80GGC').val());
    formData.append('Inv80GG', $('#txt80GG').val());
    formData.append('Inv80RRB', $('#txt80RRB').val());
    formData.append('Inv80TTA', $('#txt80TTA').val());
    formData.append('Inv80TTB', $('#txt80TTB').val());
    formData.append('Inv80U', $('#txt80U').val());
    return formData;
}

function CalculateTDS() {
    
    var PensionAmount, Gratuity, Commutation,CommutationDiff, AQP, DA, OtherIncome, ExemptedAmount,HRA,LTA,MA,LeaveEncashment;
    if (isNaN($("#Pension").val()) || $("#Pension").val() == '') { PensionAmount = 0 }
    else
        PensionAmount = $("#Pension").val();
    if (isNaN($("#Gratuity").val()) || $("#Gratuity").val() == '') { Gratuity = 0 }
    else
       Gratuity = $("#Gratuity").val();
        Gratuity =0;
    if (isNaN($("#Commutation").val()) || $("#Commutation").val() == '') {
        Commutation = 0;
    }
    else
        Commutation = $("#Commutation").val();
    if (isNaN($("#AQP").val()) || $("#AQP").val() == '') {
        AQP = 0;
    }
    else
        AQP = $("#AQP").val();
    if (isNaN($("#DA").val()) || $("#DA").val() == '') {
        DA = 0;
    }
    else
        DA = $("#DA").val();
    if (isNaN($("#OtherIncome").val()) || $("#OtherIncome").val() == '') {
        OtherIncome = 0;
    }
    else
        OtherIncome = $("#OtherIncome").val();
    if (isNaN($("#ExemptedAmount").val()) || $("#ExemptedAmount").val() == '') {
        ExemptedAmount = 0;
    }
    else
        ExemptedAmount = $("#ExemptedAmount").val();

    if (isNaN($("#HRA").val()) || $("#HRA").val() == '') {
        HRA = 0;
    }
    else
        HRA = $("#HRA").val();
    if (isNaN($("#LTA").val()) || $("#LTA").val() == '') {
        LTA = 0;
    }
    else
        LTA = $("#LTA").val();
    if (isNaN($("#MA").val()) || $("#MA").val() == '') {
        MA = 0;
    }
    else
        MA = $("#MA").val();

    //Leave Enchasement >300000 than gratuity will be taxable on diff 
    //check below code
    if (isNaN($("#LeaveEncashment").val()) || $("#LeaveEncashment").val() == '')
        {
        LeaveEncashment = 0;
    }
    else
        LeaveEncashment = $("#LeaveEncashment").val();
    if (LeaveEncashment > 700000) {
        LeaveEncashment = LeaveEncashment - 700000;
    }
    
    /*if (Commutation > 33.33) {
        CommutationDiff = Commutation - 33.33;
        Commutation = (PensionAmount * CommutationDiff) / 100;
    }
    else
        Commutation = 0;*/
    
    /* var TaxableAmount = $("#TaxableAmount").val();
   var StandardDeduction = $("#StandardDeduction").val();
   var NetTaxableAmount = $("#NetTaxableAmount").val();
   var TaxPayableAmount = $("#TaxPayableAmount").val();
   var MonthlyTDS = $("#MonthlyTDS").val();*/
   /* var commutationPerDiff;
    if (Commutation > 33.33)
        commutationPerDiff = parseFloat(Commutation) - 33.33;
        */

    var Taxableamt = parseFloat(PensionAmount) + parseFloat(Gratuity) +
        parseFloat(AQP) + parseFloat(DA) + parseFloat(OtherIncome) + parseFloat(LeaveEncashment) - parseFloat(Commutation);
    var salexmpted = parseFloat(HRA) + parseFloat(LTA) + parseFloat(MA);
    var TotTaxableamt = Taxableamt - salexmpted - parseFloat(ExemptedAmount);
    if (TotTaxableamt < 0)
        TotTaxableamt = 0;
    //alert(TotTaxableamt);
    $("#TaxableAmount").val(TotTaxableamt);

}

function CalculateExemptedAmount() {

    
    var var80DD, var80C, Mediclaim, Homeloan, IntEducationLoan, Donation;
    if (isNaN($("#80DD").val()) || $("#80DD").val() == '') {
        var80DD = 0;
    }
    else
        var80DD = $("#80DD").val();

    if (isNaN($("#80C").val()) || $("#80C").val() == '') { var80C = 0 }
    else
        var80C = $("#80C").val();

    if (isNaN($("#Mediclaim").val()) || $("#Mediclaim").val() == '') {
        //alert("hello");
        Mediclaim = 0;
    }
    else
        Mediclaim = $("#Mediclaim").val();
    if (isNaN($("#Homeloan").val()) || $("#Homeloan").val() == '') {
        Homeloan = 0;
    }
    else
        Homeloan = $("#Homeloan").val();
    if (isNaN($("#IntEducationLoan").val()) || $("#IntEducationLoan").val() == '') {
        IntEducationLoan = 0;
    }
    else
        IntEducationLoan = $("#IntEducationLoan").val();

    if (isNaN($("#Donation").val()) || $("#Donation").val() == '') {
        Donation = 0;
    }
    else
        Donation = $("#Donation").val();
    //alert(Mediclaim);
    var ExemptedAmount = parseFloat(var80DD) + parseFloat(var80C) + parseFloat(Mediclaim) +
        parseFloat(Homeloan) + parseFloat(IntEducationLoan) + parseFloat(Donation);
    $("#ExemptedAmount").val(ExemptedAmount);
}

$("#ExemptedAmount").change(function () {
    CalculateExemptedAmount();
    CalculateTDS();
    calculateTaxableAmount();
});

$("#txt80D").change(function () {

    if ($("#age").val() < 60) {

        if ($("#txt80D").val() > 25000) {
            toastrError("80D - Declaration not more than 25000");
            $("#txt80D").val("0");
        }
        else {
            CalculateExemptedAmount();
            CalculateTDS();
            // GetJsonData();
            calculateTaxableAmount();
        }

    }
    else {

        if ($("#txt80D").val() > 50000) {
            toastrError("80D - Declaration not more than 50000");
            $("#txt80D").val("0");

        }
    }
});

$("#txt80DD").change(function () {
    if ($("#txt80DD").val() > 125000) {
        toastrError("80DD - Declaration not more than 125000");
        $("#txt80DD").val("0");

    }
    else {
        CalculateExemptedAmount();
        CalculateTDS();
        calculateTaxableAmount();
    }
});

$("#txt80DDB").change(function () {

    if ($("#age").val() < 60) {

        if ($("#txt80DDB").val() > 40000) {
            toastrError("80DDB - Declaration not more than 40000");
            $("#txt80DDB").val("0");
        }
        else {
            CalculateExemptedAmount();
            CalculateTDS();
            // GetJsonData();
            calculateTaxableAmount();
        }

    }
    else {

        if ($("#txt80DDB").val() > 100000) {
            toastrError("80D - Declaration not more than 100000");
            $("#txt80DDB").val("0");

        }
    }
});

function Calculate80CTot() {

    var var5YrsTermDepositPostoffice, LIC_Pension_Plan, NSC, PPF, StampDuty, SukanyaSmriddhiYojana, TermDeposit, TF, ULIP, MF, tot;
    if (isNaN($("#txt5YrsTermDepositPostoffice").val()) || $("#txt5YrsTermDepositPostoffice").val() == '') {
        var5YrsTermDepositPostoffice = 0;
    }
    else
        var5YrsTermDepositPostoffice = $("#txt5YrsTermDepositPostoffice").val();

    if (isNaN($("#txtLIC_Pension_Plan").val()) || $("#txtLIC_Pension_Plan").val() == '') {
        LIC_Pension_Plan = 0;
    }
    else
        LIC_Pension_Plan = $("#txtLIC_Pension_Plan").val();

    if (isNaN($("#txtNSC").val()) || $("#txtNSC").val() == '')
        NSC = 0;
    else
        NSC = $("#txtNSC").val();

    if (isNaN($("#txtPPF").val()) || $("#txtPPF").val() == '')
        PPF = 0
    else
        PPF = $("#txtPPF").val();

    if (isNaN($("#txtStampDuty").val()) || $("#txtStampDuty").val() == '')
        StampDuty = 0;
    else
        StampDuty = $("#txtStampDuty").val();

    if (isNaN($("#txtSukanyaSmriddhiYojana").val()) || $("#txtSukanyaSmriddhiYojana").val() == '')
        SukanyaSmriddhiYojana = 0;
    else
        SukanyaSmriddhiYojana = $("#txtSukanyaSmriddhiYojana").val();

    if (isNaN($("#txtTermDepositBank").val()) || $("#txtTermDepositBank").val() == '')
        TermDeposit = 0;
    else
        TermDeposit = $("#txtTermDepositBank").val();


    if (isNaN($("#txtTF").val()) || $("#txtTF").val() == '')
        TF = 0;
    else
        TF = $("#txtTF").val();


    if (isNaN($("#txtULIP").val()) || $("#txtULIP").val() == '')
        ULIP = 0;
    else
        ULIP = $("#txtULIP").val();


    if (isNaN($("#txtMF").val()) || $("#txtMF").val() == '')
        MF = 0;
    else
        MF = $("#txtMF").val();

    tot = parseFloat(var5YrsTermDepositPostoffice) + parseFloat(LIC_Pension_Plan) + parseFloat(NSC) + parseFloat(PPF) + parseFloat(StampDuty) + parseFloat(SukanyaSmriddhiYojana) + parseFloat(TermDeposit) +
        parseFloat(TF) + parseFloat(ULIP) + parseFloat(MF);

    if (tot > 150000) {
        toastrError("80C - Declaration not more than 150000");
    }

}

$("#txt5YrsTermDepositPostoffice").change(function () {

    Calculate80CTot();
});

$("#txtLIC_Pension_Plan").change(function () {
    Calculate80CTot();
});

$("#txtNSC").change(function () {
    Calculate80CTot();
});

$("#txtPPF").change(function () {
    Calculate80CTot();
});

$("#txtStampDuty").change(function () {
    Calculate80CTot();
});

$("#txtSukanyaSmriddhiYojana").change(function () {
    Calculate80CTot();
});

$("#txtTermDepositBank").change(function () {
    Calculate80CTot();
});

$("#txtTF").change(function () {
    Calculate80CTot();
});

$("#txtULIP").change(function () {
    Calculate80CTot();
});

$("#txtMF").change(function () {
    Calculate80CTot();
});


$("#txtHousingLoanInt").change(function () {
    if ($("#txtHousingLoanInt").val() > 150000) {
        toastrError("Housing Loan Principle and Interest - Declaration not more than 150000");
        $("#txtHousingLoanInt").val("0");

    }

});


$("#txtHousingLoanInt1617").change(function () {
    if ($("#txtHousingLoanInt1617").val() > 30000) {
        toastrError("Housing Loan Principle and Interest Before 31/3/1999- Declaration not more than 30000");
        $("#txtHousingLoanInt1617").val("0");

    }

});


$("#txtHousingLoanInt1920").change(function () {
    if ($("#txtHousingLoanInt1920").val() > 200000) {
        toastrError("Housing Loan Principle and Interest after 31/3/1999- Declaration not more than 200000");
        $("#txtHousingLoanInt1920").val("0");

    }

});

$("#btnAddExemptionAmt").click(function () {


    var var80D, var80DD, var80DDB, tot80D, tot;
    if (isNaN($("#txt80D").val()) || $("#txt80D").val() == '')
        var80D = 0;
    else
        var80D = $("#txt80D").val();

    if (isNaN($("#txt80DD").val()) || $("#txt80DD").val() == '')
        var80DD = 0;
    else
        var80DD = $("#txt80DD").val();
    if (isNaN($("#txt80DDB").val()) || $("#txt80DDB").val() == '')
        var80DDB = 0;
    else
        var80DDB = $("#txt80DDB").val();

    tot80D = parseFloat(var80D) + parseFloat(var80DD) + parseFloat(var80DDB);

    var var5YrsTermDepositPostoffice, LIC_Pension_Plan, NSC, PPF, StampDuty, SukanyaSmriddhiYojana, TermDeposit, TF, ULIP, MF, tot80C;
    if (isNaN($("#txt5YrsTermDepositPostoffice").val()) || $("#txt5YrsTermDepositPostoffice").val() == '') {
        var5YrsTermDepositPostoffice = 0;
    }
    else
        var5YrsTermDepositPostoffice = $("#txt5YrsTermDepositPostoffice").val();

    if (isNaN($("#txtLIC_Pension_Plan").val()) || $("#txtLIC_Pension_Plan").val() == '') {
        LIC_Pension_Plan = 0;
    }
    else
        LIC_Pension_Plan = $("#txtLIC_Pension_Plan").val();

    if (isNaN($("#txtNSC").val()) || $("#txtNSC").val() == '')
        NSC = 0;
    else
        NSC = $("#txtNSC").val();

    if (isNaN($("#txtPPF").val()) || $("#txtPPF").val() == '')
        PPF = 0
    else
        PPF = $("#txtPPF").val();

    if (isNaN($("#txtStampDuty").val()) || $("#txtStampDuty").val() == '')
        StampDuty = 0;
    else
        StampDuty = $("#txtStampDuty").val();

    if (isNaN($("#txtSukanyaSmriddhiYojana").val()) || $("#txtSukanyaSmriddhiYojana").val() == '')
        SukanyaSmriddhiYojana = 0;
    else
        SukanyaSmriddhiYojana = $("#txtSukanyaSmriddhiYojana").val();

    if (isNaN($("#txtTermDepositBank").val()) || $("#txtTermDepositBank").val() == '')
        TermDeposit = 0;
    else
        TermDeposit = $("#txtTermDepositBank").val();


    if (isNaN($("#txtTF").val()) || $("#txtTF").val() == '')
        TF = 0;
    else
        TF = $("#txtTF").val();


    if (isNaN($("#txtULIP").val()) || $("#txtULIP").val() == '')
        ULIP = 0;
    else
        ULIP = $("#txtULIP").val();


    if (isNaN($("#txtMF").val()) || $("#txtMF").val() == '')
        MF = 0;
    else
        MF = $("#txtMF").val();

    tot80C = parseFloat(var5YrsTermDepositPostoffice) + parseFloat(LIC_Pension_Plan) + parseFloat(NSC) + parseFloat(PPF) + parseFloat(StampDuty) + parseFloat(SukanyaSmriddhiYojana) + parseFloat(TermDeposit) +
        parseFloat(TF) + parseFloat(ULIP) + parseFloat(MF);


    var HousingLoanInt, HousingLoanInt1617, HousingLoanInt1920, totHousingLoan;
    if (isNaN($("#txtHousingLoanInt").val()) || $("#txtHousingLoanInt").val() == '')
        HousingLoanInt = 0;
    else
        HousingLoanInt = $("#txtHousingLoanInt").val();



    if (isNaN($("#txtHousingLoanInt1617").val()) || $("#txtHousingLoanInt1617").val() == '')
        HousingLoanInt1617 = 0;
    else
        HousingLoanInt1617 = $("#txtHousingLoanInt1617").val();

    if (isNaN($("#txtHousingLoanInt1920").val()) || $("#txtHousingLoanInt1920").val() == '')
        HousingLoanInt1920 = 0;
    else
        HousingLoanInt1920 = $("#txtHousingLoanInt1920").val();


    totHousingLoan = parseFloat(HousingLoanInt) + parseFloat(HousingLoanInt1617) + parseFloat(HousingLoanInt1920);
    var var80E;

    if (isNaN($("#txt80E").val()) || $("#txt80E").val() == '')
        var80E = 0;
    else
        var80E = $("#txt80E").val();

    var var80G, var80GGB, var80GGC, totG;
    if (isNaN($("#txt80G").val()) || $("#txt80G").val() == '')
        var80G = 0;
    else
        var80G = $("#txt80G").val();

    if (isNaN($("#txt80GGB").val()) || $("#txt80GGB").val() == '')
        var80GGB = 0;
    else
        var80GGB = $("#txt80GGB").val();

    if (isNaN($("#txt80GGC").val()) || $("#txt80GGC").val() == '')
        var80GGC = 0;
    else
        var80GGC = $("#txt80GGC").val();


    var var80GG, var80RRB, var80TTA, var80TTB, var80U,totGGR;
    if (isNaN($("#txt80GG").val()) || $("#txt80GG").val() == '')
        var80GG = 0;
    else
        var80GG = $("#txt80GG").val();

    if (isNaN($("#txt80RRB").val()) || $("#txt80RRB").val() == '')
        var80RRB = 0;
    else
        var80RRB = $("#txt80RRB").val();

    if (isNaN($("#txt80TTA").val()) || $("#txt80TTA").val() == '')
        var80TTA = 0;
    else
        var80TTA = $("#txt80TTA").val();

    if (isNaN($("#txt80TTB").val()) || $("#txt80TTB").val() == '')
        var80TTB = 0;
    else
        var80TTB = $("#txt80TTB").val();
    if (isNaN($("#txt80U").val()) || $("#txt80U").val() == '')
        var80U = 0;
    else
        var80U = $("#txt80U").val();

    totGGR = parseFloat(var80GG) + parseFloat(var80RRB) + parseFloat(var80TTA) + parseFloat(var80TTB) + parseFloat(var80U);
    


    totG = parseFloat(var80E) + parseFloat(var80G) + parseFloat(var80GGB) + parseFloat(var80GGC);

    tot = parseFloat(tot80D) + parseFloat(tot80C) + parseFloat(totHousingLoan) + parseFloat(totG) + parseFloat(totGGR);


    $("#ExemptedAmount").val(tot);
    CalculateTDS();
    calculateTaxableAmount();

});

$("#80C").change(function () {
    $.ajax({
        dataType: "json",
        url: '/js/Disbursement/Validation.json',
        data: "",
        success(response) {
            //alert(response.Ex80C[0].Amount);
            // alert($("#80C").val());
            if ($("#80C").val() > response.Ex80C[0].Amount) {
                toastrError("80C - Declaration not more than 150000");
                $("#80C").val("0");
            }
            else {
                CalculateExemptedAmount();
                CalculateTDS();
                calculateTaxableAmount();
            }
        },
        error: function (err) {

        }
    });
});

$("#Pension").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});

$("#Gratuity").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#Commutation").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#AQP").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#DA").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#OtherIncome").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});

$("#HRA").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#LTA").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#MA").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#LeaveEncashment").change(function () {
    CalculateTDS();
    calculateTaxableAmount();
});
$("#StandardDeduction").change(function () {
    ///alert("hello");
    var StdDev, TaxPayable, NetTaxPay;
    if (isNaN($("#TaxableAmount").val()) || $("#TaxableAmount").val() == '') {

        TaxPayable = 0;

    }
    else
        TaxPayable = $("#TaxableAmount").val();
    //alert(TaxPayable);
    if (isNaN($("#StandardDeduction").val()) || $("#StandardDeduction").val() == '') {
        StdDev = 0;
    }
    else
        StdDev = $("#StandardDeduction").val();
    //alert(TaxPayable);
    if (TaxPayable < 50000) {
        StdDev = 0;
        $("#StandardDeduction").val("0");
    }
    NetTaxPay = TaxPayable - StdDev;

    //alert(NetTaxPay);
    $("#NetTaxableAmount").val(NetTaxPay);
    calculateTaxableAmount();
});


/*$("#TaxPayableAmount").change(function () {
   
    var TaxPayAmt, MonthlyTDS;
    
    if (isNaN($("#TaxPayableAmount").val()) || $("#TaxPayableAmount").val() == '') { TaxPayAmt = 0; }
    else
        TaxPayAmt = $("#TaxPaymentAmount").val();
    MonthlyTDS = TaxPayAmt / 12;
    $("#MonthlyTDS").val(MonthlyTDS);
});*/



function GetJsonData() {
    $.ajax({
        dataType: "json",
        url: '/js/Disbursement/Validation.json',
        data: "",
        success(response) {
            //alert(response.Ex80C[0].Amount);
        }
    });
}

function SelectFinancialYear() {
    var d = new Date();
    var n = d.getMonth();
    var yr = d.getFullYear();
    //alert(yr);
    if (n > 2)
        $("#FinancialYear").val(yr).change();
    else
        $("#FinancialYear").val(yr - 1).change();

}

function ClearData() {
    $("#Id").val("");
    $("#TDSInvestmentId").val("");
    $("#FinancialYear").val("").change();
    $("#Pensioner").val("").change();
    $("#EmployeeId").val("");
    $("#EmployeeName").val("");
    $("#EmpRegId").val("");
    $("#Pension").val("");
    $("#Gratuity").val("");
    $("#Commutation").val("");
    $("#LeaveEncashment").val("");
    $("#AQP").val("");
    $("#DA").val("");
    $("#OtherIncome").val("");
    $("#ExemptedAmount").val("");
    $("#80DD").val("");
    $("#80C").val("");
    $("#Mediclaim").val("");
    $("#Homeloan").val("");
    $("#IntEducationLoan").val("");
    $("#Donation").val("");
    $("#TaxableAmount").val("");
    $("#StandardDeduction").val("");
    $("#NetTaxableAmount").val("");
    $("#TaxPayableAmount").val("");
    $("#MonthlyTDS").val("");

    $("#TDSInvestmentId").val("");
    $("#Id").val("");
    $('#txt80D').val("");
    $('#txt80DD').val("");
    $('#txt80DDB').val("");
    $('#txt5YrsTermDepositPostoffice').val("");
    $('#txtLIC_Pension_Plan').val("");
    $('#txtNSC').val("");
    $('#txtPPF').val("");
    $('#txtStampDuty').val("");
    $('#txtSukanyaSmriddhiYojana').val("");
    $('#txtTermDepositBank').val("");
    $('#txtTF').val("");
    $('#txtULIP').val("");
    $('#txtMF').val("");
    $('#txtHousingLoanInt').val("");
    $('#txtHousingLoanInt1617').val("");
    $('#txtHousingLoanInt1920').val("");
    $('#txt80E').val("");
    $('#txt80G').val("");
    $('#txt80GGB').val("");
    $('#txt80GGC').val("");


    SelectFinancialYear();
}

$("#EmployeeId").change(function () {
    var empId = $(this).val();
    var empregId = $("#EmpRegId").val();
    var FinYear = $("#FinancialYear").val();
    $.ajax({
        url: '/Disbursement/GetPensionData',
        type: 'POST',  // http method
        data: { EmpRegId: empregId, FinYear: FinYear },  // data to submit
        success: function (data, status, xhr) {
            
            $("#Pension").val(data.pension);
            $("#Gratuity").val(data.gratuity);
            $("#Commutation").val(data.commutation);
            $("#DA").val(data.da);
            $("#AQP").val(data.aqp);
            $("#LeaveEncashment").val(data.leaveEncashment);
             GetTDSDataFromFinancialYear_Emp();

            $('#btnAddExemptionAmt').trigger('click');
            CalculateExemptedAmount();
            CalculateTDS();
            calculateTaxableAmount();
        },
        error: function (jqXhr, textStatus, errorMessage) {
        }
    });


});

$("#EmployeeName").change(function () {
    var empId = $(this).val();
    var empregId = $("#EmpRegId").val();
    var FinYear = $("#FinancialYear").val();
    $.ajax('/Disbursement/GetPensionData', {
        type: 'POST',  // http method
        data: { EmpRegId: empregId, FinYear: FinYear },  // data to submit
        success: function (data, status, xhr) {
            $("#Pension").val(data.pension);
            $("#Gratuity").val(data.gratuity);
            $("#Commutation").val(data.commutation);
            $("#DA").val(data.da);
            $("#AQP").val(data.aqp);
            $("#LeaveEncashment").val(data.leaveEncashment);
            GetTDSDataFromFinancialYear_Emp();

            CalculateExemptedAmount();
            CalculateTDS();
            calculateTaxableAmount();

        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('p').append('Error' + errorMessage);
        }
    });
});

function GetTDSDataFromFinancialYear_Emp() {
    var empregId = $("#EmpRegId").val();
    var FinYear = $("#FinancialYear").val();
    $.ajax('/Disbursement/GetTDSCalculationDetails', {
        type: 'POST',  // http method
        data: { EmpRegId: empregId, FinYear: FinYear },  // data to submit
        success: function (data, status, xhr) {
            if (data) {
                // $("#Id").val(data.id);
                //$("#Pension").val(data.pension);
                //$("#Gratuity").val(data.gratuity);
                //$("#Commutation").val(data.commutation);
                //$("#DA").val(data.da);
                //$("#AQP").val(data.aqp);
                $("#HRA").val(data.hra);
                $("#LTA").val(data.travel_Allowance);
                $("#MA").val(data.medical_Allowance);
                $("#LeaveEncashment").val(data.leaveEncashment);
                $("#OtherIncome").val(data.otherIncome);
                $("#ExemptedAmount").val(data.exemptedAmount);
                //$("#80DD").val(data.ex80DD);
                //$("#80C").val(data.ex80C);
                //$("#Mediclaim").val(data.mediclaim);
                //$("#Homeloan").val(data[0].homeLoan);
                //$("#IntEducationLoan").val(data[0].intOnEduLoan);
                //$("#Donation").val(data[0].donation);
                $("#TaxableAmount").val(data.taxableAmount);
                $("#StandardDeduction").val(data.standardDeduction);
                $("#NetTaxableAmount").val(data.netTaxableAmount);
                $("#TaxPayableAmount").val(data.taxPayable);
                $("#Cess").val(data.cess);
                $("#MonthlyTDS").val(data.monthlyTDSAmount);

                if (data.regime == "Regime 1") {
                    $("#Regime1")[0].checked = true;
                    $("#divRegima1StdDed").show();
                }
                else if (data.regime == "Regime 2") {
                    $("#Regime2")[0].checked = true;
                    $("#divRegima1StdDed").hide();
                }


                //$("#Gratuity").val(data.gratuity);
                //$("#Commutation").val(data.commutation);
                //$("#DA").val(data.da);
                //$("#AQP").val(data.aqp);
                //$("#OtherIncome").val(data.otherIncome);
                //$("#ExemptedAmount").val(data.exemptedAmount);
                //$("#80DD").val(data.ex80DD);
                //$("#80C").val(data.ex80C);
                //$("#Mediclaim").val(data.mediclaim);
                //$("#Homeloan").val(data.homeLoan);
                GetTDSInvestmentDetails(empregId, data.id);
            }
        },
        error: function (data) {
        }
    });
}

function GetTDSInvestmentDetails(empregId, tdsinvid) {

    $.ajax('/Disbursement/GetTDSInvestmentDetails', {
        type: 'POST',  // http method
        data: { EmpRegId: empregId, TDSCalculationId: tdsinvid },  // data to submit
        success: function (data, status, xhr) {            
            if (data) {
                
                $("#TDSInvestmentId").val(data.tdsInvestmentId);
                $('#txt80D').val(data.inv80D);
                $('#txt80DD').val(data.inv80DD);
                $('#txt80DDB').val(data.inv80DDB);
                $('#txt5YrsTermDepositPostoffice').val(data.inv5YrsTermDepositPostoffice);
                $('#txtLIC_Pension_Plan').val(data.invLIC_Pension_Plan);
                $('#txtNSC').val(data.invNSC);
                $('#txtPPF').val(data.invPPF);
                $('#txtStampDuty').val(data.invStampDuty);
                $('#txtSukanyaSmriddhiYojana').val(data.invSukanyaSmriddhiYojana);
                $('#txtTermDepositBank').val(data.invTermDepositBank);
                $('#txtTF').val(data.invTF);
                $('#txtULIP').val(data.invULIP);
                $('#txtMF').val(data.invMF);
                $('#txtHousingLoanInt').val(data.invHousingLoanInt);
                $('#txtHousingLoanInt1617').val(data.invHousingLoanInt1617);
                $('#txtHousingLoanInt1920').val(data.invHousingLoanInt1920);
                $('#txt80E').val(data.inv80E);
                $('#txt80G').val(data.inv80G);
                $('#txt80GGB').val(data.inv80GGB);
                $('#txt80GGC').val(data.inv80GGC);
                $('#txt80GG').val(data.inv80GG);
                $('#txt80RRB').val(data.inv80RRB);
                $('#txt80TTA').val(data.inv80TTA);
                $('#txt80TTB').val(data.inv80TTB);
                $('#txt80U').val(data.inv80U);
                $("#InvFilePath80D").attr('data-filePath',data.invFilePath80D);
                $("#InvFilePath80DD").attr('data-filePath',data.invFilePath80DD);
                $("#InvFilePath80DDB").attr('data-filePath',data.invFilePath80DDB);
                $("#InvFilePath5YrsTermDepositPostoffice").attr('data-filePath',data.invFilePath5YrsTermDepositPostoffice);
                $("#InvFilePath80E").attr('data-filePath',data.invFilePath80E);
                $("#InvFilePath80G").attr('data-filePath',data.invFilePath80G);
                $("#InvFilePath80GGB").attr('data-filePath',data.invFilePath80GGB);
                $("#InvFilePath80GGC").attr('data-filePath',data.invFilePath80GGC);
                $("#InvFilePathHousingLoanInt").attr('data-filePath',data.invFilePathHousingLoanInt);
                $("#InvFilePathHousingLoanInt1617").attr('data-filePath',data.invFilePathHousingLoanInt1617);
                $("#InvFilePathHousingLoanInt1920").attr('data-filePath',data.invFilePathHousingLoanInt1920);
                $("#InvFilePathLIC_Pension_Plan").attr('data-filePath',data.invFilePathLIC_Pension_Plan);
                $("#InvFilePathMF").attr('data-filePath',data.invFilePathMF);
                $("#InvFilePathNSC").attr('data-filePath',data.invFilePathNSC);
                $("#InvFilePath5YrsTermDepositPostoffice").attr('data-filePath',data.invFilePath5YrsTermDepositPostoffice);
                $("#InvFilePathPPF").attr('data-filePath',data.invFilePathPPF);
                $("#InvFilePathStampDuty").attr('data-filePath',data.invFilePathStampDuty);
                $("#InvFilePathSukanyaSmriddhiYojana").attr('data-filePath',data.invFilePathSukanyaSmriddhiYojana);
                $("#InvFilePathTermDepositBank").attr('data-filePath',data.invFilePathTermDepositBank);
                $("#InvFilePathTF").attr('data-filePath',data.invFilePathTF);
                $("#InvFilePathULIP").attr('data-filePath', data.invFilePathULIP);
                
                $("#InvFilePath80GG").attr('data-filePath', data.invFilePath80GG);
                $("#InvFilePath80RRB").attr('data-filePath', data.invFilePath80RRB);
                $("#InvFilePath80TTA").attr('data-filePath', data.invFilePath80TTA);
                $("#InvFilePath80TTB").attr('data-filePath', data.invFilePath80TTB);
                $("#InvFilePath80U").attr('data-filePath', data.invFilePath80U);
            }

        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('p').append('Error' + errorMessage);
        }
    });

}
$("#Regime1").change(function () {
    var reg1 = $("#Regime1").val();
    if (reg1 == "Regime 1") {
        $("#divRegima1").show();
        $("#divRegima1StdDed").show();
        //Require Approval
       /* document.getElementById("li80c").style.display = 'block';
        document.getElementById("liEduLoan").style.display = 'block';
        $("#div80D").show();
        $("#div80DD").show();
        $("#divHousingLoanInt").show();
        $("#div80G").show();
        $("#div80TTA").show();
        $("#div80TTB").show();*/
        calculateTaxableAmount();
    }
});
$("#Regime2").change(function () {

    var reg2 = $("#Regime2").val();
    if (reg2 == "Regime 2") {

        $("#divRegima1").hide();
        $("#divRegima1StdDed").hide();
        //Require approval
       /* document.getElementById("li80c").style.display = 'none';
        document.getElementById("liEduLoan").style.display = 'none';
        $("#div80D").hide();
        $("#div80DD").hide();
        $("#divHousingLoanInt").hide();
        $("#div80G").hide();
        $("#div80TTA").hide();
        $("#div80TTB").hide();*/
        calculateTaxableAmount();
    }

});
$("#btnAdd80DD").click(function () {
    //alert("hello");
    $("#div80DD").show();
});

function calculateTaxableAmount() {

    var TaxableAmount, ExemptedAmt, StdDed, amt, tot, vartds, regima, regima1, regima2,CessPer,CessAmt=0,TotTaxWithCess=0;
    if (isNaN($("#TaxableAmount").val()) || $("#TaxableAmount").val() == '') { TaxableAmount = 0 }
    else
        TaxableAmount = $("#TaxableAmount").val();
    if (isNaN($("#ExemptedAmount").val()) || $("#ExemptedAmount").val() == '') {
        ExemptedAmt = 0;
    }
    else
        ExemptedAmt = $("#ExemptedAmount").val();
    if (isNaN($("#StandardDeduction").val()) || $("#StandardDeduction").val() == '') { StdDed = 0 }
    else
        StdDed = $("#StandardDeduction").val();
    if (isNaN($("#Cess").val()) || $("#Cess").val() == '') { CessPer = 0 }
    else
        CessPer = $("#Cess").val();
    var tot;
    tot = TaxableAmount - StdDed;
    $("#NetTaxableAmount").val(tot);

    if ($("#Regime1")[0].checked && $("#age").val() < 60) {

        if (tot <= 250000) {
            amt = 0;
            $("#TaxPayableAmount").val(amt);
            $("#MonthlyTDS").val(amt);

        }
        if (tot > 250000 && tot <= 500000) {
            amt = tot * 0.05;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);
        }
        if (tot > 500000 && tot <= 1000000) {

            amt = tot * 0.20;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1000000) {
            amt = tot * 0.30;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
    }

    if ($("#Regime2")[0].checked && $("#age").val() < 60) {

        if (tot <= 250000) {
            amt = 0;
            $("#TaxPayableAmount").val(amt);
            $("#MonthlyTDS").val(amt);

        }
        if (tot > 250000 && tot <= 500000) {
            amt = tot * 0.05;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);
        }
        if (tot > 500000 && tot <= 750000) {
            amt = tot * 0.10;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 750000 && tot <= 1000000) {
            amt = tot * 0.15;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1000000 && tot <= 1250000) {
            amt = tot * 0.20;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1250000 && tot <= 1500000) {
            amt = tot * 0.25;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }

        if (tot > 1500000) {
            amt = tot * 0.30;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
    }

    if ($("#Regime1")[0].checked && ($("#age").val() >= 60 && $("#age").val() < 80)) {

        if (tot <= 300000) {
            amt = 0;
            $("#TaxPayableAmount").val(amt);
            $("#MonthlyTDS").val(amt);

        }
        if (tot > 300000 && tot <= 500000) {
            var diff = tot - 300000;
            amt = diff * 0.05;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);
        }
        if (tot > 500000 && tot <= 1000000) {
            var diff = tot - 500000;
            amt = 10000 + (diff * 0.20);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1000000) {
            var diff = tot - 1000000;
            amt = 110000 + (diff * 0.30);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
    }
    if ($("#Regime2")[0].checked && ($("#age").val() >= 60 && $("#age").val() < 80)) {

        if (tot <= 250000) {
            amt = 0;
            $("#TaxPayableAmount").val(amt);
            $("#MonthlyTDS").val(amt);

        }
        if (tot > 250000 && tot <= 500000) {
            var diff = tot - 250000;
            amt = diff * 0.05;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);
        }
        if (tot > 500000 && tot <= 750000) {
            var diff = tot - 500000;
            amt = 12500 + (diff * 0.10);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 750000 && tot <= 1000000) {
            var diff = tot - 750000;
            amt = 37500 + (diff * 0.15);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1000000 && tot <= 1250000) {
            var diff = tot - 1000000;
            amt = 75000 + (diff * 0.20);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1250000 && tot <= 1500000) {
            var diff = tot - 1250000;
            amt = 125000 + (diff * 0.25);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }

        if (tot > 1500000) {
            var diff = tot - 1500000;
            amt = 187500 + (diff * 0.30);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
    }

    if ($("#Regime1")[0].checked && $("#age").val() >= 80) {

        if (tot <= 500000) {
            amt = 0;
            $("#TaxPayableAmount").val(amt);
            $("#MonthlyTDS").val(amt);

        }
        if (tot > 500000 && tot <= 1000000) {
            var diff = tot - 500000;
            amt = diff * 0.20;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);
        }
        if (tot > 1000000) {
            var diff = tot - 1000000;
            amt = 100000 + (diff * 0.30);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
    }

    if ($("#Regime2")[0].checked && $("#age").val() >= 80) {

        if (tot <= 250000) {
            amt = 0;
            $("#TaxPayableAmount").val(amt);
            $("#MonthlyTDS").val(amt);

        }
        if (tot > 250000 && tot <= 500000) {
            var diff = tot - 250000;
            amt = diff * 0.05;
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);
        }
        if (tot > 500000 && tot <= 750000) {
            var diff = tot - 500000;
            amt = 12500 + (diff * 0.10);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 750000 && tot <= 1000000) {
            var diff = tot - 750000;
            amt = 37500 + (diff * 0.15);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1000000 && tot <= 1250000) {
            var diff = tot - 1000000;
            amt = 75000 + (diff * 0.20);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
        if (tot > 1250000 && tot <= 1500000) {
            var diff = tot - 1250000;
            amt = 125000 + (diff * 0.25);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }

        if (tot > 1500000) {
            var diff = tot - 1500000;
            amt = 187500 + (diff * 0.30);
            amt = amt.toFixed(2);
            $("#TaxPayableAmount").val(amt);
            if (CessPer > 0) {
                CessAmt = (amt * CessPer) / 100;
                TotTaxWithCess = parseFloat(amt) + parseFloat(CessAmt);
            }
            vartds = TotTaxWithCess / 12;
            vartds = vartds.toFixed(2);
            $("#MonthlyTDS").val(vartds);

        }
    }

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


$('#FinancialYear').on('change', function () {
    $("#Id").val("");
    $("#TDSInvestmentId").val("");
    $("#EmployeeId").val("");
    $("#EmployeeName").val("");
    $("#EmpRegId").val("");
    $("#Pension").val("");
    $("#Gratuity").val("");
    $("#Commutation").val("");
    $("#LeaveEncashment").val("");
    $("#AQP").val("");
    $("#DA").val("");
    $("#OtherIncome").val("");
    $("#ExemptedAmount").val("");
    $("#TaxableAmount").val("");
    $("#StandardDeduction").val("");
    $("#NetTaxableAmount").val("");
    $("#TaxPayableAmount").val("");
    $("#MonthlyTDS").val("");


    $("#TDSInvestmentId").val("");
    $("#Id").val("");
    $('#txt80D').val("");
    $('#txt80DD').val("");
    $('#txt80DDB').val("");
    $('#txt5YrsTermDepositPostoffice').val("");
    $('#txtLIC_Pension_Plan').val("");
    $('#txtNSC').val("");
    $('#txtPPF').val("");
    $('#txtStampDuty').val("");
    $('#txtSukanyaSmriddhiYojana').val("");
    $('#txtTermDepositBank').val("");
    $('#txtTF').val("");
    $('#txtULIP').val("");
    $('#txtMF').val("");
    $('#txtHousingLoanInt').val("");
    $('#txtHousingLoanInt1617').val("");
    $('#txtHousingLoanInt1920').val("");
    $('#txt80E').val("");
    $('#txt80G').val("");
    $('#txt80GGB').val("");
    $('#txt80GGC').val("");
    $("#txt80GG").val("");
    $("#txt80RRB").val("");
    $("#txt80TTA").val("");
    $("#txt80TTB").val("");
    $("#txt80U").val("");


});

$(".viewFile").click(function () {
    var path = $(this).attr("data-filePath");
    if (path != '') {
        $("#ifrmDoc").attr("src", path);
        $("#ViewDocModal").modal("show");
    } else {
        toastrWarning("No file found!!!")
    }
});