$(document).ready(function () {
    $('#btnSubmit').click(function () {
        //debugger;
        validateEntryForm();
        if ($("#medicalPageForm").valid()) {
            var request = {
                PageDetailId: $('#hdnMedicalPageDetailId').val(),
                PageNumber: $('#txtPageNumber').val(),
                EmployeeNumber: $('#txtEmployeeNumber').val(),
                Name: $('#txtName').val(),
                PPONumber: $('#txtPPONumber').val(),
                Designation: $('#txtDesignation').val(),
                Department: $('#txtDepartment').val(),
                CardCategory: $("#selCardCategory option:selected").text(),
                MobileNumber: $('#txtMobileNumber').val(),
                DateOfRetirement: $('#txtDateOfRetirement').val(),
                SpouseName: $('#txtSpouseName').val(),
                LBP: $('#txtLBP').val(),
                Dispensary: $('#txtDispensary').val(),
                NameOfDependent: $('#txtNameOfDependent').val(),
                RelationWithRetiree: $('#txtRelationWithRetiree').val(),
                DependentDob : $('#dtDependentDob').val()
            }; 
            $.ajax({
                type: 'POST',
                url: "/Mediclaim/MedicalPageDetail/Edit",
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        if (response !== null || response !== undefined) { 
                            $('#medicalPageModal').modal('show');
                        }
                        //window.location.href = "../MedicalPageDetail";
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
        $('#medicalPageForm').validate({
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

    $('#btnViewPrint').click(function () {
        window.open(window.location.href = '/Mediclaim/MedicalPageDetail/PrintPreview/' + $('#hdnMedicalPageDetailId').val(), '_blank');
    });

    $('#btnReset').click(function () {
        $("#medicalPageForm").validate().resetForm();
        //$("input[type=checkbox]").prop('checked', false);
    });

    var GetMedicalPageDetailForEdit = function () {
        var pageDetailId = $('#hdnMedicalPageDetailId').val();
        $.ajax({
            type: 'GET',
            url: "/Mediclaim/MedicalPageDetail/GetMedicalPageDetailForEdit/" + pageDetailId,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (xhr.status == '200') {
                    if (response != null || response != undefined) {
                        console.log(response);
                        $('#txtPageNumber').val(response.pageNumber);
                        $('#txtEmployeeNumber').val(response.employeeNumber);
                        $('#txtName').val(response.name);
                        $('#txtPPONumber').val(response.ppoNumber);
                        $('#txtDesignation').val(response.designation);
                        $('#txtDepartment').val(response.department);
                        $("#selCardCategory").val(response.cardCategory);
                        $('#txtMobileNumber').val(response.mobileNumber);
                        $('#txtDateOfRetirement').val(response.dateOfRetirementYYYYMMDD);
                        $('#txtSpouseName').val(response.spouseName);
                        $('#txtLBP').val(response.lbp);
                        $('#txtDispensary').val(response.dispensary);
                        $('#txtNameOfDependent').val(response.nameOfDependent);
                        $('#txtRelationWithRetiree').val(response.relationWithRetiree);
                        $('#dtDependentDob').val(response.dependentDobYYYYMMDD);
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
        GetMedicalPageDetailForEdit();
    })();
});
