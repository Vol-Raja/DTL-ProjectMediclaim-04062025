AddEditForm();

$("#btnSavePensionSlip").click(function () {
    $("#pensionSlipForm").submit();
});
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var pensionSlip = $("#pensionSlipForm").serialize()

            $.ajax({
                url: "/EmployeeRegistration/CreatePensionSlip",
                type: "POST",
                dataType: "json",
                data: pensionSlip,
                success: function (data) {

                    toastrSuccess("Employee details added successfully.");
                    setTimeout(function () { window.location.reload(); }, 5000)


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
$("#PSCommutation").change(function () {
    

    if (parseFloat($("#PSCommutation").val()) > 40) {
        alert("Commutation % must not be more than 40");
        $("#PSCommutation").val("");
        $("#TaxableAmountCommutation").val("");
        return;
    }
    else if (parseFloat($("#PSCommutation").val()) > 33.33) {
        var diff = parseFloat($("#PSCommutation").val()) - 33.33;
        var AdmissiablePension;
        if (!isNaN($("#AdmissiablePension").val()) || $("#AdmissiablePension").val() != '') {
            var taxableamt = (parseFloat($("#AdmissiablePension").val()) * diff) / 100;
            taxableamt = taxableamt.toFixed(2);
            $("#TaxableAmountCommutation").val(taxableamt); 
        }
    }
    CommutationcalculationPension();
});



function CommutationcalculationPension() {
    var currentYear = new Date();
    var DOBYear = new Date($("#DOB").val().split(" ")[0].split("-")[2] + "-" + $("#DOB").val().split(" ")[0].split("-")[1] + "-" + $("#DOB").val().split(" ")[0].split("-")[0]).getFullYear();
    var CommutationFactor = parseInt($("#AdmissiablePension").val()) * parseInt($("#PSCommutation").val()) / 100;
    var CommutationPortion;
    DOBYear = currentYear.getFullYear() - DOBYear;

    if (DOBYear == "29") {
        CommutationPortion = '9.176';
    }
    if (DOBYear == "30") {
        CommutationPortion = '9.173';
    }
    if (DOBYear == "31") {
        CommutationPortion = '9.169';
    }
    if (DOBYear == "32") {
        CommutationPortion = '9.164';
    }
    if (DOBYear == "33") {
        CommutationPortion = '9.159';
    }
    if (DOBYear == "34") {
        CommutationPortion = '9.152';
    }
    if (DOBYear == "35") {
        CommutationPortion = '9.145';
    }
    if (DOBYear == "36") {
        CommutationPortion = '9.136';
    }
    if (DOBYear == "37") {
        CommutationPortion = '9.126';
    }
    if (DOBYear == "38") {
        CommutationPortion = '9.116';
    }
    if (DOBYear == "39") {
        CommutationPortion = '9.103';
    }
    if (DOBYear == "40") {
        CommutationPortion = '9.090';
    }
    if (DOBYear == "41") {
        CommutationPortion = '9.075';
    }
    if (DOBYear == "42") {
        CommutationPortion = '9.059';
    }
    if (DOBYear == "43") {
        CommutationPortion = '9.040';
    }
    if (DOBYear == "44") {
        CommutationPortion = '9.019';
    }
    if (DOBYear == "45") {
        CommutationPortion = '8.996';
    }
    if (DOBYear == "46") {
        CommutationPortion = '8.971';
    }
    if (DOBYear == "47") {
        CommutationPortion = '8.943';
    }
    if (DOBYear == "48") {
        CommutationPortion = '8.913';
    }
    if (DOBYear == "49") {
        CommutationPortion = '8.881';
    }
    if (DOBYear == "50") {
        CommutationPortion = '8.846';
    }
    if (DOBYear == "51") {
        CommutationPortion = '8.808';
    }
    if (DOBYear == "52") {
        CommutationPortion = '8.768';
    }
    if (DOBYear == "53") {
        CommutationPortion = '8.724';
    }
    if (DOBYear == "54") {
        CommutationPortion = '8.678';
    }
    if (DOBYear == "55") {
        CommutationPortion = '8.627';
    }
    if (DOBYear == "56") {
        CommutationPortion = '8.572';
    }
    if (DOBYear == "57") {
        CommutationPortion = '8.512';
    }
    if (DOBYear == "58") {
        CommutationPortion = '8.446';
    }
    if (DOBYear == "59") {
        CommutationPortion = '8.371';
    }
    if (DOBYear == "60") {
        CommutationPortion = '8.287';
    }
    if (DOBYear == "61") {
        CommutationPortion = '8.194';
    }
    if (DOBYear == "62") {
        CommutationPortion = '8.093';
    }
    if (DOBYear == "63") {
        CommutationPortion = '7.982';
    }
    if (DOBYear == "64") {
        CommutationPortion = '7.862';
    }
    if (DOBYear == "65") {
        CommutationPortion = '7.731';
    }
    if (DOBYear == "66") {
        CommutationPortion = '7.591';
    }
    if (DOBYear == "67") {
        CommutationPortion = '7.431';
    }
    if (DOBYear == "68") {
        CommutationPortion = '7.262';
    }
    if (DOBYear == "69") {
        CommutationPortion = '7.083';
    }

    var lumpSumPayable = CommutationFactor * 12 * CommutationPortion;

    $("#CommutationPortion").attr("value", CommutationPortion);
    $("#LumpsumPayable").val(Math.ceil(lumpSumPayable));
}

function GratuityCalculation() {
    
    var Emoluments = (parseInt($("#EmolumentsForPension").val()) + (parseInt($("#DA").val())));//NPA
    var Gratuity;
    var QualifyingServiceYear = parseInt($("#QualifyingServiceYear").val());//* 2;
    var QualifyingServiceMonth = parseInt($("#QualifyingServiceMonth").val());
    var hlfmonth = 0;
    if (QualifyingServiceMonth < 3)
        hlfmonth = 0;
    if (QualifyingServiceMonth > 3 && QualifyingServiceMonth <= 8)
        hlfmonth = 0.5;
    else
        hlfmonth = 1;
    var finser = QualifyingServiceYear  + hlfmonth;
    var qualifinghlfyear = finser / 2;
    Gratuity = Emoluments * qualifinghlfyear;
    /*if (QualifyingServiceYear > 66)
        Gratuity = (parseInt(Emoluments) * parseInt(66) * 2 / 4);
    else
        Gratuity = (parseInt(Emoluments) * parseInt(QualifyingServiceYear) * 2 / 4);*/

    if ($("#GratuityType").val() == "Death") {
        if ($("#QualifyingServiceYear").val() < 1) {
            Gratuity = Math.ceil(Gratuity * 2);
        }
        if ($("#QualifyingServiceYear").val() >= 1 && $("#QualifyingServiceYear").val() < 5) {
            Gratuity = Math.ceil(Gratuity * 6);
        }
        if ($("#QualifyingServiceYear").val() >= 5 && $("#QualifyingServiceYear").val() < 11) {
            Gratuity = Math.ceil(Gratuity * 12);
        }
        if ($("#QualifyingServiceYear").val() >= 11 && $("#QualifyingServiceYear").val() < 20) {
            Gratuity = Math.ceil(Gratuity * 20);
        }
        if ($("#QualifyingServiceYear").val() >= 20) {
            var totyr = 0;
            if ($("#QualifyingServiceYear").val() > 33)
                totyr = 33
            else
                totyr = $("#QualifyingServiceYear").val();
            Gratuity = Math.ceil(Gratuity * parseFloat(totyr));
        }
    }

    if (Gratuity > 2000000)
        $("#Gratuity").attr("value", Math.ceil(2000000));
    else
        $("#Gratuity").attr("value", Math.ceil(Gratuity));
}


$("#btnSubmitPensionApp").click(function () {
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
        url: "/EmployeeRegistration/UpatePesionAppStatus",
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
        url: "/EmployeeRegistration/UpdateEmployeeRegStatusByHRAdmin",
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
    if (!isNaN($("#LeaveEncashment").val()) || $("#LeaveEncashment").val() != '') {
        var LeaveEncasmentAmt = parseFloat($("#LeaveEncashment").val());
        if (LeaveEncasmentAmt > 300000) {
            var diff = LeaveEncasmentAmt - 300000;
            $("#TaxableLeaveEncashment").val(parseFloat(diff));
        }
    }
});

$("#LeaveEncashmentDays").change(function () {
    if (!isNaN($("#LeaveEncashmentDays").val()) || $("#LeaveEncashmentDays").val() != '') {
        var LeaveEncashmentDays = parseFloat($("#LeaveEncashmentDays").val());
        var Emoluments = (parseInt($("#EmolumentsForPension").val()) + (parseInt($("#DA").val())));//NPA
        var tot = Emoluments / 30 * LeaveEncashmentDays;
        $("#LeaveEncashment").val(Math.ceil(tot));
      
    }
});


