var tblbudgetReport, tblpensionReport, tblgpfReport, tblinvestmentReport;
var currentDate, month, employer, financialyear, investmenttype, financialyears;
var activeTab = $('ul.nav li a.active').attr('id');
var budgetflow = $('#custom-tabs-four-budgetflow');
var pensionflow = $('#custom-tabs-four-penisonflow');
var pensionGraph = $('#pensionGraphs');
var gpfGraph = $('#gpfGraphs');
var budgetGraph = $('#budgetGraphs');
var investGraph = $('#investmentGraphs');
var gpfflow = $('#custom-tabs-four-gpfflow');
var investflow = $('#custom-tabs-four-investmentflow');
var search = $('#searchCriteria');
var myinFlowChart, myoutFlowChart, myChart, myChartx, myinFlowChart1, myoutFlowChart1, myChart1, myinFlowChart2, myoutFlowChart2, myChart2, myinFlowChart3, myoutFlowChart3, myChart3;

$('.nav > li > a').on("click", function (e) {
    
    var n = $(e.target).attr('id');
    BindData(month, employer, n, financialyear, investmenttype, financialyears);
    if (n == "custom-tabs-four-budget-tab") {
        search.hide();
        budgetflow.show();
        budgetGraph.hide();
        $('#budgettables').show();
        pensionflow.hide();
        gpfflow.hide();
        investflow.hide();
    }
    else if (n == "custom-tabs-four-pension-tab") {
        search.show();
        budgetflow.hide();
        pensionflow.show();
        pensionGraph.hide();
        $('#pensiontables').show();
        gpfflow.hide();
        investflow.hide();
    }
    else if (n == "custom-tabs-four-gpf-tab") {
        search.show();
        budgetflow.hide();
        pensionflow.hide();
        gpfGraph.hide();
        $('#gpftables').show();
        gpfflow.show();
        investflow.hide();
    }
    else {
        search.hide();
        investGraph.hide();
        $('#investmenttables').show();
        budgetflow.hide();
        pensionflow.hide();
        gpfflow.hide();
        investflow.show();
    }
});

function downloadPDF() {
    var canvas = document.querySelector('#myChart');
    //creates image
    var canvasImg = canvas.toDataURL("image/jpeg", 1.0);

    //creates PDF from img
    var doc = new jsPDF('landscape');
    doc.setFontSize(20);
    doc.text(15, 15, "Cool Chart");
    doc.addImage(canvasImg, 'JPEG', 10, 10, 280, 150);
    doc.save('canvas.pdf');
}

function downloadPDF1() {

    var reportPageHeight = 2000;
    var reportPageWidth = 2000;

    // create a new canvas object that we will populate with all other canvas objects
    var pdfCanvas = $('<canvas />').attr({
        id: "canvaspdf",
        width: reportPageWidth,
        height: reportPageHeight
    });



    // keep track canvas position
    var pdfctx = $(pdfCanvas)[0].getContext('2d');
    var pdfctxX = 0;
    var pdfctxY = 0;
    var buffer = 100;

    // for each chart.js chart
    $("canvas").each(function (index) {


        // get the chart height/width
        var canvasHeight = $(this).innerHeight();
        var canvasWidth = $(this).innerWidth();

        // draw the chart into the new canvas
        pdfctx.drawImage($(this)[0], pdfctxX, pdfctxY, canvasWidth, canvasHeight);
        pdfctxX += canvasWidth + buffer;

        // our report page is in a grid pattern so replicate that in the new canvas
        if (index % 2 === 1) {
            pdfctxX = 0;
            pdfctxY += canvasHeight + buffer;
        }
    });

    // create new pdf and add our new canvas as an image
    var pdf = new jsPDF('l', 'pt', [reportPageWidth, reportPageHeight]);
    pdf.addImage($(pdfCanvas)[0], 'PNG', 0, 0);

    // download the pdf
    pdf.save('report.pdf');


}

$('#btnPrint').click(function (event) {

    var n = $('ul.nav li a.active').attr('id');
    if (n == "custom-tabs-four-budget-tab")
        downloadPDF();
    else
        downloadPDF1();


});

$(function () {
    $("#searchByEmployer").change("option", function () {
        searchData();

        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    });
});

$('#pensionGraph').on("click", function (e) {
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#pensionGraphs').show();
    $('#pensiontables').hide();
});

$('#pensionTable').on("click", function (e) {
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#pensionGraphs').hide();
    $('#pensiontables').show();
});

$('#gpfGraph').on("click", function (e) {
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#gpfGraphs').show();
    $('#gpftables').hide();
});

$('#gpfTable').on("click", function (e) {
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#gpfGraphs').hide();
    $('#gpftables').show();
});

$('#budgetGraph').on("click", function (e) {
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#budgetGraphs').show();
    $('#budgettables').hide();
});

$('#budgetTable').on("click", function (e) {
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#budgetGraphs').hide();
    $('#budgettables').show();
});

$('#investmentGraph').on("click", function (e) {
    
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#investmentGraphs').show();
    $('#investmenttables').hide();
});

$('#investmenttable').on("click", function (e) {
    
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    $('#investmentGraphs').hide();
    $('#investmenttables').show();
});

$("#FinancialYear").prop("selectedIndex", 1);

$(function () {
    $("#FinancialYear").change("option", function () {
        
        searchData();

        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, k, financialyear, investmenttype, financialyears);
    });
});

$("#FinancialYears").prop("selectedIndex", 1);

$(function () {
    $("#FinancialYears").change("option", function () {
        
        searchData();

        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, k, financialyear, investmenttype, financialyears);
    });
});

$(function () {
    $("#searchByEmployer").change("option", function () {
        searchData();

        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, k, financialyear, investmenttype, financialyears);
    });
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

        BindData(month, employer, k, financialyear, investmenttype, financialyears);
    }

});

$(function () {
    $("#InvestmentType").change("option", function () {
        
        searchData();

        var k = $('ul.nav li a.active').attr('id');
        BindData(month, employer, k, financialyear, investmenttype, financialyears);
    });
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
        BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
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
    investmenttype = $("#InvestmentType").val();
    financialyear = $("#FinancialYear").val();
    financialyears = $("#FinancialYears").val();
}

$(document).ready(function () {
    
    searchData();
    $('#datepicker').val(month);
    BindData(month, employer, activeTab, financialyear, investmenttype, financialyears);
    budgetGraph.hide();
    search.hide();
});




function BindData(month, employer, flow, financialyear, investmenttype, financialyears) {
    $.ajax({
        url: "/FinanceManagement/GetFinancialAnalysisReport",
        type: "POST",
        datatype: "json",
        data: { month: month, DTLOffice: employer, Flow: flow, FinancialYear: financialyear, InvestmentType: investmenttype, FinancialYears: financialyears },
        success: function (data) {
            if (data) {
                
                if (flow == "custom-tabs-four-budget-tab") {

                    budgetFlowTable(data._budget);
                    LoadBudgetInChart(data.budgetparticulars, data.budgetAllocations);
                    LoadBudgetOutChart(data.budgetparticulars, data.budgetExpenditure);
                    LoadBudgetInOutChart(data.budgetparticulars, data.budgetAllocations, data.budgetExpenditure);
                    //redraw(data.employers, data.temp2, flow);
                }
                else if (flow == "custom-tabs-four-pension-tab") {

                    pensionFlowTable(data._pension);
                    LoadInChart(data.pensionEmployers, data.contributions);
                    LoadOutChart(data.pensionEmployers, data.pensionAmounts);
                    LoadInOutChart(data.pensionEmployers, data.contributions, data.pensionAmounts);
                }
                else if (flow == "custom-tabs-four-gpf-tab") {

                    gpfFlowTable(data._gpf);
                    LoadGPFInChart(data.gpfEmployers, data.gpfContributions);
                    LoadGPFOutChart(data.gpfEmployers, data.gpfWithdrawals);
                    LoadGPFInOutChart(data.gpfEmployers, data.gpfContributions, data.gpfWithdrawals);
                }
                else {
                    
                    investmentFlowTable(data.investmentdata);
                    LoadInvestInChart(data.investmenttitles, data.invested);
                    LoadInvestOutChart(data.investmenttitles, data.expectedROI);
                    LoadInvestInOutChart(data.investmenttitles, data.invested, data.expectedROI);
                }

                //if (flow == "custom-tabs-three-pensionoutflow-tab")
                //    setTimeout(function () {
                //        tbloutFlowReport.columns.adjust().draw();
                //    }, 100, tbloutFlowReport)
                //else if (flow == "custom-tabs-three-pensioninflow-tab")
                //    setTimeout(function () {
                //        tblinFlowReport.columns.adjust().draw();
                //    }, 100, tblinFlowReport)
            }
        },
        error: function (data) {

        }
    });
}

function investmentFlowTable(data) {

    $('#tblInvestment').show();
    var tempHtml1 = ""
    for (var i = 0; i < data.length; i++) {
        tempHtml1 += "<tr>" +
            "<td>" + data[i].financialYear + "</td>" +
            "<td>" + data[i].investmentType + "</td>" +
            "<td>" + data[i].referenceNo + "</td>" +
            "<td>" + data[i].investmentTitle + "</td>" +
            "<td>" + data[i].investedAmount + "</td>" +
            "<td>" + data[i].expectedProfitMargin + "</td>" +
            "<td>" + data[i].expectedROI + "</td>" +
            "</tr> ";
    }

    if ($.fn.DataTable.isDataTable('#tblInvestment')) {
        tblinvestmentReport.destroy();
    }

    $("#tblInvestment tbody").html(tempHtml1);
    tblinvestmentReport = $('#tblInvestment').DataTable({
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


function budgetFlowTable(data) {

    $('#budgetTbl').show();

    var tempHtml1 = ""
    for (var i = 0; i < data.length; i++) {
        tempHtml1 += "<tr>" +
            "<td>" + data[i].allocationProgram + "</td>" +
            "<td>" + data[i].allocatedFunds + "</td>" +
            "<td>" + data[i].disbursementPeriod + "</td>" +
            "<td>" + data[i].disbursementAuthority + "</td>" +
            "<td>" + data[i].disbursedAmount + "</td>" +
            "<td>" + data[i].outStanding + "</td>" +
            "</tr> ";
    }

    if ($.fn.DataTable.isDataTable('#budgetTbl')) {
        tblbudgetReport.destroy();
    }

    $("#budgetTbl tbody").html(tempHtml1);
    tblbudgetReport = $('#budgetTbl').DataTable({
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


function pensionFlowTable(data) {
    var tempHTML = ""

    $('#penisonTbl').show();
    for (var i = 0; i < data.length; i++) {
        tempHTML += "<tr>" +
            "<td>" + data[i].employeeID + "</td>" +
            "<td>" + data[i].employeeName + "</td>" +
            "<td>" + data[i].contribution + "</td>" +
            "<td>" + data[i].employer + "</td>" +
            "<td>" + data[i].pensionAmount + "</td>" +
            "</tr> ";
    }

    if ($.fn.DataTable.isDataTable('#penisonTbl')) {
        tblpensionReport.destroy();
    }

    $("#penisonTbl tbody").html(tempHTML);
    tblpensionReport = $('#penisonTbl').DataTable({
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

function gpfFlowTable(data) {
    var tempHTML = ""
    $('#gpfTbl').show();

    for (var i = 0; i < data.length; i++) {
        tempHTML += "<tr>" +
            "<td>" + data[i].employeeID + "</td>" +
            "<td>" + data[i].employeeName + "</td>" +
            "<td>" + data[i].gpfContribution + "</td>" +
            "<td>" + data[i].employer + "</td>" +
            "<td>" + data[i].gpfWithdrawal + "</td>" +
            "</tr> ";
    }

    if ($.fn.DataTable.isDataTable('#gpfTbl')) {
        tblgpfReport.destroy();
    }

    $("#gpfTbl tbody").html(tempHTML);
    tblgpfReport = $('#gpfTbl').DataTable({
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

function LoadInChart(Employers, Contributions) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myinFlowChart");
    if (myinFlowChart != null)
        myinFlowChart.destroy();
    myinFlowChart = new Chart(ctx, {
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadOutChart(Employers, PensionAmounts) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myoutFlowChart");
    if (myoutFlowChart != null)
        myoutFlowChart.destroy();
    myoutFlowChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Employers,
            datasets: [{
                data: PensionAmounts, backgroundColor: ['rgb(75, 192, 192)', 'rgb(175, 192, 192)', 'rgb(75, 92, 192)', 'rgb(175, 192, 92)', 'rgb(175, 92, 192)', 'rgb(275, 192, 192)'],
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadInOutChart(temp1, temp2, temp3) {

    var ctx = document.getElementById("myinoutFlowChart");
    if (myChartx != null)
        myChartx.destroy();
    myChartx = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: temp1,
            datasets: [{
                label: "Pension Management Inflow",
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
                label: "Pension Management Outflow",
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


function LoadGPFInChart(Employers, Contributions) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("mygpfinFlowChart");
    if (myinFlowChart1 != null)
        myinFlowChart1.destroy();
    myinFlowChart1 = new Chart(ctx, {
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadGPFOutChart(Employers, PensionAmounts) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("mygpfoutFlowChart");
    if (myoutFlowChart1 != null)
        myoutFlowChart1.destroy();
    myoutFlowChart1 = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Employers,
            datasets: [{
                data: PensionAmounts, backgroundColor: ['rgb(75, 192, 192)', 'rgb(175, 192, 192)', 'rgb(75, 92, 192)', 'rgb(175, 192, 92)', 'rgb(175, 92, 192)', 'rgb(275, 192, 192)'],
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadGPFInOutChart(temp1, temp2, temp3) {

    var ctx = document.getElementById("mygpfinoutFlowChart");
    if (myChart1 != null)
        myChart1.destroy();
    myChart1 = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: temp1,
            datasets: [{
                label: "Inflow",
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
                label: "Outflow",
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

function LoadBudgetInChart(Employers, Contributions) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("mybudgetinFlowChart");
    if (myinFlowChart2 != null)
        myinFlowChart2.destroy();
    myinFlowChart2 = new Chart(ctx, {
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadBudgetOutChart(Employers, PensionAmounts) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("mybudgetoutFlowChart");
    if (myoutFlowChart2 != null)
        myoutFlowChart2.destroy();
    myoutFlowChart2 = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Employers,
            datasets: [{
                data: PensionAmounts, backgroundColor: ['rgb(75, 192, 192)', 'rgb(175, 192, 192)', 'rgb(75, 92, 192)', 'rgb(175, 192, 92)', 'rgb(175, 92, 192)', 'rgb(275, 192, 192)'],
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadBudgetInOutChart(temp1, temp2, temp3) {

    var ctx = document.getElementById("mybudgetinoutFlowChart");
    if (myChart2 != null)
        myChart2.destroy();
    myChart2 = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: temp1,
            datasets: [{
                label: "Inflow",
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
                label: "Outflow",
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

function LoadInvestInChart(Employers, Contributions) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myinvestmentinFlowChart");
    if (myinFlowChart3 != null)
        myinFlowChart3.destroy();
    myinFlowChart3 = new Chart(ctx, {
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadInvestOutChart(Employers, PensionAmounts) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myinvestmentFlowChart");
    if (myoutFlowChart3 != null)
        myoutFlowChart3.destroy();
    myoutFlowChart3 = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Employers,
            datasets: [{
                data: PensionAmounts, backgroundColor: ['rgb(75, 192, 192)', 'rgb(175, 192, 192)', 'rgb(75, 92, 192)', 'rgb(175, 192, 92)', 'rgb(175, 92, 192)', 'rgb(275, 192, 192)'],
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
                        size: 8,
                    }
                },
            }
        }
    });

}

function LoadInvestInOutChart(temp1, temp2, temp3) {

    var ctx = document.getElementById("myinvestmentinoutFlowChart");
    if (myChart3 != null)
        myChart3.destroy();
    myChart3 = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: temp1,
            datasets: [{
                label: "Inflow",
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
                label: "Outflow",
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