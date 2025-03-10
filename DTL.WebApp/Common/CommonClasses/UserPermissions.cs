using DTL.Model.Models.UserManagement;
using System.Collections.Generic;

namespace DTL.WebApp.Common.CommonClasses
{
    public static class UserPermissions
    {
       public static IEnumerable<AssignPermissionModel> Permissions { get; set; }
    }
}
