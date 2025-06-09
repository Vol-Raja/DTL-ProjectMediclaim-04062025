
$(document).ready(function () {
    var EmployeeNumber = '';
    var PPONumber = '';
    var MedicalSectionPageNumber = '';
    var MedicalCardHolderName = '';
    var MedicalCardNo = '';
    var Designation = '';
    var PatientName = '';
    var Gender = '';
    var DateOfBirth = '';
    var Age = '';
    var ClaimFor = '';
    var MobileNumber = '';
    var CardCategory = '';
    var Address = '';
    var ClaimType = '';
    var EmailId = '';
    var Organization = '';
    var AccountHolderName = '';
    var AccountNumber = '';
    var BankName = '';
    var BICCode = '';
    var IFSCNumber = '';
    var BranchName = '';
    var srNo = 1;
    var DocumentsArray = [];
    var CNDArray = [];
    var DependentName = '';
    var DependentDob = '';
    var DependentAge = '';
    var RelationWithRetire = '';
    var isValid = true;

    $('#btnSubmit').click(function () {
        debugger;
        validateEntryForm()
        if (isValid) {
            showLoader();
            PopulateNonCashlessProperties();

            let _dependent = null;
            if (ClaimFor === 'Dependent') {
                _dependent = {
                    Name: DependentName,
                    DateOfBirth: dependentDob,
                    Age: DependentAge,
                    //DateOfBirth: DependentDob ? DependentDob : '0/0/0',  // default to empty string if falsy
                    //Age: DependentAge || 0,
                    RelationWithRetire: RelationWithRetire
                }
            }

            var request = {
                EmployeeNumber: EmployeeNumber,
                PPONumber: PPONumber,
                MedicalSectionPageNumber: MedicalSectionPageNumber,
                MedicalCardHolderName: MedicalCardHolderName,
                MedicalCardNo: MedicalCardNo,
                Designation: Designation,
                PatientName: PatientName,
                Gender: Gender,
                DateOfBirth: DateOfBirth,
                Age: Age,
                ClaimFor: ClaimFor,
                RelationWithRetire: RelationWithRetire,
                MobileNumber: MobileNumber,
                CardCategory: CardCategory,
                Address: Address,
                ClaimType: ClaimType,
                EmailId: EmailId,
                Organization: Organization,
                AccountHolderName: AccountHolderName,
                AccountNumber: AccountNumber,
                BankName: BankName,
                BICCode: BICCode,
                IFSCNumber: IFSCNumber,
                BranchName: BranchName,
                Documents: DocumentsArray,
                ClaimStatusId: 1,
                OPDCNDList: CNDArray,
                Dependent: _dependent
            }
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Raise/AddNewMediclaimNonCashless",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) {
                            $('#lblClaimNumber').text(response)
                            $('#hdnMediClaimId').val(response)
                            $('#exampleModal').modal('show');
                        }
                        $(".hide_submit").hide();
                        $(".show_submit").show();

                        setTimeout(function () {
                        window.location.href = "/Mediclaim/NonCashless"
                        }, 5000)
                       
                    }
                    hideLoader();
                },
               
                error: function (xhr, status, error) {
                    hideLoader();
                }
            });
        }
    });

    var PopulateNonCashlessProperties = function () {
        //debugger;
        EmployeeNumber = $('#txtEmployeeNumber').val();
        PPONumber = $('#txtPPONumber').val();
        MedicalSectionPageNumber = $('#txtMedicalSectionPageNumber').val();
        MedicalCardHolderName = $('#txtMedicalCardHolderName').val();
        MedicalCardNo = $('#txtMedicalCardNo').val();
        Designation = $('#txtDesignation').val();
        PatientName = $('#txtPatientName').val();
        Gender = $('.gender-radio input[type="radio"]:checked').val();
        DateOfBirth = $('#txtDateOfBirth').val();
        Age = $('#txtAge').val();
        ClaimFor = $('.ds-radio input[type="radio"]:checked').val();
        MobileNumber = $('#txtMobileNumber').val();
        CardCategory = $("#selCardCategory option:selected").text();
        Address = $('#txtAddress').val();
        ClaimType = $("#selClaimType option:selected").val();
        EmailId = $('#txtEmailId').val();
        Organization = $("#selOrganization option:selected").text();
        AccountHolderName = $('#txtAccountHolderName').val();
        AccountNumber = $('#txtAccountNumber').val();
        BankName = $('#txtBankName').val();
        BICCode = $('#txtBICCode').val();
        IFSCNumber = $('#txtIFSCNumber').val();
        BranchName = $('#txtBranchName').val();
        DependentName = $('#txtDependentName').val();
        //DependentDob = $('#txtDependentDob').val() === '' ? "01/01/1900" : $('#txtDependentDob').val();
        let dobInput = document.getElementById('txtDependentDob');
        dependentDob = dobInput.value;

        if (!dependentDob) {
            let today = new Date();
            let yyyy = today.getFullYear();
            let mm = String(today.getMonth() + 1).padStart(2, '0');
            let dd = String(today.getDate()).padStart(2, '0');
            dependentDob = `${yyyy}-${mm}-${dd}`;
        }
        //DependentAge = $('#txtDependentAge').val();
        DependentAge = $('#txtDependentAge').val();
        DependentAge = DependentAge ? parseInt(DependentAge) : 0;
        RelationWithRetire = $("#selRelatioWithRetire option:selected").text();

        $("#tblDocument tbody tr").each(function () {
            DocumentsArray.push(JSON.parse($(this).find('td input.hdndoc').val()));
        });
    }

    var validateEntryForm = function () {
        var errorCount = 0;

        if ($('#txtEmployeeNumber').val() === '') {
            $('#txtEmployeeNumber').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtEmployeeNumber').removeClass('is-invalid');

        }

        if ($('#txtPPONumber').val() === '') {
            $('#txtPPONumber').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtPPONumber').removeClass('is-invalid');

        }

        //if ($('#txtMedicalSectionPageNumber').val() === '') {
        //    $('#txtMedicalSectionPageNumber').addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#txtMedicalSectionPageNumber').removeClass('is-invalid');

        //}

        //if ($('#txtMedicalCardNo').val() === '') {
        //    $('#txtMedicalCardNo').addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#txtMedicalCardNo').removeClass('is-invalid');

        //}

        if ($('#txtOrganization').val() === '') {
            $('#txtOrganization').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtOrganization').removeClass('is-invalid');

        }
        if ($('#txtMobileNumber').val() === '') {
            $('#txtMobileNumber').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtMobileNumber').removeClass('is-invalid');

        }

        //if ($('#txtEmailId').val() === '') {
        //    $('#txtEmailId').addClass('is-invalid');
        //    errorCount += 1;
        //}

        //else {
        //    $('#txtEmailId').removeClass('is-invalid');

        //}

        if ($('#txtMedicalCardHolderName').val() === '') {
            $('#txtMedicalCardHolderName').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtMedicalCardHolderName').removeClass('is-invalid');

        }

        //if ($('#txtDesignation').val() === '') {
        //    $('#txtDesignation').addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#txtDesignation').removeClass('is-invalid');

        //}

        if ($('#txtPatientName').val() === '') {
            $('#txtPatientName').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtPatientName').removeClass('is-invalid');

        }

        if ($('.gender-radio input[type="radio"]:checked').val() === undefined) {
            $('.gender-radio').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('.gender-radio').removeClass('is-invalid');

        }

        //if ($('#txtDateOfBirth').val() === '') {
        //    $('#txtDateOfBirth').addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#txtDateOfBirth').removeClass('is-invalid');

        //}

        if ($("#selOrganization option:selected").text() === 'Select') {
            $("#selOrganization").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#selOrganization').removeClass('is-invalid');

        }


        if ($('.ds-radio input[type="radio"]:checked').val() === undefined) {
            $('.ds-radio').addClass('is-invalid');
            errorCount += 1;
        }
        else {
            //$('.ds-radio').removeClass('is-invalid');
            if ($('.ds-radio input[type="radio"]:checked').val() === 'Dependent') {

                if ($("#txtDependentName").val() === '') {
                    $("#txtDependentName").addClass('is-invalid');
                    errorCount += 1;
                }
                else {
                    $('#txtDependentName').removeClass('is-invalid');

                }

                //if ($("#txtDependentDob").val() === '') {
                //    $("#txtDependentDob").addClass('is-invalid');
                //    errorCount += 1;
                //}
                //else {
                //    $('#txtDependentDob').removeClass('is-invalid');

                //}

                if ($('#selRelatioWithRetire option:selected').val() === '') {
                    $('#selRelatioWithRetire').addClass('is-invalid');
                    errorCount += 1;
                }
                else {
                    $('#selRelatioWithRetire').removeClass('is-invalid');
                }
            }
        }



        if ($("#selCardCategory option:selected").text() === 'Select') {
            $("#selCardCategory").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#selCardCategory').removeClass('is-invalid');

        }

        if ($("#txtAddress").val() === '') {
            $("#txtAddress").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#txtAddress').removeClass('is-invalid');

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


        if ($("#selClaimType option:selected").text() === 'Select') {
            $("#selClaimType").addClass('is-invalid');
            errorCount += 1;
        }
        else {
            $('#selClaimType').removeClass('is-invalid');

        }

        //comment by rajan 19/05/25
        //if ($('#tblCND tr').length < 3) {

        //    if ($('#txtCNDDate').val() === '') {
        //        $("#txtCNDDate").addClass('is-invalid');
        //        errorCount += 1;
        //    }
        //    else {
        //        $('#txtCNDDate').removeClass('is-invalid');

        //    }

        //    if ($('#txtCNDHospitalName').val() === '') {
        //        $("#txtCNDHospitalName").addClass('is-invalid');
        //        errorCount += 1;
        //    }
        //    else {
        //        $('#txtCNDHospitalName').removeClass('is-invalid');

        //    }

        //    if ($('#txtCNDMedicineAmount').val() === '') {
        //        $("#txtCNDMedicineAmount").addClass('is-invalid');
        //        errorCount += 1;
        //    }
        //    else {
        //        $('#txtCNDMedicineAmount').removeClass('is-invalid');

        //    }

        //    if ($('#txtCNDInvestigationAmount').val() === '') {
        //        $("#txtCNDInvestigationAmount").addClass('is-invalid');
        //        errorCount += 1;
        //    }
        //    else {
        //        $('#txtCNDInvestigationAmount').removeClass('is-invalid');

        //    }

        //    if ($('#txtCNDConsultationAmount').val() === '') {
        //        $("#txtCNDConsultationAmount").addClass('is-invalid');
        //        errorCount += 1;
        //    }
        //    else {
        //        $('#txtCNDConsultationAmount').removeClass('is-invalid');

        //    }


        //    if ($('#txtCNDOtherAmount').val() === '') {
        //        $("#txtCNDOtherAmount").addClass('is-invalid');
        //        errorCount += 1;
        //    }
        //    else {
        //        $('#txtCNDOtherAmount').removeClass('is-invalid');

        //    }

        //end

        //    //if (DocumentsArray.length <= 0) {
        //    //    alert("Please upload document");
        //    //    isvalid = false;
        //    //    return;
        //    //}
        //}


        //if ($("#selDocumentType option:selected").text() === 'Select') {
        //    $("#selDocumentType").addClass('is-invalid');
        //    errorCount += 1;
        //}
        //else {
        //    $('#selDocumentType').removeClass('is-invalid');

        //}
        isValid = (errorCount > 0) ? false : true;

        $('#nonCashlessForm').validate({
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

    //----C&D Part
   // var CNDArray = [];
    $('#btnAddCND').click(function () {
        if (PopulateCNDProperties()) {
            var total = 0;
            $('#tblCND tbody').empty();
            $.each(CNDArray, function (index, item) {
                total = total + parseInt(item.TotalAmount, 10);
                //var row = '<tr><td>' + formatDate(item.OPDCNDDate) + '</td><td>'
                var row = '<tr data-index="' + index + '"><td>' + formatDate(item.OPDCNDDate) + '</td><td>'
                    + item.HospitalName + '</td><td>'
                    + item.MedicineAmount + '<span class="text-danger req_text">₹</span></td><td>'
                    + item.ConsultationAmount + '<span class="text-danger req_text">₹</span></td><td>'
                    + item.InvestigationAmount + '<span class="text-danger req_text">₹</span></td><td>'
                    + item.OtherAmount + '<span class="text-danger req_text">₹</span></td><td>'
                    + item.TotalAmount + '<span class="text-danger req_text">₹</span></td><td>'
                    + '<button type="button" class="btn btn-danger btn-sm btn_small" onclick="deleteRow(this)"><i class="fa fa-trash" aria-hidden="true"></i></button></td></tr>';
               
    //+ item.HospitalName + '</td><td>'
    //+ item.MedicineAmount + '<span class="text-danger req_text">₹</span></td><td>'
    //+ item.ConsultationAmount + '<span class="text-danger req_text">₹</span></td><td>'
    //+ item.InvestigationAmount + '<span class="text-danger req_text">₹</span></td><td>'
    //+ item.OtherAmount + '<span class="text-danger req_text">₹</span></td><td>'
    //+ item.TotalAmount + '<span class="text-danger req_text">₹</span></td><td>'
    //+ '<button type="button" class="btn btn-danger btn-sm btn_small" onclick="deleteRow(this)"><i class="fa fa-trash" aria-hidden="true"></i></button></td></tr>';

                $('#tblCND tbody').append(row);
            });
            ResetCND();
        }
    });



    //comment by rajan 19/05/25

    //$('#btnAddCND').click(function () {
    //    //Populate the fields
    //    CNDArray = [];
    //    if (PopulateCNDProperties()) {
    //        //Empty all elements from table
    //        $('#tblCND tbody').empty();
    //        var total = 0;
    //        //If there are elements in array 
    //        //Add those to table
    //        if (CNDArray.length > 0) {
    //            //If there are elements in array 
    //            //Add those to table
    //            $.each(CNDArray, function (index, item) {
    //                total = total + parseInt(item.TotalAmount, 10);
    //                var row = '<tr><td> '
    //                    + formatDate(item.OPDCNDDate) + '</td><td>'
    //                    + item.HospitalName + '</td><td>'
    //                    + item.MedicineAmount + '<span class="text-danger req_text">₹ </span></td><td>'
    //                    + item.ConsultationAmount + '<span class="text-danger req_text">₹ </span></td><td>'
    //                    + item.InvestigationAmount + '<span class="text-danger req_text">₹ </span></td><td>'
    //                    + item.OtherAmount + '<span class="text-danger req_text">₹ </span></td><td>'
    //                    + item.TotalAmount + '<span class="text-danger req_text">₹ </span></td><td>'
    //                    + '<button type="button"  class="btn btn-danger btn-sm mr-2 btn_small" onclick="deleteRow(this)"><i class="fa fa-trash" aria-hidden="true"></i></button></td><tr>'

    //                $("#tblCND tbody").append(row);
    //            });
    //        }
    //        ResetCND();
    //    }
    //});

    $('#btnClearCND').click(function () {
        ResetCND();
        CNDArray = [];
        $('#tblCND tbody').empty();

    });

    var PopulateCNDProperties = function () {
        var opd = {
            OPDCNDDate: $('#txtCNDDate').val(),
            HospitalName: $('#txtCNDHospitalName').val(),
            MedicineAmount: $('#txtCNDMedicineAmount').val(),
            InvestigationAmount: $('#txtCNDInvestigationAmount').val(),
            ConsultationAmount: $('#txtCNDConsultationAmount').val(),
            TotalAmount: $('#txtCNDTotalAmount').val(),
            OtherAmount: $('#txtCNDOtherAmount').val(),
            HospitalizationClaimType: 'CND',
            NonCashlessClaimId: 0
        }
        if (opd.OPDCNDDate !== '' && opd.HospitalName !== ''
            && opd.MedicineAmount !== '' && opd.InvestigationAmount !== ''
            && opd.ConsultationAmount !== '' && opd.TotalAmount !== '') {
            CNDArray.push(opd);
            return true;
        }
        else {
            alert('Please fill all the fields');
            return false;
        }
    }

    var ResetCND = function () {
       /* $('#CD_div').find('input:text').val('');*/   //COMMENT BY RAJAN 01/06/25
        //$('#txtCNDDate').val('');                    //COMMENT BY RAJAN 01/06/25
        //$('#txtCNDHospitalName').val('');            //COMMENT BY RAJAN 01/06/25
        $('#txtCNDMedicineAmount').val('');
        $('#txtCNDConsultationAmount').val('');
        $('#txtCNDInvestigationAmount').val('');
        $('#txtCNDOtherAmount').val('');
        $('#txtCNDTotalAmount').val('');
    }

    //---UI behavior functions when some element
    //---Select or button clicked

    $('input[name=dependent_self]').click(function () {
        if ($(this).val() === "Dependent") {
            $(".showDependent").show();
        } else {
            $(".showDependent").hide();
        }
    });


    let formatDate = function (dateString) {
        var _date = dateString.split('-')
        return _date[2] + '/' + _date[1] + '/' + _date[0];
    }
    //Some Common fcuntion
    var ResetAllArraysAndTable = function () {
        DispensaryArray = []; $('#tblDispensary tbody').empty();
        IPDArray = []; $('#tblIPD tbody').empty();
        OPDArray = []; $('#tblOPD tbody').empty();
        CNDArray = []; $('#tblCND tbody').empty();
    };

    $('#btnViewPrint').click(function () {
        window.location.href = '/Mediclaim/NonCashless/PrintPreview/' + $('#hdnMediClaimId').val();
    });
    $('#btnViewPrintLink').click(function () {
        window.location.href = '/Mediclaim/NonCashless/PrintPreview/' + $('#hdnMediClaimId').val();
    });

    $('.number').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    //$('#txtDateOfBirth').change(function () {
    //    //alert('Toing');
    //    var currentDate = new Date();
    //    var dobDate = new Date($('#txtDateOfBirth').val());
    //    var age1 = currentDate.getFullYear() - dobDate.getFullYear();
    //    $('#txtAge').val(age1);
    //});

    $('#txtDateOfBirth').change(function () {
        var dobDate = new Date($('#txtDateOfBirth').val());
        if (!isNaN(dobDate)) {
            var age = calculateAge(dobDate);
            $('#txtAge').val(age);
        }
    });


    //comment by rajan 19/05/25 comment
    $('#txtDependentDob').change(function () {
        var currentDate = new Date();
        var dobDate = new Date($('#txtDependentDob').val());
        var age1 = currentDate.getFullYear() - dobDate.getFullYear();
      
        $('#txtDependentAge').val(age1);
    });

    var showLoader = function () {
        //$('.preloader').css()
        $(".preloader").css("height", "100%");
        $(".preloader").css("display", "block");
        $(".preloader img").css("display", "block");
    };

    var hideLoader = function () {
        $(".preloader").css({ "height": "0px" });
        $(".preloader img").css({ "display": "none" });
    };

    $('#btnUpload').click(function () {
        FileUpload();
    });

    var FileUpload = function () {
        //debugger;
        var _indicator = $('#selDocumentType').val();
        var form = new FormData();
        var uploadfiles = $('#fileUpload')[0].files;

        for (var i = 0; i != uploadfiles.length; i++) {
            form.append("Files", uploadfiles[i]);
        }

        if (uploadfiles.length > 0) {
            $.ajax({
                type: 'POST',
                url: '/Mediclaim/Raise/UploadFile/noncashless/' + _indicator,
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
                                            <td><a href="${'/' + item.documentPath}" class="pdfIcons" target='_blank'><i class="fa fa-file-pdf" aria-hidden="true"></i></a></td>
                                            <td  class="text-center"><a href="#" onclick="DeleteFile('${item.documentPath}',this)" class="btn btn-danger btn-sm mr-2 btn_small"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>`
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
            alert('Please select document')
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

    $('#txtMobileNumber').focusout(function () {
        var regex = /^([+]\d{2}[ ])?\d{10}$/;
     if (regex.test($(this).val())) {
            isValid = true;
            $('#txtMobileNumber').removeClass('is-invalid');
        }
        else {
            isValid = false;
            $('#txtMobileNumber').addClass('is-invalid');

       }
    });
    window.deleteRow = function (btn) {
        debugger;
        var row = $(btn).closest('tr');
        var index = row.data('index');
        CNDArray.splice(index, 1);
        row.remove();
    };
});

// add by rajan 14/04/25  
function formatDateForInput(dateStr) {
    if (!dateStr) return '';
    const date = new Date(dateStr);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}

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
                        $('#txtAccountHolderName').val(response.accountHolderName);
                        $('#txtMedicalCardHolderName').val(response.accountHolderName);
                        $('#txtAccountNumber').val(response.accountNumber);
                        $('#txtBankName').val(response.bankName);
                        $('#txtBranchName').val(response.branchName);
                       // Set DOB
                        const dob = formatDateForInput(response.dateOfBirth);
                        $('#txtDateOfBirth').val(dob);

                        // Calculate and set Age
                        if (dob) {
                            var age = calculateAge(new Date(dob));
                            $('#txtAge').val(age);
                        }
                        $('#txtEmployeeNumber').val(response.employeeNumber);
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

//Self Invoking function to populate the table when page loads
(function () {
    $(".showDependent").hide();
    $(".claim").hide();
    $(".show_submit").hide();
})();



function UpdateCashlessIsDelete(id) {

    $.ajax({
        type: 'POST',
        url: "/Mediclaim/NonCashless/Delete/" + id,
        data: '',
        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (response, status, xhr) {
            if (xhr.status == '200') {
                if (response == "Success") {

                    //$(chkid).removeAttr("disabled");

                    //setTimeout(function () {
                    //    window.location.href = "../NonCashlessClaims";
                    //}, 2000);
                    //window.location.href = "../NonCashless";
                    window.location.href = "/Mediclaim/NonCashless"
                }
                else {
                }
            }
        },
        error: function (xhr, status, error) {

        }
    });
}

function DeleteFile(currentfile, row) {
    if (confirm("Are you sure you want to delete the user?")) {
        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Raise/DeleteFile/NonCashless",
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

//function deleteRow(rw) {
//    $(rw).parents("tr").remove();
//}

function showLoader() {
    //$('.preloader').css()
    $(".preloader").css("height", "100%");
    $(".preloader").css("display", "block");
    $(".preloader img").css("display", "block");
};

function hideLoader() {
    $(".preloader").css("height", "0");
    $(".preloader img").css("display", "none");
};