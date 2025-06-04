var value1 = '';
$(document).ready(function () {
    //debugger;
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
    
    var CardCategory = '';
   
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
    //add new variable
    var SignatureOfEmployee = '';
    var Diagnosis = '';
    var Doctor_SignAndStamp = '';
    var NameOfDoctor = '';
    var Doctor_No = '';
       

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
              
                CardCategory: CardCategory,
                
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
                SignatureOfEmployee: SignatureOfEmployee,
                Diagnosis: Diagnosis,
                Doctor_SignAndStamp: Doctor_SignAndStamp,
                NameOfDoctor: NameOfDoctor,
                Doctor_No:Doctor_No,

            };
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
    Getcreditletterdata();
    loginuser();
    var validateEntryForm = function () {
        var errorCount = 0;
        if ($('#txtMedicalCardHolderName').val() === '') {
            $('#txtMedicalCardHolderName').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtMedicalCardHolderName').removeClass('is-invalid');

        }

        //if ($('#txtMedicalCardNumber').val() === '') {
        //    $('#txtMedicalCardNumber').addClass('is-invalid');
        //    errorCount += 1;
        //}

        //else {
        //    $('#txtMedicalCardNumber').removeClass('is-invalid');

        //}

        if ($("#selCardCategory option:selected").text() === 'Select') {
            $("#selCardCategory").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#selCardCategory').removeClass('is-invalid');

        }

        if ($('#txtPPONumber').val() === '') {
            $('#txtPPONumber').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtPPONumber').removeClass('is-invalid');

        }

        if ($('#txtPatientPhoneNumber').val() === '') {
            $('#txtPatientPhoneNumber').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtPatientPhoneNumber').removeClass('is-invalid');

        }

        if ($("#selOrganization option:selected").text() === 'Select') {
            $("#selOrganization").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#selOrganization').removeClass('is-invalid');

        }

        //if ($('#txtDepartment').val() === '') {
        //    $('#txtDepartment').addClass('is-invalid');
        //    errorCount += 1;
        //}

        //else {
        //    $('#txtDepartment').removeClass('is-invalid');

        //}

        //if ($('#txtDesignation').val() === '') {
        //    $('#txtDesignation').addClass('is-invalid');
        //    errorCount += 1;
        //}

        //else {
        //    $('#txtDesignation').removeClass('is-invalid');

        //}


        if ($('#dtDateRetirement').val() === '') {
            $('#dtDateRetirement').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#dtDateRetirement').removeClass('is-invalid');

        }

        if ($('#txtPatientAddress').val() === '') {
            $('#txtPatientAddress').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtPatientAddress').removeClass('is-invalid');

        }

        if ($('#txtNameOfPatient').val() === '') {
            $('#txtNameOfPatient').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtNameOfPatient').removeClass('is-invalid');

        }

        if ($('#selRelatioWithRetire option:selected').val() === '') {
            $('#selRelatioWithRetire').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#selRelatioWithRetire').removeClass('is-invalid');
        }

        if ($("#dtDependantDOB").val() === '') {
            $("#dtDependantDOB").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#dtDependantDOB').removeClass('is-invalid');

        }

        if ($('.gender-radio input[type="radio"]:checked').val() === undefined) {
            $('.gender-radio').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('.gender-radio').removeClass('is-invalid');

        }

        if ($("#txtAccountHolderName").val() === '') {
            $("#txtAccountHolderName").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtAccountHolderName').removeClass('is-invalid');
        }

        if ($("#txtAccountNumber").val() === '') {
            $("#txtAccountNumber").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtAccountNumber').removeClass('is-invalid');
        }

        if ($("#txtBankName").val() === '') {
            $("#txtBankName").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtBankName').removeClass('is-invalid');
        }

        if ($("#txtBranchName").val() === '') {
            $("#txtBranchName").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtBranchName').removeClass('is-invalid');
        }

        //if ($("#txtBICCode").val() === '') {
        //    $("#txtBICCode").addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#txtBICCode').removeClass('is-invalid');
        //}

        //if ($("#txtIFSCNumber").val() === '') {
        //    $("#txtIFSCNumber").addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#txtIFSCNumber').removeClass('is-invalid');
        //}


        if ($('#dtDateOfAdmission').val() === '') {
            $('#dtDateOfAdmission').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#dtDateOfAdmission').removeClass('is-invalid');

        }

        if ($('#dtDateOfDischargeOrDeath').val() === '') {
            $('#dtDateOfDischargeOrDeath').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#dtDateOfDischargeOrDeath').removeClass('is-invalid');

        }

        if ($('#txtDiagnosis').val() === '') {
            $('#txtDiagnosis').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtDiagnosis').removeClass('is-invalid');

        }

        if ($('#txtTypeOfTreatment').val() === '') {
            $('#txtTypeOfTreatment').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtTypeOfTreatment').removeClass('is-invalid');

        }

        if ($('#txtAmount').val() === '') {
            $('#txtAmount').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtAmount').removeClass('is-invalid');

        }

        if ($('#txtSignatureEmployee').val() === '') {
            $('#txtSignatureEmployee').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtSignatureEmployee').removeClass('is-invalid');

        }

        if ($('#signstamp_doc').val() === '') {
            $('#signstamp_doc').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#signstamp_doc').removeClass('is-invalid');

        }

        //if ($("#selDocumentType option:selected").text() === 'Select') {
        //    $("#selDocumentType").addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#selDocumentType').removeClass('is-invalid');

        //}

        if ($('#txtNameOfHospital').val() === '') {
            $('#txtNameOfHospital').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtNameOfHospital').removeClass('is-invalid');

        }

        if ($('#txtHospitalId').val() === '') {
            $('#txtHospitalId').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtHospitalId').removeClass('is-invalid');

        }
        if ($('#txtHospitalPhoneNumber').val() === '') {
            $('#txtHospitalPhoneNumber').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtHospitalPhoneNumber').removeClass('is-invalid');

        }

        if ($('#txtEmailId').val() === '') {
            $('#txtEmailId').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtEmailId').removeClass('is-invalid');

        }

        if ($('#txtdoctreat').val() === '') {
            $('#txtdoctreat').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtdoctreat').removeClass('is-invalid');

        }

        if ($('#txtDoctor').val() === '') {
            $('#txtDoctor').addClass('is-invalid');
            errorCount += 1;
        }

        else {
            $('#txtDoctor').removeClass('is-invalid');

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
        //debugger;
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
           
            CardCategory = $('#selCardCategory option:selected').val(),
          
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
            SignatureOfEmployee = $('#txtSignatureEmployee').val(), // add new rajan
            Diagnosis = $('#txtDiagnosis').val(),                           // add new by rajan
                Doctor_SignAndStamp = $('#signstamp_doc').val(),                // add by rajan 04/04/25
                NameOfDoctor = $('#txtdoctreat').val(),  // add by rajan 22/04/25
                Doctor_No = $('#txtDoctor').val(),  // add by rajan 22/04/25

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
    //$('#dtDateOfBirth').change(function () {

    //    //alert('Toing');
    //    var currentDate = new Date();
    //    var dobDate = new Date($('#dtDateOfBirth').val());
    //    var age1 = currentDate.getFullYear() - dobDate.getFullYear();
    //    //var age2 = Math.floor((currentDate - dobDate) / (365.25 * 24 * 60 * 60 * 1000));
    //    //console.log(currentDate);
    //    //console.log(dobDate);
    //    console.log(age1);
    //    //console.log(age2);
    //    $('#txtAge').val(age1);
    //});


    //new changes by nirbhay      //comment by rajan 14/04/25
    //$('#txtPPONumber').change(function () {
    //    // alert('hii');
    //    var PPONumber = $('#txtPPONumber').val();
    //    if (PPONumber !== '') {
    //        $.ajax({
    //            type: 'GET',
    //            url: "/Mediclaim/Raise/Searchppo/" + PPONumber,
    //            data: '',//JSON.stringify(request),
    //            contentType: 'application/json; charset=utf-8',
    //            //dataType: 'json',
    //            success: function (response, status, xhr) {
    //                hideLoader();
    //                if (xhr.status == '200') {
    //                    if (response !== null || response !== undefined) {
    //                        // $('#txtPayTo').val(patient);
    //                        $('#txtMedicalCardHolderName').val(response.emP_NAME);
    //                        $('#txtPatientPhoneNumber').val(response.mobile_no);
    //                        $('#txtPatientEmailId').val(response.email_id)
    //                        //$('#txtMedicalSectionPageNumber').val(response.medicalSectionPageNumber);
    //                        //$('#txtMedicalCardNumber').val(response.medicalCardNumber);
    //                    }

    //                }
    //            },
    //            error: function (xhr, status, error) {
    //                hideLoader();
    //            }
    //        });


    //    }
    //    else {
    //        alert('Please select PPONumber');
    //        $('#txtPPONumber').val('');
    //    }
    //});
    //end new changes

    //new changes by nirbhay
    function loginuser() {
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
    function formatDateForInput(dateStr) {
        if (!dateStr) return '';
        const date = new Date(dateStr);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    }
    function Getcreditletterdata() {
        //debugger;
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
                            $('#txtPatientAddress').val(response.residentAddress);  
                            $('#txtMedicalSectionPageNumber').val(response.pageNo);
                            $('#txtPPONumber').val(response.ppoNumber);      //change by rajan 14/04/25
                            $('#dtDateOfAdmission').val(formatDateForInput(response.dateOfAdmission));
                            $('#dtDateOfDischargeOrDeath').val(response.dateOfDischargeOrDeath);
                            $('#txtMedicalCardNumber').val(response.medicalCardNumber);
                            $('#txtdoctreat').val(response.nameOfDoctor);
                            $('#txtDoctor').val(response.doctor_NO);
                            $('#txtDiagnosis').val(response.provisionalDiagnosis);
                            $('#txtTypeOfTreatment').val(response.istendedTreatment);
                            $('#selCardCategory').val(response.categoryOfRoom);
                            $('#txtPatientPhoneNumber').val(response.patientNo);

                            /*$('#dtDateOfAdmission').val(response.dateOfAdmission);*/ //comment by rajan
                            /* $('#txtTypeOfTreatment').val(response.treatment);*/ //comment by rajan
                            /*$('#txtCaseType').val(response.diagnosis);*/     //comment by rajan
                            /*$('#txtAdmissionNumber').val(response.admissionNumber);*/ ////comment by rajan
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

    //chnage by rajan 14/04/2025

    $('#txtPPONumber').change(function () {
        debugger;
        var PPONumber = $('#txtPPONumber').val();
        if (PPONumber !== '') {
            $.ajax({
                type: 'GET',
                url: "/Mediclaim/Raise/GetEmployeeAccount_data/" + PPONumber,
                data: '',
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    hideLoader();
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) {
                            $('#txtAccountHolderName').val(response.accountHolderName);
                            $('#txtMedicalCardHolderName').val(response.accountHolderName);
                            $('#txtAccountNumber').val(response.accountNumber);
                            $('#txtBankName').val(response.bankName);
                            $('#txtBranchName').val(response.branchName);
                            // Set DOB
                            const dob = formatDateForInput(response.dateOfBirth);
                            $('#dtDateOfBirth').val(dob);

                            // Calculate and set Age
                            if (dob) {
                                var age = calculateAge(new Date(dob));
                                $('#txtAge').val(age);
                            }
                        }
                    }
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            }
            )
        }
        else {
            alert('Please select PPONumber');
            $('#txtPPONumber').val('');
        }
    });


    function calculateAge(dobDate) {
        var today = new Date();
        var age = today.getFullYear() - dobDate.getFullYear();
        //var m = today.getMonth() - dobDate.getMonth();
        //if (m < 0 || (m === 0 && today.getDate() < dobDate.getDate())) {
        //    age--;
        //}
        return age;
    }


    $('#txtDateOfBirth').change(function () {
        var dobDate = new Date($('#txtDateOfBirth').val());
        if (!isNaN(dobDate)) {
            var age = calculateAge(dobDate);
            $('#txtAge').val(age);
        }
    });
    //end


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

    //$('#txtPatientEmailId').focusout(function () {
    //    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    //    if (regex.test($(this).val())) {
    //        isValid = true;
    //        $('#txtPatientEmailId').removeClass('is-invalid');
    //    }
    //    else {
    //        isValid = false;
    //        $('#txtPatientEmailId').addClass('is-invalid');
    //    }
    //});

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