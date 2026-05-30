using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Uber.Voyage.Application.Abstractions;
using Uber.Voyage.Domain.Repositories;
using Uber.Voyage.Infrastructure.Data;
using Uber.Voyage.Infrastructure.Kafka;
using Uber.Voyage.Infrastructure.Persistence;
using Uber.Voyage.Infrastructure.Persistence.Repositories;

namespace Uber.Voyage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<VoyageDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("UberVoyageWrite"))
                    .UseSnakeCaseNamingConvention());

        services.AddSingleton<IDbConnectionFactory, PostgresReadDbConnectionFactory>();

        services.AddScoped<IVoyageRepository, VoyageRepository>();

        services.AddSingleton<IKafkaProducer, KafkaProducer>();


        return services;
    }
}
