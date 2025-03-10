$(document).ready(function () {

    var dvbuserid = 0;
    var name = '';
    var emailaddress = '';
    var phonenumber = '';
    var department = '';
    var designation = '';
    var username = '';
    var address = '';
    var dashboard = '';
    var isValid = false;

    var PopulateProperties = function () {
        dvbuserid = $('#hdndvbUserId').val();
        name = $('#txtName').val();
        emailaddress = $('#txtEmailAddress').val();
        phonenumber = $('#txtPhoneNumber').val();
        department = $('#Department').val();
        designation = $('#Designation').val();
        dashboard = $('#selDashboard').val();
        username = $('#txtUsername').val();
        address = $('#txtAddress').val();
    };

    var Validate = function () {
        var count = 0;
        if (name == '') {
            $('#txtName').addClass('error');
            count = count + 1;
        }
        else {
            $('#txtName').removeClass('error');
            count = count - 1;
        }

        if (emailaddress == '') {
            $('#txtEmailAddress').addClass('error');
            count = count + 1;
        }
        else {
            $('#txtEmailAddress').removeClass('error');
            count = count - 1;
        }

        if (phonenumber == '') {
            $('#txtPhoneNumber').addClass('error');
            count = count + 1;
        }
        else {
            $('#txtPhoneNumber').removeClass('error');
            count = count - 1;
        }

        if (department == '') {
            $('#selDepartment').addClass('error');
            count = count + 1;
        }
        else {
            $('#selDepartment').removeClass('error');
            count = count - 1;
        }

        if (designation == '') {
            $('#selDesignation').addClass('error');
            count = count + 1;
        }
        else {
            $('#selDesignation').removeClass('error');
            count = count - 1;
        }

        if (dashboard == '') {
            $('#selDashboard').addClass('error');
            count = count + 1;
        }
        else {
            $('#selDashboard').removeClass('error');
            count = count - 1;
        }

        if (username == '') {
            $('#txtUsername').addClass('error');
            count = count + 1;
        }
        else {
            $('#txtUsername').removeClass('error');
            count = count - 1;
        }

        if (address == '') {
            $('#txtAddress').addClass('error');
            count = count + 1;
        }
        else {
            $('#txtAddress').removeClass('error');
            count = count - 1;
        }

        return count < 0 ? true : false;
    };

    var LoadDVBUser = function (id) {
        $.ajax({
            type: 'GET',
            url: '/UserManagement/DVBUser/GetDVBUserById/' + id,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                console.log(response);
                if (xhr.status == '200') {
                    if (response != null || response != undefined) {
                        $('#txtName').val(response.name);
                        $('#txtEmailAddress').val(response.emailAddress);
                        $('#txtPhoneNumber').val(response.phoneNumber);
                        $('#selDepartment').val(response.department);
                        $('#selDesignation').val(response.designation);
                        LoadDashboard();
                        $('#selDashboard').val(response.dashboard);
                        $('#txtUsername').val(response.username);
                        $('#txtAddress').val(response.address);
                    }
                    else {
                    }
                }
            },
            error: function (xhr, status, error) {

            }
        });
    };

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#DVBUserForm").valid() == false)
            return false;

        let IsNew = $("#IsNew").val();
        let successMsg = IsNew == 'True' ? "Data saved successfully" : "Data updated successfully";
        let msg = IsNew == 'True' ? 'Are you sure you want submit?' : 'Are you sure you want update?';

        showConfirmSwal(msg, function () {
            var url = '/UserManagement/DVBUser/Create';
            var formData = BindDVBUserFormData();
            $.ajax({
                type: "POST",
                dataType: "json",
                data: formData,
                contentType: false,
                processData: false,
                url: url,

                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        hideLoader(response);
                        console.log(response);
                        if (response == "userExists") {
                            toastr.error("Email already used.");
                            return;
                        }

                        else if (response == "usernameExists") {
                            toastr.error("Username already used.");
                            return;
                        }

                        showSuccessSwal(successMsg);
                        setTimeout(function () {
                            window.location.href = '/UserManagement/DVBUser/DVBUserList';
                        }, 2000);
                        // $('#confirmationModal').modal('show');
                    }

                },
                error: function (xhr, status, error) {
                    hideLoader();
                    console.log(error);
                    showErrorSwal('Error' + error);
                    
                }
            });
        });
    });

    //Self Invoking function to populate the table when page loads
    (function () {
        //$(".showDependent").hide();
        var mode = $('#hdndvbUserFormMode').val();
        if (mode === 'Edit') {
            LoadDVBUser($('#hdndvbUserId').val());
        } 
    })();

    $("#Department").on('change', function () {
        var selected = (this.value);
        if (selected) {
            selected = selected.replaceAll(' ', '');
            //hide all
            $('.dashboardOption').hide();
            $('.' + selected).show();

        }
    });

     
});

function BindDVBUserFormData() {
    var frm = $('#DVBUserForm');
    var formData = new FormData(frm[0]);
    return formData;
}

$('#DVBUserForm').validate({
    rules: {
        Name: {
            required: true,
        },
        EmailAddress: {
            required: true,
            email: true

        },
        PhoneNumber: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 10

        },
        Department: {
            required: true,
        },
        Designation: {
            required: true,
        },
        Address: {
            required: true,
        },
        Username: {
            required: true,
            minlength: 6,
            regex: /^[a-zA-Z0-9]+$/,
        },
        DashboardUrl: {
            required: true,
        },
        Dashboard: {
            required: true,
        }

    },
    messages: {
        Name: {
            required: "Please Provied Name",
        },
        EmailAddress: {
            required: "Please Provied Email Address",

        },
        PhoneNumber: {
            required: "Please Provied Phone Number",

        },
        Department: {
            required: "Please Provied Department",
        },
        Designation: {
            required: "Please Provied Designation",
        },
        Address: {
            required: "Please Provied Address",
        },
        Username: {
            required: "Please Provied Username",
        },
        DashboardUrl: {
            required: "Please Provied Dashboard ",
        },
        Dashboard: {
            required: "Please Provied Dashboard",
        }

    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        error.insertAfter($(element));
        //element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }

});

$(".generateDVBUserCredential").click(function () {
    if (confirm("Are you sure you want to generate credential?")) {
        showLoader();
        $.ajax('/UserManagement/DVBUser/GenerateDVBUserCredentialById', {
            type: 'POST',  // http method
            data: { dvbUserId: $(this).attr("data-dvbUserId") },  // data to submit
            success: function (data, status, xhr) {
                hideLoader();
                if (data == "success") {
                    toastrSuccess("DVBUser credential has generated and sent to registerd email id and mobile number.");
                    setTimeout(function () { window.location.reload(); }, 5000)

                }
                else {
                    toastrWarning("Please try again. somehthing went wrong")
                }
            },
            error: function (jqXhr, textStatus, errorMessage) {
                hideLoader();
                showErrorSwal('Error-' + errorMessage);
            }
        });
    }
})

function LoadDashboard() {
    var dashboardFor = $('#selDepartment').val();
    if (dashboardFor != '') {
        showLoader();
        $.ajax({
            type: 'GET',
            url: '/UserManagement/DVBUser/Dashboard/' + dashboardFor,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                hideLoader();
                if (xhr.status == '200') {
                    if (response != null || response != undefined) {
                        $.each(response, function (data, value) {
                            $("#selDashboard").append($("<option></option>").val(value.dashboardId).html(value.dashboardName));
                        })
                    }
                    else {
                    }
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    }
    else {
        hideLoader();
        alert('Please select department');
    }
}

function DeleteDVBUser(Id, rw) {

    $.ajax('/UserManagement/DVBUser/Delete/' + Id, {
        type: 'DELETE',  // http method
        data: '',  // data to submit
        success: function (data, status, xhr) {

            if (xhr.status == "200") {
                $('#deleteModal').modal('hide');
                showSuccessSwal("DVBUser deleted successfully!");
                /*var href = $('#adel').attr('href');

                window.location.href = href;*/
                //$(rw).parents("tr").remove();
                $("#" + Id).remove();
            }

        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('#deleteModal').modal('hide');
            showErrorSwal('Error-' + errorMessage);

        }

    });
};

function showLoader() {
    //$('.preloader').css()
    $(".preloader").css({ "height": "100vh", "background": "#ffffffc9;" });
    $(".preloader img").css({ "display": "block" });
};

function hideLoader() {
    $(".preloader").css({ "display": "none" });
};
    
