
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Infraestructure.Identity.Data;
using Infraestructure.Identity.Helpers;
using Infraestructure.Identity.Models;
using Infraestructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Infraestructure.Identity
{
    public static class ServiceExtensions
    {
        public static void AddIdentityLayer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<IdentityDbContext>(op =>
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            service.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();

            service.AddScoped<IAccountServices, AccountServices>();
            service.AddTransient<JWTHelper>();

            var key = configuration["JWTSettings:Key"]
                                    ?? throw new ArgumentNullException("La 'key' no fue encontrada en las settings");

            service.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.RequireHttpsMetadata = false;
                op.SaveToken = false;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    RoleClaimType = ClaimTypes.Role,
                };

                op.Events = new JwtBearerEvents()
                {

                    OnAuthenticationFailed = async context =>
                   {
                       if (context.Response.HasStarted) return;
                       context.Response.StatusCode = 500;
                       context.Response.ContentType = "application/json";
                       var result = JsonConvert.SerializeObject(new Response<string>("MIELDA PA LAS AGUILA"));
                       await context.Response.WriteAsync(result);
                   },

                    OnChallenge = async context =>
                    {
                        if (context.Response.HasStarted) return;
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("No estás autenticado."));
                        await context.Response.WriteAsync(result);
                    },

                    OnForbidden = async context =>
                    {
                        if (context.Response.HasStarted) return;
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("No tienes permisos para esta sección."));
                        await context.Response.WriteAsync(result);
                    }
                };
            });
        }
    }
}