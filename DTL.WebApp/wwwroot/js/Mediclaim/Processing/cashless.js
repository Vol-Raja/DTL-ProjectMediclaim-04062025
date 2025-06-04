$(document).ready(function () {
    //debugger;
    let designation = ''
    var LoadPendingMediclaims = function () {
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Processing/GetCashlessClaims",
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
          
                var table = $('#tblProcessingCashlessClaim').DataTable();
                table.destroy();
                $('#tblProcessingCashlessClaim tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) { 
                        $.each(response, function (index, item) {
                            index = index + 1
                            var claimStatus = '';
                            let remark = '';
                            let voucherbutton = (designation.toLowerCase() === 'da1' || designation.toLowerCase() === 'da2') ? '<a href="/Mediclaim/Voucher/AddNewVoucher/cashless" class="Generate_voucher_btn" title ="Voucher generate">Voucher Generate</a>' : '&nbsp';
                            switch (item.claimStatusId) { case 1: claimStatus = '<span class="bg-warning Status_text">Pending</span>'; voucherbutton = ''; break; case 2: claimStatus = '<span class="bg-success Status_text">Approved</span>'; remark = item.remark; break; case 3: claimStatus = '<span class="bg-danger Status_text">Rejected</span>'; remark = item.rejectReason; voucherbutton = ''; break; }
                            let _date = item.createdDateString.split('-')

                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + _date[2] + '/' + _date[1] + '/' + _date[0] + '</td><td>'
                                + item.claimId + '</td><td>'
                                + item.medicalCardNumber + '</td><td>'
                                + item.nameOfHospital + '</td><td>'                                
                                + item.cardCategory + '</td><td>'
                                + item.nameOfPatient + '</td><td>'
                                + claimStatus + '</td><td>'
                                + '<select name="selForwordTo" title="ForwardTo" class="form-select table_select" onchange="ChangeStatus(' + item.claimId + ',this)"><option value="0">Select</option><option value="DA">Forward To DA</option><option value="ASO">Forward To ASO</option><option value="SO">Forward To SO</option><option value="AM_DM">Forward To Pension-Trust</option></select>' + '</td><td>'
                                + remark +'</td><td>'
                                + '<input type="checkbox" id="chkPhysicalSubmit' + item.claimId + '" onclick="UpdatePhysicalSubmitCashless(' + item.claimId + ')">' + '</td><td class="d-flex justify-content-around">'                                
                                + '<a href="Cashless/ApproveReject/' + item.claimId + '" class="btn btn-primary btn-sm mr-2 btn_small" ><i class="far fa-eye" ></i></a> ' + voucherbutton + '</td></tr> '

                            $('#tblProcessingCashlessClaim tbody').append(row);

                            //to show the check mark on checkbox
                            var chkid = '#chkPhysicalSubmit' + item.claimId;
                            $(chkid).prop("checked", item.physicalSubmit);
                            if (item.claimStatusId != 1) {
                                $(chkid).attr("disabled", true);
                            }

                            var row = $("#tblProcessingCashlessClaim").find("tr").last();
                            if (item.forwardTo === null || item.forwardTo ==='') {
                                
                            }
                            else {
                                let selectControl = row.find('select');
                                selectControl.val(item.forwardTo == "CMO" ? "0" : item.forwardTo);

                                switch (designation) {
                                    //$(".ct option[value='X']").remove();
                                    case 'DA':
                                    case 'DA1':                                    
                                    case 'DA2': {
                                        if (item.forwardTo === 'ASO' || item.forwardTo === 'SO' || item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        //selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
                                    case 'ASO': {
                                        if (item.forwardTo === 'SO' || item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        $(chkid).prop("disabled", "disabled");
                                        //selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
                                    case 'SO': {
                                        if (item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        $(chkid).prop("disabled", "disabled");
                                        //selectControl.find('[value="SO"]').remove();
                                        break;
                                    }
                                    case 'AM_DM': {
                                        $(chkid).prop("disabled", "disabled");
                                        //selectControl.find('[value="AM/DM"]').remove();
                                        break;
                                    }
                                    default: break;
                                }
                            }

                            if (item.voucherId > 0) { row.find('a.Generate_voucher_btn').attr('href', '#'); row.find('a.Generate_voucher_btn').addClass('voucher-disabled').css('cursor', 'default'); }
                            if (item.voucherId >= 0 && item.claimStatusId === 3) { row.find('a.Generate_voucher_btn').attr('href', '#'); row.find('a.Generate_voucher_btn').addClass('voucher-disabled').css('cursor', 'default'); }
                            
                        });
                        hideLoader();
                    }
                    else {
                        var row = '<tr><td colspan="9">No data found</td></tr>';
                        $('#tblProcessingCashlessClaim tbody').append(row);
                        hideLoader();
                    }

                    table = $('#tblProcessingCashlessClaim').DataTable({
                        ordering: true,
                        dom: 'lfrtip',
                        scrollX: true,
                    });
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    };

    //var SearchClaims = function (claimDate, claimId, cardCategory, organization) {
    var SearchClaims = function (claimDate, claimId) {
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Processing/CashlessClaimsByParam/" + encodeURIComponent(claimDate) + "/" + claimId ,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                var table = $('#tblProcessingCashlessClaim').DataTable();
                table.destroy();
                $('#tblProcessingCashlessClaim tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        $.each(response, function (index, item) {
                          
                            index = index + 1
                            let claimStatus = '';
                            let remark = '';
                            let voucherbutton = (designation.toLowerCase() === 'da1' || designation.toLowerCase() === 'da2') ? '<a href="/Mediclaim/Voucher/AddNewVoucher/cashless" class="Generate_voucher_btn" title ="Voucher generate">Voucher Generate</a>' : '&nbsp';
                            switch (item.claimStatusId) { case 1: claimStatus = '<span class="bg-warning Status_text">Pending</span>'; voucherbutton = ''; break; case 2: claimStatus = '<span class="bg-success Status_text">Approved</span>'; remark = item.remark; break; case 3: claimStatus = '<span class="bg-danger Status_text">Rejected</span>'; remark = item.rejectReason; break; }
                            let _date = item.createdDateString.split('-');
                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + _date[2] + '/' + _date[1] + '/' + _date[0] + '</td><td>'
                                + item.claimId + '</td><td>'
                                + item.medicalCardNumber + '</td><td>'
                                + item.nameOfHospital + '</td><td>'
                                + item.cardCategory + '</td><td>'
                                + item.nameOfPatient + '</td><td>'
                                + claimStatus + '</td><td>'
                                + '<select name="selForwordTo" title="ForwardTo" class="form-select table_select" onchange="ChangeStatus(' + item.claimId + ',this)"><option value="0">Select</option><option value="DA">Forward To DA</option><option value="ASO">Forword To ASO</option><option value="SO">Forword To SO</option><option value="AM_DM">Forword To AM/DM</option></select>' + '</td><td>'
                                + remark + '</td><td>'
                                + '<input type="checkbox" id="chkPhysicalSubmit' + item.claimId + '" onclick="UpdatePhysicalSubmitCashless(' + item.claimId + ')">' + '</td><td class="d-flex justify-content-around">'
                                + '<a href="Cashless/ApproveReject/' + item.claimId + '" class="btn btn-primary btn-sm mr-2 btn_small"><i class="far fa-eye" ></i></a> ' + voucherbutton + '</td></tr> '

                            $('#tblProcessingCashlessClaim tbody').append(row);

                            if (item.forwardTo === null || item.forwardTo === '') {

                            }
                            else {
                                let selectControl = row.find('select');
                                selectControl.val(item.forwardTo == "CMO" ? "0" : item.forwardTo);

                                switch (designation) {
                                    //$(".ct option[value='X']").remove();
                                    case 'DA':
                                    case 'DA1':
                                    case 'DA2': {
                                        if (item.forwardTo === 'ASO' || item.forwardTo === 'SO' || item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        //selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
                                    case 'ASO': {
                                        if (item.forwardTo === 'SO' || item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        //selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
                                    case 'SO': {
                                        if (item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        //selectControl.find('[value="SO"]').remove();
                                        break;
                                    }
                                    case 'AM_DM': {
                                        selectControl.find('[value="AM_DM"]').remove();
                                        break;
                                    }
                                    default: break;
                                }
                            }

                            if (item.voucherId > 0) { row.find('a.voucherGenerate').attr('href', '#'); row.find('a.voucherGenerate').attr('disbaled', 'disbaled'); }
                            if (item.voucherId >= 0 && item.claimStatusId === 3) { row.find('a.Generate_voucher_btn').attr('href', '#'); row.find('a.Generate_voucher_btn').addClass('voucher-disabled').css('cursor', 'default'); }

                            //to show the check mark on checkbox
                            var chkid = '#chkPhysicalSubmit' + item.claimId;
                            $(chkid).prop("checked", item.physicalSubmit);
                            if (item.claimStatusId != 1) {
                                $(chkid).attr("disabled", true);
                            }
                        });
                    }
                    else {
                        var row = '<tr><td colspan="9">No data found</td></tr>';
                        $('#tblProcessingCashlessClaim tbody').append(row);
                    }

                    table.destroy();
                    table = $('#tblProcessingCashlessClaim').DataTable({
                        ordering: false,
                        dom: 'Blfrtip',
                        buttons: [
                            //{   
                            //    extend: 'excelHtml5',
                            //    title: 'Claims'
                            //},
                            //{
                            //    extend: 'pdfHtml5',
                            //    title: 'Claims'
                            //}
                        ],
                    });
                }
            },
            error: function (xhr, status, error) {

            }
        });
    }

    $('#btnSearch').click(function () {
        var claimDate = $('#txtClaimDate').val();
        var claimId = $('#txtClaimId').val();
        //var cardCategory = $('#selCardCategory').val();
        //var organization = $('#selOrganization').val();

        claimDate = (claimDate === undefined || claimDate === "") ? "1/1/0001" : claimDate;
        claimId = (claimId === undefined || claimId === '') ? 0 : claimId;
        //cardCategory = (cardCategory === undefined || cardCategory === 'Select') ? 'NA' : cardCategory;
        //organization = (organization === undefined || organization === 'Select') ? 'NA' : organization;

        //if (claimDate !== '1/1/0001' || claimId !== 0 || cardCategory !== 'Select' || organization !== 'Select') {
        if (claimDate !== '1/1/0001' || claimId !== 0 ) {
            //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
            //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
          
            SearchClaims(claimDate, claimId);
           
        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    $('#btnClear').click(function () {
        $(':input').val('');
        location.reload();

    });

    //Self Invoking function to populate the table when page loads
    (function () {
        designation = $('#hdnDesignation').val();
        LoadPendingMediclaims();
    })();
});

function UpdatePhysicalSubmitCashless(id) {
    var chkid = '#chkPhysicalSubmit' + id;
    $(chkid).attr('disabled', 'disabled');
    var request = {
        ClaimId: id,
        PhysicalSubmit: $(chkid).is(":checked"),
        ModifiedBy: null
    }
    $.ajax({
        type: 'POST',
        url: "/Mediclaim/Processing/Cashless/PhysicalSubmit",
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

function ChangeStatus(claimId, row) {
    //debugger;
    //console.log($(row).val());
    //row.setAttribute('readonly', true);
    showLoader();

    $.ajax({
        type: 'POST',
        url: '/Mediclaim/ChangeApprover/cashless/' + claimId + '/' + $(row).val(),
        data: '',
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            hideLoader();
            if (xhr.status == '200') {
                if (response !== null || response !== undefined) {
                    row.setAttribute('disabled', 'disabled');
                    //setTimeout(function () {
                    //    window.location.href = "../Cashless";
                    //}, 2000);
                }
                $(".hide_submit").hide();
                $(".show_submit").show();
                $('#exampleModal').modal('show');
            }
        },
        error: function (xhr, status, error) {
            hideLoader();
        }
    });
}


function showLoader() {
    //$('.preloader').css()
    $(".preloader").css({ "height": "100%" });
    $(".preloader").css({ "display": "block" });
    $(".preloader img").css({ "display": "block" });
};

function hideLoader() {
    $(".preloader").css({ "height": "0px"});
    $(".preloader").css({ "display": "none" });
    $(".preloader img").css({ "display": "none" });
};