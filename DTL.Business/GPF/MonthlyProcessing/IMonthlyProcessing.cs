using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;

namespace DTL.Business.GPF.EmployeeInfo
{
    public interface IMonthlyProcessing
    {
        MonthlyGPFProcessData ValidateExcel(byte[] fileBytes);
        byte[] SampleExcel(string orgCode);
        MonthlyGPFProcessedEntry ProcessEntry(MonthlyGPFProcessedEntry entry, DateTime processingDate, decimal interestRate, string employeeType);
        bool SaveEntry(MonthlyGPFProcessModel model);
        decimal GetGPFSum(string orgCode, string empCode);


        IEnumerable<MonthlyGPFProcessDataModel> GetData(string orgCode, string empCode, DateTime? dtFrom, DateTime? dtTo);

        IEnumerable<GPFSummary> GetSummary(string orgCode, DateTime? dtFrom, DateTime? dtTo);
        CurrentGPFBalance GetCurrentBalance(string orgCode, string accNo, int year);

        GPFStatement GetStatement(string orgCode, string accNo, int year);
    }
}
