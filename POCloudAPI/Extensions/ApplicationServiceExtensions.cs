using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.Helpers;
using POCloudAPI.JWTokens;
using AutoMapper;
using POCloudAPI.Interfaces;

namespace POCloudAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) {

            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
            );
            return services;
        }
    }
}
