$(document).ready(function () {
    let claimType = '';

    var SearchVouchers = function (voucherNo, claimId, pageNumber) {
        showLoader();
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Voucher/" + claimType + "/" + voucherNo + "/" + claimId + "/" + pageNumber,
            data: "",
            contentType: 'application/json; charset=utf-8',
            success: function (response, status, xhr) {
                var table = $('#tblVoucher').DataTable();
                table.destroy();
                $('#tblVoucher tbody').empty();
                if (xhr.status == '200') {
                    if (response.length > 0) {
                        $.each(response, function (index, item) {
                            index = index + 1                            
                            var row = '<tr><td>'
                                + index + '</td><td>'
                                + item.createdDateString + '</td><td>'
                                + item.voucherId + '</td><td>'                                
                                + item.pageNo + '</td><td>'
                                + item.claimId + '</td><td>'
                                + item.accountNumber + '</td><td>'
                                + item.approvedAmount + '</td><td>'
                                + item.ifscCode + '</td><td>'
                                + item.bicCode + '</td><td>'
                                + '<a href="/Mediclaim/Voucher/' + claimType + '/PrintPreview/' + item.voucherId + '" class="btn btn-primary btn-sm mr-2 btn_small"><i class="far fa-eye"></i></a> <a style="color:#fff" onclick="DeleteVoucher(' + item.voucherId+',this)" id="btnDelete" class="btn btn-danger btn-sm mr-2 btn_small"><i class="fa fa-trash"></i></a>' + '</td></tr> '
                                //< a href = "/Mediclaim/Voucher/Edit/noncashless/' + item.voucherId + '" class="btn btn-info btn-sm mr-2 btn_small" > <i class="far fa-edit"></i></a >

                            $('#tblVoucher tbody').append(row);
                        });
                        hideLoader();
                    }
                    else {
                        var row = '<tr><td colspan="10">No data found</td></tr>';
                        $('#tblVoucher tbody').append(row);
                        hideLoader();
                    }
                    table = $('#tblVoucher').DataTable({
                        ordering: false,
                        dom: 'Blfrtip',
                        buttons: [
                            //{
                            //    //extend: 'excelHtml5',
                            //    //title: 'Claims'
                            //},
                            //{
                            //    //extend: 'pdfHtml5',
                            //    //title: 'Claims'
                            //}
                        ],
                    });
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    }

    $('#btnSearch').click(function () {
        var voucherNo = $('#txtVoucherNo').val();
        var claimId = $('#txtClaimNo').val();
        var pageNumber = $('#txtPageNo').val();

        voucherNo = (voucherNo === undefined || voucherNo === '') ? 0 : voucherNo;
        claimId = (claimId === undefined || claimId === '') ? 0 : claimId;
        pageNumber = pageNumber === '' ? 'NA' : pageNumber;


        if (voucherNo !== '0' || claimId !== 0 || pageNumber !== '') {            
            SearchVouchers(voucherNo, claimId, pageNumber);

        }
        else {
            alert('Please provide atleast one condition');
        }
    });

    var showLoader = function () {
        //$('.preloader').css()
        $(".preloader").css({ "height": "100%" });
        $(".preloader").css({ "display": "block" });
        $(".preloader img").css({ "display": "block" });
    };

    var hideLoader = function () {
        $(".preloader").css({ "height": "0px" });
        $(".preloader").css({ "display": "none" });
        $(".preloader img").css({ "display": "none" });
    };

    $('#btnClear').click(function () {
        $(':input').val('');
    });

    //Self Invoking function to populate the table when page loads
    (function () {
        claimType = $('#hdnclaimtype').val();
        SearchVouchers(0,0,'NA')
    })();
});


function DeleteVoucher(id,rw) {
    if (confirm("Are you sure you want to delete the user?")) {
        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Voucher/Delete/" + id,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    if (response == "Success") {
                        $(rw).parents("tr").remove();
                    }
                    else {
                    }
                }
            },
            error: function (xhr, status, error) {

            }
        });
    }
}

 