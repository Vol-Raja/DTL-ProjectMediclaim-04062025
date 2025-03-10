$(function () {
    $("#txtPensionerName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Disbursement/AutoComplete/',
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
            $("#txtCurrentAge").val(i.item.cr);

            $("#txtMonthlyPension").val(i.item.monthlyPension);
            if (moment(i.item.dob).format("yyyy-MM-DD") != "1900-01-01") {
                $("#txtDOB").val(moment(i.item.dob).format("YYYY-MM-DD"));
                $("#txtDOB").trigger('change')
            }
            $('#Employer').val(i.item.employerName).change();
            $("#txtIncrementPercentage").val(i.item.aqpercentage);
            $("#txtIncrementAmount").val(i.item.incremenetedAmount);
            $("#txtMonthlyPensionAQP").val(i.item.monthlyAQP);
            $("#id").val(i.item.id);
            $("#eid").val(i.item.eid);
            if (i.item.ageGroup != null) {
                $('#AgeGroup').val(i.item.ageGroup).change();
                AgeSelection(i.item.cr);
            }
            else {
                $('#AgeGroup').prop('selectedIndex', 0);
                //AgeSelection(i.item.cr);
            }
            if (i.item.cr < 70) {
                /*$('#AgeGroup').attr('disabled', true);*/
                $('#btnSubmit').attr('disabled', true);
                $('#dtFrom').attr('disabled', true);
            }
            else {
                /*$('#AgeGroup').attr('disabled', true);*/
                $('#btnSubmit').attr('disabled', false);
                $('#dtFrom').attr('disabled', false);
            }
            if (moment(i.item.fromDate).format("yyyy-MM-DD") != "1900-01-01") {
                $("#dtFrom").val(moment(i.item.fromDate).format("YYYY-MM-DD"));
                $("#dtFrom").trigger('change')
                CalculateLastDate(i.item.dob, i.item.ageGroup);
            }
            else {
                $("#dtFrom").val("");
                $("#dtTo").val("");
            }
        },
        minLength: 1
    });
});

$(function () {
    $("#AgeGroup").change("option", function () {
        AQPCalculation();
    });
});

$('#dtFrom').change(function () {
    var dob = $("#txtDOB").val();
    var ageGroup = $("#AgeGroup").val();
    CalculateLastDate(dob, ageGroup);
});

function CalculateLastDate(dob, ageGroup) {
    if (dob != '') {
        var str = ageGroup.split(' - ')[1];
        var date = new Date(dob);
        var newdate = new Date(date);
        newdate.setDate(newdate.getDate());
        var dd = newdate.getDate();
        var mm = newdate.getMonth() + 1;
        var y = newdate.getFullYear() + parseInt(str);
        var someFormattedDate = mm + '/' + dd + '/' + y;
        $("#dtTo").val(moment(someFormattedDate).format("YYYY-MM-DD"));
        $("#dtTo").trigger('change')
    }
}

function AQPCalculation() {

    if ($("#AgeGroup").val() == "70 - 75") {
        Calculation(10);
    }
    if ($("#AgeGroup").val() == "75 - 80") {
        Calculation(15);
    }
    if ($("#AgeGroup").val() == "80 - 85") {
        Calculation(20);
    }
    if ($("#AgeGroup").val() == "85 - 90") {
        Calculation(30);
    }
    if ($("#AgeGroup").val() == "90 - 95") {
        Calculation(40);
    }
    if ($("#AgeGroup").val() == "95 - 100") {
        Calculation(50);
    }
    if ($("#AgeGroup").val() == "100 or more") {
        Calculation(100);
    }
}

function Calculation(incrementP) {
    var pension = $("#txtMonthlyPension").val();
    var incrementedpension = parseFloat(pension) * (parseFloat(incrementP / 100));
    var totalPension = parseFloat(pension) + parseFloat(incrementedpension);
    $("#txtIncrementPercentage").val(incrementP + "%");
    $("#txtIncrementAmount").val(incrementedpension);
    $("#txtMonthlyPensionAQP").val(totalPension);
}
function AgeSelection(age) {
    var ageSel = "";
    if (age >= 70 && age <= 75) {
        ageSel = "70 - 75";
        $('#AgeGroup').val(ageSel).change();
    }
    if (age >= 75 && age <= 80) {
        ageSel = "75 - 80";
        $('#AgeGroup').val(ageSel).change();
    }
    if (age >= 80 && age <= 85) {
        ageSel = "80 - 85";
        $('#AgeGroup').val(ageSel).change();
    }
    if (age >= 85 && age <= 90) {
        ageSel = "85 - 90";
        $('#AgeGroup').val(ageSel).change();
    }
    if (age >= 90 && age <= 95) {
        ageSel = "90 - 95";
        $('#AgeGroup').val(ageSel).change();
    }
    if (age >= 95 && age <= 100) {
        ageSel = "95 - 100";
        $('#AgeGroup').val(ageSel).change();
    }
    if (age >= 100) {
        ageSel = "100 or more";
        $('#AgeGroup').val(ageSel).change();
    }

     
}