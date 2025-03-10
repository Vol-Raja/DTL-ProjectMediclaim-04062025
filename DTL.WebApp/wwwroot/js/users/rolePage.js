$(function () {
    $('#rolePageTbl').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});


function AddPageAccess() {
    $.ajax({
        url: "/RolePageAccess/AddPageAccess",
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
                $("#modalTitle").text("Add Page Access");
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
            var rolePage = $("#addEditRolePageForm").serialize()
            rolePage += "&PageName=" + $.map($(':checkbox[name="page"]:checked'), function (n, i) {
                return n.value;
            }).join(',');
            $.ajax({
                url: "/RolePageAccess/AddEditRolePages",
                type: "POST",
                dataType: "html",
                data: rolePage,
                success: function (data) {
                    if (data == "error") {
                        toastrError("Something went wrong!!!");
                        return false;
                    }
                    if (data != null) {
                      
                            toastrSuccess(data != "add" ? "Page access has updated!!!" : "Page access has added!!!");
                            window.location.reload();
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
    $('#addEditRolePageForm').validate({
        rules: {

            roleName: {
                required: true,
            }
        },
        messages: {

            roleName: {
                required: "Please select role name.",
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

function EditRolePageAccess(rolePageId) {
    $.ajax({
        url: "/RolePageAccess/GetRolePagesById",
        type: "POST",
        dataType: "html",
        data: { rolePageId: rolePageId },
        success: function (data) {
            
            if (data == "Error") {
                toastrError("Something went wrong!!!");
                return false;
            }
            if (data != null) {
                $("#modalBodyDiv").html(data);
                $("#addEditRoleModal").modal("show");
                $("#RoleName").prop("readonly", true);
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

function DeleteRolePageAccess(rolePageId) {
    $.ajax({
        url: "/RolePageAccess/DeleteRolePages",
        type: "POST",
        dataType: "json",
        data: { rolePageId: rolePageId },
        success: function (data) {
            if (data) {
                toastrSuccess("Page Access has deleted successfully!!!");
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