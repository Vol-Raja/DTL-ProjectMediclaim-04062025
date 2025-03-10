$(document).ready(function () {
    var paymentid = ''
    var applicationid = '';
    var nameofemployee = '';
    var contactno = '';
    var loantype = '';
    var noofemi = ''
    var apporvedamount = '';
    var accountnumber = '';
    var ifsccode = '';
    var bic = ''

    var PopulateProperties = function () {
        applicationid = $('#txtApplicationId').val();
        nameofemployee =  $('#txtNameOfEmployee').val();
        contactno = $('#txtContactNo').val();
        loantype = $('#selLoanType').val();
        noofemi = $('#txtNoOfEMI').val();
        apporvedamount = $('#txtApprovedAmount').val();
        accountnumber = $('#txtAccountNumber').val();
        ifsccode = $('#txtIFSCCode').val();
        bic = $('#txtBICCode').val();
    }

    $('#txtApplicationId').blur(function () {
        LoadPaymentDetail();
    });

    $('#btnSubmit').click(function () {
        PopulateProperties();        var url = '/GPF/GeneratePayment/Create/';
        var request = {
            ApplicationId: applicationid,
            NameOfEmployee: nameofemployee,
            ContactNo: contactno,
            LoanType: loantype,
            NoOfEMI: noofemi,
            ApprovedAmount: apporvedamount,
            AccountNumber: accountnumber,
            IFSCCode: ifsccode,
            BICCode: bic
        };
        $.ajax({
            type: 'POST',
            url: url,
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    hideLoader();
                    //toastrSuccess("Payment Generated");
                    $('#confirmationModal').modal('show');
                }
                //setTimeout(function () {
                //    window.location.href = '/UserManagement//AdminUserList';
                //}, 1800);
            },
            error: function (xhr, status, error) {
                hideLoader();
                toastrError("Something went wrong!!!")
            }
        });
    });

    var LoadPaymentDetail = function () {
        showLoader();
        var applicationId = $('#txtApplicationId').val();
        $.ajax({
            type: 'GET',
            url: "/GPF/GeneratePayment/Load/ApplicationDetail/" + applicationId,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                console.log(response);
                if (xhr.status == '200') {
                    if (response != null || response != undefined) {
                        $('#txtNameOfEmployee').val(response.accountHolderName);
                        $('#txtContactNo').val(response.mobileNumber);
                        $('#selLoanType').val(response.withdrawType);
                        $('#txtNoOfEMI').val(response.noOfEMI);
                        $('#txtApprovedAmount').val(response.approvedAmount);
                        $('#txtAccountNumber').val(response.bankAccountNo);
                        $('#txtIFSCCode').val(response.ifscNo);
                        $('#txtBICCode').val(response.bc);
                        hideLoader();
                    }
                    else {
                        hideLoader();
                    }
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
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
});


function DeletePayment(Id, rw) {
    if (confirm("Are you sure you want to delete?")) {
        showLoader();
        $.ajax('/GPF/GeneratePayment/Delete/' + Id, {
            type: 'DELETE',  // http method
            data: '',  // data to submit
            success: function (data, status, xhr) {
                hideLoader();
                if (xhr.status == "200") {
                    alert("Record deleted successfully!")
                    /*var href = $('#adel').attr('href');

                    window.location.href = href;*/
                    $(rw).parents("tr").remove();
                }
                hideLoader();
            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('p').append('Error' + errorMessage);
            }
        });
    }
    else {
        hideLoader();
        alert("Canceled Data Deletion...!!!");
    }
}

function showLoader() {
    //$('.preloader').css()
    $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
    $(".preloader img").css({ "display": "block" });
};

function hideLoader() {
    $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
    $(".preloader img").css({ "display": "none" });
};