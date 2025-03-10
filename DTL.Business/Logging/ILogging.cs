using DTL.Model.Models;

namespace DTL.Business.Logging
{
    public interface ILogging
    {
        void Savelog(string methodname, string text);
    }
}
