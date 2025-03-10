using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;

namespace DTL.Business.UserManagement
{
    public interface IAssignPermission
    {
        int AddAssignPermission(AssignPermissionModel assignPermissionModel);
        IEnumerable<AssignPermissionModel> GetAssignPermissionByParam(string userId, string modulename, string submodulename);
        IEnumerable<AssignPermissionModel> GetArchiveAssignPermission();
        void DeleteAssignPermission(int assignPermissionId);
        int UpdateAssignPermission(AssignPermissionModel assignPermissionModel);
    }
}
