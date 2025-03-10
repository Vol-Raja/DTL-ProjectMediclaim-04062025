$(function () {
    //$("#example1").DataTable({
    //    "responsive": true, "lengthChange": false, "autoWidth": false,
    //    "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    //}).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#example2').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});


function AddUser() {
    $.ajax({
        url: "/Users/AddUser",
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data == "Error") {
                toastrError("Something went wrong!!!");
                return false;
            }
            if (data != null) {
                $("#modalBodyDiv").html(data);
                $("#modalTitle").text("Add User");
                $("#addEditUserModal").modal("show");
                AddEditForm();
            } else {
                toastrWarning("No record Found!!!");
            }
        },
        error: function (err) {
            toastrError("Something went wrong!!!");
        }
    })
}

function EditUser(userId) {
    $.ajax({
        url: "/Users/GetUserById",
        type: "POST",
        dataType: "html",
        data: { userId: userId },
        success: function (data) {
            if (data == "Error") {
                toastrError("Something went wrong!!!");
                return false;
            }
            if (data != null) {
                $("#modalBodyDiv").html(data);
                $("#addEditUserModal").modal("show");
                AddEditForm();
            } else {
                toastrWarning("No record Found!!!");
            }
        },
        error: function (err) {
            toastrError("Something went wrong!!!");
        }
    })
}

function AddEditForm() {
    $.validator.setDefaults({
        submitHandler: function () {
            //toastrSuccess("Form successful submitted!");
            var user = $("#addEditUserForm").serialize()
            $.ajax({
                url: "/Users/AddEditUser",
                type: "POST",
                dataType: "html",
                data: user,
                success: function (data) {
                    data = data.replace("\"", "").replace("\"", "");
                    if (data == "error") {
                        toastrError("Something went wrong!!!");
                        return false;
                    }
                    if (data == "addError") {
                        toastrError("Not able to add user. Try after sometime!!!");
                        return false;
                    }
                    if (data == "updateError") {
                        toastrError("Not able to update user. Try after sometime!!!");
                        return false;
                    }
                    if (data != null) {
                        if (data !== 'userExists') {
                            toastrSuccess(data != "add" ? "User is updated successfully!!!" : "User is added successfully!!!");
                            window.location.reload();
                        } else {
                            toastrWarning("Email Address already exists!!!");
                        }
                    } else {
                        toastrWarning("No record Found!!!");
                    }
                },
                error: function (err) {
                    toastrError("Something went wrong!!!");
                }
            })
        }
    });
    $('#addEditUserForm').validate({
        rules: {
            email: {
                required: true,
                email: true,
            },
            userName: {
                required: true,
            },
            phone: {
                required: true
            }
        },
        messages: {
            email: {
                required: "Please enter a email address",
                email: "Please enter a vaild email address"
            },
            userName: {
                required: "Please provide a username",
            },
            phone: {
                required: "Please provide a phone number",
            },
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
}

function DeleteUser(userId) {
    $.ajax({
        url: "/Users/DeleteUser",
        type: "POST",
        dataType: "json",
        data: { userId: userId },
        success: function (data) {
            if (data) {
                toastrSuccess("User has deleted successfully!!!");
            } else {
                toastrWarning("Not able to delete. Try again after sometime!!!");
            }
            window.location.reload();
        },
        error: function (err) {
            toastrError("Something went wrong!!!");
        }
    })
}