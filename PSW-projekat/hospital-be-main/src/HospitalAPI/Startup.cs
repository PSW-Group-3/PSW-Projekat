using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Identity;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HospitalAPI
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
            services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HospitalDb")).UseLazyLoadingProxies());

            services.AddDbContext<AuthenticationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HospitalDb")));

            services.AddIdentity<SecUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
            });

            services.AddScoped<IService<Room>, RoomService>();
            services.AddScoped<IRepository<Room>, RoomRepository>();

            services.AddScoped<FeedbackService>();
            services.AddScoped<FeedbackRepository>();

            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<AllergyRepository>();

            services.AddScoped<PersonService>();
            services.AddScoped<PersonRepository>();

            services.AddScoped<DoctorRepository>();
            services.AddScoped<DoctorService>();

            services.AddScoped<PatientRepository>();
            services.AddScoped<PatientService>();

            services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HospitalDbContext hospitalDbContext, AuthenticationDbContext authenticationDbContext)
        {
            //authenticationDbContext.Database.EnsureCreated();
            hospitalDbContext.Database.EnsureCreated();

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalAPI v1"));
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
