using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructure.Identity.Data;
using Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            

        }
    }
}