$(document).ready(function () {

    var gpfDetail = [];

    $('#btnCancel').click(function () {
        $('input[type=text], select').val('');
    })

    $('#btnLoadFile').click(function () {
        LoadFile();
    });

    var LoadFile = function () {
        var form = new FormData();
        var file = $('#postedFile')[0].files[0];
        form.append("File", file);

        if (file != undefined) {
            showLoader();
            var settings = {
                "url": "/GPF/Processing/LoadFile",
                "method": "POST",
                "timeout": 0,
                "processData": false,
                //"mimeType": "multipart/form-data",
                "contentType": false,
                "data": form
            };
            $.ajax(settings).done(function (response, status, xhr) {
                gpfDetail = response;
                $('#tblGPFProcessing tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        var totalContribution = 0;
                        $.each(response, function (index, item) {
                            totalContribution = totalContribution + item.memberContribution;
                            var row = '<tr><td>'
                                + item.employeeNumber + '</td><td>'
                                + item.employeeName + '</td><td>'
                                + item.employer + '</td><td>'
                                + item.designation + '</td><td>'
                                + item.memberContribution + '</td><td>'
                                //+ item.memberInterest + '</td><td>'
                                + item.gpfAmount + '</td><td>'
                                + item.month + '</td><td>'
                                + item.year + '</td></tr>'

                            $('#tblGPFProcessing tbody').append(row);
                        });
                        var totalContributionRow = '<tr><td colspan="4" class="text-right text-bold">Total Member Contribution</td><td colspan="5">'
                            + totalContribution + '/-' + '</td ></tr>';

                        $('#tblGPFProcessing tbody').append(totalContributionRow);
                    }
                    else {
                        var row = '<tr><td colspan="8">No data found</td></tr>';
                        $('#tblProcessingCashlessClaim tbody').append(row);
                    }
                }
                hideLoader();
            });
        }
        else {
            hideLoader();
            alert('Please add the file to load');
        }
    }

    $('#btnSearch').click(function () {
        var employer = $("#selEmployer option:selected").val();
        var processingDate = $('#selMonth option:selected').val() + " - " + $('#txtYear').val()//$('#txtProcessingDate').val();
        var employeeId = $('#txtEmployeeId').val();

        processingDate = (processingDate === undefined || processingDate === "") ? "1/1/0001" : processingDate;
        employer = (employer === undefined || employer === '') ? 'NA' : employer;
        employeeId = (employeeId === undefined || employeeId === '') ? 0 : employeeId;


        if (processingDate !== '1/1/0001' || employer !== '' || employeeId !== '') {
            //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
            SearchGPFProcessing(processingDate, employer, employeeId);

        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    $('#btnSearchGPFSummary').click(function () {
        var employer = $("#selGPFSummaryEmployer option:selected").val();
        employer = (employer === undefined || employer === '') ? 'NA' : employer;
        if (employer !== '') {
            LoadGPFProcessingSummary(employer);
        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    $('#btnCancelGPFSummary').click(function () {
        $("#selGPFSummaryEmployer").prop('selectedIndex', 0);
    });


    var SearchGPFProcessing = function (processingDate, employer, employeeId) {
        showLoader();
        $.ajax({
            type: 'GET',
            url: "/GPF/Processing/GetGPFProcessingDetail/" + encodeURIComponent(processingDate) + "/" + employer + "/" + employeeId,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblGPFReport tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        var totalContribution = 0;
                        var totalInterest = 0;
                        var totalGpfAmount = 0;
                        $.each(response, function (index, item) {

                            totalContribution = totalContribution + item.memberContribution;
                            totalInterest = totalInterest + item.memberInterest;
                            totalGpfAmount = totalGpfAmount = item.gpfAmount;

                            var row = '<tr><td>'
                                + item.employeeType + '</td><td>'
                                + item.employeeName + '</td><td>'
                                + item.employer + '</td><td>'
                                + item.employeeNumber + '</td><td>'
                                + item.memberContribution + '</td><td>'
                                + item.memberInterest + '</td><td>'
                                + item.gpfAmount + '</td><td>'
                                + MonthName(item.month) + '-' + item.year + '</td></tr>'

                            $('#tblGPFReport tbody').append(row);
                        });

                        var row = '<tr><td colspan="4" class="text-right text-bold">Total</td><td>'
                            + totalContribution + '</td><td>'
                            + totalInterest + '</td><td>'
                            + totalGpfAmount + '</td></tr>';

                        $('#tblGPFReport tbody').append(row);
                    }
                    else {
                        var row = '<tr><td colspan="8">No data found</td></tr>';
                        $('#tblGPFReport tbody').append(row);
                    }
                }
                hideLoader();
            },
            error: function (xhr, status, error) {

            }
        });
    }

    var MonthName = function (numericMonth) {
        switch (numericMonth) {
            case 1: return 'January'; break;
            case 2: return 'February'; break;
            case 3: return 'March'; break;
            case 4: return 'April'; break;
            case 5: return 'May'; break;
            case 6: return 'June'; break;
            case 7: return 'July'; break;
            case 8: return 'August'; break;
            case 9: return 'September'; break;
            case 10: return 'October'; break;
            case 11: return 'November'; break;
            case 12: return 'December'; break;
            default: break;
        }
    }

    var LoadGPFProcessing = function () {
        $.ajax({
            type: 'GET',
            url: "/GPF/Processing/GetGPFProcessingDetail",
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblGPFReport tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        var totalContribution = 0;
                        var totalInterest = 0;
                        var totalGpfAmount = 0;
                        $.each(response, function (index, item) {
                            totalContribution = totalContribution + item.memberContribution;
                            totalInterest = totalInterest + item.memberInterest;
                            totalGpfAmount = totalContribution + totalInterest;

                            var gpfRowAmount = item.memberContribution + item.memberInterest;

                            var row = '<tr><td>'
                                + item.employeeType + '</td><td>'
                                + item.employeeName + '</td><td>'
                                + item.employer + '</td><td>'
                                + item.employeeNumber + '</td><td>'
                                + item.memberContribution + '</td><td>'
                                + item.memberInterest + '</td><td>'
                                + gpfRowAmount + '</td><td>'
                                //+ item.gpfAmount + '</td><td>'
                                + MonthName(item.month) + '-' + item.year + '</td></tr>'

                            $('#tblGPFReport tbody').append(row);
                        });

                        var row = '<tr><td colspan="4" class="text-right text-bold">Total</td><td>'
                            + totalContribution + '</td><td>'
                            + totalInterest + '</td><td>'
                            + totalGpfAmount + '</td></tr>';

                        $('#tblGPFReport tfoot').append(row);
                    }
                    else {
                        var row = '<tr><td colspan="8">No data found</td></tr>';
                        $('#tblGPFReport tbody').append(row);
                    }
                    $("#tblGPFReport").DataTable({
                        ordering: false,
                        dom: 'Blfrtip',
                        buttons: [
                            {
                                extend: 'excelHtml5',
                                title: 'GPF Processing'
                            },
                            {
                                extend: 'pdfHtml5',
                                title: 'GPF Processing'
                            }
                        ],
                    });
                }
            },
            error: function (xhr, status, error) {

            }
        });
    }

    var LoadGPFProcessingSummary = function (employer) {
        $.ajax({
            type: 'GET',
            url: "/GPF/Processing/GetGPFProcessingDetail/Summary" + "/" + employer,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblGPFSummary tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {

                        $.each(response, function (index, item) {

                            var row = '<tr><td>'
                                + MonthName(item.month) + '-' + item.year + '</td><td>'
                                + item.memberContribution + '</td><td>'
                                + item.memberInterest + '</td></tr>'

                            $('#tblGPFSummary tbody').append(row);
                        });
                    }
                    else {
                        var row = '<tr><td colspan="3">No data found</td></tr>';
                        $('#tblGPFSummary tbody').append(row);
                    }

                    $("#tblGPFSummary").DataTable({
                        ordering: false,
                        dom: 'Blfrtip',
                        buttons: [
                            {
                                extend: 'excelHtml5',
                                title: 'GPF Summary'
                            },
                            {
                                extend: 'pdfHtml5',
                                title: 'GPF Summary'
                            }
                        ],
                    });
                    //console.log(response);
                }
            },
            error: function (xhr, status, error) {

            }
        });
    }

    var ValidateGPFProcessing = function () {
        var errorMessage = '';
        if ($('#txtProcessingDate').val() === '') {
            errorMessage = 'Please provide processing date \n';
        }
        else {
            errorMessage = '';
        }
        if ($('.employeeType-radio input[type="radio"]:checked').val() === undefined) {
            errorMessage = errorMessage + 'Please select employee type \n';
        }
        else {
            errorMessage = '';
        }

        if (errorMessage !== '') {
            alert(errorMessage);
            return false;
        }
        else {
            return true;
        }
    }

    $('#btnSubmitGPF').click(function () {
        if (gpfDetail.length > 0) {
            if (ValidateGPFProcessing()) {
                var request = {
                    EmployerName: $('#selEmployer option:selected').val(),
                    ProcessingDate: $('#txtProcessingDate').val(),
                    Interest: $('#lblInterest').text(),
                    EmployeeType: $('.employeeType-radio input[type="radio"]:checked').val(),
                    gpfProcessingList: gpfDetail
                }
                $.ajax({
                    type: 'POST',
                    url: "/GPF/Processing/SaveGPFProcessing",
                    data: JSON.stringify(request),
                    contentType: 'application/json; charset=utf-8',
                    //dataType: 'json',
                    success: function (response, status, xhr) {
                        if (xhr.status == '200') {
                            $('#tblGPFProcessing tbody').empty();
                            $('#monthly_gpf_submit').modal('hide');
                        }
                    },
                    error: function (xhr, status, error) {

                    }
                });
            }
        }
        else {
            alert('Please upload and load the data from excel file');
        }
    });

    $('#btnCancel').click(function () {
        ResetGPFProcessing();
    });

    $('#btnChangeInterest').click(function () {
        if ($('#txtInterest').val() !== '') {
            $('#lblInterest').text($('#txtInterest').val());
        }
    });

    $('#btnGPFReportPrint').click(function () {
        debugger;
        var uri = '';
        if ($('#selGPFSummaryEmployer option:selected').val() !== '') {
            uri = '/GPF/Processing/ExportGPFReportToPDF/' + $('#selGPFSummaryEmployer option:selected').val();
        }
        else {
            uri = '/GPF/Processing/ExportGPFReportToPDF';
        }

        //window.open(window.location.href = uri, '_blank');

        showLoader();
        var settings = {
            "url": uri,
            "method": "GET",
            "timeout": 0,
            "processData": false,
            //"mimeType": "multipart/form-data",
            "contentType": false,
        };
        $.ajax(settings).done(function (response, status, xhr) {
            hideLoader();
            if (xhr.status == '200') {
                if (response != '') { }
                window.open(window.location.href = response, '_blank');
            }
        });

    });

    $('#btnGPFSummaryPrint').click(function () {
        debugger;
        var uri = '';
        if ($('#selGPFSummaryEmployer option:selected').val() !== '') {
            uri = '/GPF/Processing/ExportGPFSummaryToPDF/' + $('#selGPFSummaryEmployer option:selected').val();
        }
        else {
            uri = '/GPF/Processing/ExportGPFSummaryToPDF';
        }

        //window.open(window.location.href = uri, '_blank');

        showLoader();
        var settings = {
            "url": uri,
            "method": "GET",
            "timeout": 0,
            "processData": false,
            //"mimeType": "multipart/form-data",
            "contentType": false,
        };
        $.ajax(settings).done(function (response, status, xhr) {
            hideLoader();
            if (xhr.status == '200') {
                if (response != '') { }
                window.open(window.location.href = response, '_blank');
            }
        });

    });

    var showLoader = function () {
        //$('.preloader').css()
        $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
        $(".preloader img").css({ "display": "block" });
    };

    var hideLoader = function () {
        $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
        $(".preloader img").css({ "display": "none" });
    };

    var ResetGPFProcessing = function () {
        $("#selEmployer").prop("selectedIndex", 0).val();
        $('#txtProcessingDate').val('');
        $('.employeeType-radio').prop('checked', false);
        $('#postedFile').val('');
    };




    //Self Invoking function to populate the table when page loads
    (function () {
        LoadGPFProcessing();
        LoadGPFProcessingSummary('');
    })();
});