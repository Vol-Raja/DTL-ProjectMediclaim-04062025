using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Auth
{
    public interface IAuthService
    {
        Task<IEnumerable<RolePageModel>> GetRolePageAccessAsync();
        Task<IEnumerable<RolePageModel>> GetRolePageAccessByIdAsync(string userId);
        RolePageModel GetRolePageAccessByIdAsync(int rolePageId);
        int AddEditRolePageAccess(int id,string roleId, string pageName);
        int DeleteRolePageAccess(int rolePageId);
    }
}
