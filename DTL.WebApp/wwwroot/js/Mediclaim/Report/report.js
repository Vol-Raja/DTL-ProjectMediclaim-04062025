$(document).ready(function () {
    var _claimId = 0
    var _claimStatus = '';
    var _claimType = 'Cashless';
    var _fromDate = null;
    var _toDate = null;
    var _url = '';

    var tbl = $("#tblReport").DataTable({
        ordering: true,
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Report'
            },
            {
                extend: 'pdfHtml5',
                title: 'Report'
            }
        ],
    });

    $('#btnExportExcel').click(function () {
        $("#tblReport").table2excel({
            // exclude CSS class
            exclude: ".noExl",
            name: "Claims",
            filename: "Claims", //do not include extension
            fileext: ".xls" // file extension
        });
    });

    $('#btnSearch').click(function () {
        //debugger;
        _claimStatus = $('#hdnClaimStatus').val();

        if ($('#selClaimType option:selected').val() !== 'Select') {
            _claimType = $('#selClaimType option:selected').val();
            _url = '/Mediclaim/Report/' + _claimType + '/' + _claimStatus;
        }
        else {
            alert('Please select the Claim type');
            return;
        }

        if ($('#txtClaimNumber').val() !== '') {
            _claimId = $('#txtClaimNumber').val();
            _url = '/Mediclaim/Report/' + _claimType + '/' + _claimStatus + '/' + _claimId;
        }

        if ($('#txtFromDate').val() !== '' && $('#txtToDate').val() !== '') {

            _fromDate = new Date($('#txtFromDate').val());
            _toDate = new Date($('#txtToDate').val());
            var Difference_In_Time = _toDate.getTime() - _fromDate.getTime();
            var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
            if (Difference_In_Days < 0) {
                alert('Incorrect date range');
            }
            else {
                _url = '/Mediclaim/Report/' + _claimType + '/' + _claimStatus + '/' + $('#txtFromDate').val() + '/' + $('#txtToDate').val();
            }
        }

        GetMedicalimReportDetail(_url);
    });

    $('#btnCancel').click(function () {
        $('input[type="text"]').val('');
        $("select").prop("selectedIndex", 0).val();
    });

    var GetMedicalimReportDetail = function (url) {
        $.ajax({
            type: 'GET',
            url: url,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblReport tbody').empty();

                if (response.length > 0) {
                    $.each(response, function (index, item) {
                        index = index + 1;

                        debugger;
                        var row;
                        var viewlink = '';
                        if (_claimType === 'Cashless') {
                            viewlink = '<a href="/Mediclaim/Report/CashlessPreview/' + item.claimId + '"><i class="far fa-eye"></i></a>';
                        }
                        else {
                            viewlink = '<a href="/Mediclaim/Report/NonCashlessPreview/' + item.claimId + '"><i class=" far fa-eye"></i></a>';
                        }

                        switch ($('#hdnClaimStatus').val()) {
                            case "Processed":
                                row = '<tr><td>'
                                    + index + '</td><td>'
                                    + item.claimId + '</td><td>'
                                    + item.medicalSectionPageNumber + '</td><td>'
                                    + item.patientName + '</td><td>'
                                    + item.applicationType + '</td><td>'
                                    + item.createdDateString + '</td><td>'
                                    + item.approvedAmount + '</td><td>'
                                    + viewlink
                                    + '</td ></tr>';
                                break;
                            case "Rejected":
                                row = '<tr><td>'
                                    + index + '</td><td>'
                                    + item.claimId + '</td><td>'
                                    + item.medicalSectionPageNumber + '</td><td>'
                                    + item.patientName + '</td><td>'
                                    + item.applicationType + '</td><td>'
                                    + item.createdDateString + '</td><td>'
                                    + item.rejectReason + '</td><td>'
                                    + viewlink
                                    + '</td ></tr>';
                                break;
                            case "Pending":
                                row = '<tr><td>'
                                    + index + '</td><td>'
                                    + item.claimId + '</td><td>'
                                    + item.medicalSectionPageNumber + '</td><td>'
                                    + item.patientName + '</td><td>'
                                    + item.applicationType + '</td><td>'
                                    + item.createdDateString + '</td><td>'
                                    + '<a href="#"><i class="far fa-eye" ></i ></a >'
                                    + '</td ></tr>';
                                break;
                            default:
                            // code block
                        };
                        
                        $('#tblReport tbody').append(row);
                    });
                }
                else {
                    row = '<tr><td colspan="8">No data found</td></tr>';
                    $('#tblReport tbody').append(row);
                }

                
            },
            error: function (xhr, status, error) {

            }
        });
    };

    (function () {
        //$.noConflict();
        var __url = '/Mediclaim/Report/Cashless/' + $('#hdnClaimStatus').val();
        GetMedicalimReportDetail(__url);

    })();
});
