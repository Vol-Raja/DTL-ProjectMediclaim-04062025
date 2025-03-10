using DTL.Model.Models.UserManagement;
using System.Collections.Generic;

namespace DTL.Business.UserManagement
{
    public interface ISubModule
    {
        int AddSubModule(SubModuleModel subModuleModel);
        IEnumerable<SubModuleModel> GetSubModuleByParam(int? submoduleid, string modulename, string submodulename);
        IEnumerable<SubModuleModel> GetArchiveSubModule();
        void DeleteSubModule(int subModuleid);
        int UpdateSubModule(SubModuleModel subModuleModel);
    }
}
