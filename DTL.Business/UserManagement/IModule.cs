using DTL.Model.Models.UserManagement;
using System.Collections.Generic;

namespace DTL.Business.UserManagement
{
    public interface IModule
    {
        int AddModule(ModuleModel moduleModel);
        IEnumerable<ModuleModel> GetModuleByParam(int? moduleid, string modulename);
        IEnumerable<ModuleModel> GetArchiveModule();
        void DeleteModule(int moduleid);
        int UpdateModule(ModuleModel moduleModel);

    }
}
