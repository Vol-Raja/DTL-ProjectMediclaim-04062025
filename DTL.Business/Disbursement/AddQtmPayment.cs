using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Disbursement
{
    public class AddQtmPayment : IAddQtmPayment
    {
        private readonly IDapperService _dapper;

        public AddQtmPayment(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public string InsertUpdateAddQtmPaymentDetails(AddQtmPaymentModel addQtmPaymentModel, bool isUpdate)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", (isUpdate ? addQtmPaymentModel.ID : null), DbType.Guid);
            dbparams.Add("@EmployeeRegistrationId", addQtmPaymentModel.EmployeeRegistrationId, DbType.Guid);
            dbparams.Add("@PensionerName", addQtmPaymentModel.PensionerName, DbType.String);
            dbparams.Add("@DOB", addQtmPaymentModel.DOB, DbType.Date);
            dbparams.Add("@EmployeeID", addQtmPaymentModel.EmployeeID, DbType.Int64);
            dbparams.Add("@EmployerName", addQtmPaymentModel.EmployerName, DbType.String);
            dbparams.Add("@CurrentAge", addQtmPaymentModel.CurrentAge, DbType.Int64);
            dbparams.Add("@AgeGroup", addQtmPaymentModel.AgeGroup, DbType.String);
            dbparams.Add("@MonthlyPension", addQtmPaymentModel.MonthlyPension, DbType.Decimal);
            dbparams.Add("@IncrementPercentage", addQtmPaymentModel.IncrementPercentage, DbType.String);
            dbparams.Add("@IncrementAmount", addQtmPaymentModel.IncrementAmount, DbType.Decimal);
            dbparams.Add("@AQPMonthlyPension", addQtmPaymentModel.AQPMonthlyPension, DbType.Decimal);
            dbparams.Add("@FromDate", addQtmPaymentModel.FromDate, DbType.Date);
            dbparams.Add("@ToDate", addQtmPaymentModel.ToDate, DbType.Date);
            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            if (!isUpdate)
                dbparams.Add("@CreatedBy", addQtmPaymentModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifideBy", addQtmPaymentModel.ModifideBy, DbType.String);
            _dapper.Execute("usp_InsertUpdateAddQtmPaymentDetails", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }

        public async Task<IEnumerable<AddQtmPaymentModel>> GetPensionersAllAsync()
        {
            var dbparams = new DynamicParameters();
            return await _dapper.GetAllAsync<AddQtmPaymentModel>("usp_Pensioner_GetAll", dbparams);
        }
    }
}
