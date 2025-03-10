var tblDisbusrsement;
var currentDate, year, month, employer, employeeID;


function searchData() {
    currentDate = new Date();
    year = ($("#searchByYear").val() == '' ? getYear() : $("#searchByYear").val());
    month = ($("#searchByMonth").val() == '' ? (currentDate.getMonth() + 1) : $("#searchByMonth").val());
    employer = $("#searchByEmployer").val();
    employeeID = $("#searchByEmployeeId").val();
}

$(document).ready(function () {
    searchData();
    $('#searchByMonth').val(month);
    BindData(year, month, employer, employeeID);
    
});



function BindData(year, month, employer, employeeID) {
    $.ajax({
        url: "/Disbursement/GetDisbursementReport",
        type: "POST",
        datatype: "json",
        data: { year: year, month: month, DTLOffice: employer, EmployeeID: employeeID },
        success: function (data) {
            if (data) {

                var tempHTML = ""
                for (var i = 0; i < data.length; i++) {
                    tempHTML += "<tr>" +
                        "<td>" + data[i].ppoNo + "</td>" +
                        "<td>" + data[i].employeeID + "</td>" +
                        "<td>" + data[i].employeeName + "</td>" +
                        "<td>" + data[i].employerName + "</td>" +
                        "<td>" + data[i].bank + "</td>" +
                        "<td>" + data[i].account + "</td>" +
                        "<td>" + data[i].pension + "</td>" +
                        "<td>" + data[i].tdsAmount + "</td>" +
                        "<td>" + data[i].recoveryAmount + "</td>" +
                        "<td>" + data[i].allowanceAmount + "</td>" +
                        "<td>" + data[i].aqpAmount + "</td>" +
                        "<td>" + data[i].admissiblePension + "</td>" +
                        "<td>" + data[i].month + "</td>" +
                        "</tr> ";
                }

                if ($.fn.DataTable.isDataTable('#disbusrsementRptTbl')) {
                    tblDisbusrsement.destroy();
                }

                $("#disbusrsementRptTbl tbody").html(tempHTML);
                tblDisbusrsement = $('#disbusrsementRptTbl').DataTable({
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
                setTimeout(function () {
                    tblDisbusrsement.columns.adjust().draw();
                }, 100, tblDisbusrsement)
            }
        },
        error: function (data) {

        }
    });
}


$(function () {
    $("#searchByMonth").change("option", function () {
        searchData();
        BindData(year, month, employer, employeeID);
    });
});
$(function () {
    $("#searchByEmployer").change("option", function () {
        searchData();
        BindData(year, month, employer, employeeID);
    });
});
$(function () {
    $("#searchByEmployeeId").change("option", function () {
        searchData();
        BindData(year, month, employer, employeeID);
    });
});


