$(document).ready(function () {
    var ClaimId = '';
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
    var DocumentsArray = [];
    var CNDArray = [];
    var CNDId = 0;
    var isvalid = false;
    var srNo = 1;
    var DependentId = '';
    var DependentName = '';
    var DependentDob = '';
    var DependentAge = '';
    var RelationWithRetire = '';


    $('#btnSubmit').click(function () {
        PopulateNonCashlessProperties();        
        if (ClaimType !== '') { 
            //console.log();
            if (CNDArray.length <= 0) {
                alert('Add the Hopitalization Detail by clicking "Add" button.');
                isvalid = false;
            }
        }
        else {
            alert("Please fill the claim details");
            isvalid = false;
            return;
        }
        if (DocumentsArray.length <= 0) {
            alert("Please upload document");
            isvalid = false;
            return;
        }

        let _dependent = null;
        if (ClaimFor === 'Dependent') {
            _dependent = {
                Id: DependentId,
                Name: DependentName,
                DateOfBirth: DependentDob,
                Age: DependentAge,
                RelationWithRetire: RelationWithRetire
            }
        }

        var request = {
            ClaimId : ClaimId,
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
            ClaimStatusId : 1,            
            OPDCNDList: CNDArray,
            Dependent: _dependent
        }       
        showLoader();
        if (isvalid) {
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/Modify/NonCashless",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        hideLoader();
                        $(".hide_submit").hide();
                        $(".show_submit").show();
                        $('#exampleModal').modal('show');

                        setTimeout(function () {
                            window.location.href = "/Mediclaim/NonCashless"
                        }, 3000)
                    }
                },
                error: function (xhr, status, error) {
                    hideLoader();
                }
            });
        }
        else {
            hideLoader();
        }
    });

    var PopulateNonCashlessProperties = function () {
         
        ClaimId = $('#hdnMediClaimId').val();
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
        Organization = $('#selOrganization').val();
        AccountHolderName = $('#txtAccountHolderName').val();
        AccountNumber = $('#txtAccountNumber').val();
        BankName = $('#txtBankName').val();
        BICCode = $('#txtBICCode').val();
        IFSCNumber = $('#txtIFSCNumber').val();
        BranchName = $('#txtBranchName').val();
        DependentName = $('#txtDependentName').val();
        DependentDob = $('#txtDependentDob').val();
        DependentAge = $('#txtDependentAge').val();
        RelationWithRetire = $("#selRelatioWithRetire option:selected").text();

        $("#tblDocument tbody tr").each(function () {

            DocumentsArray.push(JSON.parse($(this).find('td input.hdndoc').val()));
        });

        isvalid = true;
    }

    var LoadNonCashlessDetailForEdit = function () {
        showLoader();
        ClaimId = $('#hdnMediClaimId').val();
        $.ajax({
            type: 'GET',
            url: '/Mediclaim/Modify/NonCashless/' + $('#hdnMediClaimId').val(),
            data: "",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
             
                $('#tblDocument tbody').empty();
                $('#txtEmployeeNumber').val(response.employeeNumber);
                $('#txtPPONumber').val(response.ppoNumber);
                $('#txtMedicalSectionPageNumber').val(response.medicalSectionPageNumber);
                $('#txtMedicalCardHolderName').val(response.medicalCardHolderName);
                $('#txtMedicalCardNo').val(response.medicalCardNo);
                $('#txtDesignation').val(response.designation);
                $('#txtPatientName').val(response.patientName);
                if (response.gender === 'Male') {
                    $('#rdMale').prop('checked', true);
                }
                else if (response.gender === 'Female'){
                    $('#rdFemale').prop('checked', true);
                }
                else {
                    $('#rdOther').prop('checked', true);
                }
                $('#txtDateOfBirth').val(response.dateOfBirthYYYYMMDD);
                $('#txtAge').val(response.age);
                if (response.claimFor === 'Dependent') {
                    console.log(response.relationWithRetire);
                    $('#dependent').prop('checked', true);
                    $(".showDependent").show();
                    switch (response.relationWithRetire) {
                        case 'Self': $("#selRelatioWithRetire").prop("selectedIndex", 1).val(); break;
                        case 'Spouse': $("#selRelatioWithRetire").prop("selectedIndex", 2).val(); break;
                        case 'Parents': $("#selRelatioWithRetire").prop("selectedIndex", 3).val(); break;
                        case 'Child': $("#selRelatioWithRetire").prop("selectedIndex", 4).val(); break;
                        default: $("#selRelatioWithRetire").prop("selectedIndex", 0).val(); break;
                    }
                    DependentId = response.dependent.id;
                    $('#txtDependentName').val(response.dependent.name);
                    $('#txtDependentDob').val(response.dependent.dateOfBirthYYYYMMDD);
                    $('#txtDependentAge').val(response.dependent.age);
                    $("#selRelatioWithRetire").val(response.dependent.relationWithRetire);

                }
                else {
                    $('#self').prop('checked', true);
                    $(".showDependent").hide();
                }
                $('#txtMobileNumber').val(response.mobileNumber);
                $('#txtEmailId').val(response.emailId);
                $("#selCardCategory").val(response.cardCategory);
                switch (response.cardCategory)
                {
                    case 'General': $("#selCardCategory").prop("selectedIndex", 1).val(); break;                   
                    case 'Private': $("#selCardCategory").prop("selectedIndex", 2).val(); break;
                    case 'Semi Private': $("#selCardCategory").prop("selectedIndex", 3).val(); break;
                    default: $("#selCardCategory").prop("selectedIndex", 0).val(); break;
                }
                $('#txtAddress').val(response.address);
                $("#selClaimType").val(response.claimType);

                if (response.opdcndList.length > 0) {
                    CNDId = response.opdcndList[0].opdcndId;
                    $('#txtCNDDate').val(response.opdcndList[0].opdcndDateYYYYMMDD);
                    $('#txtCNDHospitalName').val(response.opdcndList[0].hospitalName);
                    $('#txtCNDMedicineAmount').val(response.opdcndList[0].medicineAmount);
                    $('#txtCNDInvestigationAmount').val(response.opdcndList[0].investigationAmount);
                    $('#txtCNDConsultationAmount').val(response.opdcndList[0].consultationAmount);
                    $('#txtCNDTotalAmount').val(response.opdcndList[0].totalAmount);
                    $('#txtCNDOtherAmount').val(response.opdcndList[0].otherAmount);
                    HospitalizationClaimType: response.opdcndList[0].hospitalizationClaimType;
                    NonCashlessClaimId: response.opdcndList[0].nonCashlessClaimId;
                };

                $('#txtEmail').val(response.emailId);
                $('#selOrganization').val(response.organization);
                $('#txtAccountHolderName').val(response.accountHolderName);
                $('#txtAccountNumber').val(response.accountNumber);
                $('#txtBankName').val(response.bankName);
                $('#txtBICCode').val(response.bicCode),
                $('#txtIFSCNumber').val(response.ifscNumber);
                $('#txtBranchName').val(response.branchName);


                if (response.documents.length > 0) {
                    $.each(response.documents, function (index, item) {
                        var _hdnItem = JSON.stringify(item);
                        var row = `<tr>
                                            <td>
                                                ${srNo}
                                                <input type="hidden" class="hdndoc" value='${_hdnItem}'>
                                            </td>
                                            <td width="65%">${item.documentFor}</td>
                                            <td><a href="${'/' + item.documentPath}" class="pdfIcons" target='_blank'><i class="fa fa-file-pdf" aria-hidden="true"></i></a></td>
                                            <td><a href="#" onclick="DeleteFile('${item.documentPath.replaceAll("\\", "|")}','${item.documentId}',this)" class="btn btn-danger btn-sm mr-2 btn_small"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>`
                        $('#tblDocument tbody').append(row);
                        srNo = srNo + 1;
                    });
                };

                hideLoader();
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    }
      

    $('#btnAddCND').click(function () {
        //Populate the fields
        CNDArray = [];
        if (PopulateCNDProperties()) {
            //Empty all elements from table
            $('#tblCND tbody').empty();
            var total = 0;
            //If there are elements in array 
            //Add those to table
            if (CNDArray.length > 0) {
                //If there are elements in array 
                //Add those to table
                $.each(CNDArray, function (index, item) {
                    total = total + parseInt(item.TotalAmount, 10);
                    var row = '<tr><td> '
                        + formatDate(item.OPDCNDDate) + '</td><td>'
                        + item.HospitalName + '</td><td>'
                        + item.MedicineAmount + '<span class="text-danger req_text">₹ </span></td><td>'
                        + item.ConsultationAmount + '<span class="text-danger req_text">₹ </span></td><td>'
                        + item.InvestigationAmount + '<span class="text-danger req_text">₹ </span></td><td>'
                        + item.OtherAmount + '<span class="text-danger req_text">₹ </span></td><td>'
                        + item.TotalAmount + '<span class="text-danger req_text">₹ </span></td><td>'
                        + '<button type="button"  class="btn btn-danger btn-sm mr-2 btn_small" onclick="deleteRow(this)"><i class="fa fa-trash" aria-hidden="true"></i></button></td><tr>'

                    $("#tblCND tbody").append(row);
                });
            }
            ResetCND();
        }
    });

    var PopulateCNDProperties = function () {
        var opd = {
            OPDCNDId: CNDId,
            OPDCNDDate: $('#txtCNDDate').val(),
            HospitalName: $('#txtCNDHospitalName').val(),
            MedicineAmount: $('#txtCNDMedicineAmount').val(),
            InvestigationAmount: $('#txtCNDInvestigationAmount').val(),
            ConsultationAmount: $('#txtCNDConsultationAmount').val(),
            TotalAmount: $('#txtCNDTotalAmount').val(),
            OtherAmount: $('#txtCNDOtherAmount').val(),
            HospitalizationClaimType: 'CND',
            NonCashlessClaimId: ClaimId
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
        $('#CD_div').find('input:text').val('');
        $('#txtCNDDate').val('');
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
        window.open(window.location.href = '/Mediclaim/NonCashless/PrintPreview/'+ $('#hdnMediClaimId').val(), '_blank');
    });
    $('#btnViewPrintLink').click(function () { 
        window.open(window.location.href = '/Mediclaim/NonCashless/PrintPreview/6'+ $('#hdnMediClaimId').val(), '_blank');
    });

    $('.number').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    $('#txtDateOfBirth').change(function () {
        //alert('Toing');
        var currentDate = new Date();
        var dobDate = new Date($('#txtDateOfBirth').val());
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


    $('#txtDependentDob').change(function () {
        //alert('Toing');
        var currentDate = new Date();
        var dobDate = new Date($('#txtDependentDob').val());
        var age1 = currentDate.getFullYear() - dobDate.getFullYear();
        $('#txtDependentAge').val(age1);
    });

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
                                            <td>${item.documentFor}</td>
                                            <td><a href="${'/' + item.documentPath}" class="pdfIcons" target='_blank'><i class="fa fa-file-pdf" aria-hidden="true"></i></a></td>
                                            <td><a onclick="DeleteFile('${item.documentPath}','${item.documentId}',this)" class="btn btn-danger btn-sm mr-2 btn_small"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>`
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

   //Self Invoking function to populate the table when page loads
   (function () {
       $(".showDependent").hide();
       $(".claim").hide();
       $(".show_submit").hide();
       LoadNonCashlessDetailForEdit();
   })();
});

function DeleteFile(currentfilePath, documentId, row) {
    if (confirm("Are you sure you want to delete the user?")) {
        showLoader();
        $.ajax({
            type: 'POST',
            url: "/Mediclaim/Modify/DeleteFile/NonCashless/" + documentId,
            data: JSON.stringify(currentfilePath.replaceAll("|", "\\")),
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

function deleteRow(rw) {
    $(rw).parents("tr").remove();
}