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
        var sql = @"
            SELECT 
                p.id, 
                p.razon_social AS RazonSocial, 
                p.tax_id AS TaxId, 
                p.type AS Type, 
                p.contact_name AS ContactName, 
                p.contact_email AS ContactEmail, 
                p.contact_phone AS ContactPhone, 
                p.address AS Address, 
                p.city AS City, 
                p.state AS State, 
                p.country AS Country, 
                p.postal_code AS PostalCode, 
                p.is_active AS IsActive, 
                p.created_by AS CreatedById, 
                p.created_at AS CreatedAt, 
                p.updated_at AS UpdatedAt,
                u.id, u.username, u.email, u.password_hash AS PasswordHash, u.first_name AS FirstName, u.last_name AS LastName, u.role, u.is_active AS IsActive, u.created_at AS CreatedAt, u.updated_at AS UpdatedAt
            FROM proveedor p 
            LEFT JOIN usuario u ON p.created_by = u.id 
            WHERE p.id = @Id";
        
        var partners = await connection.QueryAsync<Partner, User, Partner>(
            sql,
            (partner, user) =>
            {
                partner.CreatedBy = user;
                return partner;
            },
            new { Id = id },
            splitOn: "id"
        );
        
        return partners.FirstOrDefault();
    }

    public async Task<IEnumerable<Partner>> GetAllAsync()
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            SELECT 
                p.id, 
                p.razon_social AS RazonSocial, 
                p.tax_id AS TaxId, 
                p.type AS Type, 
                p.contact_name AS ContactName, 
                p.contact_email AS ContactEmail, 
                p.contact_phone AS ContactPhone, 
                p.address AS Address, 
                p.city AS City, 
                p.state AS State, 
                p.country AS Country, 
                p.postal_code AS PostalCode, 
                p.is_active AS IsActive, 
                p.created_by AS CreatedById, 
                p.created_at AS CreatedAt, 
                p.updated_at AS UpdatedAt,
                u.id, u.username, u.email, u.password_hash AS PasswordHash, u.first_name AS FirstName, u.last_name AS LastName, u.role, u.is_active AS IsActive, u.created_at AS CreatedAt, u.updated_at AS UpdatedAt
            FROM proveedor p 
            LEFT JOIN usuario u ON p.created_by = u.id";
        
        var partnerDict = new Dictionary<int, Partner>();
        
        var partners = await connection.QueryAsync<Partner, User, Partner>(
            sql,
            (partner, user) =>
            {
                if (!partnerDict.TryGetValue(partner.Id, out var partnerEntry))
                {
                    partnerEntry = partner;
                    partnerDict.Add(partner.Id, partnerEntry);
                }
                partnerEntry.CreatedBy = user;
                return partnerEntry;
            },
            splitOn: "id"
        );
        
        return partnerDict.Values;
    }

    public async Task<int> CreateAsync(Partner partner)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            INSERT INTO proveedor (
                razon_social, tax_id, type, contact_name, contact_email, 
                contact_phone, address, city, state, country, 
                postal_code, is_active, created_by, created_at, updated_at
            )
            VALUES (
                @RazonSocial, @TaxId, @Type, @ContactName, @ContactEmail,
                @ContactPhone, @Address, @City, @State, @Country,
                @PostalCode, @IsActive, @CreatedById, @CreatedAt, @UpdatedAt
            )
            RETURNING id";
        
        return await connection.QuerySingleAsync<int>(sql, partner);
    }

    public async Task UpdateAsync(Partner partner)
    {
        using var connection = _dbConnection.CreateConnection();
        var sql = @"
            UPDATE proveedor 
            SET razon_social = @RazonSocial,
                tax_id = @TaxId,
                type = @Type,
                contact_name = @ContactName,
                contact_email = @ContactEmail,
                contact_phone = @ContactPhone,
                address = @Address,
                city = @City,
                state = @State,
                country = @Country,
                postal_code = @PostalCode,
                is_active = @IsActive,
                updated_at = @UpdatedAt
            WHERE id = @Id";
        
        await connection.ExecuteAsync(sql, partner);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _dbConnection.CreateConnection();
        await connection.ExecuteAsync(
            "DELETE FROM proveedor WHERE id = @Id",
            new { Id = id }
        );
    }
} 