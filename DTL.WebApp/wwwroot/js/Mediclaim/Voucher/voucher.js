$(document).ready(function () {
    let numberToWord = new NumberToWord();
  
    var PayTo = '';
    var ApprovedAmount = '';
    var AmountInWords = '';
    var BankName = '';
    var PageNo = '';
    var BankBranch = '';
    var AccountNumber = '';
    var BICCode = '';
    var IFSCCode = '';
    var ClaimId = '';
    var ClaimType = '';
    var HospitalName = '';
    var IsDelete = '';

    $('#btnSubmit').click(function () {
        validateEntryForm();
        if ($("#voucherForm").valid()) {
            PopulateVoucherProperties();
            var request = {
                PayTo: PayTo,
                ApprovedAmount: ApprovedAmount,
                AmountInWords: AmountInWords,
                BankName: BankName,
                PageNo: PageNo,
                BankBranch: BankBranch,
                AccountNumber: AccountNumber,
                BICCode: BICCode,
                IFSCCode: IFSCCode,
                ClaimId: ClaimId,
                ClaimType: ClaimType,
                HospitalName: HospitalName
            };
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Voucher/SaveMediclaimVoucher",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        if (response != null || response != undefined) {
                            $('#voucherNumber').text(response)
                            $('#approvemsg').modal('show');

                            setTimeout(function () {
                                //window.location.assign("/Mediclaim/Processing/CashlessClaims");
                                window.location.href = '/Mediclaim/' + $('#selClaimType option:selected').val()+'/Vouchers';
                            }, 3000);
                        }
                        else {
                        }
                    }
                },
                error: function (xhr, status, error) {

                }
            });
        }
    });

    var PopulateVoucherProperties = function () {
        PayTo = $('#txtPayTo').val();
        ApprovedAmount = $('#txtApprovedAmount').val();
        AmountInWords = $('#txtApprovedAmountInWords').val();
        BankName = $('#txtBankName').val();
        PageNo = $('#txtEntryPageNo').val();
        BankBranch = $('#txtBankBranch').val();
        AccountNumber = $('#txtAccountNo').val();
        BICCode = $('#txtBICCode').val();
        IFSCCode = $('#txtIFSCCode').val();
        ClaimId = $('#txtClaimId').val();
        ClaimType = $('#selClaimType option:selected').val();
        HospitalName = $('#txtHospitalName').val();
    };

    $('#btnReset').click(function () {
        $("#voucherForm").validate().resetForm();
        $('input[type="text"]').val('');
        $('input[type="number"]').val('')
    });

    var validateEntryForm = function () {
        /*$.validator.setDefaults({
            submitHandler: function () {
                alert("Form successful submitted!");
            }
        });*/
        $('#voucherForm').validate({
            focusCleanup: true,
            rules: {
                /*txtNameOfHospital: "required",
                txtHospitalId: "required",
                txtHospitalPhoneNumber: "required",
                txtEmailId: {
                    required: true,
                    email: true,
                },
                txtHospitalAddress: "required",
                password: {
                    required: true,
                    minlength: 5
                },
                terms: {
                    required: true
                },*/
            },
            errorElement: 'span',
            errorPlacement: function (error, element) {
                error.addClass('invalid-feedback');
                element.closest('.form-group').append(error);
            },
            highlight: function (element, errorClass, validClass) {
                $(element).addClass('is-invalid');
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
            }
        });

    };

    $('#txtEntryPageNo').blur(function () {
        //GetVoucherDetail();
    });

    $('#txtClaimId').blur(function () {
        //GetApprovedAmount();
    });

    $('#btnSearch').click(function () {
        debugger;
        var claimType = $('#selClaimType').val();
        var claimNumber = $('#txtClaimId').val();
        if (claimType !== '') {
            showLoader();
            $.ajax({
                type: 'GET',
                url: "/Mediclaim/Voucher/Detail/" + claimType + "/" + claimNumber,
                data: '',
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        if (response != null || response != undefined) {
                            let patient = (claimType === 'cashless') ? response.accountHolderName : response.accountHolderName;
                            $('#txtPayTo').val(patient);
                            $('#txtApprovedAmount').val(response.approvedAmount);
                            $('#txtApprovedAmountInWords').val(numberToWord.ToWord($('#txtApprovedAmount').val()));
                            $('#txtBankName').val(response.bankName);
                            $('#txtEntryPageNo').val(response.medicalSectionPageNumber);
                            $('#txtBankBranch').val(response.branchName);
                            $('#txtAccountNo').val(response.accountNumber);
                            $('#txtBICCode').val(response.bicCode);
                            $('#txtIFSCCode').val(response.ifscNumber);
                            if ($('#selClaimType').val() === 'cashless') {
                                $('#txtHospitalName').val(response.nameOfHospital);
                            }
                            if ($('#selClaimType').val() === 'noncashless') {
                                $('#txtHospitalName').val(response.opdcndList[0].hospitalName);
                            }
                            
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
        else {
            alert('Please select ClaimType');
            $('#txtClaimId').val('');
        }
    });


    var showLoader = function() {
        //$('.preloader').css()
        $(".preloader").css({ "height": "100%" });
        $(".preloader").css({ "display": "block" });
        $(".preloader img").css({ "display": "block" });
    };

    var hideLoader = function() {
        $(".preloader").css({ "height": "0px" });
        $(".preloader").css({ "display": "none" });
        $(".preloader img").css({ "display": "none" });
    };

    var GetVoucherDetail = function () {
        var pageNumber = $('#txtEntryPageNo').val();
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Voucher/Detail/" + pageNumber,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    if (response != null || response != undefined) {
                        $('#txtPayTo').val(response.nameOfCardHolder);
                        $('#txtBankBranch').val(response.bankName);
                        $('#txtAccountNo').val(response.accountNumber)
                        $('#txtBICCode').val(response.bicCode)
                        $('#txtIFSCCode').val(response.ifscCode)
                    }
                    else {
                    }
                }
            },
            error: function (xhr, status, error) {

            }
        });
    };

    var GetApprovedAmount = function () {
        var claimType = $('#selClaimType').val();
        var claimNumber = $('#txtClaimId').val();
        if (claimType !== '') {
            $.ajax({
                type: 'GET',
                url: "/Mediclaim/Voucher/ApprovedAmount/" + claimType + "/" + claimNumber,
                data: '',
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        if (response != null || response != undefined) {
                            console.log(response);
                            $('#txtApprovedAmount').val(response);
                        }
                        else {
                        }
                    }
                },
                error: function (xhr, status, error) {                 
                }
            });
        }
        else {
            alert('Please select ClaimType');
            $('#txtClaimId').val('');
        }
    };

    //Self Invoking function to populate the table when page loads
    (function () {
        var claimType = $('#hdnClaimType').val();
        $('#selClaimType').attr('disabled', 'disabled')
        $('#selClaimType').val(claimType);
    })();
});

function ToWords(number) {

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

