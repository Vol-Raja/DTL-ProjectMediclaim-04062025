$(function () {
    $('#roleTbl').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});

function AddRole() {
    $.ajax({
        url: "/Roles/AddRole",
        type: "POST",
        dataType: "html",
        //data: { roleId: roleId },
        success: function (data) {
            if (data == "Error") {
                toastrError("Something went wrong!!!");
                return false;
            }
            if (data != null) {
                $("#modalBodyDiv").html(data);
                $("#modalTitle").text("Add Role");
                $("#addEditRoleModal").modal("show");
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

function EditRole(roleId) {
    $.ajax({
        url: "/Roles/GetRoleById",
        type: "POST",
        dataType: "html",
        data: { roleId: roleId },
        success: function (data) {
            if (data == "Error") {
                toastrError("Something went wrong!!!");
                return false;
            }
            if (data != null) {
                $("#modalBodyDiv").html(data);
                $("#modalTitle").text("Edit Role");
                $("#addEditRoleModal").modal("show");
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
            var role = $("#addEditRoleForm").serialize()
            $.ajax({
                url: "/Roles/AddEditRole",
                type: "POST",
                dataType: "html",
                data: role,
                success: function (data) {
                    if (data == "error") {
                        toastrError("Something went wrong!!!");
                        return false;
                    }
                    if (data != null) {
                        if (data !== 'duplicate') {
                            toastrSuccess(data != "add" ? "Role is updated successfully!!!" : "Role is added successfully!!!");
                            window.location.reload();
                        } else {
                            toastrWarning("Role name is existing!!!");
                            window.location.reload();
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
    $('#addEditRoleForm').validate({
        rules: {

            name: {
                required: true,
            }
        },
        messages: {

            name: {
                required: "Please provide a username",
            }
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


function DeleteRole(roleId) {
    $.ajax({
        url: "/Roles/DeleteRole",
        type: "POST",
        dataType: "json",
        data: { roleId: roleId },
        success: function (data) {
            if (data) {
                toastrSuccess("Role has deleted successfully!!!");
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