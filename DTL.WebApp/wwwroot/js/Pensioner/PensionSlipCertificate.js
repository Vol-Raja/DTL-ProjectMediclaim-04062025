$(document).ready(function () {

    $('#btnCancel').click(function () {
        $('input[type=text], select').val('');
    })

    $('#btnSearch').click(function () {
     
        var month = $('#selMonth option:selected').val();//$('#txtProcessingDate').val();
        var year = $('#txtYear').val();     
        var ppono = $('#txtPPONo').val();

        month = (month === undefined || month === '') ? 0 : month;
        year = (year === undefined || year === '') ? 0 : year;
        ppono = (ppono === undefined || ppono === '') ? 0 : ppono;


        if (year !== '' || month !== '' || ppono !== '') {
            //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
            GetPensionSlipCetificate(month, year, ppono);
        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    var GetPensionSlipCetificate = function (month, year, ppono) {
        showLoader();
        $.ajax({
            type: 'GET',
            url: "/Pensioner/GetPensionSlipCertificateByParam/" + month + "/" + year + "/" + ppono,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblPensionSlipCertificate tbody').empty();
                $('#tblPensionSlipCertificate tfoot').empty();
                if (response.length > 0) {
                    var totalPensionAmount = 0;
                    //var totalInterest = 0;
                    //var totalGpfAmount = 0;
                    $.each(response, function (index, item) {

                        index = index + 1;
                        totalPensionAmount = totalPensionAmount + item.admissiablePension;
                        //totalContribution = totalContribution + item.memberContribution;
                        //totalInterest = totalInterest + item.memberInterest;
                        //totalGpfAmount = totalGpfAmount = item.gpfAmount;

                        var row = '<tr><td>'
                            + index + '</td><td>'
                            + item.ppoOrderId + '</td><td>'
                            + item.employeeName + '</td><td>'
                            + item.dtlOffice + '</td><td>'
                            + item.admissibleForFromDateMMYYYY + '</td><td>'
                            + item.admissiablePension + '</td></tr>'

                        $('#tblPensionSlipCertificate tbody').append(row);
                    });

                    /*
                    var row = '<tr><td colspan="5" class="text-right text-bold">Total Amount:</td><td class="text-bold">'
                        + totalPensionAmount + '</td></tr>';
                   
                    $('#tblPensionSlipCertificate tfoot').append(row);
                    */
                }
                else {
                    var row = '<tr><td colspan="6">No data found</td></tr>';
                    $('#tblPensionSlipCertificate tbody').append(row);
                }

                $("#tblPensionSlipCertificate").DataTable({
                    ordering: true,
                    dom: 'Blfrtip',
                    buttons: [
                        
                        {
                            extend: 'excelHtml5',
                            title: 'Generate Pension'
                        },
                        {
                            extend: 'csvHtml5',
                            title: 'Generate Pension'
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'Generate Pension'
                        }
                    ],
                });

                hideLoader();
            },
            error: function (xhr, status, error) {

            }
        });
    }

    var MonthName = function (numericMonth) {
        switch (numericMonth) {
            case 1: return 'January';
            case 2: return 'February';
            case 3: return 'March';
            case 4: return 'April';
            case 5: return 'May';
            case 6: return 'June';
            case 7: return 'July';
            case 8: return 'August';
            case 9: return 'September';
            case 10: return 'October';
            case 11: return 'November';
            case 12: return 'December';
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

    //Self Invoking function to populate the table when page loads
    (function () {
        GetPensionSlipCetificate(0, 0, 0);
    })();
});