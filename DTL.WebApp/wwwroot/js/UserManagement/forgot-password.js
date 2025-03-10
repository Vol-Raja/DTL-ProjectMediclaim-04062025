
function GenereateOTP() {
    var email = $('#txtEmailAddress').val();
    if (email !== '') {
        $.ajax('/UserManagement/ForgotPassword/' + email + '/GenerateOtp', {
            type: 'POST',  // http method
            data: '',  // data to submit
            success: function (data, status, xhr) {
                console.log(data);
                if (xhr.status == "200") {
                    window.location.href = '/Identity/Account/OTP?v=' + email + '&c=' + data;
                } 
            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('p').append('Error' + errorMessage);
            }
        });
    }
    else
    alert("Please type in your valid Email address");
};

function VerifyOTP() { 
    const urlParams = new URLSearchParams(location.search);
    var email = urlParams.get('v');
    var token = urlParams.get('c');
    //for (const [key, value] of urlParams) { console.log(`${key}: ${value}`); }

    var _otp = $('#digit-1').val() + '' + $('#digit-2').val() + '' + $('#digit-3').val() + '' + $('#digit-4').val() + '' + $('#digit-5').val() + '' + $('#digit-6').val()

    var _request = {
        userEmail: email,
        otp: _otp,
        otpValidationToken : token
    }

    if (email !== '') {
        $.ajax('/UserManagement/ForgotPassword/' + email + '/VerifyOtp', {
            type: 'POST',  // http method
            data: JSON.stringify(_request),  // data to submit
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                if (xhr.status == "200") {
                    //window.location.href = '/Identity/Account/OTP?v=' + email + '&c=' + data;
                    window.location.href = '/UserManagement/ForgotPassword?v=' + email;
                }
            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('p').append('Error' + errorMessage);
            }
        });
    }
    else
        alert("Please type in your valid Email address");
};

function ResetPassword() {
    const urlParams = new URLSearchParams(location.search);
    var email = urlParams.get('v');
    var password = $('#txtpassword').val();
    var confirmpassword = $('#txtconfirmpassword').val();
    if (password !== '' && confirmpassword !== '') {

        if (password === confirmpassword) {
            var _request = {
                password: password,
                confirmPassword: confirmpassword
            }

            $.ajax('/UserManagement/ForgotPassword/' + email, {
                type: 'POST',  // http method
                data: JSON.stringify(_request),  // data to submit
                success: function (data, status, xhr) {
                    console.log(data);
                    if (xhr.status == "200") {
                        window.location.href = '/Login';
                    }
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    $('p').append('Error' + errorMessage);
                }
            });
        }
        else {
            alert('Password and Confirm Password does not match');
        }
    }
    else {
        alert('Please provide password and confirmpassword');
    }       
}