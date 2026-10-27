using Microsoft.Extensions.Configuration;
using SistemaEscolar.Data.Interfaces;
using SistemaEscolar.Shared.Mappers;
using SistemaEscolar.Shared.Models;
using System;
using System.Data.Common;

namespace SistemaEscolar.Data.Repositories;

public class UserRepository : BaseRepository<Usuario>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    { }

    public async Task<Usuario?> GetByUserNameAsync(string userName)
    {
        var query = "SELECT * FROM Usuarios WHERE UserName = @UserName";
        var parameters = new DbParameter[]
        {
            CreateParameter("UserName", userName)
        };
        return await QuerySingleAsync(query, UserMapper.Map, parameters);
    }

    public async Task<int> RegisterAsync(Usuario obj)
    {
        var query = """
            INSERT INTO Usuarios (UserName, Password, Estatus) VALUES 
            (@UserName, @Password, @Estatus); 
            SELECT CAST(SCOPE_IDENTITY() as int)
            """;

        var parameters = new DbParameter[]
        {
            CreateParameter("UserName", obj.UserName),
            CreateParameter("Password", obj.Password),
            CreateParameter("Estatus", obj.Estatus),
        };

        obj.Id = await ExecuteScalarAsync(query, parameters);
        return obj.Id;
    }
}
