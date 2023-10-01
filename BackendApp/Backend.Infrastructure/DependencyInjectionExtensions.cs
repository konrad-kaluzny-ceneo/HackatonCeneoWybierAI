using Backend.Infrastructure.Models;
using Backend.Infrastructure.Repositories;
using Backend.Model.Interfaces;
using iHome.Microservices.Devices.Infrastructure.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddFirestoreRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAcademyRepository, FirestoreAcademyRepository>();
        return services;
    }

    public static IServiceCollection AddFirestoreConnectionFactory(this IServiceCollection services, Action<FirestoreOptions> optionsPredicate)
    {
        var options = new FirestoreOptions();
        optionsPredicate(options);

        services.AddScoped<IFirestoreConnectionFactory>(provider => 
            new FirestoreConnectionFactory(options));

        return services;
    }
}
