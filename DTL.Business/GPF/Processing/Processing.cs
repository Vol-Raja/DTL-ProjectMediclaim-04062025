using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DTL.Business.GPF.Processing
{
    public class Processing : IProcessing
    {
        private readonly IDapperService _dapper;

        public Processing(IDapperService dapper)
        {
            _dapper = dapper;
        }
        public IEnumerable<GPFProcessing> GetGPFProcessingSummaryByParam(string employer, string employeName, string employeNumber)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Employer", employer, DbType.String);
            dbparams.Add("@EmployeNumber", employeNumber, DbType.String);
            dbparams.Add("@EmployeName", employeName, DbType.String);
            return _dapper.GetAll<GPFProcessing>("GetGPFProcessingSummaryByParam", dbparams);
        }
        
        public IEnumerable<GPFProcessing> GetDetailByParam(string employer, int? month, int? year, string employeeNumber, string employeeName)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Employer", employer, DbType.String);
            dbparams.Add("@Month", month, DbType.Int32);
            dbparams.Add("@Year", year, DbType.Int32);
            dbparams.Add("@EmployeeId", employeeNumber, DbType.String);
            dbparams.Add("@EmployeeName", employeeName, DbType.String);
            return _dapper.GetAll<GPFProcessing>("GetGPFReportByParam", dbparams);
        }

        public void SaveGPFProcessing(IList<GPFProcessing> gpfProcessingModel, string createdBy)
        {
            
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GPFProcessing>));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, gpfProcessingModel);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }

            var dbparams = new DynamicParameters();
            dbparams.Add("@xml", xmlString, DbType.Xml);
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            _dapper.Execute("SaveGPFProcessing", dbparams);

        }
    }
}
