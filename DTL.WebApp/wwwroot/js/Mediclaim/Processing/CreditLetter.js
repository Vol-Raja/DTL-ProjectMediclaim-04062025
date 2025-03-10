$(document).ready(function () {
    debugger;
    let designation = ''
    var LoadPendingMediclaims = function () {
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Processing/GetCashlessCreditlatter",
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
                            var StatusCredit = '';
                            let remark = '';
                            let aprremark = '';
                            let voucherbutton = (designation.toLowerCase() === 'da1' || designation.toLowerCase() === 'da2') ? '<a href="/Mediclaim/Voucher/AddNewVoucher/cashless" class="Generate_voucher_btn" title ="Voucher generate">Voucher Generate</a>' : '&nbsp';
                            switch (item.statusCreditId) { case 1: StatusCredit = '<span class="bg-warning Status_text">Pending</span>'; voucherbutton = ''; break; case 2: StatusCredit = '<span class="bg-success Status_text">Approved</span>'; remark = item.remark; break; case 3: StatusCredit = '<span class="bg-danger Status_text">Rejected</span>'; remark = item.remark; voucherbutton = ''; break; }
                            let _date = item.issueDate.split('-')

                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + _date[2].split("T")[0] + '/' + _date[1] + '/' + _date[0] + '</td><td>'
                                /*+ item.claimId + '</td><td>'*/
                                /*+ item.medicalCardNumber + '</td><td>'*/
                                + item.nameOfHospital + '</td><td>'
                                + item.categoryOfRoom + '</td><td>'
                                + item.patientName + '</td><td>'
                                + StatusCredit + '</td><td>'
                                //+ '<select name="selForwordTo" title="ForwardTo" class="form-select table_select" onchange="ChangeStatus(' + item.employeeNo + ',this)"><option value="0">Select</option><option value="HospitalUser1">Forward To HospitalUser1</option><option value="HospitalUser2">Forward To HospitalUser2</option><option value="HospitalUser3">Forward To HospitalUser3</option><option value="HospitalUser4">Forward To HospitalUser4</option></select>' + '</td><td>'
                                + remark + '</td><td>'
                               /* + aprremark + '</td><td>'*/
                                //+ '<input type="checkbox" id="chkPhysicalSubmit' + item.claimId + '" onclick="UpdatePhysicalSubmitCashless(' + item.claimId + ')">' + '</td><td class="d-flex justify-content-around">'
                                //+ '<a href="Cashless/ApproveReject/' + item.claimId + '" class="btn btn-primary btn-sm mr-2 btn_small" ><i class="far fa-eye" ></i></a> ' + voucherbutton + '</td></tr> '
                                + '<a href="Cashless/ApproveRejectCreditLetter/' + item.serialNo + '" class="btn btn-primary btn-sm mr-2 btn_small" ><i class="far fa-eye" ></i></a> ' + voucherbutton + '</td></tr> '
                            $('#tblProcessingCashlessClaim tbody').append(row);

                            //to show the check mark on checkbox
                            var chkid = '#chkPhysicalSubmit' + item.claimId;
                            $(chkid).prop("checked", item.physicalSubmit);
                            if (item.claimStatusId != 1) {
                                $(chkid).attr("disabled", true);
                            }

                            var row = $("#tblProcessingCashlessClaim").find("tr").last();
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
                                        if (item.forwardTo === 'HospitalUser2' || item.forwardTo === 'HospitalUser3' || item.forwardTo === 'HospitalUser4') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        //selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
                                    case 'HospitalUser2': {
                                        if (item.forwardTo === 'HospitalUser3' || item.forwardTo === 'HospitalUser4') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        $(chkid).prop("disabled", "disabled");
                                        //selectControl.find('[value="ASO"]').remove();
                                        break;
                                    }
                                    case 'HospitalUser3': {
                                        if (item.forwardTo === 'HospitalUser4') {
                                            selectControl.attr('disabled', 'disabled');
                                        }
                                        $(chkid).prop("disabled", "disabled");
                                        //selectControl.find('[value="SO"]').remove();
                                        break;
                                    }
                                    case 'HospitalUser4': {
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
    (function () {
        designation = $('#hdnDesignation').val();
        LoadPendingMediclaims();
    })();
});
//changed undo
//function ChangeStatus(employeeNo, row) {
//    debugger;
//    showLoader();

//    $.ajax({
//        type: 'POST',
//        url: '/Mediclaim/ChangeApproverHospital/cashless/' + employeeNo + '/' + $(row).val(),
//        data: '',
//        contentType: 'application/json; charset=utf-8',
//        //dataType: 'json',
//        success: function (response, status, xhr) {
//            hideLoader();
//            if (xhr.status == '200') {
//                if (response !== null || response !== undefined) {
//                    row.setAttribute('disabled', 'disabled');
//                }
//                $(".hide_submit").hide();
//                $(".show_submit").show();
//                $('#exampleModal').modal('show');
//            }
//        },
//        error: function (xhr, status, error) {
//            hideLoader();
//        }
//    });
//}

function showLoader() {
    //$('.preloader').css()
    $(".preloader").css({ "height": "100%" });
    $(".preloader").css({ "display": "block" });
    $(".preloader img").css({ "display": "block" });
};

function hideLoader() {
    $(".preloader").css({ "height": "0px" });
    $(".preloader").css({ "display": "none" });
    $(".preloader img").css({ "display": "none" });
};