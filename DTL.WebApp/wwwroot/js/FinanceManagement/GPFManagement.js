var tbloutFlowReport, tblinFlowReport;
var currentDate, month, employer, employeeID;
var activeTab = $('ul.nav li a.active').attr('id');
var inoutflow = $('#custom-tabs-three-gpfioflow');
var outflow = $('#custom-tabs-three-gpfoutflow');
var inflow = $('#custom-tabs-three-gpfinflow');


$('.nav > li > a').on("click", function (e) {
    
    var n = $(e.target).attr('id');
    BindData(month, employer, employeeID, n);
    if (n == "custom-tabs-three-gpfoutflow-tab") {
        outflow.show();
        inflow.hide();
        inoutflow.hide();
    }
    else if (n == "custom-tabs-three-gpfinflow-tab") {
        outflow.hide();
        inflow.show();
        inoutflow.hide();
    }
    else {
        outflow.hide();
        inflow.hide();
        inoutflow.show();
    }
});


$("#datepicker").datepicker({
    changeMonth: true,
    changeYear: true,
    showButtonPanel: true,
    dateFormat: 'mm/yy',
    onClose: function (dateText, inst) {
        $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
        searchData();
        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, employeeID, k);
    }

});

function searchData() {
    currentDate = new Date();
    var mon = (currentDate.getMonth() + 1);
    var yr = (currentDate.getFullYear());
    var monyr = mon.toString() + '/' + yr.toString();
    var data = ($("#datepicker").val());

    month = (data == '' ? monyr : data);
    employer = $("#searchByEmployer").val();
    employeeID = $("#searchByEmployeeId").val();
}

$(document).ready(function () {
    
    searchData();
    $('#datepicker').val(month);
    BindData(month, employer, employeeID, activeTab);

});

function BindData(month, employer, employeeID, flow) {
    $.ajax({
        url: "/FinanceManagement/GetGPFManagementReport",
        type: "POST",
        datatype: "json",
        data: { month: month, DTLOffice: employer, EmployeeID: employeeID, Flow: flow },
        success: function (data) {
            if (data) {
                if (flow == "custom-tabs-three-gpfoutflow-tab") {
                    outFlowTable(data._reports);
                    
                    redraw(data.employers, data.temp2, flow);
                }
                else if (flow == "custom-tabs-three-gpfinflow-tab") {
                    inflowTable(data._reports);
                    redraw(data.employers, data.temp2, flow);
                }
                else {
                    
                    $("#myChart").remove();
                    LoadInChart(data.employers, data.gpfContributions);
                    LoadOutChart(data.employers, data.gpfWithdrawals);
                    LoadInOutChart(data.employers, data.gpfContributions, data.gpfWithdrawals);
                }

                if (flow == "custom-tabs-three-gpfoutflow-tab")
                    setTimeout(function () {
                        tbloutFlowReport.columns.adjust().draw();
                    }, 100, tbloutFlowReport)
                else if (flow == "custom-tabs-three-gpfinflow-tab")
                    setTimeout(function () {
                        tblinFlowReport.columns.adjust().draw();
                    }, 100, tblinFlowReport)
            }
        },
        error: function (data) {

        }
    });
}


$(function () {
    $("#searchByEmployer").change("option", function () {
        searchData();
        
        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, employeeID, k);
    });
});
$(function () {
    $("#searchByEmployeeId").change("option", function () {
        searchData();
        
        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, employeeID, k);
    });
});

function inflowTable(data) {
    
    $('#gpfRptTbl1').show();

    var tempHtml1 = ""
    for (var i = 0; i < data.length; i++) {
        tempHtml1 += "<tr>" +
            "<td>" + data[i].month + "</td>" +
            "<td>" + data[i].employeeName + "</td>" +
            "<td>" + data[i].employeeID + "</td>" +
            "<td>" + data[i].gpfContribution + "</td>" +
            "<td>" + data[i].employer + "</td>" +
            "</tr> ";
    }

    if ($.fn.DataTable.isDataTable('#gpfRptTbl1')) {
        tblinFlowReport.destroy();
    }

    $('#gpfRptTbl').hide();
    $("#gpfRptTbl1 tbody").html(tempHtml1);
    tblinFlowReport = $('#gpfRptTbl1').DataTable({
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
        scrollY: "calc(100%-100px)",
        scrollX: true,
        'select': {
            'style': 'multi'
        }
    });
}


function outFlowTable(data) {
    var tempHTML = ""
    $('#gpfRptTbl').show();
    
    $('#gpfRptTbl1').hide();
    for (var i = 0; i < data.length; i++) {
        tempHTML += "<tr>" +
            "<td>" + data[i].month + "</td>" +
            "<td>" + data[i].employeeName + "</td>" +
            "<td>" + data[i].employeeID + "</td>" +
            "<td>" + data[i].gpfWithdrawal + "</td>" +
            "<td>" + data[i].employer + "</td>" +
            "</tr> ";
    }

    if ($.fn.DataTable.isDataTable('#gpfRptTbl')) {
        tbloutFlowReport.destroy();
    }

    $("#gpfRptTbl tbody").html(tempHTML);
    tbloutFlowReport = $('#gpfRptTbl').DataTable({
        paging: true,
        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
        order: [[1, "asc"]],
        searching: false,
        ordering: true,
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
        scrollY: "calc(100%-100px)",
        scrollX: true,
        'select': {
            'style': 'multi'
        }
    });
}

function LoadChart(temp1, temp2, flow) {
    
    var dat;
    if (flow == "custom-tabs-three-gpfinflow-tab") {
        dat = "GPF Management Inflow";
    }
    else if (flow == "custom-tabs-three-gpfoutflow-tab") {
        dat = "GPF Management Outflow";
    }
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myChart");
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: temp1,
            datasets: [{
                label: dat,
                data: temp2,
                backgroundColor: [
                    'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                ],
                borderColor: [
                    'rgb(75, 192, 192)'
                    , 'rgb(75, 192, 192)', 'rgb(75, 192, 192)', 'rgb(75, 192, 192)', 'rgb(75, 192, 192)', 'rgb(75, 192, 192)'
                ]
            }]
        },
        options: {
            responsive: true,
            scales: {

            },

        }
    });

}

function redraw(temp1, temp2, flow) {
    
    $("#myChart").remove();// removing previous canvas element
    $("#chart_box").after("<canvas id='myChart' width='800' height='400'></canvas>");
    LoadChart(temp1, temp2, flow);

}

function LoadInChart(Employers, Contributions) {
    
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myinFlowChart");
    var myinFlowChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Employers,
            datasets: [{
                data: Contributions, backgroundColor: ['rgb(255, 99, 132)', 'rgb(255, 159, 64)', 'rgb(255, 205, 86)', 'rgb(75, 192, 192)', 'rgb(54, 162, 235)', 'rgb(120, 200, 235)'],
            }]
        },
        options: {
            plugins: {
                datalabels: {
                    display: true,
                    align: 'bottom',
                    backgroundColor: '#ccc',
                    borderRadius: 3,
                    font: {
                        size: 18,
                    }
                },
            }
        }
    });

}

function LoadOutChart(Employers, GPFWithdrawals) {
    
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myoutFlowChart");
    var myoutFlowChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Employers,
            datasets: [{
                data: GPFWithdrawals, backgroundColor: ['rgb(75, 192, 192)', 'rgb(175, 192, 192)', 'rgb(75, 92, 192)', 'rgb(175, 192, 92)', 'rgb(175, 92, 192)', 'rgb(275, 192, 192)'],
            }]
        },
        options: {
            plugins: {
                datalabels: {
                    display: true,
                    align: 'bottom',
                    backgroundColor: '#ccc',
                    borderRadius: 3,
                    font: {
                        size: 18,
                    }
                },
            }
        }
    });

}

function LoadInOutChart(temp1, temp2, temp3) {
    var ctx = document.getElementById("myinoutFlowChart");
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: temp1,
            datasets: [{
                label: "GPF Management Inflow",
                data: temp2,
                backgroundColor: [
                    'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                    , 'rgba(75, 192, 192)'
                ],
                borderColor: [
                    'rgb(75, 292, 192)'
                    , 'rgb(75, 292, 192)', 'rgb(75, 292, 192)', 'rgb(75, 292, 192)', 'rgb(75, 292, 192)', 'rgb(75, 292, 192)'
                ]
            }, {
                label: "GPF Management Outflow",
                data: temp3,
                backgroundColor: [
                    'rgba(175, 92, 92)'
                    , 'rgba(175, 92, 92)'
                    , 'rgba(175, 92, 92)'
                    , 'rgba(175, 92, 92)'
                    , 'rgba(175, 92, 92)'
                    , 'rgba(175, 92, 92)'
                ],
                borderColor: [
                    'rgb(175, 92, 92)'
                    , 'rgb(175, 92, 92)', 'rgb(175, 92, 92)', 'rgb(175, 92, 92)', 'rgb(175, 92, 92)', 'rgb(175, 92, 92)'
                ]
            }
            ]
        },
        options: {
            responsive: true,
            scales: {

            },

        }
    });

}
