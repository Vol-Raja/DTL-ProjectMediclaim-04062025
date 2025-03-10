using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;

namespace DTL.Business.UserManagement
{
    public interface IHospitalUser
    {
        int AddHospitalUser(HospitalUserModel hospitalUserModel);
        IEnumerable<HospitalUserModel> GetHospitalUserByParam(int? hospitaluserid, string nameOfHospital, string emailaddress, string typeOfHospiptal, string phoneNumber);
        IEnumerable<HospitalUserModel> GetArchiveHospitalUser();
        void HospitalUserToggleIsDeleteFlag(int hospitaluserid, bool isDeleteFlag, string modifiedBy);
        int UpdateHospitalUser(HospitalUserModel hospitalUserModel);
        int DeleteHospitalUserPermanently(Guid id);
        HospitalUserModel GetHospitalUserByUsername(string name);
    }
}
