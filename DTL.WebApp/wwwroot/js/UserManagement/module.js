$(document).ready(function () {

    var moduleid = 0;
    var modulename = '';
    var isValid = false;

    var PopulateProperties = function () {
        moduleid = $('#hdnmoduleid').val();
        modulename = $('#txtModuleName').val();
    };

    var Validate = function () {
        var count = 0;
        if (modulename == '') {
            $('#txtModuleName').addClass('error');
            count = count + 1;
        }
        else {
            $('#txtModuleName').removeClass('error');
            count = count - 1;
        }

        return count < 0 ? true : false;
    };

    var LoadModule = function (id) {
        $.ajax({
            type: 'GET',
            url: '/UserManagement/Module/GetModuleById/' + id,
            data: '',
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (response, status, xhr) {
                if (response != null || response != undefined) {
                    //$('#hdnmoduleid').val(response.moduleId);
                    $('#txtModuleName').val(response.moduleName);
                }
            },
            error: function (xhr, status, error) {

            }
        });
    };

    $('#btnSubmit').click(function () {
        //debugger;
        PopulateProperties();
        if (Validate() === true) {
            var url = moduleid !== '0' ? '/UserManagement/Module/Update' : '/UserManagement/Module/Create';
            var request = {
                ModuleId: moduleid !== '' ? moduleid : 0,
                ModuleName: modulename
            };
            $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify(request),
                contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                success: function (response, status, xhr) {
                    if (xhr.status == '200') {
                        $('#hdnmoduleid').val(response);
                            toastrSuccess("Success");
                    }
                    setTimeout(function () {
                        window.location.href = '/UserManagement/Module/ModuleList';
                    }, 900);
                },
                error: function (xhr, status, error) {

                }
            });
        }
    });

    //Self Invoking function to populate the table when page loads
    (function () {
        //$(".showDependent").hide();
        var mode = $('#hdnmoduleFormMode').val();
        if (mode === 'Edit') {
            LoadModule($('#hdnmoduleid').val());
        }
    })();
});

function DeleteModule(Id, rw) {
    if (confirm("Are you sure you want to delete the module?"))

        $.ajax('/UserManagement/Module/Delete/' + Id, {
            type: 'DELETE',  // http method
            data: '',  // data to submit
            success: function (data, status, xhr) {

                if (xhr.status == "200") {
                    alert("Module deleted successfully!")
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
