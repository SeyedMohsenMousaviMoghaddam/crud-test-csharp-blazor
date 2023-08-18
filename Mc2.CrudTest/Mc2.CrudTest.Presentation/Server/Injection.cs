
using IbanNet.DependencyInjection;
using IbanNet.DependencyInjection.ServiceProvider;
using IbanNet.Registry.Swift;
using Mc2.CrudTest.Shared.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using ZymLabs.NSwag.FluentValidation;

namespace Mc2.CrudTest.Presentation;

public static class Injection
{
    public static IServiceCollection PresentationInjection(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment _env)
    {
        services.AddProblems(configuration, _env);

        services.AddHttpContextAccessor();
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
        services.AddIbanNet(opts => opts
        .UseRegistryProvider(new SwiftRegistryProvider())
        //.WithRule<MyCustomRule>()
        );
        services.AddScoped(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });


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