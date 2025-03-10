using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.ContactUs
{
    public class ContactUsService : IContactUsService
    {
        private readonly IDapperService _dapper;

        public ContactUsService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditContactUsData(ContactUsModel model, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? model.ID : null), DbType.Guid);
            dbparams.Add("@Name", model.Name, DbType.String);
            dbparams.Add("@NameHindi", model.NameHindi, DbType.String);
            dbparams.Add("@Designation", model.Designation, DbType.String);
            dbparams.Add("@DesignationHindi", model.DesignationHindi, DbType.String);
            dbparams.Add("@Telephone", model.Telephone, DbType.String);
            dbparams.Add("@PhoneNumber", model.PhoneNumber, DbType.String);
            dbparams.Add("@EmailAddress", model.EmailAddress, DbType.String);
           
            dbparams.Add("@IsDeleted", model.IsDeleted, DbType.Boolean);
           
            if (!isEdit)
                dbparams.Add("@CreatedBy", model.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", model.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditContactUs", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<ContactUsModel> GetContactUsModelByParam(Guid? Id, bool? IsDeleted)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
          
            return _dapper.GetAll<ContactUsModel>("GetContactUsByParam", dbparams);
        }

    }
}

