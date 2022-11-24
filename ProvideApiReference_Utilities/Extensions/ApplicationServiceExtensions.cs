

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_DataAccess.DbInitializer;
using ProvideApiReference_DataAccess.Repositroy;
using ProvideApiReference_DataAccess.Repositroy.IRepository;
using ProvideApiReference_Utilities.Helpers;

namespace ProvideApiReference_Utilities.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config )
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(connectionString));

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
