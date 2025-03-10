using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.GeneratePension
{
    public interface IGeneratePension
    {
        IEnumerable<GeneratePensionModel> GetGeneratePension(int? month, int? year, string bank, string employer, int? ppono);
    }
}
