using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;

namespace DTL.Business.UserManagement
{
    public interface IAdminUser
    {
        int AddAdminUser(AdminUserModel adminUserModel);
        IEnumerable<AdminUserModel> GetAdminUserByParam(int? adminuserid, string name, string emailaddress, string phonenumber);
        IEnumerable<AdminUserModel> GetArchiveAdminUser();
        void AdminUserToggleIsDeleteFlag(int adminuserid, bool isDeleteFlag, string modifiedBy);
        int UpdateAdminUser(AdminUserModel adminUserModel);
        int DeleteAdminUserPermanently(int adminuserId);
        AdminUserModel GetAdminUserByUsername(string username);

    }
}
