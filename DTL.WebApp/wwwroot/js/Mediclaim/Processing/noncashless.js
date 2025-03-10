$(document).ready(function () {   
    let designation = '';
    var LoadPendingMediclaims = function () {        
        showLoader();
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Processing/GetNonCashlessClaims",
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                console.log("this response", response)
                var table = $('#tblProcessingNonCashlessClaim').DataTable();
                table.destroy();
                $('#tblProcessingNonCashlessClaim tbody').empty();
                if (xhr.status == '200') {

                    if (response.length > 0) {
                        $.each(response, function (index, item) {
                            index = index + 1
                            let claimStatus = '';
                            let remark = '';
                            let voucherbutton = (designation.toLowerCase() === 'da1' || designation.toLowerCase() === 'da2') ? '<a href="/Mediclaim/Voucher/AddNewVoucher/noncashless" class="Generate_voucher_btn" title ="Voucher generate">Voucher Generate</a>' : '&nbsp';
                            switch (item.claimStatusId) {
                                case 1: claimStatus = '<span class="bg-warning Status_text">Pending</span>'; voucherbutton = ''; break; case 2: claimStatus = '<span class="bg-success Status_text">Approved</span>'; remark = item.remark; break; case 3: claimStatus = '<span class="bg-danger Status_text">Rejected</span>'; remark = item.rejectReason; voucherbutton = ''; break; }
                            let _date = item.createdDateString.split('-')
                            
                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + _date[2] + '-' + _date[1] + '-' + _date[0] + '</td><td>'
                                + item.claimId + '</td><td>'
                                + item.ppoNumber + '</td><td>'
                                + item.claimType + '</td><td>'
                                + item.organization + '</td><td>'
                                + item.cardCategory + '</td><td>'
                                + item.patientName + '</td><td>'
                                + claimStatus + '</td><td>'
                                + '<select name="selForwordTo" class="form-select table_select" onchange="ChangeStatus(' + item.claimId + ',this)"><option value="">Select</option><option value="ASO">Forword To ASO</option><option value="SO">Forword To SO</option><option value="AM_DM">Forword To AM/DM</option></select>' + '</td><td>'                                
                                + remark + '</td><td>'
                                + '<input type="checkbox" id="chkPhysicalSubmit' + item.claimId + '" onclick="UpdatePhysicalSubmit(' + item.claimId + ')">' + '</td><td class="d-flex justify-content-around">'
                                + '<a href="NonCashless/ApproveReject/' + item.claimId + '" class="btn btn-primary btn-sm mr-2 btn_small" title="ApproveReject"><i class="far fa-eye" ></i ></a >' + voucherbutton + ' </td></tr> '

                            $('#tblProcessingNonCashlessClaim tbody').append(row);

                            //to show the check mark on checkbox
                            var chkid = '#chkPhysicalSubmit' + item.claimId;
                            $(chkid).prop("checked", item.physicalSubmit);
                            if (item.claimStatusId != 1) {
                                $(chkid).attr("disabled", true);
                            }

                            var row = $("#tblProcessingNonCashlessClaim").find("tr").last();

                            if (item.forwardTo === null || item.forwardTo === '') {

                            }
                            else {
                                let selectControl = row.find('select'); 
                                selectControl.val(item.forwardTo);                                

                                switch (designation)
                                {
                                    //$(".ct option[value='X']").remove();
                                    case 'DA1': {
                                        if (item.forwardTo === 'ASO' || item.forwardTo === 'SO' || item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        //selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
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
                        $('#tblProcessingNonCashlessClaim tbody').append(row);
                        hideLoader();
                    }

                    table = $('#tblProcessingNonCashlessClaim').DataTable({
                        ordering: true,
                        dom: 'Blfrtip',
                        scrollX: true,
                    });
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    };

    var SearchClaims = function (claimDate, claimId, cardCategory, organization) {
        showLoader();
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Processing/NonCashlessClaimsByParam/" + encodeURIComponent(claimDate) + "/" + claimId + "/" + cardCategory + "/" + organization,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                var table = $('#tblProcessingNonCashlessClaim');
                table.DataTable().destroy();
                             
                $('#tblProcessingNonCashlessClaim tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        $.each(response, function (index, item) {
                            index = index + 1
                            var claimStatus = '';
                            let remark = '';
                            let voucherbutton = '<a href="/Mediclaim/Voucher/AddNewVoucher/noncashless" class="Generate_voucher_btn" title ="Voucher generate">Voucher Generate</a>';
                            switch (item.claimStatusId) { case 1: claimStatus = '<span class="bg-warning Status_text">Pending</span>'; voucherbutton = ''; break; case 2: claimStatus = '<span class="bg-success Status_text">Approved</span>'; remark = item.remark; break; case 3: claimStatus = '<span class="bg-danger Status_text">Rejected</span>'; remark = item.rejectReason; break; }
                            let _date = item.createdDateString.split('-');
                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + _date[2] + '-' + _date[1] + '-' + _date[0] + '</td><td>'
                                + item.claimId + '</td><td>'
                                + item.ppoNumber + '</td><td>'
                                + item.claimType + '</td><td>'
                                + item.organization + '</td><td class="address">'
                                + item.cardCategory + '</td><td>'
                                + item.patientName + '</td><td>'
                                + claimStatus + '</td><td>'
                                + '<select name="selForwordTo" class="form-select table_select" onchange="ChangeStatus(' + item.claimId + ',this)"><option value="">Select</option><option value="ASO">Forword To ASO</option><option value="SO">Forword To SO</option><option value="AM_DM">Forword To AM/DM</option></select>' + '</td><td>'
                                + remark + '</td><td>'
                                + '<input type="checkbox" id="chkPhysicalSubmit' + item.claimId + '" onclick="UpdatePhysicalSubmit(' + item.claimId + ')">' + '</td><td class="d-flex justify-content-around">'
                                + '<a href="NonCashless/ApproveReject/' + item.claimId + '" class="btn btn-primary btn-sm mr-2 btn_small" title="ApproveReject"><i class="far fa-eye" ></i ></a >' + voucherbutton + ' </td></tr> '

                            $('#tblProcessingNonCashlessClaim tbody').append(row);

                            var row = $("#tblProcessingNonCashlessClaim").find("tr").last();

                            if (item.forwardTo === null || item.forwardTo === '') {

                            }
                            else {
                                let selectControl = row.find('select'); 
                                selectControl.val(item.forwardTo);

                                switch (designation) {
                                    //$(".ct option[value='X']").remove();
                                    case 'DA1': {
                                        if (item.forwardTo === 'ASO' || item.forwardTo === 'SO' || item.forwardTo === 'AM_DM') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
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
                            

                            if (item.voucherId > 0) { row.find('a.Generate_voucher_btn').attr('href', '#'); row.find('a.Generate_voucher_btn').addClass('voucher-disabled').css('cursor', 'default');}
                            if (item.voucherId >= 0 && item.claimStatusId === 3) { row.find('a.Generate_voucher_btn').attr('href', '#'); row.find('a.Generate_voucher_btn').addClass('voucher-disabled').css('cursor', 'default'); }
                            //to show the check mark on checkbox
                            var chkid = '#chkPhysicalSubmit' + item.claimId;
                            $(chkid).prop("checked", item.physicalSubmit);
                            if (item.claimStatusId != 1) {
                                $(chkid).attr("disabled", true);
                            }
                        });
                        hideLoader();
                    }
                    else {
                        var row = '<tr><td colspan="10">No data found</td></tr>';
                        $('#tblProcessingNonCashlessClaim tbody').append(row);
                        hideLoader();
                    }
                    //table.DataTable({
                    //    ordering: false,
                    //    dom: 'Blfrtip',
                    //    buttons: [
                    //        //    {
                    //        //        //extend: 'excelHtml5',
                    //        //        //title: 'Claims'
                    //        //    },
                    //        //    {
                    //        //        //extend: 'pdfHtml5',
                    //        //        //title: 'Claims'
                    //        //    }
                    //    ],
                    //});
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    }
  
    $('#btnSearch').click(function () {


            var claimDate = $('#txtClaimDate').val();
            var claimId = $('#txtClaimId').val();
            var cardCategory = $('#selCardCategory').val();
            var organization = $('#selOrganization').val();

            claimDate = (claimDate === undefined || claimDate === "") ? "1/1/0001" : claimDate;
            claimId = (claimId === undefined || claimId === '') ? 0 : claimId;
            cardCategory = (cardCategory === undefined || cardCategory === 'Select') ? 'NA' : cardCategory;
            organization = (organization === undefined || organization === 'Select') ? 'NA' : organization;

            if (claimDate !== '1/1/0001' || claimId !== 0 || cardCategory !== 'Select' || organization !== 'Select') {
                //SearchClaims(claimDate.replaceAll("-","/"), claimId, pageNumber);
                SearchClaims(claimDate, claimId, cardCategory, organization);

            }
            else {
                alert('Please provide atleast one condition');
                validateEntryForm();
            }
        
    });

    $('#btnClear').click(function () {
        $(':input').val('');
        $('#selCardCategory').val('Select');
    });

    //Self Invoking function to populate the table when page loads
    (function () {
        designation = $('#hdndesignation').val();
        LoadPendingMediclaims();
    })();
});

function UpdatePhysicalSubmit(id) {
    showLoader();
    var chkid = '#chkPhysicalSubmit' + id;
    $(chkid).attr("disabled", true);
    var request = {
        ClaimId: id,
        PhysicalSubmit: $(chkid).is(":checked"),
        ModifiedBy: null
    }
    $.ajax({
        type: 'POST',
        url: "/Mediclaim/Processing/Noncashless/PhysicalSubmit",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {
                if (response == "Success") {

                    $(chkid).removeAttr("disabled");
                    hideLoader();

                    //setTimeout(function () {
                    //    window.location.href = "../NonCashlessClaims";
                    //}, 2000);
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

function ChangeStatus(claimId, row) {
    //console.log($(row).val());
    //row.setAttribute('disabled', 'disabled')
    showLoader();

    $.ajax({
        type: 'POST',
        url: '/Mediclaim/ChangeApprover/noncashless/' + claimId + '/' + $(row).val(),
        data: '',
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            hideLoader();
            if (xhr.status == '200') {
                if (response !== null || response !== undefined) {
                    row.setAttribute('disabled', 'disabled')
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
    $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
    $(".preloader img").css({ "display": "block" });
};

function hideLoader() {
    $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
    $(".preloader img").css({ "display": "none" });
};