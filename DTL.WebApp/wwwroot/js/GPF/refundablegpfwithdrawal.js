$(document).ready(function () {
    var WithdrawId = 0;
    var UniqueNumber = '';
    var WithdrawType = '';
    var AccountHolderName = '';
    var EmployeId = '';
    var Designation = '';
    var Employer = '';
    var MonthlyGPFPay = '';
    var DateOfJoining = '';
    var PurposeOfWithdrawal = '';
    var MobileNumber = '';
    var DependantsName = '';
    var DependentsAge = '';
    var DependentsAddress = '';
    var TotalAdvancedAmount = '';
    var NoOfEMI = '';
    var MonthlyDeduction = '';
    var PurposeOfRefundable = '';
    var PostingLastThreeYear = '';
    var DateOfApplication = '';
    var ReasonOfAdvance = '';
    var BankAccountNo = '';
    var IFSCNo = '';
    var BranchName = '';
    var BC = '';
    var BankName = '';
    var operationType = ''
    var documentUploadFlag = false;

    $('#submit_form').click(function () {
        documentUploadFlag = false;
        operationType = 'submit';
        if (ValidateSubmit()) {
            saveData();
        }
        else {
            /* alert('Please provide fields whcih are highlighted in red.');*/
            $('#validationModal').modal('show');
            $('#lblErrorMessage').text('Please provide fields which are highlighted in red.');
            $(':input[type="text"],:input[type="number"],:input[type="date"],:input[type="file"], select  ').addClass('is-invalid');
        }
    });

    $('#save_form').click(function () {
        operationType = 'save';
        documentUploadFlag = false;
        if (ValidateSave()) {
            saveData();
        }
        else {
            $('#validationModal').modal('show');
            $('#lblErrorMessage').text('Please provide fields which are highlighted in red.');
            $(':input[type="text"],:input[type="number"],:input[type="date"],:input[type="file"], select  ').addClass('is-invalid');
        }
    });

    $('#btnUploadSalarySlip').click(function () {
        documentUploadFlag = true;
        operationType = 'save';
        //If application is already saved
        if (parseInt($('#hdnWithdrawalId').val(), 10) === 0) {
            saveData();
        }
        else {
            FileUpload($('#hdnWithdrawalId').val());
        }
    });

    $('#btnUploadIdCard').click(function () {
        documentUploadFlag = true;
        operationType = 'save';
        if (parseInt($('#hdnWithdrawalId').val(), 10) === 0) {
            saveData();
        }
        else {
            FileUpload($('#hdnWithdrawalId').val());
        }
    });

    var saveData = function () {
        PopulateProperties();
        var request = {
            WithdrawId: WithdrawId,
            WithdrawType: WithdrawType,
            AccountHolderName: AccountHolderName,
            EmployeId: EmployeId,
            Designation: Designation,
            Employer: Employer,
            MonthlyGPFPay: MonthlyGPFPay,
            DateOfJoining: DateOfJoining,
            PurposeOfWithdrawal: PurposeOfWithdrawal,
            MobileNumber: MobileNumber,
            DependantsName: DependantsName,
            DependentsAge: DependentsAge,
            DependentsAddress: DependentsAddress,
            TotalAdvancedAmount: TotalAdvancedAmount,
            NoOfEMI: NoOfEMI,
            PostingLastThreeYear: PostingLastThreeYear,
            MonthlyDeduction: MonthlyDeduction,
            DateOfApplication: DateOfApplication,
            PurposeOfRefundable: PurposeOfRefundable,
            ReasonOfAdvance: ReasonOfAdvance,
            BankAccountNo: BankAccountNo,
            IFSCNo: IFSCNo,
            BranchName: BranchName,
            BC: BC,
            BankName: BankName,
            OperationType: operationType,
            ApplicationStatus: operationType == 'save' ? 0 : 1
        }
        $.ajax({
            type: 'POST',
            url: '/GPF/Withdrawal/Save/RefundableGPF',
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    if (response !== null || response !== undefined) {

                        if (parseInt($('#hdnWithdrawalId').val(), 10) === 0) {
                            $('#hdnWithdrawalId').val(response);
                        }

                        FileUpload($('#hdnWithdrawalId').val());

                        if (!documentUploadFlag) {
                            if (operationType == 'submit') {
                                $('#lblSubmitModal').text('Your Unique Id is ' + response);
                                $('#on_submit').modal('show');
                            }
                            if (operationType == 'save') {
                                $('#on_save').modal('show');
                            }
                        }
                    }
                    //$(".hide_submit").hide();
                    //$(".show_submit").show();

                     setTimeout(function () {
                         window.location.href = '/GPF/Withdrawal/RefundableList';
                    }, 2000);
                }
            },
            error: function (xhr, status, error) {

            }
        });
    }

    $('#btnViewRefundableGPFApplication').click(function () {
        window.open(window.location.href = '/GPF/Withdrawal/RefundableGPF/' + $('#hdnWithdrawalId').val(), '_blank');
    });

    $('#cancel_form').click(function () {
        window.location.href = '/GPF/Withdrawal/RefundableList';
    });

    var PopulateProperties = function () {
        WithdrawId: $('#hdnWithdrawalId').val();
        WithdrawType = 'Refundable';
        AccountHolderName = $('#txtAccountHolderName').val();
        EmployeId = $('#txtEmployeeId').val();
        Designation = $('#txtDesignation').val();
        //Employer = $('#txtEmployer').val();
        Employer = $('#selEmployer option:selected').val();
        MonthlyGPFPay = $('#txtMonthlyGPFPay').val() == '' ? 0 : $('#txtMonthlyGPFPay').val();
        DateOfJoining = $('#txtDateOfJoining').val();
        PurposeOfWithdrawal = $('#txtPurposeOfWithdrawal').val();
        MobileNumber = $('#txtMobileNumber').val();
        DependantsName = $('#txtDependantsName').val();
        DependentsAge = $('#txtDependentsAge').val() == '' ? 0 : $('#txtDependentsAge').val();
        DependentsAddress = $('#txtDependentsAddress').val();
        TotalAdvancedAmount = $('#txtTotalAdvanceAmount').val() == '' ? 0 : $('#txtTotalAdvanceAmount').val();
        NoOfEMI = $('#txtNoOfEMI').val();
        PostingLastThreeYear = $('#txtPostingLastThreeYear').val();
        MonthlyDeduction = $('#txtMonthlyDeduction').val();
        DateOfApplication = $('#txtDateOfApplication').val();
        PurposeOfRefundable = $('#selPurposeOfRefundable option:selected').val();
        ReasonOfAdvance = $('#txtReasonOfAdvance').val();
        BankAccountNo = $('#txtBankAccountNo').val();
        IFSCNo = $('#txtIFSCNo').val();
        BranchName = $('#txtBranchName').val();
        BC = $('#txtBC').val();
        BankName = $('#txtBankName').val();
    }

    var FileUpload = function (withdrawId) {
        var _indicator = '';
        var form = new FormData();
        var salaryslipfiles = $('#fileUploadSalarySlip')[0].files;
        var idcardfile = $('#fileUploadIdCard')[0].files[0];

        for (var i = 0; i != salaryslipfiles.length; i++) {
            form.append("Files", salaryslipfiles[i]);
            _indicator = 's';
        }
        if (idcardfile != null) {
            form.append("Files", idcardfile);
            _indicator = _indicator + 'i';
        }

        if (salaryslipfiles.length > 0 || idcardfile != undefined) {
            
            $.ajax({
                type: 'POST',
                url: '/GPF/Withdrawal/Upload/refundable/' + withdrawId + '/' + _indicator,
                data: form,
                contentType: 'application/json; charset=utf-8',
                timeout: 0,
                processData: false,
                contentType: false,
                //dataType: 'json',
                success: function (response, status, xhr) {
                    $('#fileUploadSalarySlip').val('');
                    $('#fileUploadIdCard').val('');
                    $('#on_upload').modal('show');
                    LoadGPFWithDrawalDocument(withdrawId);
                },
                error: function (xhr, status, error) {

                }
            });
        }
    }

    var LoadGPFWithdrawalData = function (id) {
        $.ajax({
            type: 'GET',
            url: '/Get/GPF/Modify/refundable/' + $('#hdnWithdrawalId').val(),
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                WithdrawId = response.withdrawId
                WithdrawType = response.withdrawType;
                $('#txtAccountHolderName').val(response.accountHolderName);
                $('#txtEmployeeId').val(response.employeId);
                $('#txtDesignation').val(response.designation);
                //$('#txtEmployer').val(response.employer);
                console.log(response.employer)
                $('#selEmployer').val(response.employer)
                $('#txtMonthlyGPFPay').val(response.monthlyGPFPay);
                $('#txtDateOfJoining').val(response.dateOfJoiningYYYYMMDD);
                $('#txtPurposeOfWithdrawal').val(response.purposeOfWithdrawal);/////---------<<<<<
                $('#txtMobileNumber').val(response.mobileNumber);
                $('#txtDependantsName').val(response.dependantsName);
                $('#txtDependentsAge').val(response.dependentsAge);
                $('#txtDependentsAddress').val(response.dependentsAddress);
                //if (_hdnwithdrawalType === 'refundable') {
                    $('#txtTotalAdvanceAmount').val(response.totalAdvancedAmount);
                    $('#txtNoOfEMI').val(response.noOfEMI);
                    $('#txtMonthlyDeduction').val(response.monthlyDeduction);
                    $('#selPurposeOfRefundable').val(response.purposeOfRefundable);
                //}
                //else {
                //    $('#txtTotalGPFContribution').val(response.totalGPFContribution);
                //    $('#txtTotalWithdrawalAmount').val(response.totalWithdrawalAmount);
                //    $('#txtRemainingContirbution').val(response.remainingContribution);
                //};
                $('#txtPostingLastThreeYear').val(response.postingLastThreeYear);
                $('#txtDateOfApplication').val(response.dateOfApplicationYYYYMMDD);
                $('#txtReasonOfAdvance').val(response.reasonOfAdvance);
                $('#txtBankAccountNo').val(response.bankAccountNo);
                $('#txtIFSCNo').val(response.ifscNo);
                $('#txtBranchName').val(response.branchName);
                $('#txtBC').val(response.bc);
                $('#txtBankName').val(response.bankName);

                $('#hdnUniqueNumber').val(response.uniqueNumber);

                gpfDocuments = response.gpfDocuments;
                PopulateDocument(gpfDocuments);
            },
            error: function (xhr, status, error) {

            }
        });
    };

    var PopulateDocument = function (_documents) {
        if (_documents.length > 0) {
            $('#tblDocument tbody').empty();
            if (_documents.length > 0) {
                $.each(_documents, function (index, item) {
                    if (item.documentFor === 'SalarySlip') {
                        var _url = '\\' + item.documentPath;
                        var row = '<tr><td>'
                            + 'Salary Slip' + '</td><td>'
                            + '<a href=' + _url + ' target="_blank" class="btn btn-primary btn-sm">Preview</a> <button type="button"  class="btn btn-primary btn-sm" onclick="DeleteFile(' + item.documentId + ')">Delete</button>' + '</td><tr>'

                        $('#tblDocument tbody').append(row);
                    };
                    if (item.documentFor === 'IdCard') {
                        var _url = '\\' + item.documentPath;
                        var row = '<tr><td>'
                            + 'Id Card' + '</td><td>'
                            + '<a href=' + _url + ' target="_blank" class="btn btn-primary btn-sm">Preview</a> <button type="button"  class="btn btn-primary btn-sm" onclick="DeleteFile(' + item.documentId + ')">Delete</button>' + '</td><tr>'

                        $('#tblDocument tbody').append(row);
                    }
                });
            }
        };
    };

    var ValidateSubmit = function () {
        var _invalidFields = 0
        $(':input', 'form').each(function (i, o) {
            //$('#op').append($(o).attr('custom') + ' value:' + $(o).val() + '<br/>');
            //console.log(o);
            //console.log(o.type);
            //console.log(o.id);
            if (o.type == 'text') {
                if ($('#' + o.id).val() == '') {
                    $('#' + o.id).addClass('input-error');
                    _invalidFields = _invalidFields + 1;
                }
            }
            if (o.type == 'textarea') {
                if ($('#' + o.id).val() == '') {
                    $('#' + o.id).addClass('input-error');
                    _invalidFields = _invalidFields + 1;
                }
            }
            if (o.type == 'number') {
                if ($('#' + o.id).val() == '' ||  parseInt($('#' + o.id).val(),10) > 50 ) {
                    $('#' + o.id).addClass('input-error');
                    _invalidFields = _invalidFields + 1;
                }
            }
            if (o.type == 'select-one') {
                if ($('#' + o.id + ' option:selected').val().toLowerCase() == ''
                    || $('#' + o.id + ' option:selected').val().toLowerCase() == 'select' ) {
                    $('#' + o.id).addClass('input-error');
                    _invalidFields = _invalidFields + 1;
                }
            }

            if (o.type == 'date') {
                if ($('#' + o.id).val() == '') {
                    $('#' + o.id).addClass('input-error');
                    _invalidFields = _invalidFields + 1;
                }
            }

            if (o.type == 'file') {
                console.log($('#' + o.id).val());
                if ($('#' + o.id).val() == '') {
                    if ($('#tblDocument tbody tr').length == 0) {
                        $('#' + o.id).addClass('input-error');
                        _invalidFields = _invalidFields + 1;
                    }
                    else {
                        $('#' + o.id).removeClass('input-error');
                    }
                }
            }

        })
        return _invalidFields > 0 ? false : true;
    };

    var ValidateSave = function () {
        var _invalidFields = 0
        if ($('#txtEmployeeId').val() == '') {
            $('#txtEmployeeId').addClass('input-error');
            _invalidFields = _invalidFields + 1;
        }
        else { $('#txtEmployeeId').removeClass('input-error'); }

        if ($('#txtDateOfJoining').val() == '') {
            $('#txtDateOfJoining').addClass('input-error');
            _invalidFields = _invalidFields + 1;
        }
        else { $('#txtDateOfJoining').removeClass('input-error'); }

        if ($('#txtDateOfApplication').val() == '') {
            $('#txtDateOfApplication').addClass('input-error');
            _invalidFields = _invalidFields + 1;
        }
        else { $('#txtDateOfApplication').removeClass('input-error'); }

        if ($('#txtNoOfEMI').val() == '' || parseInt($('#txtNoOfEMI').val(), 10) > 50) {
            $('#txtNoOfEMI').addClass('input-error');
            _invalidFields = _invalidFields + 1;
        }
        else { $('#txtNoOfEMI').removeClass('input-error'); }

        return _invalidFields > 0 ? false : true;
    };

    $('#refundableForm').validate({
        focusCleanup: true,
        rules: {
          
        }, 
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    //Self Invoking function to populate the table when page loads
    (function () {
        var _withdrawId = parseInt($('#hdnWithdrawalId').val(), 10);
        if (_withdrawId > 0) {
            LoadGPFWithdrawalData(_withdrawId);
        }
    })();
});


function LoadGPFWithDrawalDocument(withdrawId) {
    $.ajax({
        type: 'GET',
        url: "/GPF/Withdrawal/Modify/Document/" + withdrawId,
        data: "",
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {
                $('#tblDocument tbody').empty();

                $.each(response, function (index, item) {
                    if (item.documentFor === 'SalarySlip') {
                        var _url = '\\' + item.documentPath;
                        var row = '<tr><td>'
                            + 'Salary Slip' + '</td><td>'
                            + '<a href=' + _url + ' target="_blank" class="btn btn-primary btn-sm">Preview</a> <button type="button"  class="btn btn-primary btn-sm" onclick="DeleteFile(' + item.documentId + ')">Delete</button>' + '</td><tr>'

                        $('#tblDocument tbody').append(row);
                    };
                    if (item.documentFor === 'IdCard') {
                        var _url = '\\' + item.documentPath;
                        var row = '<tr><td>'
                            + 'Id Card' + '</td><td>'
                            + '<a href=' + _url + ' target="_blank" class="btn btn-primary btn-sm">Preview</a> <button type="button"  class="btn btn-primary btn-sm" onclick="DeleteFile(' + item.documentId + ')">Delete</button>' + '</td><tr>'

                        $('#tblDocument tbody').append(row);
                    }
                });
            }
        },
        error: function (xhr, status, error) {

        }
    });
}

function DeleteFile(id) {
    $.ajax({
        type: 'POST',
        url: "/GPF/Withdrawal/Modify/Document/Delete/" + id,
        data: "",
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {

                LoadGPFWithDrawalDocument($('#hdnWithdrawalId').val());
            }
            else {
                alert(response);
            }
        },
        error: function (xhr, status, error) {

        }
    });
}