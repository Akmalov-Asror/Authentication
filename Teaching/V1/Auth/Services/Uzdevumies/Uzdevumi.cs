using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Teaching.Common.Data;
using Teaching.Common.Entities.Dto_s;
using Teaching.V1.Auth.Services.Interfaces;

public class Uzdevumi : IUzdevumi
{
    private readonly IDataConnection _dataConnection;

    public Uzdevumi(IDataConnection dataConnection) 
        => _dataConnection = dataConnection;

    public async ValueTask<List<UzdevumiDto>> GetByFilter()
    {
        using IDbConnection conn = _dataConnection.GetConnection();
        var sql = "SELECT Id, Title, Description FROM Uzdevumi"; 
        var result = await conn.QueryAsync<UzdevumiDto>(sql);
        return result.AsList();
    }

    public async ValueTask<CreateDto> Create(CreateDto dto)
    {
        using IDbConnection conn = _dataConnection.GetConnection();
        var sql = @"INSERT INTO Uzdevumi (Title, Description)
                    VALUES (@Title, @Description);
                    SELECT last_insert_rowid()";
        var id = await conn.ExecuteScalarAsync<int>(sql, new { dto.Name, dto.Description });
        return dto;
    }

    public async ValueTask<UpdateUzdevumiDto> UpdateUzdevumi(Guid id, UpdateUzdevumiDto dto)
    {
        using IDbConnection conn = _dataConnection.GetConnection();
        var sql = @"UPDATE Uzdevumi 
                    SET Title = @Title,
                        Description = @Description
                    WHERE Id = @Id";
        await conn.ExecuteAsync(sql, new { dto.Name, dto.Description, id });
        return dto;
    }
}
