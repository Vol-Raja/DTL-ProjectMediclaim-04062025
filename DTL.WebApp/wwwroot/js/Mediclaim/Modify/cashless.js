$(document).ready(function () {
    var ClaimId = '';
    var NameOfHospital = '';
    var HospitalPhoneNumber = '';
    var HospitalAddress = '';
    var HospitalId = '';
    var EmailId = '';
    var NameOfPatient = '';
    var PatientEmailId = '';
    var Gender = '';
    var PatientPhoneNumber = '';
    var PatientAddress = '';
    var DateOfBirth = '';
    var Age = '';
    var MedicalSectionPageNumber = '';
    var NameOfCardHolder = '';
    var MedicalCardNumber = '';
    var AdmissionNumber = '';
    var CardCategory = '';
    var CaseType = '';
    var TypeOfTreatment = '';
    var Amount = '';
    var DateOfAdmission = '';
    var DateOfDischargeOrDeath = '';   
    var AccountHolderName = '';
    var AccountNumber = '';
    var BankName = '';
    var BICCode = '';
    var IFSCNumber = '';
    var BranchName = '';
    var srNo = 1
    var DocumentsArray = [];
    var PPONumber = '';
    var Organization = '';
    var Department = '';
    var Designation = '';
    var DateOfRetirement = '';
    var RelationWithRetire = '';
    var DependantDOB = '';
    var DependantAge = '';

    var isValid = false;

    $('#btnReset').click(function () {
        $("#cashlessForm").validate().resetForm();
        $("input[type=checkbox]").prop('checked', false);
        DocumentsArray = [];
    });

    $('#btnSubmit').click(function () {
        //debugger;
        validateEntryForm();
        if (isValid) {
            showLoader();
            PopulateCashlessProperties();
            var request = {
                ClaimId: ClaimId,
                NameOfHospital: NameOfHospital,
                HospitalPhoneNumber: HospitalPhoneNumber,
                HospitalAddress: HospitalAddress,
                HospitalId: HospitalId,
                EmailId: EmailId,
                NameOfPatient: NameOfPatient,
                PatientEmailId: PatientEmailId,
                Gender: Gender,
                PatientPhoneNumber: PatientPhoneNumber,
                PatientAddress: PatientAddress,
                DateOfBirth: DateOfBirth,
                Age: Age,
                MedicalSectionPageNumber: MedicalSectionPageNumber,
                NameOfCardHolder: NameOfCardHolder,
                MedicalCardNumber: MedicalCardNumber,
                AdmissionNumber: AdmissionNumber,
                CardCategory: CardCategory,
                CaseType: CaseType,
                TypeOfTreatment: TypeOfTreatment,
                Amount: Amount,
                DateOfAdmission: DateOfAdmission,
                DateOfDischargeOrDeath: DateOfDischargeOrDeath,
                AccountHolderName: AccountHolderName,
                AccountNumber: AccountNumber,
                BankName: BankName,
                BICCode: BICCode,
                IFSCNumber: IFSCNumber,
                BranchName: BranchName,
                Documents: DocumentsArray,
                PPONumber: PPONumber,
                Organization: Organization,
                Department: Department,
                Designation: Designation,
                DateOfRetirement: DateOfRetirement,
                RelationWithRetire: RelationWithRetire,
                DependantDOB: DependantDOB,
                DependantAge: DependantAge,
            }
            $.ajax({
                type: 'POST',
                url: '/Mediclaim/Modify/UpdateCashless',
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    hideLoader();
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) {                             
                            $('#hdnMediclaimId').val(response);
                        }
                        $('#exampleModal').modal('show');

                        setTimeout(function () {
                            window.location.href = "/Mediclaim/Cashless";
                        }, 2000);
                    }
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            });
        }
    });

    var validateEntryForm = function () {

        var errorCount = 0;

        //For text
        $.each($('input:text'), function (data, item) {
            if ($(item).val() === '') {
                $(item).addClass('is-invalid');
                errorCount += 1;
            }
            else {
                $(item).removeClass('is-invalid');

            }
        });

        //For textarea
        $.each($('textarea'), function (data, item) {
            if ($(item).val() === '') {
                $(item).addClass('is-invalid');
                errorCount += 1;
            }
            else {
                $(item).removeClass('is-invalid');
            }
        });

        //For date
        $.each($('input[type="date"]'), function (data, item) {
            if ($(item).val() === '') {
                $(item).addClass('is-invalid');
                errorCount += 1;
            }
            else {
                $(item).removeClass('is-invalid');
            }
        });

        //For email
        $.each($('input[type="email"]'), function (data, item) {
            if ($(item).val() === '') {
                $(item).addClass('is-invalid');
                errorCount += 1;
            }
            else {
                $(item).removeClass('is-invalid');
            }
        });

        //For number
        $.each($('input[type="number"]'), function (data, item) {
            if ($(item).val() === '') {
                $(item).addClass('is-invalid');
                errorCount += 1;
            }
            else {
                $(item).removeClass('is-invalid');
            }
        });

        isValid = (errorCount > 0) ? false : true;

        ValidateEmailFormat($('#txtEmailId').val(),'#txtEmailId');
        ValidateEmailFormat($('#txtPatientEmailId').val(),'#txtPatientEmailId');

        $('#cashlessForm').validate({
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
            /*errorElement: 'span',*/
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

    var PopulateCashlessProperties = function () {
        ClaimId = $('#hdnMediclaimId').val(),
            NameOfHospital = $('#txtNameOfHospital').val(),
            HospitalPhoneNumber = $('#txtHospitalPhoneNumber').val(),
            HospitalAddress = $('#txtHospitalAddress').val(),
            HospitalId = $('#txtHospitalId').val(),
            EmailId = $('#txtEmailId').val(),
            NameOfPatient = $('#txtNameOfPatient').val(),
            PatientEmailId = $('#txtPatientEmailId').val(),
            Gender = $('.gender-radio input[type="radio"]:checked').val(),
            PatientPhoneNumber = $('#txtPatientPhoneNumber').val(),
            PatientAddress = $('#txtPatientAddress').val(),
            DateOfBirth = $('#dtDateOfBirth').val(),
            Age = $('#txtAge').val(),
            MedicalSectionPageNumber = $('#txtMedicalSectionPageNumber').val(),
            NameOfCardHolder = $('#txtMedicalCardHolderName').val(),
            MedicalCardNumber = $('#txtMedicalCardNumber').val(),
            AdmissionNumber = $('#txtAdmissionNumber').val(),
            CardCategory = $('#selCardCategory option:selected').val(),
            CaseType = $('#txtCaseType').val(),
            TypeOfTreatment = $('#txtTypeOfTreatment').val(),
            Amount = $('#txtAmount').val(),
            DateOfAdmission = $('#dtDateOfAdmission').val(),
            DateOfDischargeOrDeath = $('#dtDateOfDischargeOrDeath').val(),
            AccountHolderName = $('#txtAccountHolderName').val(),
            AccountNumber = $('#txtAccountNumber').val(),
            BankName = $('#txtBankName').val(),
            BICCode = $('#txtBICCode').val(),
            IFSCNumber = $('#txtIFSCNumber').val(),
            BranchName = $('#txtBranchName').val(),
            PPONumber = $('#txtPPONumber').val(),
            Organization = $('#selOrganization').val(),
            Department = $('#txtDepartment').val(),
            Designation = $('#txtDesignation').val(),
            DateOfRetirement = $('#dtDateRetirement').val(),
            RelationWithRetire = $("#selRelatioWithRetire option:selected").val();
            DependantDOB = $('#txtDependentDob').val(),
            DependantAge = $('#txtDependentAge').val(),
            $("#tblDocument tbody tr").each(function () {
            DocumentsArray.push(JSON.parse($(this).find('td input.hdndoc').val()));
        });
    };
     
    var LoadCashlessData = function () {
        ClaimId = $('#hdnMediclaimId').val();
        //debugger;
        $.ajax({
            type: 'GET',
            url: '/Mediclaim/Modify/Cashless/' + ClaimId,
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                console.log(response);
                $('#tblDocument tbody').empty();
                $('#txtNameOfHospital').val(response.nameOfHospital);
                $('#txtHospitalPhoneNumber').val(response.hospitalPhoneNumber);
                $('#txtHospitalAddress').val(response.hospitalAddress);
                $('#txtHospitalId').val(response.hospitalId);
                $('#txtEmailId').val(response.emailId);
                $('#txtNameOfPatient').val(response.nameOfPatient);
                Gender = response.gender;
                if (Gender === 'Male') {
                    $('#rdMale').attr('checked', 'checked');
                }
                else if (Gender === 'Female') {
                    $('#rdFemale').attr('checked', 'checked');
                }
                else {
                    $('#rdOther').attr('checked', 'checked');
                }
                $('#txtPatientPhoneNumber').val(response.patientPhoneNumber);
                $('#txtPatientEmailId').val(response.patientEmailId);
                $('#txtPatientAddress').val(response.patientAddress);
                $('#dtDateOfBirth').val(response.dependantDOBString);
                $('#txtAge').val(response.age);
                $('#txtMedicalSectionPageNumber').val(response.medicalSectionPageNumber);
                $('#txtMedicalCardHolderName').val(response.nameOfCardHolder);
                $('#txtMedicalCardNumber').val(response.medicalCardNumber);
                $('#txtAdmissionNumber').val(response.admissionNumber);
                $('#selCardCategory').val(response.cardCategory);
                $('#txtCaseType').val(response.caseType);
                $('#txtTypeOfTreatment').val(response.typeOfTreatment);
                $('#txtAmount').val(response.amount);
                $('#dtDateOfAdmission').val(response.dateOfAdmissionString);
                $('#dtDateOfDischargeOrDeath').val(response.dateOfDischargeOrDeathString);
                $('#txtAccountHolderName').val(response.accountHolderName);
                $('#txtAccountNumber').val(response.accountNumber);
                $('#txtBankName').val(response.bankName);
                $('#txtBICCode').val(response.bicCode),
                  $('#txtIFSCNumber').val(response.ifscNumber);
                $('#txtBranchName').val(response.branchName);
                $('#txtPPONumber').val(response.ppoNumber);
                $('#selOrganization').val(response.organization);
                $('#txtDepartment').val(response.department);
                $('#txtDesignation').val(response.designation);
                $('#dtDateRetirement').val(response.dateOfRetirementString);
                $("#selRelatioWithRetire").val(response.relationWithRetire);
                $('#txtDependentDob').val(response.dependantDOBString);
                $('#txtDependentAge').val(response.dependantAge);
              
                if (response.documents.length > 0) {
                    $.each(response.documents, function (index, item) {
                        var _hdnItem = JSON.stringify(item);
                        var row = `<tr>
                                            <td>
                                                ${srNo}
                                                <input type="hidden" class="hdndoc" value='${_hdnItem}'>
                                            </td>
                                            <td width="65%">${item.documentFor}</td>
                                            <td class="text_center"><a href="${'/' + item.documentPath}" class="pdfIcons" target='_blank'><i class="fa fa-file-pdf" aria-hidden="true"></i></a></td>
                                            <td><a href="#" onclick="DeleteFile('${item.documentPath.replaceAll("\\", "|")}','${item.documentId}',this)" class="btn btn-danger btn-sm mr-2 btn_small"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>`
                        $('#tblDocument tbody').append(row);
                        srNo = srNo + 1;
                    });
                };

            },
            error: function (xhr, status, error) {

            }
        });
    };
    $('#txtDependentDob').change(function () {
        debugger;
        //alert('Toing');
        var currentDate = new Date();
        var dobDate = new Date($('#txtDependentDob').val());
        var age1 = currentDate.getFullYear() - dobDate.getFullYear();
        $('#txtDependentAge').val(age1);
    });
    $('#dtDateOfBirth').change(function () {
        //alert('Toing');
        var currentDate = new Date();
        var dobDate = new Date($('#dtDateOfBirth').val());
        var age1 = currentDate.getFullYear() - dobDate.getFullYear();
        //var age2 = Math.floor((currentDate - dobDate) / (365.25 * 24 * 60 * 60 * 1000));
        //console.log(currentDate);
        //console.log(dobDate);
        //console.log(age1);
        //console.log(age2);
        $('#txtAge').val(age1);
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

    $('#btnUpload').click(function () {
        FileUpload();
    });

    var FileUpload = function () {
        var _indicator = $('#selDocumentType').val();
        var form = new FormData();
        var uploadfiles = $('#fileUpload')[0].files;

        for (var i = 0; i != uploadfiles.length; i++) {
            form.append("Files", uploadfiles[i]);
        }

        if (uploadfiles.length > 0) {
            showLoader();
            $.ajax({
                type: 'POST',
                url: '/Mediclaim/Modify/UploadFile/cashless/' + _indicator,
                data: form,
                contentType: 'application/json; charset=utf-8',
                timeout: 0,
                processData: false,
                contentType: false,
                success: function (response, status, xhr) {
                    $('#fileUpload').val('');
                    if (xhr.status == "200") {
                        if ($('#tblDocument tbody tr').length >= 0)
                            if (response.length > 0) {
                                $.each(response, function (index, item) {
                                    var _hdnItem = JSON.stringify(item);
                                    var row = `<tr>
                                            <td>
                                                ${srNo}
                                                <input type="hidden" class="hdndoc" value='${_hdnItem}'>
                                            </td>
                                            <td width="65%">${item.documentFor}</td>
                                            <td class="text_center"><a href="${'/' + item.documentPath}" class="pdfIcons" target='_blank'><i class="fa fa-file-pdf" aria-hidden="true"></i></a></td>
                                            <td><a href="#" onclick="DeleteFile('${item.documentPath}',this)" class="btn btn-danger btn-sm mr-2 btn_small"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>`
                                    $('#tblDocument tbody').append(row);
                                });
                                srNo = srNo + 1;
                            };
                    }
                    hideLoader();
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            });
        }
        else {
            hideLoader();
            alert('Please upload document')
        }
    };

    /*
    $('#txtEmailId').focusout(function () {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test($(this).val())) {
            isValid = true;
            $('#txtEmailId').removeClass('is-invalid');
        }
        else {
            isValid = false;
            $('#txtEmailId').addClass('is-invalid');
        }
    });

    $('#txtPatientEmailId').focusout(function () {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test($(this).val())) {
            isValid = true;
            $('#txtPatientEmailId').removeClass('is-invalid');
        }
        else {
            isValid = false;
            $('#txtPatientEmailId').addClass('is-invalid');
        }
    });
    */

    var ValidateEmailFormat = function (email, id) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test(email)) {
            isValid = true;
            $(id).removeClass('is-invalid');
        }
        else {
            isValid = false;
            $(id).addClass('is-invalid');
        }
    };

    //Self Invoking function to populate the table when page loads
    (function () {
        LoadCashlessData();
        //$(".showDependent").hide();
        //$(".claim").hide();
        //$(".show_submit").hide();
    })();
});

function DeleteFile(currentfilePath, documentId, row) {    
    if (confirm("Are you sure you want to delete the user?")) {
        showLoader();
        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Modify/DeleteFile/Cashless/" + documentId,
            data: JSON.stringify(currentfilePath.replaceAll("|","\\")),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    $(row).parents("tr").remove();
                }
                else {
                    alert(response);
                }
                hideLoader();
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    }
}

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