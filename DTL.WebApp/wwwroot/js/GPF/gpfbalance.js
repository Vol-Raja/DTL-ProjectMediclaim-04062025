$(document).ready(function () {

    $('#btnSearchGPFSummary').click(function () {
        var employer = $("#selEmployer option:selected").val();
        var employeeName = $('#txtEmployeeName').val();
        var employeeId = $('#txtEmployeeId').val();

        employeeName = (employeeName === undefined || employeeName === "") ? "NA" : employeeName;
        employer = (employer === undefined || employer === '') ? 'NA' : employer;
        employeeId = (employeeId === undefined || employeeId === '') ? 0 : employeeId;


        if (employeeName !== '' || employer !== '' || employeeId !== '') {
            //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
            SearchGPFProcessing(employeeName, employer, employeeId);

        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    var SearchGPFProcessing = function (employeeName, employer, employeeId) {
        showLoader();
        $.ajax({
            type: 'GET',
            url: "/GPF/GPFBalance/GetGPFBalance/" + employeeName + "/" + employer + "/" + employeeId,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblGPFBalance tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        debugger;
                        var totalContribution = 0;
                        var totalInterest = 0;
                        var totalGpfAmount = 0;
                        $.each(response, function (index, item) {

                            //totalContribution = totalContribution + item.memberContribution;
                            //totalInterest = totalInterest + item.memberInterest;
                            //totalGpfAmount = totalGpfAmount = item.gpfAmount;

                            var row = '<tr><td>'
                                + MonthName(item.month) + '-' + item.year + '</td><td>'
                                + item.memberContribution + '</td><td>'
                                + item.memberInterest + '</td><td>'
                                + item.gpfAmount + '</td></tr>'

                            $('#tblGPFBalance tbody').append(row);
                        });

                        //var row = '<tr><td colspan="4" class="text-right text-bold">Total</td><td>'
                        //    + totalContribution + '</td><td>'
                        //    + totalInterest + '</td><td>'
                        //    + totalGpfAmount + '</td></tr>';

                        $('#tblGPFBalance tbody').append(row);
                    }
                    else {
                        var row = '<tr><td colspan="4">No data found</td></tr>';
                        $('#tblGPFBalance tbody').append(row);
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


    var showLoader = function () {
        //$('.preloader').css()
        $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
        $(".preloader img").css({ "display": "block" });
    };

    var hideLoader = function () {
        $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
        $(".preloader img").css({ "display": "none" });
    };
})