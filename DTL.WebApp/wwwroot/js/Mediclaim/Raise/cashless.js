var value1 = '';
$(document).ready(function () {
    debugger;
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
   
    value1 = sessionStorage.getItem('someKey');
    var intId = parseInt(value1, 10);  // Convert back to integer
    $('#btnReset').click(function () {
        $("#cashlessForm").validate().resetForm();
        $("input[type=checkbox]").prop('checked', false);
        DocumentsArray = [];
    });

    $('#btnSubmit').click(function () { 
        //debugger;
        validateEntryForm();
        if (isValid) {
            showLoader()
            PopulateCashlessProperties();
            var request = {
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
                DependantAge:DependantAge ,
            }
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Raise/AddNewMediclaimCashless",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    hideLoader();
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) {
                            $('#lblClaimNumber').text(response)
                            $('#hdnMediClaimId').val(response)
                            setTimeout(function () {
                                window.location.href = "/Mediclaim/Cashless";
                            }, 2000);
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
    });
    debugger;
    Getcreditletterdata();
    loginuser();
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

        //For select
        $.each($('select'), function (data, item) {
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
        if ($('#selRelationWithRetire option:selected').val() === '') {
            $('#selRelatioWithRetire').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#selRelatioWithRetire').removeClass('is-invalid');
        }
        isValid = (errorCount > 0) ? false : true;
       
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
                //element.closest('.form-group').append(error);
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
            DependantDOB = $('#dtDependantDOB').val(),
            DependantAge = $('#txtDependantAge').val(),
        $("#tblDocument tbody tr").each(function () {
            DocumentsArray.push(JSON.parse($(this).find('td input.hdndoc').val()));
        });
    }


    $('#btnViewPrint').click(function () {
        window.location.href = '/Mediclaim/Cashless/PrintPreview/' + $('#hdnMediClaimId').val();
    });

    $('#btnViewPrintLink').click(function () {
        window.location.href = '/Mediclaim/Cashless/PrintPreview/' + $(this).next("input").text();
    });
    $('#dtDependantDOB').change(function () {
    
        //alert('Toing');
        var currentDate = new Date();
        var dobDate = new Date($('#dtDependantDOB').val());
        var age1 = currentDate.getFullYear() - dobDate.getFullYear();
        $('#txtDependantAge').val(age1);
    });
    $('#dtDateOfBirth').change(function () {
     
        //alert('Toing');
        var currentDate = new Date();
        var dobDate = new Date($('#dtDateOfBirth').val());
        var age1 = currentDate.getFullYear() - dobDate.getFullYear();
        //var age2 = Math.floor((currentDate - dobDate) / (365.25 * 24 * 60 * 60 * 1000));
        //console.log(currentDate);
        //console.log(dobDate);
        console.log(age1);
       //console.log(age2);
        $('#txtAge').val(age1);
    });
    //new changes by nirbhay
    $('#txtPPONumber').change(function () {
        debugger;
       // alert('hii');
        var PPONumber = $('#txtPPONumber').val();
        if (PPONumber !== '') {
            $.ajax({
                type: 'GET',
                url: "/Mediclaim/Raise/Searchppo/" + PPONumber,
                data: '',//JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    hideLoader();
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) {
                           // $('#txtPayTo').val(patient);
                            $('#txtMedicalCardHolderName').val(response.emP_NAME);
                            $('#txtPatientPhoneNumber').val(response.mobile_no);
                            $('#txtPatientEmailId').val(response.email_id)
                            //$('#txtMedicalSectionPageNumber').val(response.medicalSectionPageNumber);
                            //$('#txtMedicalCardNumber').val(response.medicalCardNumber);
                        }
                    
                    }
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            });


        }
        else {
            alert('Please select PPONumber');
            $('#txtPPONumber').val('');
        }
    });
    //end new changes

    //new changes by nirbhay
    function loginuser() {
        debugger;
        var PPONumber1 = $('#dropdownMenuLink label').text().trim();
        if (PPONumber1 !== '') {
            $.ajax({
                type: 'GET',
                url: "/Mediclaim/Raise/Loginuser/" + PPONumber1,
                data: '',
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    hideLoader();
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) {
                            $('#txtNameOfHospital').val(response.nameOfHospital);
                            $('#txtHospitalId').val(response.hospitalUserId);
                            $('#txtHospitalPhoneNumber').val(response.phonenumber);
                            $('#txtEmailId').val(response.emailAddress);
                            $('#txtHospitalAddress').val(response.address);
                        }

                    }
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            }
            )
        }
    };

    //end new changes

    //new changes by nirbhay
    function Getcreditletterdata() {
        debugger;
        var PPONumber2 = value1;
        if (PPONumber2 !== '') {
            $.ajax({
                type: 'GET',
                url: "/Mediclaim/Raise/Getcreditletterdata/" + PPONumber2,
                data: '',
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    hideLoader();
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) {
                            $('#txtNameOfHospital').val(response.nameOfHospital);
                            $('#txtHospitalId').val(response.hospitalId);
                            $('#txtHospitalPhoneNumber').val(response.hospitalPhoneNumber);
                            $('#txtEmailId').val(response.emailId);
                            $('#txtHospitalAddress').val(response.hospitalAddress);
                            $('#txtMedicalCardHolderName').val(response.pensionerName);
                            $('#txtNameOfPatient').val(response.patientName);
                            $('#selCardCategory').val(response.categoryOfRoom);
                            $('#selRelatioWithRetire').val(response.relation);
                            $('#dtDateOfAdmission').val(response.dateOfAdmission);
                            $('#txtTypeOfTreatment').val(response.treatment);
                            $('#txtPatientAddress').val(response.residentAddress);
                            $('#txtCaseType').val(response.diagnosis);
                            $('#txtMedicalSectionPageNumber').val(response.pageNo);
                            $('#txtPPONumber').val(response.ppoNumber);
                            $('#dtDateOfAdmission').val(response.dateOfAdmission);
                            $('#dtDateOfDischargeOrDeath').val(response.dateOfDischargeOrDeath);
                            $('#txtMedicalCardNumber').val(response.medicalCardNumber);
                            $('#txtAdmissionNumber').val(response.admissionNumber);
                        }

                    }
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            }
            )
        }
    };

    //end new changes

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
            $.ajax({
                type: 'POST',
                url: '/Mediclaim/Raise/UploadFile/cashless/' + _indicator,
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
                                            <td>${item.documentFor}</td>
                                            <td>${item.fileName}</td>
                                            <td><a href="#" onclick="DeleteFile('${item.documentPath}',this)" class="btn btn-danger btn-sm mr-2 btn_small"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>`
                                $('#tblDocument tbody').append(row);
                            });
                            srNo = srNo + 1;
                        };
                    }
                },
                error: function (xhr, status, error) {

                }
            });
        }
        else {
            alert('Please upload document')
        }
    };

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
  
    //Self Invoking function to populate the table when page loads
    (function () {
        //$(".showDependent").hide();
        $(".claim").hide();
        $(".show_submit").hide();
    });

});

function UpdateCashlessIsDelete(id) {

    var chkid = id;
    /*$(chkid).attr("disabled", true);
    var request = {
        ClaimId: id,
        PhysicalSubmit: $(chkid).is(":checked"),
        ModifiedBy: null
    }*/
    $.ajax({
        type: 'POST',
        url: "/Mediclaim/Cashless/Delete/" + id,
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {
                if (response == "Success") {

                    //$(chkid).removeAttr("disabled");

                    //setTimeout(function () {
                    //    window.location.href = "../NonCashlessClaims";
                    //}, 2000);
                    window.location.href = "../Cashless";
                }
                else {
                }
            }
        },
        error: function (xhr, status, error) {

        }
    });


    var showLoader = function () {
        //$('.preloader').css()
        $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
        $(".preloader img").css({ "display": "block" });
    };

    var hideLoader = function () {
        $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
        $(".preloader img").css({ "display": "none" });
    };
}

function DeleteFile(currentfile, row) {
    if (confirm("Are you sure you want to delete the user?")) {
        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Raise/DeleteFile/Cashless",
            data: JSON.stringify(currentfile),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    $(row).parents("tr").remove();
                }
                else {
                    alert(response);
                }
            },
            error: function (xhr, status, error) {

            }
        });
    }
}

//$('#dropdownMenuLink label').text().trim();