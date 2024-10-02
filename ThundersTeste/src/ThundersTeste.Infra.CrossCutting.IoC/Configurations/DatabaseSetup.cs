using System;
using ThundersTeste.Infra.Data.Context.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace ThundersTeste.Infra.CrossCutting.IoC.Configurations;

public static class DatabaseSetup
{
    public static void AddDatabaseSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton<ApplicationDbContext>();
        ApplicationDbContext.ConfigureMappings();
    }
}
