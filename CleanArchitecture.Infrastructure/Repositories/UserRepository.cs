using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseConnection _dbConnection;

    public UserRepository(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM users WHERE id = @Id",
            new { Id = id }
        );
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM users WHERE email = @Email",
            new { Email = email }
        );
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QueryAsync<User>("SELECT * FROM users");
    }

    public async Task<int> CreateAsync(User user)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            INSERT INTO users (name, email, password, created_at, updated_at)
            VALUES (@Name, @Email, @Password, @CreatedAt, @UpdatedAt)
            RETURNING id";
        
        return await connection.QuerySingleAsync<int>(sql, user);
    }

    public async Task UpdateAsync(User user)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            UPDATE users 
            SET name = @Name,
                email = @Email,
                password = @Password,
                updated_at = @UpdatedAt
            WHERE id = @Id";
        
        await connection.ExecuteAsync(sql, user);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _dbConnection.CreateConnection();
        await connection.ExecuteAsync(
            "DELETE FROM users WHERE id = @Id",
            new { Id = id }
        );
    }
} 