using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using DTL.Business.Visitor;
using DTL.Model.Models;

namespace DTL.Website.Common
{
    public class VisitorCounterMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        IVisitorService _visitorService;

        public VisitorCounterMiddleware(RequestDelegate requestDelegate, IVisitorService visitorService)
        {
            _requestDelegate = requestDelegate;
            _visitorService = visitorService;
        }

        public async Task Invoke(HttpContext context)
        {
            string visitorId = context.Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                //Save to db
                var id = Guid.NewGuid();
                VisitorModel visitorModel = new VisitorModel();
                visitorModel.Id = id;
                visitorModel.VisitDate = DateTime.Now;
                _visitorService.AddEditVisitorData(visitorModel);

                context.Response.Cookies.Append("VisitorId", id.ToString(), new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTimeOffset.Now.AddDays(30)
                });
            }

            await _requestDelegate(context);
        }
    }
}
