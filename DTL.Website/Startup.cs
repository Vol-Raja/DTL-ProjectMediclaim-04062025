using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTL.Business.CMS.Tender;
using DTL.Business.CMS.Link;
using DTL.Business.CMS.Notice;
using DTL.Business.CMS.WhatsNew;
using DTL.Business.CMS.About;
using DTL.Business.CMS.AimVision;
using DTL.Business.CMS.BannerImage;
using DTL.Business.Dapper;
using DTL.Business.CMS.SocialMediaAccount;
using DTL.Business.CMS.RuleActHelpline;
using DTL.Business.CMS.Form;
using DTL.Business.CMS.Event;
using DTL.Business.CMS.ContactUs;
using DTL.Business.Grievance;
using DTL.Business.UserManagement;
using DTL.Business.CMS.FooterLegalSection;
using DTL.Business.CMS.CMSHospital;
using DTL.Business.CMS.Feedback;
using DTL.Business.CMS.Testimony;

using DTL.Business.Visitor;

namespace DTL.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);
            
                    Configuration.GetConnectionString("DefaultConnection");
           
            // services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddScoped<IDapperService, DapperService>();
            //CMS
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<INoticeService, NoticeService>();
            services.AddScoped<IWhatsNewService, WhatsNewService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IAimVisionService, AimVisionService>();
            services.AddScoped<IBannerImageService, BannerImageService>();
            services.AddScoped<IRuleActHelplineService, RuleActHelplineService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IAddGrievance, AddGrievance>();
            services.AddScoped<IAssignPermission, AssignPermission>();
            services.AddScoped<ISocialMediaAccountService, SocialMediaAccountService>();
            services.AddScoped<IFooterLegalSectionService, FooterLegalSectionService>();
            services.AddScoped<ICMSHospitalService, CMSHospitalService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ITestimonyService, TestimonyService>();
            services.AddScoped<IVisitorService, VisitorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
       
        }
    }
}
