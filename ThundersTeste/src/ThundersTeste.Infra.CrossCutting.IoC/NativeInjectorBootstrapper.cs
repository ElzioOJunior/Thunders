using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Infra.Data.UnitOfWork;
using ThundersTeste.Infra.Data.Repositories;
using ThundersTeste.Infra.CrossCutting.IoC.Configurations;
using ThundersTeste.Infra.CrossCutting.Environments.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using MediatR;
using System;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;

namespace ThundersTeste.Infra.CrossCutting.IoC;

public static class NativeInjectorBootstrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        RegisterData(services);
        RegisterMediatR(services);
        RegisterEnvironments(services, configuration);
    }

    private static void RegisterData(IServiceCollection services)
    {
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDatabaseSetup();
    }

    private static void RegisterMediatR(IServiceCollection services)
    {
        const string applicationAssemblyName = "ThundersTeste.Application";
        var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
    }

    private static void RegisterEnvironments(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>());
    }

}
