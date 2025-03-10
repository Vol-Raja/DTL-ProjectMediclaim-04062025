$(document).ready(function () {

    var assignpermissionid = 0;
    var userid = '';
    var modulename = '';
    var submodulename = '';
    var create = false;
    var view = false;
    var edit = false;
    var _delete = false;
    var usertype = ''
    var _count = 0;
    var PopulateProperties = function () {
        userid = $('#hdnUserId').val();
        modulename = $('#selModule').val();
        submodulename = $('#selSubModule').val();
        create = $('#create').is(":checked");
        view = $('#view').is(":checked");
        edit = $('#edit').is(":checked");
        _delete = $('#delete').is(":checked");
    };

    var Validate = function () {
        var count = 0;
        if (modulename == '') {
            $('#selModule').addClass('error');
            count = count + 1;
        }
        else {
            $('#selModule').removeClass('error');
            count = count - 1;
        }

        if (submodulename == '') {
            $('#selSubModule').addClass('error');
            count = count + 1;
        }
        else {
            $('#selSubModule').removeClass('error');
            count = count - 1;
        }
        hideLoader();
        return count < 0 ? true : false;
    };

    var LoadUsersPermission = function (userid) {
        $.ajax({
            type: 'GET',
            url: '/UserManagement/LoadAssignPermission/' + userid,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#tblPermission tbody').empty();
                if (response != null || response != undefined) {
                    if (response.length > 0) {
                        $.each(response, function (index, item) {

                            var _permissions = ''
                            if (item.create === true) {
                                _permissions = _permissions + 'Create';
                                _permissions = _permissions + ', ';
                            }
                            if (item.view === true) {
                                _permissions = _permissions + 'View';
                                _permissions = _permissions + ', ';
                            }
                            if (item.edit === true) {
                                _permissions = _permissions + 'Edit';
                                _permissions = _permissions + ', ';
                            }
                            if (item.delete === true) {
                                _permissions = _permissions + 'Delete';
                                _permissions = _permissions + ', ';
                            }

                            _permissions = _permissions.slice(0, -2);


                            var row = '<tr><td>'
                                + item.moduleName + '</td><td>'
                                + item.subModuleName + '</td><td>'
                                + _permissions + '</td><td>'
                                + '<a href="javascript:(void)" onclick="DeletePermission(' + item.assignPermissionId + ')">Delete Permission</a > ' + '</td ></tr>'

                            $('#tblPermission tbody').append(row);

                            _count = _count + 1;
                        });
                    }
                    else {
                        _count = 0;
                        var row = '<tr><td colspan="4">No Data Found</td></tr>';
                        $('#tblPermission tbody').append(row);
                    }
                }
            },
            error: function (xhr, status, error) {

            }
        });
    };

    $('#selModule').change(function () {
        LoadSubModule($('#selModule').val());
    })

    var LoadSubModule = function (modulename) {
        showLoader();
        $.ajax({
            type: 'GET',
            url: '/UserManagement/AssignPermission/Submodule/' + modulename,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                $('#selSubModule').empty();
                if (response != null || response != undefined) {
                    $('#selSubModule').append('<option value="">Select</option>');
                    if (response.length > 0) {
                        $.each(response, function (index, item) {
                            var option = $('<option/>')
                            option.html(item.subModuleName);
                            option.val(item.subModuleName);
                            $('#selSubModule').append(option);
                        });
                        hideLoader();
                    }
                    else {
                        var option = $('<option/>')
                        option.html('NA');
                        option.val('NA');
                        $('#selSubModule').append(option);
                        hideLoader();
                        toastrWarning('No submodule found');
                    }
                }
            },
            error: function (xhr, status, error) {
                hideLoader();
            }
        });
    }

    $('#btnAddNew').click(function () {        
        PopulateProperties();
        if (Validate() === true) {
            //var url = _count > 0 ? '/UserManagement/AssignPermission/Update' : '/UserManagement/AssignPermission/Create';
            var url = '/UserManagement/AssignPermission/';
            var request = {
                AssignPermissionId: assignpermissionid,
                ModuleName: modulename,
                SubModuleName: submodulename,
                Create: create,
                View: view,
                Edit: edit,
                Delete: _delete,
                UserId: userid
            };
            showLoader();
            $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        LoadUsersPermission(userid);
                        hideLoader();
                        toastrSuccess("Permission Assigned");
                        ResetFields();
                        return;
                    }
                    //setTimeout(function () {
                    //    window.location.href = '/UserManagement//AdminUserList';
                    //}, 1800);
                },
                error: function (xhr, status, error) {
                    hideLoader();
                    ResetFields();
                    toastrError("Something went wrong!!!")
                }
            });
        }
    });

    $('#btnReset').click(function () {
        ResetFields();
        ResetDropdownlist();
    });

    var ResetDropdownlist = function () {
        $('#selModule').val('');
        $('#selSubModule').val('');
    }
    var ResetFields = function () {
        //$('#selModule').val('');
        //$('#selSubModule').val('');
        $('#create').prop('checked', false);
        $('#view').prop('checked', false);
        $('#edit').prop('checked', false);
        $('#delete').prop('checked', false);
    }


    var showLoader = function () {
        //$('.preloader').css()
        $(".preloader").css({ "height": "100vh", "background": "#000", "opacity": "0.8" });
        $(".preloader img").css({ "display": "block" });
    };

    var hideLoader = function () {
        $(".preloader").css({ "height": "0px", "background": "#f4f6f9", "opacity": "1" });
        $(".preloader img").css({ "display": "none" });
    };

    //Self Invoking function to populate the table when page loads
    (function () {
        usertype = $('#hdnUserType').val();
        LoadUsersPermission($('#hdnUserId').val());

    })();
});

function DeletePermission(Id) {

    if (confirm("Are you sure you want to delete the user?"))

        $.ajax('/UserManagement/AssignPermission/Delete/' + Id, {
            type: 'DELETE',  // http method
            data: '',  // data to submit
            success: function (data, status, xhr) {

                if (xhr.status == "200") {
                    alert("Permission deleted successfully!")
                    /*var href = $('#adel').attr('href');
                    
                    window.location.href = href;*/
                    // $(rw).parents("tr").remove();
                    setTimeout(function () { window.location.reload(); }, 700)
                }

            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('p').append('Error' + errorMessage);
            }
        });
    else
        alert("Canceled Data Deletion...!!!");
}
