using System;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Data;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(IConfiguration configuration)
    {
        var baseConnectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
        
        var username = configuration.GetValue<string>("DatabaseCredentials:Username")
            ?? throw new ArgumentNullException(nameof(configuration), "Database username not found.");
        
        var password = configuration.GetValue<string>("DatabaseCredentials:Password")
            ?? throw new ArgumentNullException(nameof(configuration), "Database password not found.");

        _connectionString = $"{baseConnectionString};Username={username};Password={password}";
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
} 