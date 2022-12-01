using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_Models;
using ProvideApiReference_Models.Models;
using ProvideApiReference_Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_Utilities.Extensions
{
    public static class IdentityServiceExtension
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<ApplicationUser>(opt =>
            {
               // opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders(); ;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey=key,
                        ValidateIssuer=false,//search later
                        ValidateAudience =false//search later
                    };
                });
            services.AddScoped<TokenService>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(SD.RequreAdminRolePolicy, policy => policy.RequireRole(SD.AdminRole));
                opt.AddPolicy(SD.ModeratePhotoRolePolicy, policy => policy.RequireRole(SD.AdminRole, SD.ModeratorRole));
            });
            return services;
        }
    }
}
