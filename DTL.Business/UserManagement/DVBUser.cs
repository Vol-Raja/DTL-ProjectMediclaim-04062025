using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.UserManagement
{
    public class DVBUser : IDVBUser
    {
        private readonly IDapperService _dapper;
        public DVBUser(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public int AddDVBUser(DVBUserModel dvbUserModel)
        {
            int outputId = 0;

            var dbparams = new DynamicParameters();
            dbparams.Add("@ID", dvbUserModel.ID, DbType.Guid);
            dbparams.Add("@Name", dvbUserModel.Name, DbType.String);
            dbparams.Add("@EmailAddress", dvbUserModel.EmailAddress, DbType.String);
            dbparams.Add("@PhoneNumber", dvbUserModel.PhoneNumber, DbType.String);
            dbparams.Add("@Department", dvbUserModel.Department, DbType.String);
            dbparams.Add("@Designation", dvbUserModel.Designation, DbType.String);
            dbparams.Add("@CreatedBy", dvbUserModel.CreatedBy, DbType.String);
            dbparams.Add("@Dashboard", dvbUserModel.Dashboard, DbType.String);
            dbparams.Add("@Address", dvbUserModel.Address, DbType.String);
            dbparams.Add("@Username", dvbUserModel.Username, DbType.String);
            dbparams.Add("@DVBUserId", outputId, DbType.Int32, direction: ParameterDirection.Output);
            dbparams.Add("@Address", dvbUserModel.Address, DbType.String);
            dbparams.Add("@Username", dvbUserModel.Username, DbType.String);
            dbparams.Add("@Dashboard", dvbUserModel.Dashboard, DbType.String);
            dbparams.Add("@DashboardUrl", dvbUserModel.DashboardUrl, DbType.String);

            _dapper.Execute("SaveDVBUser", dbparams);
            outputId = dbparams.Get<int>("@DVBUserId");
            return outputId;
        }

        public IEnumerable<DVBUserModel> GetDVBUserByParam(int? dvbuserid, string name, string emailaddress, string department, string designation)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Name", name, DbType.String);
            dbparams.Add("@EmailAddress", emailaddress, DbType.String);
            dbparams.Add("@Department", department, DbType.String);
            dbparams.Add("@Designation", designation, DbType.String);
            dbparams.Add("@DVBUserId", dvbuserid, DbType.Int32);

            return _dapper.GetAll<DVBUserModel>("GetDVBUserByParam", dbparams);
        }

        public IEnumerable<DVBUserModel> GetArchiveDVBUser()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<DVBUserModel>("GetArchiveDVBUser", dbparams);
        }

        public void DVBUserToggleIsDeleteFlag(int dvbuserid, bool isDeleteFlag, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DVBUserId", dvbuserid, DbType.Int32);
            dbparams.Add("@IsDelete", isDeleteFlag, DbType.Boolean);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            _dapper.Execute("DVBUserToggleIsDeleteFlag", dbparams);
        }

        public int UpdateDVBUser(DVBUserModel dvbUserModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Name", dvbUserModel.Name, DbType.String);
            dbparams.Add("@EmailAddress", dvbUserModel.EmailAddress, DbType.String);
            dbparams.Add("@PhoneNumber", dvbUserModel.PhoneNumber, DbType.String);
            dbparams.Add("@Department", dvbUserModel.Department, DbType.String);
            dbparams.Add("@Designation", dvbUserModel.Designation, DbType.String);
            dbparams.Add("@ModifiedBy", dvbUserModel.ModifideBy, DbType.String);
            dbparams.Add("@Dashboard", dvbUserModel.Dashboard, DbType.String);
            dbparams.Add("@Address", dvbUserModel.Address, DbType.String);
            dbparams.Add("@Username", dvbUserModel.Username, DbType.String);
            dbparams.Add("@DVBUserId", dvbUserModel.DVBUserId, DbType.Int32);
            dbparams.Add("@Address", dvbUserModel.Address, DbType.String);
            dbparams.Add("@Username", dvbUserModel.Username, DbType.String);
            dbparams.Add("@Dashboard", dvbUserModel.Dashboard, DbType.String);
            dbparams.Add("@DashboardUrl", dvbUserModel.DashboardUrl, DbType.String);
            return _dapper.Execute("UpdateDVBUser", dbparams);
        }

        public IEnumerable<DesignationModel> GetDesignations()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<DesignationModel>("SELECT DesignationId, DesignationName FROM dbo.MasterDesignation WHERE IsDeleted = 0 ORDER BY 2 ASC", dbparams, CommandType.Text);
        }
        public IEnumerable<DepartmentModel> GetDepartments()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<DepartmentModel>("SELECT Id, Name,Description,IsDeleted FROM dbo.MasterDepartment ORDER BY 2 ASC", dbparams, CommandType.Text);
        }
        public IEnumerable<DocumentModel> GetDocuments()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<DocumentModel>("SELECT DocumentId, DocumentName FROM dbo.MasterDocumentList WHERE IsDeleted = 0 ORDER BY 2 ASC", dbparams, CommandType.Text);
        }

        public IEnumerable<DashboardModel> GetDashboards(string dashboardFor)
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<DashboardModel>($"SELECT DashboardId, DashboardName, DashboardFor FROM dbo.MasterDashboard WHERE DashboardFor= '{dashboardFor}' AND IsDeleted = 0   ORDER BY 2 ASC", dbparams, CommandType.Text);
        }
        public int DeleteDVBUserPermanently(Guid id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", id);

            return _dapper.Execute($"Delete FROM DVBUser WHERE Id=@Id", dbparams, CommandType.Text);
        }

        public DVBUserModel GetDVBUserByUsername(string username)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Username", username);

            return _dapper.Get<DVBUserModel>($"Select * FROM DVBUser WHERE Username=@Username", dbparams, CommandType.Text);


        }
    }
}
