$(document).ready(function () {

    $('#btnCancel').click(function () {
        $('input[type=text], select').val('');
    })

    $('#btnSearch').click(function () {
        //debugger;
        var employer = $("#selEmployer option:selected").val();
        var month = $('#selMonth option:selected').val();//$('#txtProcessingDate').val();
        var year = $('#txtYear').val();
        var bank = $('#txtBank').val();
        var ppono = $('#txtPPONo').val();

        employer = (employer === undefined || employer === '') ? 'NA' : employer;
        month = (month === undefined || month === '') ? 0 : month;
        year = (year === undefined || year === '') ? 0 : year;
        bank = (bank === undefined || bank === '') ? 'NA' : bank;
        ppono = (ppono === undefined || ppono === '') ? 0 : ppono;  


        if (year !== '' || month !== '' || bank !== '' || employer !== '' || ppono !== '') {
            //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
            GetGeneratePension(month, year, bank, employer, ppono);
        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    var GetGeneratePension = function (month, year, bank, employer, ppono) {
        showLoader();
        
        $.ajax({
            type: 'GET',
            url: "/Pensioner/GetGeneratePensionByParam/" + month + "/" + year + "/" + bank + "/" + employer + "/" + ppono,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            
            success: function (response, status, xhr) {
                debugger;
                $('#tblGeneratePension tbody').empty();
                if (response.length > 0) {
                    //var totalContribution = 0;
                    //var totalInterest = 0;
                    //var totalGpfAmount = 0;
                    $.each(response, function (index, item) {

                        index = index + 1;

                        //totalContribution = totalContribution + item.memberContribution;
                        //totalInterest = totalInterest + item.memberInterest;
                        //totalGpfAmount = totalGpfAmount = item.gpfAmount;

                        var row = '<tr><td>'
                            + index + '</td><td>'
                            + item.ppoOrderId + '</td><td>'
                            + item.employeeName + '</td><td>'
                            + item.dtlOffice + '</td><td>'
                            + item.admissiablePension + '</td><td>'
                            + item.admissibleForFromDateMMYYYY + '</td><td>'
                            + item.bankAccountName + '</td><td>'
                            + item.bankAccountNumber + '</td></tr>'

                        $('#tblGeneratePension tbody').append(row);
                    });

                    /*
                    var row = '<tr><td colspan="3" class="text-right text-bold">Total</td><td>'
                        + totalContribution + '</td><td>'
                        + totalInterest + '</td><td>'
                        + totalGpfAmount + '</td></tr>';
                    */
                    $('#tblGeneratePension tbody').append(row);
                    //$('#tblGeneratePension').DataTable().rows().invalidate();

                }
                else {
                    var row = '<tr><td colspan="8">No data found</td></tr>';
                    $('#tblGeneratePension tbody').append(row);
                }

                /*
                $("#tblGeneratePension").DataTable({
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
                */

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
        GetGeneratePension(0, 0, 'NA', 'NA', 0);
    })();
});