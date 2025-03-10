//window.onload = SelectFinancialYear;
var tblbudgetDec;
var myChart;

AddEditForm();
$("#btnDeclareBudget").click(function () {
    if (confirm("Please check the details before Submission!")) {
        $("#BudgetManagementForm").submit();
    }
    else {
        alert("Canceled Budget Declaration...!!!");
    }
});

function ClearData() {

    //$("#FinancialYear").val("").change();
    $("#AllocationProgram").val("");
    $("#Others").val("");
    $("#AllocatedFund").val("");
    $("#DisbursementPeriod").val("");
    $("#DisbursementAuthority").val("");
    //SelectFinancialYear();

}

function DeleteAllocation(rw) {
    $(rw).parents("tr").remove();
    var tbl = document.getElementById("tblbudgetDec");
    if (tbl.rows.length == 1) {
        document.getElementById("tblbudgetDec").style.display = 'none';
    }

}

function Addparticular() {
    BudgetAddToTable();
    ClearData();
}
function BudgetAddToTable() {

    
        
    if ($("#FinancialYear").val() == "") {
        toastrError("Please select financial year");
        return;
    }
    if ($("#AllocationProgram").val() == "" && $("#Others").val() == "") {
        toastrError("Please select Allocation Program");
        return;
    }
    if ($("#AllocatedFund").val() == "") {
        toastrError("Please enter Allocated Fund");
        return;
    }
    if ($("#DisbursementPeriod").val() == "") {
        toastrError("Please select Disbursement Period");
        return;
    }
    if ($("#DisbursementAuthority").val() == "") {
        toastrError("Please select Disbursement Authority");
        return;
    }
    document.getElementById("tblbudgetDec").style.display = '';
    document.getElementById("divbtnDeclare").style.display = '';
    document.getElementById("tblbudgetDecview").style.display = 'none';
    //if()
    var allocationprog = "";
    if ($("#AllocationProgram").val() != "")
        allocationprog = $("#AllocationProgram").val();
    if ($("#AllocationProgram").val() == "" && $("#Others").val() !== "")
        allocationprog = $("#Others").val();
    $("#tblbudgetDec").append("<tr>" +
        "<td>" + $("#FinancialYear").val() + "</td>" +
        "<td>" + allocationprog+ "</td>" +
        "<td>" + $("#AllocatedFund").val() + "</td>" +
        "<td>" + $("#DisbursementPeriod").val() + "</td>" +
        "<td>" + $("#DisbursementAuthority").val() + "</td>" +
        "<td>" +
        " <a href='javascript: void (0)' title='delete allocation' onclick='DeleteAllocation(this);'>" +
        "<i class='fa fa - trash'></i>Delete</a>" +
        "</td>" +
        "</tr>");
}

$("#AllocationProgram").on('change', function () {
    var tbl = document.getElementById("tblbudgetDec");
    for (var i = 1; i < tbl.rows.length; i++) {
        if (tbl.rows[i].cells[1].innerText == $("#AllocationProgram").val()) {
            alert("Progaram already allocated please select another program!");
            $("#AllocationProgram").val("").change();
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
$("#FinancialYear").on('change', function () {
    document.getElementById("divViewBudget").style.display = 'none';
    
    if ($("#FinancialYear").val() != "") {
        $("#RevFinancialYear").val($("#FinancialYear").val())
        var tbl = document.getElementById("tblbudgetDec");
        for (var i = 1; i < tbl.rows.length; i++) {
            if (tbl.rows[i].cells[0].innerText != $("#FinancialYear").val()) {
                alert("Please check financial year!");
                $("#FinancialYear").val("").change();
                break;
            }

        }
        GetBudgetDeclareByFinancialYear();
    }
    
});

function BindBudgetDeclarationData() {
    var frm = $('#BudgetManagementForm');
    var formData = new FormData(frm[0]);
    var BudgetDeclaration = new Array();

    var tbl = document.getElementById("tblbudgetDec");
    for (var i = 1; i < tbl.rows.length; i++) {
        var itemlist = {};
        itemlist.id = $('#Id').val();
        
        itemlist.FinancialYear = tbl.rows[i].cells[0].innerText;
        itemlist.AllocationProgram = tbl.rows[i].cells[1].innerText
        itemlist.AllocatedFunds = tbl.rows[i].cells[2].innerText;
        itemlist.DisbursementPeriod = tbl.rows[i].cells[3].innerText;
        itemlist.DisbursementAuthority = tbl.rows[i].cells[4].innerText;
        itemlist.Type = "Insert";
        BudgetDeclaration.push(itemlist);
    }
    return BudgetDeclaration;
}
function AddEditForm() {
    
        $.validator.setDefaults({
            submitHandler: function () {
                var tbl = document.getElementById("tblbudgetDec");
                if (tbl.rows.length == 1) {
                    toastrError("No data for Budget. Please enter data!")
                    return;
                }
                if ($("#Operation").val == "Update")
                    DeleteBudgetByFinancialYear();
                var budgetDeclaration = BindBudgetDeclarationData();

                var lstbugdet = {};
                lstbugdet.BudgetDeclaration = budgetDeclaration;
                $.ajax({
                    url: "/FinanceManagement/AddEditBudgetDeclaration",
                    type: "POST",
                    dataType: "json",
                    data: lstbugdet,
                    success: function (data) {
                        //alert(data);
                        toastrSuccess("Data saved successfully!");
                        GetBudgetDeclareByFinancialYear();
                        document.getElementById("tblbudgetDec").style.display = 'none';
                        document.getElementById("divbtnAddParticular").style.display = 'none';
                        document.getElementById("divbtnDeclare").style.display = 'none';

                        /*ClearData();
                        $("#tblbudgetDec tbody tr").remove();
                        document.getElementById("tblbudgetDec").style.display = 'none';*/
                        getChartData("save");
                        $("#tblbudgetDec tbody tr").remove();
                        //if (data == "add" || data == "update") {

                        //}
                    },
                    error: function (err) {
                        toastrError("Budget not declared correctly. Please try later");
                    }
                });
            }
        });
        $('#BudgetManagementForm').validate({
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

function SelectFinancialYear() {
    var d = new Date();
    var n = d.getMonth();
    var yr = d.getFullYear();
    var prevyr = yr - 1;
    var nxtyr = yr + 1;
    var strcurryr = yr + '-' + nxtyr;
    var strprevyr = prevyr + '-' + yr;
    //alert(yr);
    if (n > 2)
        $("#FinancialYear").val(strcurryr).change();
    else
        $("#FinancialYear").val(strprevyr).change();

}

function GetBudgetDeclareByFinancialYear() {
    
    var FinYear = $("#FinancialYear").val();
    
    $.ajax('/FinanceManagement/GetBudgetDeclarationByFinancialYear', {
        type: 'POST',  // http method
        data: { FinancialYear: FinYear },  // data to submit

        success: function (data, status, xhr) {
            if (data) {
                $("#tblbudgetDecview tbody tr").remove();
                var tempHTML = ""
                if (data.length > 0) {

                    document.getElementById("tblbudgetDecview").style.display = '';
                    document.getElementById("divViewBudget").style.display = '';
                    for (var i = 0; i < data.length; i++) {
                        $("#tblbudgetDecview").append("<tr>" +

                            "<td>" + data[i].financialYear + "</td>" +
                            "<td>" + data[i].allocationProgram + "</td>" +
                            "<td>" + data[i].allocatedFunds + "</td>" +
                            "<td>" + data[i].disbursementPeriod + "</td>" +
                            "<td>" + data[i].disbursementAuthority + "</td>" +
                            
                            "</tr>");
                        tempHTML += "<tr>" +

                            "<td>" + data[i].financialYear + "</td>" +
                            "<td>" + data[i].allocationProgram + "</td>" +
                            "<td>" + data[i].allocatedFunds + "</td>" +
                            "<td>" + data[i].disbursementPeriod + "</td>" +
                            "<td>" + data[i].disbursementAuthority + "</td>" +
                            "</tr>";
                    }


                    if ($.fn.DataTable.isDataTable('#tblbudgetDecview')) {
                        tblbudgetDec.destroy();
                    }

                    $("#tblbudgetDecview tbody").html(tempHTML);
                    tblbudgetDec = $('#tblbudgetDecview').DataTable({
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
                       /* tblbudgetDecview.columns.adjust().draw();*/
                    }, 100, tblbudgetDecview)
                    
                    document.getElementById("divbtnAddParticular").style.display = 'none';
                    document.getElementById("divbtnDeclare").style.display = 'none';
                    getChartData("view");
                }
                else {
                    document.getElementById("tblbudgetDecview").style.display = 'none';
                    document.getElementById("divbtnAddParticular").style.display = '';
                    document.getElementById("divViewBudget").style.display = 'none';
                    document.querySelector("#chartReport").innerHTML = '<canvas id="myChart"></canvas>';

                }
            }
        },
        error: function (data) {
        }
    });
}



function getChartData(mode) {
    if(mode=="save")
        var tbl = document.getElementById("tblbudgetDec");
    else if (mode == "view")
        var tbl = document.getElementById("tblbudgetDecview");
    var xAllocationProgramVal = [], yAllocationFundVal = [],colors=[];
    for (var i = 1; i < tbl.rows.length; i++) {
        
        xAllocationProgramVal.push(tbl.rows[i].cells[1].innerText);
        yAllocationFundVal.push(tbl.rows[i].cells[2].innerText);
        colors.push('#' + Math.floor(Math.random() * 16777215).toString(16));
    }
    var pchart = document.getElementById("myChart");

    if (myChart != null)
        myChart.destroy();

    myChart = new Chart(pchart, {
        type: "pie",
        data: {
            labels: xAllocationProgramVal,
            datasets: [{
                backgroundColor: colors,
                borderWidth: 1,
                data: yAllocationFundVal
            }]
        },
        options: {
            title: {
                display: true,
                text: "Budget Allocation for " + $("#FinancialYear").val()
            }
        }
    });

    
}
