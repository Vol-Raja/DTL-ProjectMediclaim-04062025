using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;

namespace DTL.Business.Disbursement
{
   public class AddTDSInvestment : IAddTDSInvestment
    {
        private readonly IDapperService _dapper;

        public AddTDSInvestment(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }
        public string AddEditTDSInvestment(TDSInvestmentModel tDSInvestmentModel,bool isEdit)
        {
            var dbparam = new DynamicParameters();
            dbparam.Add("@TDSInvestmentId", (isEdit ? tDSInvestmentModel.TDSInvestmentId : null), DbType.Guid);
            dbparam.Add("@TDSCalculationId", tDSInvestmentModel.TDSCalculationId, DbType.Guid);
            dbparam.Add("@Inv80D", tDSInvestmentModel.Inv80D, DbType.Decimal);
            dbparam.Add("@InvFile80D", tDSInvestmentModel.InvFile80D, DbType.Binary);
            dbparam.Add("@Inv80DD", tDSInvestmentModel.Inv80DD, DbType.Decimal);
            dbparam.Add("@InvFile80DD", tDSInvestmentModel.InvFile80DD, DbType.Binary);
            dbparam.Add("@Inv80DDB", tDSInvestmentModel.Inv80DDB, DbType.Decimal);
            dbparam.Add("@InvFile80DDB", tDSInvestmentModel.InvFile80DDB, DbType.Binary);
            dbparam.Add("@Inv5YrsTermDepositPostoffice", tDSInvestmentModel.Inv5YrsTermDepositPostoffice, DbType.Decimal);
            dbparam.Add("@InvFile5YrsTermDepositPostoffice", tDSInvestmentModel.InvFile5YrsTermDepositPostoffice, DbType.Binary);
            dbparam.Add("@InvLIC_Pension_Plan", tDSInvestmentModel.InvLIC_Pension_Plan, DbType.Decimal);
            dbparam.Add("@InvFileLIC_Pension_Plan", tDSInvestmentModel.InvFileLIC_Pension_Plan, DbType.Binary);
            dbparam.Add("@InvNSC", tDSInvestmentModel.InvNSC, DbType.Decimal);
            dbparam.Add("@InvFileNSC", tDSInvestmentModel.InvFileNSC, DbType.Binary);
            dbparam.Add("@InvPPF", tDSInvestmentModel.InvPPF, DbType.Decimal);
            dbparam.Add("@InvFilePPF", tDSInvestmentModel.InvFilePPF, DbType.Binary);
            dbparam.Add("@InvStampDuty", tDSInvestmentModel.InvStampDuty, DbType.Decimal);
            dbparam.Add("@InvFileStampDuty", tDSInvestmentModel.InvFileStampDuty, DbType.Binary);
            dbparam.Add("@InvSukanyaSmriddhiYojana", tDSInvestmentModel.InvSukanyaSmriddhiYojana, DbType.Decimal);
            dbparam.Add("@InvFileSukanyaSmriddhiYojana", tDSInvestmentModel.InvFileSukanyaSmriddhiYojana, DbType.Binary);
            dbparam.Add("@InvTermDepositBank", tDSInvestmentModel.InvTermDepositBank, DbType.Decimal);
            dbparam.Add("@InvFileTermDepositBank", tDSInvestmentModel.InvFileTermDepositBank, DbType.Binary);
            dbparam.Add("@InvTF", tDSInvestmentModel.InvTF, DbType.Decimal);
            dbparam.Add("@InvFileTF", tDSInvestmentModel.InvFileTF, DbType.Binary);
            dbparam.Add("@InvULIP", tDSInvestmentModel.InvULIP, DbType.Decimal);
            dbparam.Add("@InvFileULIP", tDSInvestmentModel.InvFileULIP, DbType.Binary);
            dbparam.Add("@InvMF", tDSInvestmentModel.InvMF, DbType.Decimal);
            dbparam.Add("@InvFileMF", tDSInvestmentModel.InvFileMF, DbType.Binary);
            dbparam.Add("@InvHousingLoanInt", tDSInvestmentModel.InvHousingLoanInt, DbType.Decimal);
            dbparam.Add("@InvFileHousingLoanInt", tDSInvestmentModel.InvFileHousingLoanInt, DbType.Binary);
            dbparam.Add("@InvHousingLoanInt1617", tDSInvestmentModel.InvHousingLoanInt1617, DbType.Decimal);
            dbparam.Add("@InvFileHousingLoanInt1617", tDSInvestmentModel.InvFileHousingLoanInt1617, DbType.Binary);
            dbparam.Add("@InvHousingLoanInt1920", tDSInvestmentModel.InvHousingLoanInt1920, DbType.Decimal);
            dbparam.Add("@InvFileHousingLoanInt1920", tDSInvestmentModel.InvFileHousingLoanInt1920, DbType.Binary);
            dbparam.Add("@Inv80E", tDSInvestmentModel.Inv80E, DbType.Decimal);
            dbparam.Add("@InvFile80E", tDSInvestmentModel.InvFile80E, DbType.Binary);
            dbparam.Add("@Inv80G", tDSInvestmentModel.Inv80G, DbType.Decimal);
            dbparam.Add("@InvFile80G", tDSInvestmentModel.InvFile80G, DbType.Binary);
            dbparam.Add("@Inv80GGB", tDSInvestmentModel.Inv80GGB, DbType.Decimal);
            dbparam.Add("@InvFile80GGB", tDSInvestmentModel.InvFile80GGB, DbType.Binary);
            dbparam.Add("@Inv80GGC", tDSInvestmentModel.Inv80GGC, DbType.Decimal);
            dbparam.Add("@InvFile80GGC", tDSInvestmentModel.InvFile80GGC, DbType.Binary);
            dbparam.Add("@Inv80GG", tDSInvestmentModel.Inv80GG, DbType.Decimal);
            dbparam.Add("@InvFile80GG", tDSInvestmentModel.InvFile80GG, DbType.Binary);
            dbparam.Add("@Inv80RRB", tDSInvestmentModel.Inv80RRB, DbType.Decimal);
            dbparam.Add("@InvFile80RRB", tDSInvestmentModel.InvFile80RRB, DbType.Binary);
            dbparam.Add("@Inv80TTA", tDSInvestmentModel.Inv80TTA, DbType.Decimal);
            dbparam.Add("@InvFile80TTA", tDSInvestmentModel.InvFile80TTA, DbType.Binary);
            dbparam.Add("@Inv80TTB", tDSInvestmentModel.Inv80TTB, DbType.Decimal);
            dbparam.Add("@InvFile80TTB", tDSInvestmentModel.InvFile80TTB, DbType.Binary);
            dbparam.Add("@Inv80U", tDSInvestmentModel.Inv80U, DbType.Decimal);
            dbparam.Add("@InvFile80U", tDSInvestmentModel.InvFile80U, DbType.Binary);

            if (!isEdit)
                dbparam.Add("@CreatedBy", tDSInvestmentModel.CreatedBy, DbType.String);
            else
                dbparam.Add("@ModifideBy", tDSInvestmentModel.ModifideBy, DbType.String);

            dbparam.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("sp_AddEditTDSInvestment", dbparam);
            var result = dbparam.Get<String>("@ReturnMsg");
            return result;
        }
        public TDSInvestmentModel GetTDSInvestmentDetails(Guid TDSCalculationId)
        {
          var dbparams = new DynamicParameters();
            dbparams.Add("@TDSCalculationId", TDSCalculationId, DbType.Guid);
           
            var data = _dapper.Get<TDSInvestmentModel>("sp_GetTDSInvestmentDetails", dbparams);
            return data;
        }
    }
}
