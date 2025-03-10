AddEditForm();

$("#btnServiceHistory").click(function () {
    $("#ServiceHistory").submit();
});
$(document).ready(function () {
    PensionCalculation();
    FamilyPension();
    GratuityCalculation();
    Commutationcalculation();
    HalfYearCalculation();
});
var CommutationTableValues = new Object();
function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            var formData = BindServiceFormData();
            $.ajax({
                url: "/EmployeeRegistration/CreateServiceHistory",
                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    PensionCalculation();
                    FamilyPension();
                    GratuityCalculation();
                    Commutationcalculation();
                    HalfYearCalculation()
                    if (data == "add") {
                        toastrSuccess("Service history has added successfully!");

                    } else {
                        toastrSuccess("Service history has updated successfully!");
                    }
                    $("#custom-tabs-three-pensionSlip-tab").trigger("click");
                },
                error: function (err) {
                    toastrError("Something went wrong!!!");
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

    var k = $("#DOB").val();
    var m = $("#ServiceStartDate").val();
    var n = $("#ServiceEndDate").val();
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
    var DOBYear = new Date($("#DOB").val().split(" ")[0].split("-")[2] + "-" + $("#DOB").val().split(" ")[0].split("-")[1] + "-" + $("#DOB").val().split(" ")[0].split("-")[0]).getFullYear();
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
    var DOBYear = new Date($("#DOB").val().split(" ")[0].split("-")[2] + "-" + $("#DOB").val().split(" ")[0].split("-")[1] + "-" + $("#DOB").val().split(" ")[0].split("-")[0]).getFullYear();
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

function Commutationcalculation() {
    var currentYear = new Date();
    var DOBYear = new Date($("#DOB").val().split(" ")[0].split("-")[2] + "-" + $("#DOB").val().split(" ")[0].split("-")[1] + "-" + $("#DOB").val().split(" ")[0].split("-")[0]).getFullYear();
    var CommutationFactor = parseInt($("#AdmissiablePension").val()) * parseInt($(".Commutation").val()) / 100;
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
    $("#LumpsumPayable").html(Math.round(lumpSumPayable));
}

function HalfYearCalculation() {
    let TotalServiceYear = parseInt($("#TotalServiceYear").val()) * 2;
    let TotalServiceMonth = $("#TotalServiceMonth").val();

    if (TotalServiceMonth <= 3) {
        $("#HalfYear").val(TotalServiceYear + " units");
    }
    else if (TotalServiceMonth > 3 && TotalServiceMonth <= 6) {
        $("#HalfYear").val(parseInt(TotalServiceYear) + " half units");
    }
    else if (TotalServiceMonth > 6 && TotalServiceMonth <= 9) {
        $("#HalfYear").val(parseInt(TotalServiceYear) + 1 + " half units");
    }
    else if (TotalServiceMonth > 10 && TotalServiceMonth <= 12) {
        $("#HalfYear").val(parseInt(TotalServiceYear) + 2 + " units");
    }

}
$(".serviceCount").change("input", function () {
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
});


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
    formData.append('DA', $('#DA').val());
    formData.append('NPA', $('#NPA').val());
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
        url: "/EmployeeRegistration/GetFile",
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