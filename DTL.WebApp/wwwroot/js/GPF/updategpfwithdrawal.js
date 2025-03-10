$(document).ready(function () {
    var _hdnwithdrawalType = ''
    var WithdrawId = '';
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

    //Refundable
    var TotalAdvancedAmount = '';
    var NoOfEMI = '';
    var MonthlyDeduction = '';


    //NonRefundable
    var TotalGPFContribution = '';
    var TotalWithdrawalAmount = '';
    var RemainingContribution = '';

    var PurposeOfRefundable = '';
    var PostingLastThreeYear = '';
    var DateOfApplication = '';
    var ReasonOfAdvance = '';
    var BankAccountNo = '';
    var IFSCNo = '';
    var BranchName = '';
    var BC = '';
    var BankName = '';

    var ApplicationStatusId = '';
    var RejectReason = '';

    var gpfDocuments = [];

    var LoadGPFWithdrawalData = function () {
        _hdnwithdrawalType = $('#hdnWithdrawalType').val();
        $.ajax({
            type: 'GET',
            url: '/Get/GPF/Modify/' + _hdnwithdrawalType + '/' + $('#hdnWithdrawalId').val(),
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
                $('#selEmployer').val(response.employer)
                $('#txtMonthlyGPFPay').val(response.monthlyGPFPay);
                $('#txtDateOfJoining').val(response.dateOfJoiningYYYYMMDD);
                $('#txtPurposeOfWithdrawal').val(response.purposeOfWithdrawal);/////---------<<<<<
                $('#txtMobileNumber').val(response.mobileNumber);
                $('#txtDependantsName').val(response.dependantsName);
                $('#txtDependentsAge').val(response.dependentsAge);
                $('#txtDependentsAddress').val(response.dependentsAddress);
                if (_hdnwithdrawalType === 'refundable') {
                    $('#txtTotalAdvanceAmount').val(response.totalAdvancedAmount);
                    $('#txtNoOfEMI').val(response.noOfEMI);
                    $('#txtMonthlyDeduction').val(response.monthlyDeduction);
                    $('#selPurposeOfRefundable').val(response.purposeOfRefundable);
                }
                else {
                    $('#txtTotalGPFContribution').val(response.totalGPFContribution);
                    $('#txtTotalWithdrawalAmount').val(response.totalWithdrawalAmount);
                    $('#txtRemainingContirbution').val(response.remainingContribution);
                };
                $('#txtPostingLastThreeYear').val(response.postingLastThreeYear);
                $('#txtDateOfApplication').val(response.dateOfApplicationYYYYMMDD);
                $('#txtReasonOfAdvance').val(response.reasonOfAdvance);
                $('#txtBankAccountNo').val(response.bankAccountNo);
                $('#txtIFSCNo').val(response.ifscNo);
                $('#txtBranchName').val(response.branchName);
                $('#txtBC').val(response.bc);
                $('#txtBankName').val(response.bankName);

                gpfDocuments = response.gpfDocuments;
                PopulateDocument(gpfDocuments);
            },
            error: function (xhr, status, error) {

            }
        });
    };

    $('#btnUpdate').click(function () {
        PopulateGFPWithdrawalData();
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
            //Refundable
            TotalAdvancedAmount: _hdnwithdrawalType === 'refundable' ? TotalAdvancedAmount : 0,
            NoOfEMI: _hdnwithdrawalType === 'refundable' ? NoOfEMI : 0,
            MonthlyDeduction: _hdnwithdrawalType === 'refundable' ? MonthlyDeduction : 0,
            //NonRefundable
            TotalGPFContribution: _hdnwithdrawalType === 'nonrefundable' ? TotalGPFContribution : 0,
            TotalWithdrawalAmount: _hdnwithdrawalType === 'nonrefundable' ? TotalWithdrawalAmount : 0,
            RemainingContribution: _hdnwithdrawalType === 'nonrefundable' ? RemainingContribution : 0,

            PostingLastThreeYear: PostingLastThreeYear,
            DateOfApplication: DateOfApplication,
            PurposeOfRefundable: _hdnwithdrawalType === 'refundable' ? PurposeOfRefundable : null,
            ReasonOfAdvance: ReasonOfAdvance,
            BankAccountNo: BankAccountNo,
            IFSCNo: IFSCNo,
            BranchName: BranchName,
            BC: BC,
            BankName: BankName
        };
        $.ajax({
            type: 'POST',
            url: '/GPF/Modify/' + WithdrawType,
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    FileUpload(_hdnwithdrawalType)
                }
                else {
                }
            },
            error: function (xhr, status, error) {

            }
        });

    });

    var FileUpload = function (applicationsubarea) {
        var form = new FormData();
        var _indicator = '';
        var salaryslipfiles = $('#fileUploadSalarySlip')[0].files;
        var idcardfile = $('#fileUploadIdCard')[0].files[0];

        if (salaryslipfiles.length > 0 || idcardfile != undefined ) {
            for (var i = 0; i != salaryslipfiles.length; i++) {
                form.append("Files", salaryslipfiles[i]);
                _indicator = 's';
            }
            if (idcardfile != null) {
                form.append("Files", idcardfile);
                _indicator = _indicator + 'i';
            }
            var withdrawalId = $('#hdnWithdrawalId').val();
            var settings = {
                "url": "/GPF/Withdrawal/Modify/Document/" + applicationsubarea + "/" + withdrawalId + "/" + _indicator,
                "method": "POST",
                "timeout": 0,
                "processData": false,
                //mimeType": "multipart/form-data",
                "contentType": false,
                "data": form
            };
            $.ajax(settings).done(function (response) {
                _indicator = '';
                $("#fileUploadSalarySlip").val('');
                $("#fileUploadIdCard").val('');
                GetDocuments($('#hdnWithdrawalId').val());
            });
        }
    }

    var PopulateGFPWithdrawalData = function () {
        AccountHolderName = $('#txtAccountHolderName').val();
        EmployeId = $('#txtEmployeeId').val();
        Designation = $('#txtDesignation').val();
        //Employer = $('#txtEmployer').val();
        Employer = $('#selEmployer option:selected').val();
        MonthlyGPFPay = $('#txtMonthlyGPFPay').val();
        DateOfJoining = $('#txtDateOfJoining').val();
        PurposeOfWithdrawal = $('#txtPurposeOfWithdrawal').val();
        MobileNumber = $('#txtMobileNumber').val();
        DependantsName = $('#txtDependantsName').val();
        DependentsAge = $('#txtDependentsAge').val();
        DependentsAddress = $('#txtDependentsAddress').val();

        if (_hdnwithdrawalType === 'refundable') {
            TotalAdvancedAmount = $('#txtTotalAdvanceAmount').val();
            NoOfEMI = $('#txtNoOfEMI').val();
            MonthlyDeduction = $('#txtMonthlyDeduction').val();
            PurposeOfRefundable = $('#selPurposeOfRefundable').val();
        }
        else {
            TotalGPFContribution = $('#txtTotalGPFContribution').val();
            TotalWithdrawalAmount = $('#txtTotalWithdrawalAmount').val();
            RemainingContribution = $('#txtRemainingContirbution').val();
        };
        PostingLastThreeYear = $('#txtPostingLastThreeYear').val();
        DateOfApplication = $('#txtDateOfApplication').val();
        ReasonOfAdvance = $('#txtReasonOfAdvance').val();
        BankAccountNo = $('#txtBankAccountNo').val();
        IFSCNo = $('#txtIFSCNo').val();
        BranchName = $('#txtBranchName').val();
        BC = $('#txtBC').val();
        BankName = $('#txtBankName').val();
    };
    var PopulateDocument = function (_documents) {
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
        };
    };

    (function () {
        setTimeout(function () {
            LoadGPFWithdrawalData();
        }, 1000);
    })();

});

function DeleteFile(id) {
    $.ajax({
        type: 'POST',
        url: "/GPF/Withdrawal/Modify/Document/Delete/" + id,
        data: "",
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {

                GetDocuments($('#hdnWithdrawalId').val());
            }
            else {
                alert(response);
            }
        },
        error: function (xhr, status, error) {

        }
    });
}

function GetDocuments(referenceId) {
    console.log(referenceId);
    $.ajax({
        type: 'GET',
        url: "/GPF/Withdrawal/Modify/Document/" + referenceId,
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
