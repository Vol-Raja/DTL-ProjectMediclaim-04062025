$(document).ready(function () {

    //var MedicalCardId = '';
    var CardNo = '';
    var EmployeeNo = '';
    var PPONo = '';
    var NameOfCardHolder = '';
    var Employer = '';
    var DateOfBirth = '';
    var Age = '';
    var Gender = '';
    var MedicalSectionPageNo = '';
    var CardCategory = '';
    var MobileNumber = '';
    var Address = '';
    var MedicalHistory = '';
    var BankName = '';
    var BICCode = '';
    var IFSCCode = '';
    var AccountNumber = '';
    var NameOfDependent = '';
    var RelationWithRetiree = '';
    var DependentDob = '';

    $('#btnSubmit').click(function () {
        //debugger;
        validateEntryForm();
        if ($("#medicalCardForm").valid()) {
            PopulateCardProperties();
            var request = {
                //MedicalCardId: MedicalCardId,
                CardNo: CardNo,
                EmployeeNo: EmployeeNo,
                PPONo: PPONo,
                NameOfCardHolder: NameOfCardHolder,
                Employer: Employer,
                DateOfBirth: DateOfBirth,
                Age: Age,
                Gender: Gender,
                MedicalSectionPageNo: MedicalSectionPageNo,
                CardCategory: CardCategory,
                MobileNumber: MobileNumber,
                Address: Address,
                MedicalHistory: MedicalHistory,
                BankName: BankName,
                BICCode: BICCode,
                IFSCCode: IFSCCode,
                AccountNumber: AccountNumber,
                NameOfDependent : NameOfDependent,
                RelationWithRetiree : RelationWithRetiree,
                DependentDob : DependentDob
            };

            console.log(request);
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/MedicalCard/SaveMedicalCard",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        if (response != null || response != undefined) {
                            $('#lblCardNumber').text(response)
                            $('#hdnMedicalCardId').val(response)
                            $('#cardModal').modal('show');
                        }
                        else {
                        }
                        //if (response !== null || response !== undefined) {
                        //$('#lblClaimNumber').text(hdnMediclaimVoucherId)
                        //$('#hdnMediclaimVoucherId').val(response)
                        //}
                        //$(".hide_submit").hide();
                        //$(".show_submit").show();
                    }
                },
                error: function (xhr, status, error) {

                }
            });
        }
    });

    var validateEntryForm = function () {
        /*$.validator.setDefaults({
            submitHandler: function () {
                alert("Form successful submitted!");
            }
        });*/
        $('#medicalCardForm').validate({
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

    var PopulateCardProperties = function () {
        CardNo = $('#txtCardNo').val();
        EmployeeNo = $('#txtEmployeeNo').val();
        PPONo = $('#txtPPONo').val();
        NameOfCardHolder = $('#txtNameOfCardHolder').val();
        Employer = $('#Employer').val();
        DateOfBirth = $('#dtDateOfBirth').val();
        Age = $('#txtAge').val();
        Gender = $('#selGender').val();
        MedicalSectionPageNo = $('#txtMedicalSectionPageNo').val();
        CardCategory = $('#selCardCategory').val();
        MobileNumber = $('#txtMobileNumber').val();
        Address = $('#txtAddress').val();
        MedicalHistory = $('#txtMedicalHistory').val();
        BankName = $('#txtBankName').val();
        BICCode = $('#txtBICCode').val();
        IFSCCode = $('#txtIFSCCode').val();
        AccountNumber = $('#txtAccountNumber').val();
        NameOfDependent = $('#txtNameOfDependent').val();
        RelationWithRetiree = $('#txtRelationWithRetiree').val();
        DependentDob = $('#dtDependentDob').val();
    };

    $('#btnViewPrint').click(function () {
        window.open(window.location.href = '/Mediclaim/MedicalCard/PrintPreview/' + $('#hdnMedicalCardId').val(), '_blank');
    });

    $('#btnReset').click(function () {
        $("#medicalCardForm").validate().resetForm();
        //$("input[type=checkbox]").prop('checked', false);
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


    //Self Invoking function to populate the table when page loads
    (function () {
        //$(".showDependent").hide();
        $(".claim").hide();
        $(".show_submit").hide();
    })();
});