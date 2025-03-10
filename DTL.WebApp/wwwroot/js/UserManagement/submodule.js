$(document).ready(function () {

    var submoduleid = 0;
    var modulename = '';
    var submodulename = '';
    var isValid = false;

    var PopulateProperties = function () {
        submoduleid = $('#hdnsubmoduleid').val();
        modulename = $('#selModule').val();
        submodulename = $('#txtsubModuleName').val();
    };

    var Validate = function () {
        var count = 0;
        if (modulename == '') {
            $('#selModule').addClass('error');
            count = count + 1;
        }
        else {
            $('#selmoduleName').removeClass('error');
            count = count - 1;
        }

        if(submodulename == '') {
            $('#txtsubModuleName').addClass('error');
            count = count + 1;
        }
        else {
            $('#txtsubModuleName').removeClass('error');
        count = count - 1;
    }

        return count < 0 ? true : false;
    };

    var LoadSubModule = function (id) {
        $.ajax({
            type: 'GET',
            url: '/UserManagement/SubModule/GetSubModuleById/' + id,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (response != null || response != undefined) {
                    //$('#hdnmoduleid').val(response.moduleId);
                    $('#selModule').val(response.moduleName);
                    $('#txtsubModuleName').val(response.subModuleName);
                }
            },
            error: function (xhr, status, error) {

            }
        });
    };

    $('#btnSubmit').click(function () {
        PopulateProperties();
        if (Validate() === true) {
            var url = submoduleid !== '0' ? '/UserManagement/SubModule/Update' : '/UserManagement/SubModule/Create';
            var request = {
                SubModuleId: submoduleid !== '' ? submoduleid : 0,
                ModuleName: modulename,
                SubModuleName: submodulename
            };
            $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        $('#hdnsubmoduleid').val(response);
                            toastrSuccess("Success");
                    }
                    setTimeout(function () {
                        window.location.href = '/UserManagement/SubModule/SubModuleList';
                    }, 900);
                },
                error: function (xhr, status, error) {
                    toastrError(error);
                }
            });
        }
    });

    //Self Invoking function to populate the table when page loads
    (function () {
        //$(".showDependent").hide();
        var mode = $('#hdnsubmoduleFormMode').val();
        if (mode === 'Edit') {
            LoadSubModule($('#hdnsubmoduleid').val());
        }
    })();
});

function DeleteSubModule(Id, rw) {
    if (confirm("Are you sure you want to delete the module?"))

        $.ajax('/UserManagement/SubModule/Delete/' + Id, {
            type: 'DELETE',  // http method
            data: '',  // data to submit
            success: function (data, status, xhr) {

                if (xhr.status == "200") {
                    alert("SubModule deleted successfully!")
                    /*var href = $('#adel').attr('href');

                    window.location.href = href;*/
                    $(rw).parents("tr").remove();
                }

            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('p').append('Error' + errorMessage);
            }
        });
    else
        alert("Canceled Data Deletion...!!!");
}
