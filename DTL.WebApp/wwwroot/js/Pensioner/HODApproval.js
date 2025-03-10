var empID;

$(".btnApproveSubmitPensionApp").click(function () {
    var empId = this.value;
    empID = empId;
    UpdatePPO(empId, 5, "HOD")
    location.reload(true)
});

$(".btnRejectPensionApp").click(function () {
    debugger;
    var empId = this.value;
    UpdatePPO(empId, 6, "HOD")
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
    UpdateEmployeeRegStatusByHRAdmin(empId, 6 , role, $("#RejectionReason").val());

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