using DTL.Model.CommonModels;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace DTL.Business.Common
{
    public static class ReadJson
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static CommonModel LoadJson()
        {
            try
            {
                CommonModel items = new CommonModel();
                using (StreamReader r = new StreamReader("Data.json"))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<CommonModel>(json);
                }
                return items;
            }
            catch(Exception ex)
            {
                log.Error("LoadJson", ex);
                return null;
            }
        }
    }
}
