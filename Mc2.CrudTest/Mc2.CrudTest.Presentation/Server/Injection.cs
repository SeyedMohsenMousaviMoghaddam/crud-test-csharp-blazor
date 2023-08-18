
namespace Mc2.CrudTest.Presentation;

public static class Injection
{
    public static IServiceCollection PresentationInjection(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment _env)
    {
        services.AddProblems(configuration, _env);
        services.AddDependantServices();
        if (_env.EnvironmentName != "Production") services.addSwagger();

        return services;
    }


    private static IServiceCollection AddDependantServices(this IServiceCollection serviceCollection)
    {
        //serviceCollection.AddScoped<I_, _>();
        return serviceCollection;
    }

    private static IServiceCollection addSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen();
        return serviceCollection;
    }

    private static IServiceCollection AddProblems(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var activeExceptionDetails = Convert.ToBoolean(configuration["ActiveExceptionDetails"]);

        //services.AddProblemDetails(x =>
        //{

        //});

        return services;
    }
}