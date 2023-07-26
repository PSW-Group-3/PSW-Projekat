using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.CouncilOfDoctors;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.CouncilOfDoctors;

using HospitalLibrary.Core.Repository.Notification;
using HospitalLibrary.Core.Service.Notification;
using HospitalLibrary.Identity;
using HospitalLibrary.Settings;
using HospitalLibrary.Core.IntegrationConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using HospitalLibrary.Core.AggregatDoctor;
using HospitalLibrary.Core.Model.Aggregate;

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
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HospitalDb")).UseLazyLoadingProxies());

            services.AddDbContext<AuthenticationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HospitalDb")));

            services.AddIdentity<SecUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
            });

            services.AddScoped<IService<Room>, RoomService>();
            services.AddScoped<IRepository<Room>, RoomRepository>();

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<FeedbackService>();
            services.AddScoped<FeedbackRepository>();

            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<AllergyRepository>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();

            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();

            services.AddScoped<StatisticsService>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();

            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();


            services.AddScoped<IBedService, BedService>();
            services.AddScoped<IBedRepository, BedRepository>();
            services.AddScoped<IBloodConsumptionService, BloodConsumptionService>();
            services.AddScoped<IBloodConsumptionRepository, BloodConsumptionRepository>();
            services.AddScoped<IBloodRepository, BloodRepository>();

            services.AddScoped<ITherapyService, TherapyService>();
            services.AddScoped<ITherapyRepository, TherapyRepository>();

            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();

            services.AddScoped<IBloodService, BloodService>();
            services.AddScoped<IBloodRepository, BloodRepository>();


            services.AddScoped<ICouncilOfDoctorsService, CouncilOfDoctorsService>();
            services.AddScoped<ICouncilOfDoctorsRepository, CouncilOfDoctorsRepository>();

            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<ISymptomRepository, SymptomRepository>();
            
            services.AddScoped<IExaminationService, ExaminationService>();
            services.AddScoped<IExaminationRepository, ExaminationRepository>();

            services.AddScoped<IIntegrationConnection, IntegrationHTTPConnection>();
            services.AddScoped<DoctorExaminationEventsRepository>();
            services.AddScoped<ExaminationStatisticService>();

            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IMealRepository, MealRepository>();

            services.AddScoped<IMealAnswerService, MealAnswerService>();
            services.AddScoped<IMealAnswerRepository, MealAnswerRepository>();

            services.AddScoped<IMealQuestionService, MealQuestionService>();
            services.AddScoped<IMealQuestionRepository, MealQuestionRepository>();

            services.AddScoped<ITrainingService, TrainingService>();

            services.AddScoped<SchedulingAppointmentEventsRepository>();
            services.AddScoped<SchedulingStatisticsService>();
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
