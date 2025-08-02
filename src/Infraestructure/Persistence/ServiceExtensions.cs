

using Application.Interfaces;
using Infraestructure.Persistence.Data;
using Infraestructure.Persistence.Repositories;
using Infraestructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IParkingSlotService, ParkingSlotServices>();
            services.AddTransient<IParkingSessionServices, ParkingSessionServices>();
        }
    }
}