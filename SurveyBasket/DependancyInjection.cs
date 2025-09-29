using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyBasket.Persistence;
using System.Reflection;

namespace SurveyBasket
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Controllers + FluentValidation
            services.AddControllers()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    });

            // Custom Services + Mapster
            services.AddServices()
                    .AddMapster();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPollService, PollService>();
            return services;
        }

        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
