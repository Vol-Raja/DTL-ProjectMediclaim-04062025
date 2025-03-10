using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.PensionSlip
{
    public class PensionSlipServie : IPensionSlipService
    {
        private readonly IDapperService _dapper;
        public PensionSlipServie(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public int AddPensionSlip(PensionSlipModel pensionSlipModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ABPLast10Months", pensionSlipModel.ABPLast10Months, DbType.Int32);
            dbparams.Add("@AdmissiableDate", pensionSlipModel.AdmissiableDate, DbType.DateTime2);
            dbparams.Add("@AdmissibleForFromDate_Enhanced", pensionSlipModel.AdmissibleForFromDate_Enhanced, DbType.DateTime2);
            dbparams.Add("@AdmissibleForFromDate_Normal", pensionSlipModel.AdmissibleForFromDate_Normal, DbType.DateTime2);
            dbparams.Add("@AdmissibleForToDate_Enhanced", pensionSlipModel.AdmissibleForToDate_Enhanced, DbType.DateTime2);
            dbparams.Add("@AdmissibleForToDate_Normal", pensionSlipModel.AdmissibleForToDate_Normal, DbType.DateTime2);
            dbparams.Add("@Commutation", pensionSlipModel.Commutation, DbType.Int32);
            dbparams.Add("@CommutedPortion", pensionSlipModel.CommutedPortion, DbType.Int32);
            dbparams.Add("@CreatedBy", pensionSlipModel.CreatedBy, DbType.String);
            dbparams.Add("@CreatedDate", pensionSlipModel.CreatedDate, DbType.DateTime2);
            dbparams.Add("@EmolumentsForPension", pensionSlipModel.EmolumentsForPension, DbType.Int32);
            dbparams.Add("@Gratuity", pensionSlipModel.Gratuity, DbType.Int32);
            dbparams.Add("@GratuityType", pensionSlipModel.GratuityType, DbType.String);
            //dbparams.Add("@flag", pensionSlipModel.ModifideBy, DbType.Int32);
            //dbparams.Add("@flag", pensionSlipModel.ModifideDate, DbType.Int32);
            dbparams.Add("@PensionAtNormalRate", pensionSlipModel.PensionAtNormalRate, DbType.Int32);
            dbparams.Add("@PensionEnhancedRate", pensionSlipModel.PensionEnhancedRate, DbType.Int32);
            return _dapper.Execute("sp_AddPensionSlip", dbparams);
        }
    }
}
