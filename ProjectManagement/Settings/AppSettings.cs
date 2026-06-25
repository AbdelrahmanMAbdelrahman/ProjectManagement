using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementApplication.Interfaces;
using ProjectManagementApplication.Services;
using ProjectManagementDomain.Models.Auth;
using ProjectManagementDomain.Models.Options;
using ProjectManagmentInfrasturcture;
using System.Text;
using System.Threading.Tasks.Sources;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectManagement.Settings
{
    public static class AppSettings
    {
        public static IServiceCollection PrepareAppServices(
            this IServiceCollection service, ConfigurationManager config) {
            service.PrepareDatabaseConnection(config);
            service.PrepareDependencyInjection();
            service.PrepareCors();
            service.PrepareAuth(config);
            return service;
        }
        public static IServiceCollection PrepareDatabaseConnection(
            this IServiceCollection service, ConfigurationManager config)
        {
              string? ConnectionString = config.GetConnectionString("DefaultConnection");
            service.AddDbContext<AppDbContext>(options=>options.UseSqlServer(ConnectionString));
            return service;
        }
        public static IServiceCollection PrepareCors(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("ProjectManagement", policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins([
                        "http://127.0.0.1:5500"
                        ]);

                });
            });
            return service;
        }
        public static IServiceCollection PrepareDependencyInjection(this IServiceCollection service)
        {
            service.AddScoped<IAuth, AuthService>();
            service.AddScoped<IProject, ProjectService>();
            service.AddScoped<ITask,TaskService>();
            return service;
        }
        public static IServiceCollection PrepareAuth(this IServiceCollection service, IConfiguration config)
        {
            JwtOptions options = config.GetSection(JwtOptions.Section).Get<JwtOptions>()!;
            service.Configure<MailOptions>(config.GetSection("MailSettings"));
            service.AddIdentityCore<AppUser>();
            service.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            service.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.Section)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", option =>
            {
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            //service.AddAuthorization(options => {
            //    foreach (var perm in DefaultPermission.GetProperties())
            //    {
            //        options.AddPolicy(perm!, policy => policy.RequireClaim(DefaultPermission.Type, perm!));
            //    }

            //});
            return service;

        }
    }
}
