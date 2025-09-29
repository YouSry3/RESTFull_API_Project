using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
namespace SurveyBasket
{
public static class DependancyInjection
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

        services
            .AddServices()
            .AddMapster();

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
        services.AddSingleton<IMapper>(new Mapper(config));
        return services;
    }
}

}

