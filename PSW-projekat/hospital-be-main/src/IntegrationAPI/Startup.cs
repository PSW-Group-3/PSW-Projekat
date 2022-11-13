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
            services.AddDbContext<IntegrationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IntegrationDb")));

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddControllers();
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
            services.AddScoped<IReportSettingsRepository, ReportSettingsRepository>();
            services.AddScoped<IReportSettingsService, ReportSettingsService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
