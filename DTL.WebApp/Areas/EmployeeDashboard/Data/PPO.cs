using Dapper;
using DTL.Business.Dapper;
using DTL.WebApp.Areas.EmployeeDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Data
{
    public class PPO :IPPO
    {
        private readonly IDapperService _dapper;
        public PPO(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }
        public IEnumerable<PPOEmployee> GetEmployeePPO(string ppo_no)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PpoNo", ppo_no, DbType.String);
            return _dapper.GetAll<PPOEmployee>("GetAll_PPO", dbparams);
        }

        public PPOEmployee ViewEmployee(string ppoemp)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PpoNo", ppoemp, DbType.String);
           // return _dapper.GetAll<PPOEmployee>("GetAll_PPO", dbparams);
            var PpoDetail = _dapper.Get<PPOEmployee>("GetAll_PPO", dbparams);
            return (PpoDetail);
        }
    }
}
