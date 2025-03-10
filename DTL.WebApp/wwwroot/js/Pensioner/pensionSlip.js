AddEditForm();

$("#btnSavePensionSlip").click(function () {
    debugger;
    var empId = $("#EmployeeRegistrationId").val()
    $("#pensionSlipForm").submit();
    UpdatePPO(empId, 1, "")
});
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var pensionSlip = $("#pensionSlipForm").serialize()

            $.ajax({
                url: "/Pensioner/CreatePensionSlip",
                type: "POST",
                dataType: "json",
                data: pensionSlip,
                success: function (data) {

                    toastrSuccess("Employee details added successfully.");
                    setTimeout(function () {
                        window.location.href = '/Pensioner/';
                }, 5000)


                },
                error: function (err) {
                    toastrError("Something went wrong!!!");
                }
            })
        }
    });
    $('#pensionSlipForm').validate({
        rules: {
            ABPFor10Months: {
                required: true
            },
            EmolumentsForPension: {
                required: true
            },
            AdmissiablePension: {
                required: true
            },
            AdmissiableDate: {
                required: true
            },
            PensionAtEnhancedRate: {
                required: true
            },
            AdmissibleForFromDate_Enhanced: {
                required: true
            },
            AdmissibleForToDate_Enhanced: {
                required: true
            },
            PensionAtNormalRate: {
                required: true
            },
            AdmissiableForNormalFromDate: {
                required: true
            },
            AdmissiableForNormalToDate: {
                required: true
            },
            Commutation: {
                required: true
            },
            CommutationPortion: {
                required: true
            },
            GratuityType: {
                required: true
            },
            Gratuity: {
                required: true
            }
        },
        messages: {
            ABPFor10Months: {
                require: "Please provide ABP for 10 months."
            },
            EmolumentsForPension: {
                require: "Please provide emoluments for pension."
            },
            AdmissiablePension: {
                require: "Please provide admissiable pension."
            },
            AdmissiableDate: {
                require: "Please provide admissiable date."
            },
            PensionAtEnhancedRate: {
                require: "Please provide pension at enhanced rate."
            },
            AdmissibleForFromDate_Enhanced: {
                require: "Please provide admissible for from date enhanced."
            },
            AdmissibleForToDate_Enhanced: {
                require: "Please provide admissible for to date enhanced."
            },
            PensionAtNormalRate: {
                require: "Please provide pension at normal rate."
            },
            AdmissiableForNormalFromDate: {
                require: "Please provide admissiable for normal from date."
            },
            AdmissiableForNormalToDate: {
                require: "Please provide admissiable for normal to date."
            },
            Commutation: {
                require: "Please provide commutation."
            },
            CommutationPortion: {
                require: "Please provide commutation portion."
            },
            GratuityType: {
                require: "Please provide gratuity type."
            },
            Gratuity: {
                require: "Please provide gratuity."
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
$("#GratuityType").change("option", function () {
    GratuityCalculation();
});

$("#ABPLast10Months").change(function () {
    GratuityCalculation();
});

$("#PSCommutation").change(function () { 

    var _commutationPercent = $("#PSCommutation").val();
    if (parseFloat(_commutationPercent) > 40) {
        alert("Commutation % must not be more than 40");
        $("#PSCommutation").val('0');
        $("#TaxableAmountCommutation").val('0');
        $("#PSCommutation").focus();
        return;
    }
    else /*if (parseFloat($("#PSCommutation").val()) >= 33.33)*/
    {
        var diff = parseFloat($("#PSCommutation").val()) - 33.33;
        var AdmissiablePension;
        if (!isNaN($("#AdmissiablePension").val()) || $("#AdmissiablePension").val() != '') {
            var taxableamt = (parseFloat($("#AdmissiablePension").val()) * diff) / 100;
            taxableamt = Math.ceil(taxableamt);            
            $("#TaxableAmountCommutation").val(taxableamt);
        }
    }
    /*
    else if (parseFloat($("#PSCommutation").val()) < 33.33) {
        alert("Commutation % must be between 33.33% to 40%");
        $("#PSCommutation").val("");
        $("#TaxableAmountCommutation").val("");
        $("#PSCommutation").focus();
        return;
    }
    */
    //CommutationcalculationPension();
    Commutationcalculation();
});

function GratuityCalculation() {
    //debugger;
    var Emoluments = (parseInt($("#EmolumentsForPension").val()) + (parseInt($("#DA").val())));//NPA
    var Gratuity;
    var QualifyingServiceYear = parseInt($("#QualifyingServiceYear").val());//* 2;

    /* As per new rule book - ((Emolument + DA) * 6monthly service)/4 */
    var QualifyingServiceMonth = parseInt($("#QualifyingServiceMonth").val());
    var hlfmonth = 0;
    if (QualifyingServiceMonth < 3)
        hlfmonth = 0;
    else if (QualifyingServiceMonth > 3 && QualifyingServiceMonth <= 8)
        hlfmonth = 0.5;
    else
        hlfmonth = 1;
    var finser = QualifyingServiceYear  + hlfmonth;
    var qualifinghlfyear = finser / 2;
    
    Gratuity = Emoluments * qualifinghlfyear
    
    //Gratuity = (Emoluments * (QualifyingServiceYear * 2))/4;
    if (QualifyingServiceYear > 66)
        Gratuity = (parseInt(Emoluments) * parseInt(66) * 2 / 4);
    else
        Gratuity = (parseInt(Emoluments) * parseInt(QualifyingServiceYear) * 2 / 4);

    if ($("#GratuityType").val() == "Death") {
        if ($("#QualifyingServiceYear").val() < 1) {
            Gratuity = Math.ceil(Gratuity * 2);
        }
        else if ($("#QualifyingServiceYear").val() >= 1 && $("#QualifyingServiceYear").val() < 5) {
            Gratuity = Math.ceil(Gratuity * 6);
        }
        else if ($("#QualifyingServiceYear").val() >= 5 && $("#QualifyingServiceYear").val() < 11) {
            Gratuity = Math.ceil(Gratuity * 12);
        }
        else if ($("#QualifyingServiceYear").val() >= 11 && $("#QualifyingServiceYear").val() < 20) {
            Gratuity = Math.ceil(Gratuity * 20);
        }
        else if ($("#QualifyingServiceYear").val() >= 20) {
            var totyr = 0;
            if ($("#QualifyingServiceYear").val() > 33)
                totyr = 33
            else
                totyr = $("#QualifyingServiceYear").val();
            Gratuity = Math.ceil(Gratuity * parseFloat(totyr));
        }
    }

    var _dtOfEndService = new Date($('#ServiceEndDatePs').val()).getFullYear();
    if (_dtOfEndService <= 2016) {
        if (Gratuity > 1000000)
            $("#Gratuity").attr("value", Math.ceil(1000000));
        else
            $("#Gratuity").attr("value", Math.ceil(Gratuity));
    }
    else if (_dtOfEndService > 2016){
        if (Gratuity > 2000000)
            $("#Gratuity").attr("value", Math.ceil(2000000));
        else
            $("#Gratuity").attr("value", Math.ceil(Gratuity));
    }

}


$("#btnSubmitPensionApp").click(function () {
    debugger;
    var empId = $("#EmployeeRegistrationId").val()
    UpdatePPO(empId, 4, "HR")
});

$("#btnApproveSubmitPensionApp").click(function () {
    var empId = $("#EmployeeRegistrationId").val()
    UpdatePPO(empId, 5, "HOD")
});

$("#btnRejectPensionApp").click(function () {
    var empId = $("#EmployeeRegistrationId").val()
    UpdatePPO(empId, 6, "HOD")
});

$("#btnApprovePensionApp").click(function () {
    var empId = $("#EmployeeRegistrationId").val()
    UpdatePPO(empId, 7, "AM")
});
$("#btnAMRejectPensionApp").click(function () {
    var empId = $("#EmployeeRegistrationId").val()
    UpdatePPO(empId, 6, "AM")
});
$("#btnSaveRejection").click(function () {
    var empId = $("#EmployeeRegistrationId").val()
    var role = $("#role").val();
          UpdateEmployeeRegStatusByHRAdmin(empId, 1, role, $("#RejectionReason").val());
    
});


function UpdatePPO(empId, status, role) {
    $.ajax({
        url: "/Pensioner/UpatePesionAppStatus",
        type: "POST",
        dataType: "json",
        data: { empId: empId, status: status },
        success: function (data) {
           //toastrSuccess("Pension Application Status has updated by " + role + ".");
        },
        error: function (err) {
            toastrError("Something went wrong!!!");
        }
    })
}

function UpdateEmployeeRegStatusByHRAdmin(employeeId, status, role,remarks) {
    $.ajax({
        url: "/Pensioner/UpdateEmployeeRegStatusByHRAdmin",
        type: "POST",
        dataType: "json",
        data: { employeeId: employeeId, Role: role, status: status, Remarks:remarks },
        success: function (data) {
            toastrSuccess("Pension Application Status has updated by " + role + ".");
            $("#RejectionReason").val("");
        },
        error: function (err) {
            toastrError("Something went wrong!!!");
        }
    })
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

$("#LeaveEncashment").change(function () {
    //debugger;
    _leaveEncashment();
});

$("#LeaveEncashmentDays").change(function () {
    //debugger;
    _leaveEncashment();
});

function _leaveEncashment() {
    if (!isNaN($("#LeaveEncashmentDays").val()) || $("#LeaveEncashmentDays").val() != '') {
        var LeaveEncashmentDays = parseFloat($("#LeaveEncashmentDays").val());
        var Emoluments;//NPA
        if ($("#ReasonOfRetirement").val() != "Death") {
            Emoluments = (parseInt($("#EmolumentsForPension").val()) + (parseInt($("#DA").val())));
        }
        else {
            var basicPay = $("#BasicPay").val();
            var daPercent = $("#DA_Percent").val();
            daAmount = ((basicPay * daPercent) / 100);
            Emoluments = (parseInt($("#EmolumentsForPension").val()) + Math.ceil(daAmount));
        }
        var tot = Emoluments / 30 * LeaveEncashmentDays;
        $("#LeaveEncashment").val(Math.ceil(tot));

        if (!isNaN($("#LeaveEncashment").val()) || $("#LeaveEncashment").val() != '') {
            var LeaveEncasmentAmt = parseFloat($("#LeaveEncashment").val());
            if (LeaveEncasmentAmt > 300000) {
                var diff = LeaveEncasmentAmt - 300000;
                $("#TaxableLeaveEncashment").val(parseFloat(diff));
            }
            else {
                $("#TaxableLeaveEncashment").val(0);
            }
        }
    }
    else {
        $("#LeaveEncashmentDays").val('0');
        $("#LeaveEncashment").val('0');
    }

    _totalLumpsumPayable();
}


function _totalLumpsumPayable() {
    var _gratuityTotal = 0;
    var _commutationTotal = 0;
    var _leaveEncashmentTotal = 0;

    //debugger;

    if (!isNaN($("#Gratuity").val()) || $("#Gratuity").val() != '') {
        _gratuityTotal = parseInt($("#Gratuity").val());
    }

    if (!isNaN($("#LumsumPayableCommutation").val()) || $("#LumsumPayableCommutation").val() != '') {
        _commutationTotal = parseInt($("#LumsumPayableCommutation").val());
    }

    if (!isNaN($("#LeaveEncashment").val()) || $("#LeaveEncashment").val() != '') {
        _leaveEncashmentTotal = parseInt($("#LeaveEncashment").val());
    }

    var _totalLumpsumPayable;
    _totalLumpsumPayable = _gratuityTotal + _commutationTotal + _leaveEncashmentTotal;

    $('#LumpsumPayable').val(_totalLumpsumPayable);
}