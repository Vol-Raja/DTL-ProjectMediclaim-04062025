using DTL.Model.Models;
using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.UserManagement
{
    public interface IDVBUser
    {
        int AddDVBUser(DVBUserModel dvbUserModel);
        IEnumerable<DVBUserModel> GetDVBUserByParam(int? dvbuserid, string name, string emailaddress, string department, string designation);
        IEnumerable<DVBUserModel> GetArchiveDVBUser();
        void DVBUserToggleIsDeleteFlag(int dvbuserid, bool isDeleteFlag, string modifiedBy);
        int UpdateDVBUser(DVBUserModel dvbUserModel);
        IEnumerable<DesignationModel> GetDesignations();
        public IEnumerable<DepartmentModel> GetDepartments();
        IEnumerable<DocumentModel> GetDocuments();
        IEnumerable<DashboardModel> GetDashboards(string dashboardFor);
        int DeleteDVBUserPermanently(Guid id);
        DVBUserModel GetDVBUserByUsername(string username);

    }
}
