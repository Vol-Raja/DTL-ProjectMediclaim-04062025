//window.onload = RevSelectFinancialYear;
var tblrevbudgetDec;
var myChart;

RevAddEditForm();
$("#btnModifyBudget").click(function () {
    if (confirm("Please check the details before Submission!")) {
        $("#BudgetRevisionForm").submit();
        location.reload();
    }
    else {
        alert("Canceled Budget Modification...!!!");
    }
});

function RevClearData() {

    //$("#FinancialYear").val("").change();
    $("#RevAllocationProgram").val("");
    $("#RevOthers").val("");
    $("#RevAllocatedFund").val("");
    $("#RevDisbursementPeriod").val("");
    $("#RevDisbursementAuthority").val("");
    //RevSelectFinancialYear();

}

function RevDeleteAllocation(rw) {
    $(rw).parents("tr").remove();
    var tbl = document.getElementById("tblrevbudgetDec");
    if (tbl.rows.length == 1) {
        document.getElementById("tblrevbudgetDec").style.display = 'none';
    }

}

function RevAddparticular() {
    RevBudgetAddToTable();
    RevClearData();
}
function RevBudgetAddToTable() {



    if ($("#RevFinancialYear").val() == "") {
        toastrError("Please select financial year");
        return;
    }
    if ($("#RevAllocationProgram").val() == "" && $("#RevOthers").val() == "") {
        toastrError("Please select Allocation Program");
        return;
    }
    if ($("#RevAllocatedFund").val() == "") {
        toastrError("Please enter Allocated Fund");
        return;
    }
    if ($("#RevDisbursementPeriod").val() == "") {
        toastrError("Please select Disbursement Period");
        return;
    }
    if ($("#RevDisbursementAuthority").val() == "") {
        toastrError("Please select Disbursement Authority");
        return;
    }
    document.getElementById("tblrevbudgetDec").style.display = '';
    //if()
    var allocationprog = "";
    if ($("#RevAllocationProgram").val() != "")
        allocationprog = $("#RevAllocationProgram").val();
    if ($("#RevAllocationProgram").val() == "" && $("#RevOthers").val() !== "")
        allocationprog = $("#RevOthers").val();
    $("#tblrevbudgetDec").append("<tr>" +
        "<td>" + $("#RevFinancialYear").val() + "</td>" +
        "<td>" + allocationprog + "</td>" +
        "<td>" + $("#RevAllocatedFund").val() + "</td>" +
        "<td>" + $("#RevDisbursementPeriod").val() + "</td>" +
        "<td>" + $("#RevDisbursementAuthority").val() + "</td>" +
        "<td>" +
        " <a href='javascript: void (0)' title='delete allocation' onclick='RevDeleteAllocation(this);'>" +
        "<i class='fa fa - trash'></i>Delete</a>" +
        "</td>" +
        "</tr>");
}

$("#RevAllocationProgram").on('change', function () {
    var tbl = document.getElementById("tblrevbudgetDec");
    for (var i = 1; i < tbl.rows.length; i++) {
        if (tbl.rows[i].cells[1].innerText == $("#RevAllocationProgram").val()) {
            alert("Progaram already allocated please select another program!");
            $("#RevAllocationProgram").val("").change();
            break;
        }
    }
});
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
$("#RevFinancialYear").on('change', function () {
    //if (confirm("Are you sure you want data from another Financial Year?")) {
        GetBudgetDeclarationByFinancialYear();
       // break;
   // }
    //if ($("#RevFinancialYear").val() != "") {
    //    var tbl = document.getElementById("tblrevbudgetDec");
    //    for (var i = 1; i < tbl.rows.length; i++) {
    //        if (tbl.rows[i].cells[0].innerText != $("#RevFinancialYear").val()) {
    //            if (confirm("Are you sure you want data from another Financial Year?")) {
    //                GetBudgetDeclarationByFinancialYear();
    //                break;
    //            }
    //            else
    //                break;
    //        }

    //    }
    //    //GetBudgetDeclarationByFinancialYear();
    //}

});

function RevBindBudgetDeclarationData() {
    var frm = $('#BudgetRevisionForm');
    var formData = new FormData(frm[0]);
    var revBudgetDeclaration = new Array();

    var tbl = document.getElementById("tblrevbudgetDec");
    for (var i = 1; i < tbl.rows.length; i++) {
        var revitemlist = {};
        revitemlist.id = $('#Id').val();

        revitemlist.FinancialYear = tbl.rows[i].cells[0].innerText;
        revitemlist.AllocationProgram = tbl.rows[i].cells[1].innerText
        revitemlist.AllocatedFunds = tbl.rows[i].cells[2].innerText;
        revitemlist.DisbursementPeriod = tbl.rows[i].cells[3].innerText;
        revitemlist.DisbursementAuthority = tbl.rows[i].cells[4].innerText;
        revitemlist.Type = "Update";
        revBudgetDeclaration.push(revitemlist);
    }
    return revBudgetDeclaration;
}
function RevAddEditForm() {
        $.validator.setDefaults({
            submitHandler: function () {
                var tbl = document.getElementById("tblrevbudgetDec");
                //if (tbl.rows.length == 1) {
                //    toastrError("No data for Budget. Please enter data!")
                //    return;
                //}

                DeleteBudgetByFinancialYear();
                var revbudgetDeclaration = RevBindBudgetDeclarationData();

                var revlstbugdet = {};
                revlstbugdet.BudgetDeclaration = revbudgetDeclaration;
                $.ajax({
                    url: "/FinanceManagement/AddEditBudgetDeclaration",
                    type: "POST",
                    dataType: "json",
                    data: revlstbugdet,
                    success: function (data) {
                        //alert(data);
                        toastrSuccess("Data saved successfully!");
                        /*RevClearData();
                        $("#tblrevbudgetDec tbody tr").remove();
                        document.getElementById("tblrevbudgetDec").style.display = 'none';*/
                        getRevChartData();
                        //if (data == "add" || data == "update") {

                        //}
                    },
                    error: function (err) {
                        toastrError("Budget not declared correctly. Please try later");
                    }
                });
            }
        });
        $('#BudgetRevisionForm').validate({
            /* rules: {
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
     
             },*/
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
function GetBudgetDeclarationByFinancialYear() {
    var FinYear = $("#RevFinancialYear").val();
    $.ajax('/FinanceManagement/GetBudgetDeclarationByFinancialYear', {
        type: 'POST',  // http method
        data: { FinancialYear: FinYear },  // data to submit

        success: function (data, status, xhr) {
            if (data) {
                $("#tblrevbudgetDec tbody tr").remove();
                var tempHTML = "";
                if (data.length > 0) {

                    document.getElementById("tblrevbudgetDec").style.display = '';
                    for (var i = 0; i < data.length; i++) {
                        $("#tblrevbudgetDec").append("<tr>" +

                            "<td>" + data[i].financialYear + "</td>" +
                            "<td>" + data[i].allocationProgram + "</td>" +
                            "<td>" + data[i].allocatedFunds + "</td>" +
                            "<td>" + data[i].disbursementPeriod + "</td>" +
                            "<td>" + data[i].disbursementAuthority + "</td>" +
                            "<td>" +
                            " <a href='javascript: void (0)' title='delete allocation' onclick='RevDeleteAllocation(this);'>" +
                            "<i class='fa fa - trash'></i>Delete</a>" +
                            "</td>" +
                            "</tr>");

                        tempHTML += "<tr>" +

                            "<td>" + data[i].financialYear + "</td>" +
                            "<td>" + data[i].allocationProgram + "</td>" +
                            "<td>" + data[i].allocatedFunds + "</td>" +
                            "<td>" + data[i].disbursementPeriod + "</td>" +
                            "<td>" + data[i].disbursementAuthority + "</td>" +
                            "<td>" +
                            " <a href='javascript: void (0)' title='delete allocation' onclick='RevDeleteAllocation(this);'>" +
                            "<i class='fa fa - trash'></i>Delete</a>" +
                            "</td>" +
                            "</tr>";

                    }
                    if ($.fn.DataTable.isDataTable('#tblrevbudgetDec')) {
                        tblrevbudgetDec.destroy();
                    }

                    $("#tblrevbudgetDec tbody").html(tempHTML);
                    tblrevbudgetDec = $('#tblrevbudgetDec').DataTable({
                        paging: true,
                        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
                        order: [[1, "asc"]],
                        searching: false,
                        ordering: false,
                        responsive: false,
                        info: true,
                        dom: 'Bfrtip',
                        buttons: ['pageLength', {
                            extend: 'csv',
                            className: "btn btn-outline-info",
                            text: '<i class="nav-icon fas  fa-file-excel"></i>',
                            title: "Export Excel",
                            //action: function (e, dt, node, config) {
                            //    //ExportNewPackages('excel');
                            //}
                        },
                            {
                                extend: 'pdf',
                                className: "btn btn-outline-info",
                                text: '<i class="nav-icon fas  fa-file-pdf"></i>',
                                title: "Export PDF",
                                //action: function (e, dt, node, config) {
                                //    //ExportNewPackages('pdf');
                                //}
                            },
                            {
                                //extend: 'pdf',
                                className: "btn btn-outline-info",
                                text: '<i class="nav-icon fas  fa-paper-plane"></i>',
                                title: "Send To",
                                action: function (e, dt, node, config) {
                                    //ExportNewPackages('pdf');
                                }
                            }
                        ],
                        fixedHeader: true,
                        scrollY: "calc(100%)",
                        scrollX: true,
                        'select': {
                            'style': 'multi'
                        }
                    });
                    setTimeout(function () {
                        /*tblrevbudgetDec.columns.adjust().draw();*/
                    }, 100, tblrevbudgetDec)
                    
                }
                getRevChartData();
            }
        },
        error: function (data) {
        }
    });
}

function DeleteBudgetByFinancialYear() {
    var FinYear = $("#RevFinancialYear").val();
    $.ajax('/FinanceManagement/DeleteBudgetByFinancialYear', {
        type: 'POST',  // http method
        data: { FinancialYear: FinYear },  // data to submit

        success: function (data, status, xhr) {
            if (data) {


            }
        },
        error: function (data) {
        }
    });
}
function RevSelectFinancialYear() {
    var d = new Date();
    var n = d.getMonth();
    var yr = d.getFullYear();
    var prevyr = yr - 1;
    var nxtyr = yr + 1;
    var strcurryr = yr + '-' + nxtyr;
    var strprevyr = prevyr + '-' + yr;
    //alert(yr);
    if (n > 2)
        $("#RevFinancialYear").val(strcurryr).change();
    else
        $("#RevFinancialYear").val(strprevyr).change();
    GetBudgetDeclarationByFinancialYear();
}
function getRevChartData() {
    var tblrev = document.getElementById("tblrevbudgetDec");
    var xrevAllocationProgramVal = [], yrevAllocationFundVal = [], revcolors = [];
    for (var i = 1; i < tblrev.rows.length; i++) {

        xrevAllocationProgramVal.push(tblrev.rows[i].cells[1].innerText);
        yrevAllocationFundVal.push(tblrev.rows[i].cells[2].innerText);
        revcolors.push('#' + Math.floor(Math.random() * 16777215).toString(16));
    }
    var prevchart = document.getElementById("myRevChart");
    if (myChart != null)
        myChart.destroy();
    myChart = new Chart(prevchart, {
        type: "pie",
        data: {
            labels: xrevAllocationProgramVal,
            datasets: [{
                backgroundColor: revcolors,
                borderWidth: 1,
                data: yrevAllocationFundVal
            }]
        },
        options: {
            title: {
                display: true,
                text: "Budget Allocation for " + $("#RevFinancialYear").val()
            }
        }
    });
}
