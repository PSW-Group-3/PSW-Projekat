using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Service;
using IntegrationLibrary.Core.Service.BloodBanks;
using IntegrationLibrary.Settings;
using IntegrationLibrary.Core.BloodBankConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using IntegrationLibrary.Core.Repository.Reports;
using IntegrationLibrary.Core.Service.Reports;
using IntegrationLibrary.Core.Repository.BloodRequests;
using IntegrationLibrary.Core.Service.BloodRequests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IntegrationLibrary.Core.Service.Newses;
using IntegrationLibrary.Core.Repository.Newses;
using IntegrationLibrary.Core.Service.ScheduledOrders;
using IntegrationLibrary.Core.Repository.ScheduledOrder;
using IntegrationLibrary.Core.Service.Tenders;
using IntegrationLibrary.Core.Repository.Tenders;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using IntegrationLibrary.Core.HospitalConnection;
using IntegrationLibrary.Core.Service.EmergencyBloodRequests;
using IntegrationAPI.Adapters;
using IntegrationLibrary.Core.Service.HostedServices;
using IntegrationLibrary.Core.Repository.EmergencyBloodRequests;
using IntegrationLibrary.Core.SFTPConnection;
using HospitalLibrary.Settings;

namespace IntegrationAPI
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
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<IntegrationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IntegrationDb"))
                                                                            .UseLazyLoadingProxies());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddControllers().AddNewtonsoftJson( options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntegrationAPI", Version = "v1" });
            });

            services.AddScoped<IBloodBankService, BloodBankService>();
            services.AddScoped<IBloodBankRepository, BloodBankRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IBloodBankConnection, BloodBankHTTPConnection>();
            services.AddScoped<IRabbitMQService, RabbitMQService>();
            services.AddHostedService<BloodReportHostedService>();
            services.AddHostedService<BloodRequestHostedService>();
            services.AddScoped<IReportSettingsRepository, ReportSettingsRepository>();
            services.AddScoped<IReportSettingsService, ReportSettingsService>();
            services.AddScoped<IBloodRequestRepository, BloodRequestRepository>();
            services.AddScoped<IBloodRequestService, BloodRequestService>();
            services.AddScoped<IReportSendingService, ReportSendingService>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ISheduledOrderRepository, ScheduledOrderRepository>();
            services.AddScoped<IScheduledOrderService, ScheduledOrderService>();
            services.AddScoped<ITenderRepository, TenderRepository>();
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<IHospitalConnection, HospitalHTTPConnection>();
            services.AddScoped<IEmergencyBloodRequestService, EmergencyBloodRequestService>();
            services.AddScoped<IEmergencyBloodRequestRepository, EmergencyBloodRequestRepository>();
            services.AddScoped<ISFTPService, SFTPService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IntegrationDbContext integrationDbContext)
        {
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IntegrationAPI v1"));
            }

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
