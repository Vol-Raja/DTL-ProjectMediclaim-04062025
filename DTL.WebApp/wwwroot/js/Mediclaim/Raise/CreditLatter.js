$(document).ready(function () {
    var PatientName = '';
    var PensionerName = '';
    var Relation = '';
    var EmployeeNo = '';
    var PageNo = '';
    var CategoryOfRoom = '';
    var NameOfDoctor = '';
    var PatientNo = '';
    var IssueDate = '';
    var ResidentAddress = '';
    var ProvisionalDiagnosis = '';
    var IstendedTreatment = '';
    var DurationOfStay = '';
    var DateOfAdmission = '';
    var DateOfDischargeOrDeath = '';
    var Diagnosis = '';
    var Treatment = '';
    var SignatureDischargeTime = '';  
    var PPONumber = '';
    var isValid = false;
    var Organization = '';   // add 11/02/2025
    var Casetype = "";
    var Doctor_No = "";
    var srNo = 1
    var DocumentsArray = [];

    $('#btnReset').click(function () {
        $("#cashlessForm").validate().resetForm();
        $("input[type=checkbox]").prop('checked', false);
        DocumentsArray = [];
    });

    $('#btnSubmit1').click(function () {
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
                PatientName: PatientName,
                PensionerName: PensionerName,
                Relation: Relation,
                EmployeeNo: EmployeeNo,
                // PageNo: PageNo,
                CategoryOfRoom: CategoryOfRoom,
                NameOfDoctor: NameOfDoctor,
                PatientNo: PatientNo,
                IssueDate: IssueDate,
                ResidentAddress: ResidentAddress,
                ProvisionalDiagnosis: ProvisionalDiagnosis,
                IstendedTreatment: IstendedTreatment,
                DurationOfStay: DurationOfStay,
                DateOfAdmission: DateOfAdmission,
                // DateOfDischargeOrDeath: DateOfDischargeOrDeath,
                //Diagnosis: Diagnosis,
                //Treatment: Treatment,
                //SignatureDischargeTime: SignatureDischargeTime,
                PPONumber: PPONumber,
                Doctor_No: Doctor_No,
                Casetype: Casetype,
                Documents: DocumentsArray,
                Organization: Organization,
               
            }
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Raise/AddNewAdmission",
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
                                window.location.href = "/Mediclaim/Cashless/IndexCredit";
                            }, 2000);
                        }
                        $(".hide_submit").hide();
                        $(".show_submit").show();
                        //$('#exampleModal').modal('show');
                    }
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            });
        }
    });
    
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
            PatientName = $('#txtPatient').val(),
            PensionerName = $('#txtPensionerName').val(),
            Relation = $('#txtRelation').val(),
            EmployeeNo = $('#txtempno').val(),
            //PageNo = $('#txtPageNumber').val(),
            CategoryOfRoom = $("#selCardCategory option:selected").val(),
            NameOfDoctor = $('#txtdocno').val(),
            PatientNo = $('#txtPatientNumber').val(),
            IssueDate = $('#dtDateRetirement').val(),
            ResidentAddress = $('#txtResidentAddress').val(),
            ProvisionalDiagnosis = $('#txtProvisional').val(),
            IstendedTreatment = $('#txtIstended').val(),
            DurationOfStay = $('#txtDurationNumber').val(),
            DateOfAdmission = $('#dtAdmission').val(),
            //DateOfDischargeOrDeath = $('#dtDischarge').val(),
            //Diagnosis = $('#txtDiagnosis').val(),
            //Treatment = $('#txtTypeOfTreatment').val(),
            //SignatureDischargeTime = $('#txtSignatureDischargeTime').val(),
            //        PPONumber = $('#txtPPONumber').val()
            Doctor_No = $('#txtDoctorNumber').val(),
            Casetype = getSelectedCaseTypeText(),
            Organization = $('#selOrganization').val(),
            PPONumber = $('#txtPPONumber').val(),
            $("#tblDocument tbody tr").each(function () {
                DocumentsArray.push(JSON.parse($(this).find('td input.hdndoc').val()));
            });
    }
    // Add New by Rajan 9-01-2025
    function getSelectedCaseTypeText() {
        let selectedValue = $("input[name='selectedOption']:checked").val();
        return selectedValue;
    }
    $("input[name='selectedOption']").on('change', function () {
        Casetype = getSelectedCaseTypeText();
    });
   // End

    //new changes by nirbhay
    //$('#txtPPONumber').change(function () {
    //    debugger;
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
    //                        $('#txtPensionerName').val(response.accountHolderName);
    //                        $('#txtempno').val(response.employeeNumber);
    //                        // $('#txtPayTo').val(patient);
    //                        //$('#txtPensionerName').val(response.emP_NAME);
    //                        //$('#txtPatientNumber').val(response.mobile_no);
    //                        //$('#txtPatientEmailId').val(response.email_id)
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


    $('#txtPPONumber').change(function () {
        //debugger;
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
                            $('#txtPensionerName').val(response.accountHolderName);
                            $('#txtempno').val(response.employeeNumber);
                           
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
    $('#btnUpload').click(function () {
        FileUpload_CreditReport();
    });

    //--------------- change by Rajan 01/10/2025---------------------//

    var FileUpload_CreditReport = function () {
       
        var _indicator = $('#selDocumentType').val();
        var form = new FormData();
        var uploadfiles = $('#fileUpload')[0].files;

        for (var i = 0; i != uploadfiles.length; i++) {
            form.append("Files", uploadfiles[i]);
        }

        if (uploadfiles.length > 0) {
            $.ajax({
                type: 'POST',
                url: '/Mediclaim/Raise/Upload_Report/Creditletter/' + _indicator,
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

    //Self Invoking function to populate the table when page loads
    (function () {
        //$(".showDependent").hide();
        $(".claim").hide();
        $(".show_submit").hide();
    });



});

function DeleteFile(currentfile, row) {
    if (confirm("Are you sure you want to delete the user?")) {
        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Raise/DeleteFile1/Creditletter",
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