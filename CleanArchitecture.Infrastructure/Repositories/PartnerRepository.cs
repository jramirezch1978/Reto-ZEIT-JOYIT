using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories;

public class PartnerRepository : IPartnerRepository
{
    private readonly DatabaseConnection _dbConnection;

    public PartnerRepository(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Partner> GetByIdAsync(int id)
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Partner>(
            "SELECT * FROM partners WHERE id = @Id",
            new { Id = id }
        );
    }

    public async Task<IEnumerable<Partner>> GetAllAsync()
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QueryAsync<Partner>("SELECT * FROM partners");
    }

    public async Task<int> CreateAsync(Partner partner)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            INSERT INTO partners (name, description, created_at, updated_at)
            VALUES (@Name, @Description, @CreatedAt, @UpdatedAt)
            RETURNING id";
        
        return await connection.QuerySingleAsync<int>(sql, partner);
    }

    public async Task UpdateAsync(Partner partner)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            UPDATE partners 
            SET name = @Name,
                description = @Description,
                updated_at = @UpdatedAt
            WHERE id = @Id";
        
        await connection.ExecuteAsync(sql, partner);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _dbConnection.CreateConnection();
        await connection.ExecuteAsync(
            "DELETE FROM partners WHERE id = @Id",
            new { Id = id }
        );
    }
} 