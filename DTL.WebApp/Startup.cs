using DTL.Business.Auth;
using DTL.Business.Dapper;
using DTL.Business.PensionSlip;
using DTL.WebApp.Data;
using DTL.Business.EmployeeRegistration;
using DTL.Business.ServiceDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTL.Business.Form5;
using DTL.Business.NomineeDetails;
using DTL.Business.Mediclaim.MediclaimRaise;
using DTL.Business.Mediclaim.Detail.NonCashless;
using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.Business.Mediclaim.Processing;
using DTL.Business.Mediclaim.Voucher;
using DTL.Business.Mediclaim.MedicalCard;
using DTL.Business.Mediclaim.MedicalPageDetail;
using DTL.Business.GPF.Withdrawal;
using DTL.WebApp.Models;
using DTL.Business.Disbursement;
using DTL.Business.GPF.LoanProcessing;
using DTL.Business.GPF.ModifyWithdrawal;
using DTL.Business.Mediclaim.Report;
using DTL.Business.Mediclaim.Modify;
using DTL.Business.GeneratePension;
using DTL.Business.FinanceManagement;
using DTL.Business.LegalSection;
using DTL.Business.Grievance;
using DTL.Business.UserManagement;
using DTL.Business.GPF.GeneratePayment;
using DTL.Business.CMS.Tender;
using DTL.Business.CMS.Link;
using DTL.Business.CMS.Notice;
using DTL.Business.CMS.WhatsNew;
using DTL.Business.CMS.About;
using DTL.Business.CMS.AimVision;
using DTL.Business.CMS.BannerImage;
using DTL.Business.Logging;
using DTL.Business.ApprovalLadder;
using DTL.Business.CMS.Event;
using DTL.Business.CMS.ContactUs;
using DTL.Business.CMS.Form;
using DTL.Business.CMS.SocialMediaAccount;
using DTL.Business.CMS.RuleActHelpline;
using DTL.Business.Mediclaim.Disbursement;
using DTL.Model.Models.CMS;
using DTL.Business.CMS.FooterLegalSection;
using DTL.Business.CMS.CMSHospital;
using DTL.Business.CMS.Feedback;
using DTL.Business.CMS.Testimony;
using DTL.Business.GPF.EmployeeInfo;
using DTL.Business.GPF.Settlement;
using DTL.Business.GPF.EDLIS;
using DTL.WebApp.Areas.EmployeeDashboard.Data;

namespace DTL.WebApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddScoped<IDapperService, DapperService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAddEmployee, AddEmployee>();
            services.AddScoped<IForm5, Form5>();
            services.AddScoped<INomineeDetails, NomineeDetails>();
            services.AddScoped<IAddQtmPayment, AddQtmPayment>();
            services.AddScoped<IAddTDSCalculation, AddTDSCalculation>();
            services.AddScoped<IPensionSlipService, PensionSlipService>();
            services.AddScoped<IRecoveryAllowance, RecoveryAllowance>();
            services.AddScoped<IDisbursementReport, DisbursementReport>();
            services.AddScoped<IAddTDSInvestment, AddTDSInvestment>();
            // services.AddScoped<IAddGPFWithdrawalRequest, AddGPFWithdrawalRequest>();
            services.AddScoped<IAddBudgetDeclaration, AddBudgetDeclaration>();
            services.AddScoped<IPensionManagement, PensionManagement>();
            services.AddScoped<IServiceDetails, ServiceDetails>();

            services.AddScoped<IGPFManagement, GPFManagement>();
            services.AddScoped<IAddInvestmentDeclaration, AddInvestmentDeclaration>();
            services.AddScoped<ILegalSectionForm, LegalSectionForms>();
            services.AddScoped<IWithdrawal, Withdrawal>();
            services.AddScoped<IGeneratePension, GeneratePension>();
            services.AddScoped<IAddGrievance, AddGrievance>();
            //Mediclaim
            services.AddScoped<IRaise, Raise>();
            services.AddScoped<INonCashlessDetail, NonCashlessDetail>();
            services.AddScoped<ICashlessDetail, CashlessDetail>();
            services.AddScoped<IProcessing, Processing>();
            services.AddScoped<IVoucher, Voucher>();
            services.AddScoped<IMedicalCard, MedicalCard>();
            services.AddScoped<IMedicalPageDetail, MedicalPageDetail>();
            services.AddScoped<IReport, Report>();
            services.AddScoped<IModify, Modify>();
            services.AddScoped<IDisbursment, Disbursment>();
            //GPF
            services.AddScoped<DTL.Business.GPF.Processing.IProcessing, DTL.Business.GPF.Processing.Processing>();
            services.AddScoped<IWithdrawal, Withdrawal>();
            services.AddScoped<ILoanProcessing, LoanProcessing>();
            services.AddScoped<ISettlement, Settlement>();
            services.AddScoped<IEDLIS, EDLIS>();
            services.AddScoped<IModifyWithdrawal, ModifyWithdrawal>();
            services.AddScoped<IGeneratePayment, GeneratePayment>();
            services.AddScoped<IEmployeeInfo, EmployeeInfo>();
            services.AddScoped<IMonthlyProcessing, MonthlyProcessing>();
            //UserManagement
            services.AddScoped<IDVBUser, DVBUser>();
            services.AddScoped<IAdminUser, AdminUser>();
            services.AddScoped<IHospitalUser, HospitalUser>();
            services.AddScoped<IModule, Module>();
            services.AddScoped<ISubModule, SubModule>();
            services.AddScoped<IAssignPermission, AssignPermission>();
            services.AddScoped<IForgotPassword, ForgotPassword>();
            services.AddScoped<IEmployeeUser, EmployeeUser>();   //add by rajan 26/02/2028
            //CMS
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<INoticeService, NoticeService>();
            services.AddScoped<IWhatsNewService, WhatsNewService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IAimVisionService, AimVisionService>();
            services.AddScoped<IBannerImageService, BannerImageService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<ICMSHospitalService, CMSHospitalService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ITestimonyService, TestimonyService>();
            services.AddScoped<ISocialMediaAccountService, SocialMediaAccountService>();
            services.AddScoped<IRuleActHelplineService, RuleActHelplineService>();
            services.AddScoped<IFooterLegalSectionService, FooterLegalSectionService>();
            //Logging
            services.AddScoped<ILogging, Logging>();
            //ApprovalLadder
            services.AddScoped<IApprovalLadder, ApprovalLadder>();
            //add by rajan 21/02/2025
            //EmployeeDashboard
            services.AddScoped<IPPO, PPO>();
            services.AddScoped<IGPF, GPF>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "defaultArea", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
