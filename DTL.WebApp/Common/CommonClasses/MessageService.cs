using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace DTL.WebApp.Common.CommonClasses
{
    public class MessageService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IConfiguration _configuration;
        private readonly string ApiKey = "";
        private readonly string Campaign = "";
        private readonly string Routeid = "";
        private readonly string Type = "";
        private readonly string SenderId = "";
        private readonly string TemplateId = "";
        private readonly string PeId = "";
        private readonly string Username = "";
        private readonly string Password = "";

        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
            ApiKey = _configuration.GetValue<string>("SmsApi:ApiKey");
            Campaign = _configuration.GetValue<string>("SmsApi:Campaign");
            Routeid = _configuration.GetValue<string>("SmsApi:Routeid");
            Type = _configuration.GetValue<string>("SmsApi:Type");
            SenderId = _configuration.GetValue<string>("SmsApi:SenderId");
            TemplateId = _configuration.GetValue<string>("SmsApi:TemplateId");
            PeId = _configuration.GetValue<string>("SmsApi:PeId");
            Username = _configuration.GetValue<string>("SmsApi:Username");
            Password = _configuration.GetValue<string>("SmsApi:Password");

        }

        public async Task<string> SendCredential(string mobilenumber, string message)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("SmsApi:BaseUrl"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    //GET Method

                    var request = _configuration.GetValue<string>("SmsAPi:RequestUrl");

                    request = HttpUtility.UrlEncode(string.Format(request, Username, Password, ApiKey, Campaign, Routeid, Type, mobilenumber, SenderId, message, TemplateId, PeId));

                    HttpResponseMessage response = await client.GetAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var _data = await response.Content.ReadAsStringAsync();
                        return _data;
                    }
                    else {
                        return "Error in sending sms";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("MessageService SendCredential", ex);
                return ex.Message;
            }
        }

        internal Task SendCredential(object phoneNumber, string textmessage)
        {
            throw new NotImplementedException();
        }
    }
}
