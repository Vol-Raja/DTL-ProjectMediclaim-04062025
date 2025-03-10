using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{

    public class TDSInvestmentModel : BaseModel
    {
        [Required]
        public Guid TDSInvestmentId { get; set; }
        public Guid TDSCalculationId { get; set; }
        public decimal Inv80D { get; set; }
        public byte[] InvFile80D { get; set; }
        public string InvFilePath80D { get; set; }
        public decimal Inv80DD { get; set; }
        public byte[] InvFile80DD { get; set; }
        public string InvFilePath80DD { get; set; }
        public decimal Inv80DDB { get; set; }
        public byte[] InvFile80DDB { get; set; }
        public string InvFilePath80DDB { get; set; }
        public decimal Inv5YrsTermDepositPostoffice { get; set; }
        public byte[] InvFile5YrsTermDepositPostoffice { get; set; }
        public string InvFilePath5YrsTermDepositPostoffice { get; set; }
        public decimal InvLIC_Pension_Plan { get; set; }
        public byte[] InvFileLIC_Pension_Plan { get; set; }
        public string InvFilePathLIC_Pension_Plan { get; set; }
        public decimal InvNSC { get; set; }
        public byte[] InvFileNSC { get; set; }
        public string InvFilePathNSC { get; set; }
        public decimal InvPPF { get; set; }
        public byte[] InvFilePPF { get; set; }
        public string InvFilePathPPF { get; set; }
        public decimal InvStampDuty { get; set; }
        public byte[] InvFileStampDuty { get; set; }
        public string InvFilePathStampDuty { get; set; }
        public decimal InvSukanyaSmriddhiYojana { get; set; }
        public byte[] InvFileSukanyaSmriddhiYojana { get; set; }
        public string InvFilePathSukanyaSmriddhiYojana { get; set; }
        public decimal InvTermDepositBank { get; set; }
        public byte[] InvFileTermDepositBank { get; set; }
        public string InvFilePathTermDepositBank { get; set; }
        public decimal InvTF { get; set; }
        public byte[] InvFileTF { get; set; }
        public string InvFilePathTF { get; set; }
        public decimal InvULIP { get; set; }
        public byte[] InvFileULIP { get; set; }
        public string InvFilePathULIP { get; set; }
        public decimal InvMF { get; set; }
        public byte[] InvFileMF  { get; set; }
        public string InvFilePathMF  { get; set; }
        public decimal InvHousingLoanInt { get; set; }
        public byte[] InvFileHousingLoanInt { get; set; }
        public string InvFilePathHousingLoanInt { get; set; }
        public decimal InvHousingLoanInt1617 { get; set; }
        public byte[] InvFileHousingLoanInt1617 { get; set; }
        public string InvFilePathHousingLoanInt1617 { get; set; }
        public decimal InvHousingLoanInt1920 { get; set; }
        public byte[] InvFileHousingLoanInt1920 { get; set; }
        public string InvFilePathHousingLoanInt1920 { get; set; }
        public decimal Inv80E { get; set; }
        public byte[] InvFile80E { get; set; }
        public string InvFilePath80E { get; set; }
        public decimal Inv80G { get; set; }
        public byte[] InvFile80G { get; set; }
        public string InvFilePath80G { get; set; }
        public decimal Inv80GGB { get; set; }
        public byte[] InvFile80GGB { get; set; }
        public string InvFilePath80GGB { get; set; }
        public decimal Inv80GGC { get; set; }
        public byte[] InvFile80GGC { get; set; }
        public string InvFilePath80GGC { get; set; }
        public decimal Inv80GG { get; set; }
        public byte[] InvFile80GG { get; set; }
        public string InvFilePath80GG { get; set; }
        public decimal Inv80RRB { get; set; }
        public byte[] InvFile80RRB { get; set; }
        public string InvFilePath80RRB { get; set; }
        public decimal Inv80TTA { get; set; }
        public byte[] InvFile80TTA { get; set; }
        public string InvFilePath80TTA { get; set; }
        public decimal Inv80TTB { get; set; }
        public byte[] InvFile80TTB { get; set; }
        public string InvFilePath80TTB { get; set; }
        public decimal Inv80U { get; set; }
        public byte[] InvFile80U { get; set; }
        public string InvFilePath80U { get; set; }


    }
}
