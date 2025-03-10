$(document).ready(function () {

    var PayTo = '';
    var ApprovedAmount = '';
    var AmountInWords = '';
    var EntryNo = '';
    var PageNo = '';
    var BankBranch = '';
    var AccountNumber = '';
    var BICCode = '';
    var IFSCCode = '';
    var ClaimId = '';
    var ClaimType = '';
    var IsDelete = '';

    $('#btnSubmit').click(function () {
        validateEntryForm();
        if ($("#voucherForm").valid()) {
            PopulateVoucherProperties();
            var request = {
                VoucherId: $('#hdnMediclaimVoucherId').val(),
                PayTo: PayTo,
                ApprovedAmount: ApprovedAmount,
                AmountInWords: AmountInWords,
                EntryNo: EntryNo,
                PageNo: PageNo,
                BankBranch: BankBranch,
                AccountNumber: AccountNumber,
                BICCode: BICCode,
                IFSCCode: IFSCCode,
                ClaimId: ClaimId,
                ClaimType: ClaimType,
            };
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Voucher/Edit",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        if (response != null || response != undefined) {
                            $('#approvemsg').modal('show');

                            setTimeout(function () {
                                window.location.href = '/Mediclaim/' + $('#selClaimType option:selected').val() + '/Vouchers';
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
        EntryNo = $('#txtEntryNo').val();
        PageNo = $('#txtEntryPageNo').val();
        BankBranch = $('#txtBankBranch').val();
        AccountNumber = $('#txtAccountNo').val();
        BICCode = $('#txtBICCode').val();
        IFSCCode = $('#txtIFSCCode').val();
        ClaimId = $('#txtClaimId').val();
        ClaimType = $('#selClaimType option:selected').val();

    };

    $('#btnReset').click(function () {
        $("#voucherForm").validate().resetForm();
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

    var GetVoucherDetailForEdit = function () {
        var voucherId = $('#hdnMediclaimVoucherId').val();
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/Voucher/VoucherDetail/" + voucherId,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    if (response != null || response != undefined) {

                        $('#txtPayTo').val(response.payTo);
                        $('#txtApprovedAmount').val(response.approvedAmount);
                        $('#txtApprovedAmountInWords').val(response.amountInWords);
                        $('#txtEntryNo').val(response.entryNo);
                        $('#txtEntryPageNo').val(response.pageNo);
                        $('#txtBankName').val(response.bankName);
                        $('#txtBankBranch').val(response.bankBranch);
                        $('#txtAccountNo').val(response.accountNumber);
                        $('#txtBICCode').val(response.bicCode);
                        $('#txtIFSCCode').val(response.ifscCode);
                        $('#txtClaimId').val(response.claimId);
                        $('#selClaimType').val(response.claimType);

                    }
                    else {
                        alert('No data found');
                    }
                }
            },
            error: function (xhr, status, error) {
            }
        });
    };

    //Self Invoking function to populate the table when page loads
    (function () {
        GetVoucherDetailForEdit();
    })();
});
