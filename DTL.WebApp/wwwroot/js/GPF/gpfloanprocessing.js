$(document).ready(function () {

    var _create = false;
    var _edit = false;
    var _view = false;
    var _delete = false;

    $('.numberOnly').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });

    $('#btnCancel').click(function () {
        $('input[type=text], select').val('');
    })

    $('#btnSearch').click(function () {
        var _date = new Date($('#txtDate').val());
        //var employer = $('#txtemployer').val();//$("#selEmployer option:selected").val();
        //var processingDate = $('#selMonth option:selected').val() + " - " + $('#txtYear').val()//$('#txtProcessingDate').val();
        var employer = $("#selEmployer").val();
        var processingDate = _date.getMonth() + " - " + _date.getFullYear()//$('#txtProcessingDate').val();
        var employeeId = $('#txtEmployeeId').val();
        var applicationNumber = $('#txtApplicationNo').val()
        var statusId = $('#selStatus').val();

        processingDate = (processingDate === undefined || processingDate === "") ? "1/1/0001" : processingDate;
        employer = (employer === undefined || employer === '') ? 'NA' : employer;
        employeeId = (employeeId === undefined || employeeId === '') ? 0 : employeeId;
        applicationNumber = (applicationNumber === undefined || applicationNumber === '') ? 'NA' : applicationNumber;
        statusId = (statusId === undefined || statusId === '') ? 0 : statusId;


        if (processingDate !== '1/1/0001' || employer !== '' || employeeId !== '' || applicationNumber !== '' || statusId !== '') {
            //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
            SearchGPFWithdrawal(processingDate, employer, employeeId, applicationNumber, statusId);

        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    var SearchGPFWithdrawal = function (processingDate, employer, employeeId, applicationNumber, statusId) {
        showLoader();
        $.ajax({
            type: 'GET',
            url: "/GPF/LoanProcessing/GetGPFWithdrawal/" + encodeURIComponent(processingDate) + "/" + employer + "/" + employeeId + "/" + statusId + "/" + applicationNumber,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblLoanProcessing tbody').empty();             
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        debugger;
                        $.each(response, function (index, item) {
                            debugger;
                            index = index + 1
                            var _status = '';
                            switch (item.applicationStatus) { case 1: _status = 'Pending'; break; case 2: _status = 'Approved'; break; case 3: _status = 'Reject'; break; }
                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + item.accountHolderName + '</td><td>'
                                + item.employer + '</td><td>'
                                + item.employeId + '</td><td>'
                                + item.uniqueNumber + '</td><td>'
                                + item.dateOfApplicationString + '</td><td>'
                                + _status + '</td><td>'
                                + '<input type="checkbox" id="chkPhysicalSubmit' + item.withdrawId + '" onclick="UpdatePhysicalSubmit(' + item.withdrawId + ')">' + '</td><td class="d-flex justify-content-around">'
                                + ((_view == true) ? '<a href="/GPF/LoanProcessing/View/' + item.withdrawId + '"><i class="far fa-eye" ></i ></a > ' + '</td ></tr>' : '</td ></tr>')

                            $('#tblLoanProcessing tbody').append(row);

                            //to show the check mark on checkbox
                            var chkid = '#chkPhysicalSubmit' + item.withdrawId;
                            $(chkid).prop("checked", item.physicalSubmit);
                        });
                        ResetSearchFields();
                    }
                    else {
                        var row = '<tr><td colspan="9">No data found</td></tr>';
                        $('#tblLoanProcessing tbody').append(row);
                    }
                    hideLoader();
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    }

    var LoadPendingGPFWithdrawals = function () {
        $.ajax({
            type: 'GET',
            url: "/GPF/LoanProcessing/GetPendingGPFWithdrawals",
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblLoanProcessing tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        console.log(response);
                        $.each(response, function (index, item) {
                            index = index + 1
                            var _status = '';
                            switch (item.applicationStatus) { case 1: _status = 'Pending'; break; case 2: _status = 'Approved'; break; case 3: _status = 'Rejected'; break; case 4: _status = 'Pending'; break; }
                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + item.accountHolderName + '</td><td>'
                                + item.employer + '</td><td>'
                                + item.employeId + '</td><td>'
                                + item.uniqueNumber + '</td><td>'
                                + item.dateOfApplicationString + '</td><td>'
                                + _status + '</td><td>'
                                + '<input type="checkbox" id="chkPhysicalSubmit' + item.withdrawId + '" onclick="UpdatePhysicalSubmit(' + item.withdrawId + ')">' + '</td><td class="d-flex justify-content-around">'
                                + ((_view == true) ? '<a href="/GPF/LoanProcessing/View/' + item.withdrawId + '"><i class="far fa-eye" ></i ></a > ' + '</td ></tr>' : '</td ></tr>')

                            $('#tblLoanProcessing tbody').append(row);

                            //to show the check mark on checkbox
                            var chkid = '#chkPhysicalSubmit' + item.withdrawId;
                            $(chkid).prop("checked", item.physicalSubmit);
                        });
                    }
                    else {
                        var row = '<tr><td colspan="9">No data found</td></tr>';
                        $('#tblLoanProcessing tbody').append(row);
                    }
                    //console.log(response);
                }
            },
            error: function (xhr, status, error) {

            }
        });
    };

    var ResetSearchFields = function () {
        $("#selEmployer").prop("selectedIndex", 0).val(); 
        $('#txtApplicationNo').val('');
        $("#selStatus").prop("selectedIndex", 0).val();
        $('#txtDate').val('');
        $('#txtEmployeeId').val('');
        $('#postedFile').val('');
    };

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

        var __create = $('#hdnCreate').val();
        var __edit = $('#hdEdit').val();
        var __delete = $('#hdnDelete').val();
        var __view = $('#hdnView').val();

        if (__create === 'true') { _create = true; }
        if (__edit === 'true') { _edit = true; }
        if (__delete === 'true') { _delete = true; }
        if (__view === 'true') { _view = true; }


        LoadPendingGPFWithdrawals();
    })();
});

function UpdatePhysicalSubmit(id) {
    var chkid = '#chkPhysicalSubmit' + id;
    $(chkid).attr("disabled", true);
    var request = {
        WithdrawId: id,
        PhysicalSubmit: $(chkid).is(":checked"),
        ModifiedBy: null
    }
    $.ajax({
        type: 'POST',
        url: "/GPF/LoanProcessing/PhysicalSubmit",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {
                if (response == "Success") {

                    $(chkid).removeAttr("disabled");

                    //setTimeout(function () {
                    //    window.location.href = "../NonCashlessClaims";
                    //}, 2000);
                }
                else {
                }
            }
        },
        error: function (xhr, status, error) {

        }
    });
}