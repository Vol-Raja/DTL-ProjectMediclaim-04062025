var tbloutFlowReport, tblinFlowReport;
var currentDate, month, employer, employeeID;
var activeTab = $('ul.nav li a.active').attr('id');
var inoutflow = $('#custom-tabs-three-ioflow');
var outflow = $('#custom-tabs-three-pensionoutflow');
var inflow = $('#custom-tabs-three-pensioninflow');


$('.nav > li > a').on("click", function (e) {

    var n = $(e.target).attr('id');
    BindData(month, employer, employeeID, n);
    if (n == "custom-tabs-three-pensionoutflow-tab") {
        outflow.show();
        inflow.hide();
        inoutflow.hide();
    }
    else if (n == "custom-tabs-three-pensioninflow-tab") {
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

$('#btnPrint').click(function (event) {
    
    var n = $('ul.nav li a.active').attr('id');
    if (n == "custom-tabs-three-pensioninflow-tab" || n == "custom-tabs-three-pensionoutflow-tab")
        downloadPDF();
    else
        downloadPDF1();


});

//$('#btnPrint').click(function (event) {
//    // get size of report page
//    var reportPageHeight = $('#reportPage').innerHeight();
//    var reportPageWidth = $('#reportPage').innerWidth();

//    // create a new canvas object that we will populate with all other canvas objects
//    var pdfCanvas = $('<canvas />').attr({
//        id: "canvaspdf",
//        width: reportPageWidth,
//        height: reportPageHeight
//    });

//    

//    // keep track canvas position
//    var pdfctx = $(pdfCanvas)[0].getContext('2d');
//    var pdfctxX = 0;
//    var pdfctxY = 0;
//    var buffer = 100;

//    // for each chart.js chart
//    $("myChart").each(function (index) {

//        
//        // get the chart height/width
//        var canvasHeight = $(this).innerHeight();
//        var canvasWidth = $(this).innerWidth();

//        // draw the chart into the new canvas
//        pdfctx.drawImage($(this)[0], pdfctxX, pdfctxY, canvasWidth, canvasHeight);
//        pdfctxX += canvasWidth + buffer;

//        // our report page is in a grid pattern so replicate that in the new canvas
//        if (index % 2 === 1) {
//            pdfctxX = 0;
//            pdfctxY += canvasHeight + buffer;
//        }
//    });

//    // create new pdf and add our new canvas as an image
//    var pdf = new jsPDF('l', 'pt', [reportPageWidth, reportPageHeight]);
//    pdf.addImage($(pdfCanvas)[0], 'PNG', 0, 0);

//    // download the pdf
//    pdf.save('report.pdf');
//});

//add event listener to button
/*document.getElementById('btnPrint').addEventListener("click", downloadPDF);*/



//donwload pdf from original canvas
function downloadPDF() {    
    var canvasImg = document.querySelector('#myChart').toDataURL("image/png", 1.0);
    
    //creates PDF from img
    var doc = new jsPDF('landscape');
    doc.setFontSize(20);
     
    doc.addImage(canvasImg, 'png', 10, 10, 280, 150);
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
    $("#custom-tabs-three-pensioninflow-tab").trigger('click');

});

function BindData(month, employer, employeeID, flow) {
    $.ajax({
        url: "/FinanceManagement/GetOutflowReport",
        type: "POST",
        datatype: "json",
        data: { month: month, DTLOffice: employer, EmployeeID: employeeID, Flow: flow },
        success: function (data) {
            if (data) {
                if (flow == "custom-tabs-three-pensionoutflow-tab") {
                    outFlowTable(data._reports);

                    redraw(data.employers, data.temp2, flow);
                }
                else if (flow == "custom-tabs-three-pensioninflow-tab") {
                    inflowTable(data._reports);
                    redraw(data.employers, data.temp2, flow);
                }
                else {

                    $("#myChart").remove();
                    LoadInChart(data.employers, data.contributions);
                    LoadOutChart(data.employers, data.pensionAmounts);
                    LoadInOutChart(data.employers, data.contributions, data.pensionAmounts);
                }

                if (flow == "custom-tabs-three-pensionoutflow-tab")
                    setTimeout(function () {
                        tbloutFlowReport.columns.adjust().draw();
                    }, 100, tbloutFlowReport)
                else if (flow == "custom-tabs-three-pensioninflow-tab")
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

    $('#pensionRptTbl1').show();

    var tempHtml1 = ""
    var lastTotalHtml = ""
    for (var i = 0; i < data.length; i++) {

        if (data[i].employeeName != "Total") {
            tempHtml1 += "<tr>" +
                "<td>" + data[i].month + "</td>" +
                "<td>" + data[i].employeeName + "</td>" +
                "<td>" + data[i].employeeID + "</td>" +
                "<td>" + data[i].contribution + "</td>" +
                "<td>" + data[i].employer + "</td>" +
                "</tr> ";
        } else {
            lastTotalHtml = "<tr>" +
                "<td>" + data[i].month + "</td>" +
                "<td>" + data[i].employeeName + "</td>" +
                "<td>" + data[i].employeeID + "</td>" +
                "<td>" + data[i].contribution + "</td>" +
                "<td>" + data[i].employer + "</td>" +
                "</tr> ";
        }
    }
    tempHtml1 = tempHtml1 + lastTotalHtml;
    if ($.fn.DataTable.isDataTable('#pensionRptTbl1')) {
        tblinFlowReport.destroy();
    }

    $('#pensionRptTbl').hide();
    $("#pensionRptTbl1 tbody").html(tempHtml1);
    tblinFlowReport = $('#pensionRptTbl1').DataTable({
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
    $('#pensionRptTbl').show();

    $('#pensionRptTbl1').hide();

    for (var i = 0; i < data.length; i++) {
        if (data[i].employeeName != "Total") {
            tempHTML += "<tr>" +
                "<td>" + data[i].month + "</td>" +
                "<td>" + data[i].employeeName + "</td>" +
                "<td>" + data[i].employeeID + "</td>" +
                "<td>" + data[i].pensionAmount + "</td>" +
                "<td>" + data[i].employer + "</td>" +
                "</tr> ";
        } else {
            lastTotalHtml = "<tr>" +
                "<td>" + data[i].month + "</td>" +
                "<td>" + data[i].employeeName + "</td>" +
                "<td>" + data[i].employeeID + "</td>" +
                "<td>" + data[i].pensionAmount + "</td>" +
                "<td>" + data[i].employer + "</td>" +
                "</tr> ";
        }
    }
    tempHTML = tempHTML + lastTotalHtml;
    if ($.fn.DataTable.isDataTable('#pensionRptTbl')) {
        tbloutFlowReport.destroy();
    }

    $("#pensionRptTbl tbody").html(tempHTML);
    tbloutFlowReport = $('#pensionRptTbl').DataTable({
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

function LoadChart(temp1, temp2, flow) {

    var dat;
    if (flow == "custom-tabs-three-pensioninflow-tab") {
        dat = "Pension Management Inflow";
    }
    else if (flow == "custom-tabs-three-pensionoutflow-tab") {
        dat = "Pension Management Outflow";
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
            export: true,
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
        },
        export: true
    });

}

function LoadOutChart(Employers, PensionAmounts) {

    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    var ctx = document.getElementById("myoutFlowChart");
    var myoutFlowChart = new Chart(ctx, {
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
                        size: 18,
                    }
                },
            },
            export: true
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
            export: true,
            scales: {

            },

        }
    });

}
