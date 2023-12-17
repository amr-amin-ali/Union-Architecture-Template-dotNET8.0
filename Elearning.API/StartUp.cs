namespace Elearning.API
{
    using Elearning.Contracts.Common;
    using Elearning.Contracts.Repositories;
    using Elearning.Contracts.Services;
    using Elearning.Entittes;
    using Elearning.Entittes.DbContexts;
    using Elearning.Entittes.Models;
    using Elearning.Repositiores.Identity_Repos;
    using Elearning.Repositories;
    using Elearning.Services;
    using Elearning.Services.Identity_Repos;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authentication.Negotiate;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;

    using NLog;
    using NLog.Extensions.Logging;

    using System.Configuration;
    using System.Text;
    using System.Text.Json.Serialization;

    using Taskaty.Services;

    using static Org.BouncyCastle.Math.EC.ECCurve;

    public class Startup
    {
        public IConfiguration ConfigRoot { get; }
        public Startup(IConfiguration configuration) => ConfigRoot = configuration;// var options = new LdapOptions();// configuration.GetSection("LdapConfiguration").Bind(options);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(c =>
            {
                //   c.Filters.Add(typeof(CheckPermisions));
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            );

            services.AddDbContext<ElearningContext>(opt =>
            {
                opt.UseSqlServer(ConfigRoot.GetConnectionString("DefaultConnection"));
            });

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ElearningContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Authentication
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
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = ConfigRoot["JWT:ValidAudience"],
                    ValidIssuer = ConfigRoot["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigRoot["JWT:Secret"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddTransient<IAuthService, AuthService>();
            #endregion

            #region Email
            services.AddTransient<IEmailService, EmailService>();
            #endregion

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<ICustomLoggerService, CustomLoggerService>();

            #region Users Management
            services.AddTransient<IUsersManagementService, UsersManagementService>();
            #endregion


            services.AddControllers()
            .AddJsonOptions(options =>
            {
                //updated  from ReferenceHandler.Preserve to ReferenceHandler.IgnoreCycles  to get objects inside multiple objects
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            #region NLog
            services.AddTransient<ICustomLoggerService, CustomLoggerService>();
            LogManager.Configuration = new NLogLoggingConfiguration(ConfigRoot.GetSection("NLog"));
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();
            //try
            //{
            //    logger.Debug("Init main");
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, "Stopped program because of exception");
            //}
            //finally
            //{
            //    LogManager.Shutdown();
            //}
            #endregion

            //Create the default firebase app instance
            #region FirebaseAdmin
            //services.AddFirebaseAdmin();
            #endregion


        }

        public void Configure(WebApplication app, IWebHostEnvironment webHostEnvironment)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            builder.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
