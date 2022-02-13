using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.JWTokens;

namespace POCloudAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) {

            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
            );
            return services;
        }
    }
}
