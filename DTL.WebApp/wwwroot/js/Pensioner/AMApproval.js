var empID;

$(".btnApprovePensionApp").click(function () {
    debugger;
    var empId = $(this).attr("value");
    UpdatePPO(empId, 7, "AM")
    location.reload(true)
});
$(".btnAMRejectPensionApp").click(function () {
    debugger;
    var empId = this.value;
    UpdatePPO(empId, 6, "AM")
    empID = empId;
});

function UpdatePPO(empId, status, role) {
    $.ajax({
        url: "/Pensioner/UpatePesionAppStatus",
        type: "POST",
        dataType: "json",
        data: { empId: empId, status: status, role: role },
        success: function (data) {
            debugger;
            //toastrSuccess("Pension Application Status has updated by " + role + ".");
        }
    })
}

$("#btnSaveRejection").click(function () {
    debugger;
    var empId = empID;
    var role = $("#role").val();
    UpdateEmployeeRegStatusByHRAdmin(empId, 1, role, $("#RejectionReason").val());

});

function UpdateEmployeeRegStatusByHRAdmin(employeeId, status, role, remarks) {
    $.ajax({
        url: "/Pensioner/UpdateEmployeeRegStatusByHRAdmin",
        type: "POST",
        dataType: "json",
        data: { employeeId: employeeId, Role: role, status: status, Remarks: remarks },
        success: function (data) {
            toastrSuccess("Pension Application Status has updated by " + role + ".");
            $("#RejectionReason").val("");
        },
        error: function (err) {
            toastrError("Something went wrong!!!");
        }
    })
}