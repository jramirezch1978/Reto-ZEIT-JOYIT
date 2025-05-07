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
            "SELECT * FROM usuario WHERE id = @Id",
            new { Id = id }
        );
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM usuario WHERE email = @Email",
            new { Email = email }
        );
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM usuario WHERE username = @Username",
            new { Username = username }
        );
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = _dbConnection.CreateConnection();
        return await connection.QueryAsync<User>("SELECT * FROM usuario");
    }

    public async Task<int> CreateAsync(User user)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            INSERT INTO usuario (
                username, email, password_hash, first_name, last_name,
                role, is_active, created_at, updated_at
            )
            VALUES (
                @Username, @Email, @PasswordHash, @FirstName, @LastName,
                @Role, @IsActive, @CreatedAt, @UpdatedAt
            )
            RETURNING id";
        
        return await connection.QuerySingleAsync<int>(sql, user);
    }

    public async Task UpdateAsync(User user)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            UPDATE usuario 
            SET username = @Username,
                email = @Email,
                password_hash = @PasswordHash,
                first_name = @FirstName,
                last_name = @LastName,
                role = @Role,
                is_active = @IsActive,
                updated_at = @UpdatedAt
            WHERE id = @Id";
        
        await connection.ExecuteAsync(sql, user);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _dbConnection.CreateConnection();
        await connection.ExecuteAsync(
            "DELETE FROM usuario WHERE id = @Id",
            new { Id = id }
        );
    }
} 