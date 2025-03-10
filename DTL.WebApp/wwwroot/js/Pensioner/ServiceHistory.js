AddEditForm();

$('.number').keypress(function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$("#ReasonOfRetirement").change("option", function () {
    $("#GratuityType option[value=" + $("#ReasonOfRetirement").val() + "]").attr('selected', 'selected');
    calDA();
    calDR();
    PensionCalculation();
    Commutationcalculation();
});

$("#btnServiceHistory").click(function () {
    $("#ServiceHistory").submit();
});

$(document).ready(function () {

    var _reasonOfRetire = $("#hdnReasonOfRetirement").val();
    if (_reasonOfRetire == 'Retirement' || _reasonOfRetire == 'Death' || _reasonOfRetire == 'VRS') {
        $("#ReasonOfRetirement option[value=" + _reasonOfRetire + "]").attr('selected', 'selected');
        $("#GratuityType option[value=" + _reasonOfRetire + "]").attr('selected', 'selected');
    }

    calTotalService();

    PensionCalculation();
    FamilyPension();
    GratuityCalculation();
    Commutationcalculation();
    HalfYearCalculation();

    _leaveEncashment();
    _totalLumpsumPayable()
});

/*Team VirAj*/

$("#ABPLast10Months").change(function () {
    var abpLast10Months = $("#ABPLast10Months").val();
    var basicPay = $("#BasicPay").val();
    if (abpLast10Months != '' || basicPay != '') {
        if (abpLast10Months > basicPay) {
            $("#EmolumentsForPension").val(abpLast10Months);
        }
        else {
            $("#EmolumentsForPension").val(basicPay);
        }

        PensionCalculation();
    }
    else if ($("ABPLast10Months").val() == '') {
        $("ABPLast10Months").val('0');
    }
});

function _calAdmissible() {
    const _dob = new Date($("#DOBPs").val());
    const _serviceStartDate = new Date($("#ServiceStartDatePs").val());
    const _serviceEndDate = new Date($("#ServiceEndDatePs").val());

    var _yr = _dob.getFullYear() + 67;
    var _mt = _dob.getMonth() + 1;

    var xt = new Date(_yr, _mt, 01);
    $('#AdmissibleForToDate_Enhanced').val(xt);
    //alert('Date' + xt);

    var yt = new Date(_serviceEndDate.getFullYear(), _serviceEndDate.getMonth() + 1, 1);
    $('#AdmissiableDate').val(yt);
    alert('Date' + yt);
}

function calTotalService() {
    //debugger;
    const _dob = new Date($("#DOBPs").val());
    const _serviceStartDate = new Date($("#ServiceStartDatePs").val());
    const _serviceEndDate = new Date($("#ServiceEndDatePs").val());
    var _totalServiceYear = 0;
    var _months = 0;
    var _qualifiedServiceYear;

    _totalServiceYear = _serviceEndDate.getFullYear() - _serviceStartDate.getFullYear();
    //alert(_totalServiceYear);
    //_months = _serviceEndDate.getMonth() - _serviceStartDate.getMonth() + (_serviceEndDate.getFullYear() - _serviceStartDate.getFullYear()) * 12;
    //alert(_totalServiceYear);
    //_qualifiedServiceYear = _months / 6; //6months = 1Unit
    //console.log(_totalServiceYear);

    if (_totalServiceYear > 33) {
        $('#TotalServiceYear').val('33');
    }
    else {
        $('#TotalServiceYear').val(_totalServiceYear);
    }

    if (_totalServiceYear != '') {
        _serviceCalulation();
    }

    //$('#QualifyingServiceYear').val(_totalServiceYear);
    //$('#HalfYear').val(_totalServiceYear * 2);

    //_calAdmissible();
}

function calDA() {
    var basicPay = $("#BasicPay").val();
    var daPercent = $("#DA_Percent").val();
    var daAmount = $("#DA").val();

    if (daPercent !== undefined || daPercent !== ''
        || daAmount !== undefined || daAmount !== '')
    {
        if (daPercent != '0') {
            if ($("#ReasonOfRetirement").val() != "Death") {
                daAmount = ((basicPay * daPercent) / 100);
            }
            else {
                daAmount = ((basicPay * daPercent) / 100) / 2;
            }
            $("#DA").val(Math.ceil(daAmount));
        }

        totalMonthlyPension();
    }
}
function calDAP() {
    var basicPay = $("#BasicPay").val();
    var daPercent = $("#DA_Percent").val();
    var daAmount = $("#DA").val();

    if (daPercent !== undefined || daPercent !== ''
        || daAmount !== undefined || daAmount !== '') {
        if (daAmount != '0') {
            daPercent = ((daAmount * 100) / basicPay);
            $("#DA_Percent").val(daPercent.toFixed(2));
        }
    }
}
function calDR() {
    var basicPay;
    
    if ($("#ReasonOfRetirement").val() != "Death") {
        basicPay = $("#AdmissiablePension").val();
    }
    else {
        basicPay = $("#EmolumentsForPension").val();
    }

    var drPercent = $("#DRPercent").val();
    var drAmount = $("#DR").val();

    if (drPercent !== undefined || drPercent !== ''
        || drAmount !== undefined || drAmount !== '') {
        if (drPercent != '0') {
            if ($("#ReasonOfRetirement").val() != "Death") {
                drAmount = ((basicPay * drPercent) / 100);
            }
            else {
                drAmount = ((basicPay * drPercent) / 100) / 2;
            }
            $("#DR").val(Math.ceil(drAmount));
        }
        totalMonthlyPension();

        Commutationcalculation();
    }
}
function calDRP() {
    var basicPay = $("#AdmissiablePension").val();
    var drPercent = $("#DRPercent").val();
    var drAmount = $("#DR").val();

    if (drPercent !== undefined || drPercent !== ''
        || drAmount !== undefined || drAmount !== '') {
        if (drAmount != '0') {
            drPercent = ((drAmount * 100) / basicPay);
            $("#DRPercent").val(drPercent.toFixed(2));
        }
        Commutationcalculation();
    }
}
function totalMonthlyPension() {
    var basicPay = 0;
    var drAmount = 0;
    var monthlyPension = 0;

    if (!isNaN($("#AdmissiablePension").val()) && $("#AdmissiablePension").val() != '') {
        basicPay = $("#AdmissiablePension").val();
    }

    if (!isNaN($("#DR").val()) && $("#DR").val() != '') {
        drAmount = $("#DR").val();
    }

    if ($("#ReasonOfRetirement").val() != "Death") {
        monthlyPension = parseInt(basicPay) + parseInt(drAmount);
    }
    else {
        basicPay = parseInt($("#EmolumentsForPension").val()) * 50 / 100
        monthlyPension = parseInt(basicPay) + parseInt(drAmount);
    }
    $("#totalMonthlyPension").val(monthlyPension);
}

var CommutationTableValues = new Object();
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            //toastrSuccess("Form successful submitted!");
            //var serviceHistoryModel = $("#ServiceHistory").serialize();
            var formData = BindServiceFormData();

            console.log(formData);
            $.ajax({
                url: "/Pensioner/CreateServiceHistory",
                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //debugger;

                    PensionCalculation();
                    FamilyPension();
                    GratuityCalculation();
                    Commutationcalculation();
                    HalfYearCalculation();

                    if (data == "add") {
                        toastrSuccess("Service history has added successfully!");

                    } else {
                        toastrSuccess("Service history has updated successfully!");
                    }  
                    $("#custom-tabs-three-pensionSlip-tab").trigger("click");
                },
                error: function (err) {
                    //debugger;

                    toastrError("Something Went Wrong!!!");
                }
            });
        }
    });
    $('#ServiceHistory').validate({
        rules: {
            DTLDepartmentName: {
                required: true,
            },
            ReasonOfRetirement: {
                required: true
            },
            TotalServiceYear: {
                required: true
            },
            TotalserviceMonth: {
                required: true
            },
            TotalserviceDay: {
                required: true
            },
            QualifyingServiceYear: {
                required: true
            },
            QualifyingServiceMonth: {
                required: true
            },
            QualifyingServiceDays: {
                required: true
            },
            BasicPay: {
                required: true
            },
            NPA: {
                required: true
            }
        },
        messages: {
            DTLDepartmentName: {
                required: "Please provide DTL Department Name.",
            },
            //CategoryOfEmployeement: {
            //    required: "Please provide category of employeement.",
            //},
            ReasonOfRetirement: {
                required: "Please provide reason of retirement.",
            },
            TotalServiceYear: {
                required: "Please provide total service year.",
            },
            TotalserviceMonth: {
                required: "Please provide total service month.",
            },
            TotalserviceDay: {
                required: "Please provide total service day.",
            },
            QualifyingServiceYear: {
                required: "Please provide qualifying service year.",
            },
            QualifyingServiceMonth: {
                required: "Please provide qualifying service month.",
            },
            QualifyingServiceDays: {
                required: "Please provide qualifying service days.",
            },
            BasicPay: {
                required: "Please provide basic pay.",
            },
            NPA: {
                required: "Please provide NPA.",
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
function PensionCalculation() {
    
    var k = $("#DOBPs").val();
    var m = $("#ServiceStartDatePs").val();
    var n = $("#ServiceEndDatePs").val();
    $("#EmolumentsForPension").attr("value", $("#BasicPay").val());
    $("#DOBPs").val(moment(k).format("YYYY-MM-DD")).change();
    $("#ServiceStartDatePs").val(moment(m).format("YYYY-MM-DD")).change();
    $("#ServiceEndDatePs").val(moment(n).format("YYYY-MM-DD")).change();
    if ($("#QualifyingServiceYear").val() > 10) {
        if ((parseInt($("#EmolumentsForPension").val()) * 50) / 100 < 9000) {
            $("#AdmissiablePension").attr("value", 9000);
        }
        else {
            $("#AdmissiablePension").attr("value", (parseInt($("#EmolumentsForPension").val()) * 50) / 100);
        }
    }
    else {
        $("#AdmissiablePension").attr("value", 0);
    }
    var currentYear = new Date().getFullYear();
    //var DOBYear = new Date($("#DOBPs").val().split(" ")[0].split("-")[2] + "-" + $("#DOB").val().split(" ")[0].split("-")[1] + "-" + $("#DOB").val().split(" ")[0].split("-")[0]).getFullYear();
    var DOBYear = new Date($("#DOBPs").val()).getFullYear();
    var yearDifference = currentYear - DOBYear;

    if (yearDifference >= 80) {
        if (yearDifference >= 80 && yearDifference < 85) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#AdmissiablePension").val()) + parseInt($("#AdmissiablePension").val() * 20 / 100)));
        }
        if (yearDifference >= 85 && yearDifference < 90) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#AdmissiablePension").val()) + parseInt($("#AdmissiablePension").val() * 30 / 100)));
        }
        if (yearDifference >= 90 && yearDifference < 95) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#AdmissiablePension").val()) + parseInt($("#AdmissiablePension").val() * 40 / 100)));
        }
        if (yearDifference >= 85 && yearDifference < 100) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#AdmissiablePension").val()) + parseInt($("#AdmissiablePension").val() * 50 / 100)));
        }
        if (yearDifference >= 100) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#AdmissiablePension").val()) + parseInt($("#AdmissiablePension").val() * 100 / 100)));
        }
    }
}
function FamilyPension() {
    var currentYear = new Date().getFullYear();
    //var DOBYear = new Date($("#DOBPs").val().split(" ")[0].split("-")[2] + "-" + $("#DOB").val().split(" ")[0].split("-")[1] + "-" + $("#DOB").val().split(" ")[0].split("-")[0]).getFullYear();
    var DOBYear = new Date($("#DOBPs").val()).getFullYear();
    var yearDifference = currentYear - DOBYear;
    $("#PensionAtNormalRate").attr("value", parseInt($("#EmolumentsForPension").val()) * 30 / 100);
    $("#PensionAtEnhancedRate").attr("value", parseInt($("#EmolumentsForPension").val()) * 50 / 100);
    if (yearDifference >= 80) {
        if (yearDifference >= 80 && yearDifference < 85) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#EmolumentsForPension").val()) + parseInt($("#EmolumentsForPension").val() * 20 / 100)));
        }
        if (yearDifference >= 85 && yearDifference < 90) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#EmolumentsForPension").val()) + parseInt($("#EmolumentsForPension").val() * 30 / 100)));
        }
        if (yearDifference >= 90 && yearDifference < 95) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#EmolumentsForPension").val()) + parseInt($("#EmolumentsForPension").val() * 40 / 100)));
        }
        if (yearDifference >= 85 && yearDifference < 100) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#EmolumentsForPension").val()) + parseInt($("#EmolumentsForPension").val() * 50 / 100)));
        }
        if (yearDifference >= 100) {
            $("#AdmissiablePension").attr("value", Math.ceil(parseInt($("#EmolumentsForPension").val()) + parseInt($("#EmolumentsForPension").val() * 100 / 100)));
        }
    }
}

/*
function GratuityCalculation() {
    debugger;
    var Emoluments = (parseInt($("#EmolumentsForPension").val()) + (parseInt($("#DA").val())));//NPA
    var Gratuity;
    var QualifyingServiceYear = parseInt($("#QualifyingServiceYear").val());//* 2;
    var QualifyingServiceMonth = parseInt($("#QualifyingServiceMonth").val());
    var hlfmonth = 0;
    if (QualifyingServiceMonth < 3)
        hlfmonth = 0;
    else if (QualifyingServiceMonth > 3 && QualifyingServiceMonth <= 8)
        hlfmonth = 0.5;
    else
        hlfmonth = 1;
    var finser = QualifyingServiceYear + hlfmonth;
    var qualifinghlfyear = finser / 2;
    Gratuity = Emoluments * qualifinghlfyear;

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
*/

function Commutationcalculation() {
    //debugger;
    //var currentYear = new Date();
    //var DOBYear = new Date($("#DOBPs").val().split(" ")[0].split("-")[2] + "-" + $("#DOB").val().split(" ")[0].split("-")[1] + "-" + $("#DOB").val().split(" ")[0].split("-")[0]).getFullYear();
    //var DOBYear = new Date($("#DOBPs").val()).getFullYear();

    //This will be calculated as per Service End Date or Retirement Date
    //The next birthday for commutation portion is after retirement year
    var birthDayNextYear = new Date($("#ServiceEndDatePs").val()).getFullYear();
    var DOBYear = new Date($("#DOBPs").val()).getFullYear();
    DOBYear = (birthDayNextYear - DOBYear) + 1;

    var _CommutationPercent;
    //alert('Value' + $("#PSCommutation").val());
    if ($("#PSCommutation").val() != '')
    { _CommutationPercent = parseInt($("#PSCommutation").val()); }
    else { _CommutationPercent = 0; }

    var CommutationFactor = (parseInt($("#AdmissiablePension").val()) * _CommutationPercent) / 100;

    var CommutationPortion;
    
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
        if (DOBYear == "70") {
            CommutationPortion = '6.897';
        }

    var commutationAmount = CommutationFactor * 12 * CommutationPortion;

    if ($("#GratuityType").val() != "Death") {
        $("#CommutationPortion").attr("value", CommutationPortion);
        $("#LumsumPayableCommutation").attr("value", Math.ceil(commutationAmount));
    }
    else {
        $("#CommutationPortion").attr("value", '0');
        $("#LumsumPayableCommutation").attr("value", '0');
    }
    
    //deduction for 15years as Emi
    //var monthlyDeduction = Math.ceil(commutationAmount) / 180;
    //$('#commutationDeduction').val(Math.ceil(monthlyDeduction));
    $('#commutationDeduction').val(Math.ceil(CommutationFactor));

}

function HalfYearCalculation() {
    let TotalServiceYear = parseInt($("#TotalServiceYear").val()) * 2;
    let TotalServiceMonth = $("#TotalServiceMonth").val();

    if (TotalServiceMonth <3) {
        $("#HalfYear").val(TotalServiceYear + " units");
    }
    else if (TotalServiceMonth >= 3 && TotalServiceMonth <= 8) {
        $("#HalfYear").val(parseInt(TotalServiceYear) + " half units");
    }
    //else if (TotalServiceMonth > 6 && TotalServiceMonth < 9) {
    //    $("#HalfYear").val(parseInt(TotalServiceYear) + 1 + " half units");
    //}
    else if (TotalServiceMonth >= 9 && TotalServiceMonth <= 12) {
        $("#HalfYear").val(parseInt(TotalServiceYear) + 1 + " units");
    }
    
}

$(".serviceCount").on("input propertychange", function () {
    _serviceCalulation();
});

function _serviceCalulation() {
    let TotalServiceYear = $("#TotalServiceYear").val();
    let TotalServiceMonth = $("#TotalServiceMonth").val();
    let TotalServiceDays = $("#TotalServiceDays").val();
    let AdditionalServiceYears = $("#AdditionalServiceYears").val();
    let AdditionalServiceMonth = $("#AdditionalServiceMonth").val();
    let AdditionalServiceDays = $("#AdditionalServiceDays").val();
    let ServiceNotCountedYear = $("#ServiceNotCountedYear").val();
    let ServiceNotCountedMonth = $("#ServiceNotCountedMonth").val();
    let ServiceNotCountedDays = $("#ServiceNotCountedDays").val();

    if (TotalServiceYear == "")
        TotalServiceYear = 0;
    if (TotalServiceMonth == "")
        TotalServiceMonth = 0;
    if (TotalServiceDays == "")
        TotalServiceDays = 0;
    if (AdditionalServiceYears == "")
        AdditionalServiceYears = 0;
    if (AdditionalServiceMonth == "")
        AdditionalServiceMonth = 0;
    if (AdditionalServiceDays == "")
        AdditionalServiceDays = 0;
    if (ServiceNotCountedYear == "")
        ServiceNotCountedYear = 0;
    if (ServiceNotCountedMonth == "")
        ServiceNotCountedMonth = 0;
    if (ServiceNotCountedDays == "")
        ServiceNotCountedDays = 0;


    var calculatedYear = (parseInt(TotalServiceYear) + parseInt(AdditionalServiceYears)) - parseInt(ServiceNotCountedYear);
    var calculatedMonth = (parseInt(TotalServiceMonth) + parseInt(AdditionalServiceMonth)) - parseInt(ServiceNotCountedMonth);
    var calculatedDays = (parseInt(TotalServiceDays) + parseInt(AdditionalServiceDays)) - parseInt(ServiceNotCountedDays);



    if (calculatedMonth % 2 == 0) {
        if (calculatedMonth === 2) {
            if (calculatedYear % 4 === 0) {
                if (calculatedDays > 29) {
                    calculatedMonth = parseInt(calculatedMonth) + 1;
                    calculatedDays = parseInt(calculatedDays) - 29;
                }
                else {

                    calculatedDays = parseInt(calculatedDays) - parseInt(ServiceNotCountedDays);
                }
            }
            else {
                if (calculatedDays > 28) {
                    calculatedMonth = parseInt(calculatedMonth) + 1;
                    calculatedDays = parseInt(calculatedDays) - 28;
                }
                else {

                    calculatedDays = parseInt(calculatedDays) - parseInt(ServiceNotCountedDays);
                }
            }
        }
        else {
            if (calculatedDays > 30) {
                while (calculatedDays > 30) {
                    calculatedMonth = parseInt(calculatedMonth) + 1;
                    calculatedDays = parseInt(calculatedDays) - 30;
                }
            }
            else {

                calculatedDays = parseInt(calculatedDays) - parseInt(ServiceNotCountedDays);
            }
        }
    }
    else {
        if (calculatedDays > 31) {
            while (calculatedDays > 31) {
                calculatedMonth = parseInt(calculatedMonth) + 1;
                calculatedDays = parseInt(calculatedDays) - 31;
            }
        }
        else {
            calculatedDays = parseInt(calculatedDays) - parseInt(ServiceNotCountedDays);
        }
    }

    while (calculatedMonth > 12) {
        calculatedYear = calculatedYear + 1;
        calculatedMonth = calculatedMonth - 12;

    }

    $("#QualifyingServiceYear").val(calculatedYear);
    $("#QualifyingServiceMonth").val(calculatedMonth);
    $("#QualifyingServiceDays").val(calculatedDays);

    HalfYearCalculation();
}

$("#btnUploadPaySlip").click(function () {
    $("#UploadPaySlip").trigger("click")
});
function BindServiceFormData() {
    var frm = $('#ServiceHistory');
    var formData = new FormData(frm[0]);
    formData.append('files', $('input[type=file]')[0]);
    formData.append('Id', $('#Id').val());
    formData.append('EmployeeRegistrationId', $('#EmployeeRegistrationId').val());
    formData.append('DTLDepartmentName', $('#DTLDepartmentName').val());
    formData.append('ReasonOfRetirement', $('#ReasonOfRetirement').val());
    formData.append('IsMedicalCardRequired', $('#IsMedicalCardRequired').val());
    formData.append('TotalServiceYear', $('#TotalServiceYear').val());
    formData.append('TotalServiceMonth', $('#TotalServiceMonth').val());
    formData.append('TotalServiceDays', $('#TotalServiceDays').val());
    formData.append('AdditionalServiceYears', $('#AdditionalServiceYears').val());
    formData.append('AdditionalServiceMonth', $('#AdditionalServiceMonth').val());
    formData.append('AdditionalServiceDays', $('#AdditionalServiceDays').val());
    formData.append('ServiceNotCountedYear', $('#ServiceNotCountedYear').val());
    formData.append('ServiceNotCountedMonth', $('#ServiceNotCountedMonth').val());
    formData.append('ServiceNotCountedDays', $('#ServiceNotCountedDays').val());
    formData.append('QualifyingServiceYear', $('#QualifyingServiceYear').val());
    formData.append('QualifyingServiceMonth', $('#QualifyingServiceMonth').val());
    formData.append('QualifyingServiceDays', $('#QualifyingServiceDays').val());
    formData.append('BasicPay', $('#BasicPay').val());
    formData.append('DAPercent', $('#DA_Percent').val());
    formData.append('DA', $('#DA').val());
    formData.append('NPA', $('#NPA').val());
    //formData.append('DOB', $('#DOB').val());
    //formData.append('ServiceStartDate', $('#ServiceStartDate').val());
    //formData.append('ServiceEndDate', $('#ServiceEndDate').val());
    return formData;
}

$("#closePaySlipModal").click(function () {
    DeleteTempFile($("#ifrmPaySlip1").attr("src"));
    DeleteTempFile($("#ifrmPaySlip2").attr("src"));
    DeleteTempFile($("#ifrmPaySlip3").attr("src"));
});

$("#viewPaySlip").click(function () {
    var fileName = "payslip";
    var employeeId = $("#EmployeeRegistrationId").val();
    $.ajax({
        url: "/Pensioner/GetFile",
        type: "POST",
        dataType: "json",
        data: { fileName: fileName, employeeId: employeeId },
        success: function (data) {
            if (data != "NoRecord") {
                $("#ifrmPaySlip1").attr('src', data.paySlip1);
                $("#ifrmPaySlip2").attr('src', data.paySlip2);
                $("#ifrmPaySlip3").attr('src', data.paySlip3);
            } else {
                $("#modal-body-payslip").html("<div class='text - center'>No Document Found!!!</div>")
            }
            $("#modal-paySlip").modal("show");
        },
        error: function (err) {
            toastrError("Something went wrong!!!")
        }

    })
});