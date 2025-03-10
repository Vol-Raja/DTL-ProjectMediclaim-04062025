$(document).ready(function () {

    var adminuserid = 0;
    var name = '';
    var username = '';
    var emailaddress = '';
    var phonenumber = '';
    var isValid = false;

    var PopulateProperties = function () {
        adminuserid = $('#hdnAdminUserId').val();
        name = $('#txtName').val();
        username = $('#txtUsername').val();
        emailaddress = $('#txtEmailAddress').val();
        phonenumber = $('#txtPhoneNumber').val();
    };

    var Validate = function () {
        var count = 0;
        if (name == '') {
            $('#txtName').addClass('is-invalid');
            count = count + 1;
        }
        else {
            $('#txtName').removeClass('is-invalid');
            count = count - 1;
        }

        if (emailaddress == '') {
            $('#txtEmailAddress').addClass('is-invalid');
            count = count + 1;
        }
        else {
            $('#txtEmailAddress').removeClass('is-invalid');
            count = count - 1;
        }

        if (phonenumber == '') {
            $('#txtPhoneNumber').addClass('is-invalid');
            count = count + 1;
        }
        else {
            $('#txtPhoneNumber').removeClass('is-invalid');
            count = count - 1;
        }
        if (username == '' || (/^[a-zA-Z0-9]+$/.test(username) == false && /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(username) == false)) {
            $('#txtUsername').addClass('is-invalid');
            count = count + 1;
        }
        else {
            $('#txtUsername').removeClass('is-invalid');
            count = count - 1;
        }
        $('#txtEmailAddress').focusout(function () {
            
                var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (regex.test($(this).val())) {
                    isValid = true;
                    $('#txtEmailAddress').removeClass('is-invalid');
                }
                else {
                    isValid = false;
                    $('#txtEmailAddress').addClass('is-invalid');
                }
            });
        return count < 0 ? true : false;
    };

    var LoadAdminUser = function (id) {
        $.ajax({
            type: 'GET',
            url: '/UserManagement/AdminUser/GetAdminUserById/' + id,
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
                        $('#txtUsername').val(response.userName);
                    }
                    else {
                    }
                }
            },
            error: function (xhr, status, error) {

            }
        });
    };

    $('#btnSubmit').click(function () {
        PopulateProperties();
        var isAdmin = $('#hdnRole')?.val() == "Admin";
        if (Validate() === true) {
            var url = adminuserid !== '0' ? '/UserManagement/AdminUser/Update' : '/UserManagement/AdminUser/Create';

            if (isAdmin == true) {
                url = "/home/UpdateAdminUser"
            }
            var request = {
                AdminUserid: adminuserid !== '' ? adminuserid : 0,
                Name: name,
                EmailAddress: emailaddress,
                PhoneNumber: phonenumber,
                UserName: username,
            };
            let IsNew = adminuserid == '0';
            let successMsg = IsNew ? "Data saved successfully" : "Data updated successfully";
            let msg = IsNew ? 'Are you sure you want submit?' : 'Are you sure you want update?';

            showConfirmSwal(msg, function () {

                showLoader();

                $.ajax({
                    type: 'POST',
                    url: url,
                    data: JSON.stringify(request),
                    contentType: 'application/json; charset=utf-8',
                    //dataType: 'json',
                    success: function (response, status, xhr) {
                        if (xhr.status == '200') {
                            hideLoader();
                            if (response == 'userExists') {
                                toastrWarning("Email Id is already used");
                                return;
                            } if (response == 'userNameExists') {
                                toastrWarning("Username is already used");
                                return;
                            }
                            else {
                                $('#hdnAdminUserId').val(response)
                            }
                        }

                        setTimeout(function () {
                            if (isAdmin === true)
                                window.location.href = '/home';
                            else
                                window.location.href = '/UserManagement/AdminUser/AdminUserList';

                        }, 2000);
                        showSuccessSwal(successMsg);
                    },
                    error: function (xhr, status, error) {
                        hideLoader();
                        showErrorSwal('Error:' + error)

                    }
                });
            });
        }

    });

    //Self Invoking function to populate the table when page loads
    (function () {
        //$(".showDependent").hide();
        var mode = $('#hdnadminUserFormMode').val();
        if (mode === 'Edit' || mode === "view") {
            LoadAdminUser($('#hdnAdminUserId').val());
        }
    })();
});



function DeleteAdminUser(Id, rw) {


    $.ajax('/UserManagement/AdminUser/Delete/' + Id, {
        type: 'DELETE',  // http method
        data: '',  // data to submit
        success: function (data, status, xhr) {

            if (xhr.status == "200") {
                showSuccessSwal("Admin user deleted successfully!");
                setTimeout(function () {
                    window.location.reload();
                }, 4000);
            }

        },
        error: function (jqXhr, textStatus, errorMessage) {
            showErrorSwal('Error-' + errorMessage);
        }
    });

}

function showLoader() {
    //$('.preloader').css()
    $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
    $(".preloader img").css({ "display": "block" });
};

function hideLoader() {
    $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
    $(".preloader img").css({ "display": "none" });
};
