using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Uber.Voyage.Application.Abstractions;

namespace Uber.Voyage.Infrastructure.Data;

public class PostgresReadDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public PostgresReadDbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("UberVoyageRead")
            ?? throw new InvalidOperationException("ReadDb connection string is missing.");
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
