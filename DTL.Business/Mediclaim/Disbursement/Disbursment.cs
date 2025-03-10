using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Mediclaim.Disbursement
{
    public class Disbursment : IDisbursment
    {
        private readonly IDapperService _dapper;
        public Disbursment(IDapperService dapper)
        {
            _dapper = dapper;
        }

        #region Cashless
        public IEnumerable<CashlessModel> GetDisbursedCashlessClaims(bool disbursed, bool? paid)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Disbursed", disbursed, DbType.Boolean);
            dbparams.Add("@Paid", paid, DbType.Boolean);
            return _dapper.GetAll<CashlessModel>("GetCashlessDisbursedClaimByParam", dbparams);
        }

        public int PayCashlessClaim(int claimId, bool paid, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            dbparams.Add("@Paid", paid, DbType.Boolean);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("UpdateCashlessPaidFlag", dbparams);
        }
        // add by nirbhay 27/02/2025
        public IEnumerable<CashlessModel> GetDisbusCashlessByPPO(string PPO, bool paid,bool Disbus)
        {
            if (string.IsNullOrWhiteSpace(PPO))
            {
                throw new ArgumentException("PPO number cannot be null or empty", nameof(PPO));
            }

            var dbparams = new DynamicParameters();
            dbparams.Add("@ppo", PPO, DbType.String);
            dbparams.Add("@paid", paid, DbType.Boolean);
            dbparams.Add("@Disbus", Disbus, DbType.Boolean);

            string sql = "SELECT * FROM MediclaimCashless WHERE PPONumber = @ppo and Disbursed=@Disbus and Paid=@paid";

            return _dapper.GetAll<CashlessModel>(sql, dbparams, CommandType.Text);
        }

//end
        #endregion

        #region NonCashless
        public IEnumerable<NonCashlessModel> GetDisbursedNonCashlessClaims(bool disbursed, bool? paid)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Disbursed", disbursed, DbType.Boolean);
            dbparams.Add("@Paid", paid, DbType.Boolean);
            return _dapper.GetAll<NonCashlessModel>("GetNonCashlessDisbursedClaimByParam", dbparams);           
        }


        private IEnumerable<OPDCNDModel> GetOPDCNDList(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<OPDCNDModel>("GetOPDCNDByClaimId", dbparams);
        }

        public int PayNonCashlessClaim(int claimId, bool paid, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            dbparams.Add("@Paid", paid, DbType.Boolean);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("UpdateNonCashlessPaidFlag", dbparams);
        }
        #endregion
    }
}
