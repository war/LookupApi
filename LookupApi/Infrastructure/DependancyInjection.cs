using LookupApi.Infrastructure.Common.Interfaces;
using LookupApi.Infrastructure.Persistence;

namespace LookupApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory>(provider =>
            new DbConnectionFactory(configuration));

        return services;
    }
}
