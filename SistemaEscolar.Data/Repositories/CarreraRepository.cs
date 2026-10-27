using Microsoft.Extensions.Configuration;
using SistemaEscolar.Data.Interfaces;
using SistemaEscolar.Shared.Models;

namespace SistemaEscolar.Data.Repositories;

public class CarreraRepository : BaseRepository<Carrera>, ICarreraRepository
{
    public CarreraRepository(IConfiguration configuration) :
        base(configuration)
    { }

    public async Task<int> GetCountActiveAsync()
    {
        string query =
            """
            SELECT
            	COUNT(id) as totalActivas
            FROM Carreras
            WHERE estatus = 1
            """;

        return await ExecuteScalarAsync(query);
    }
}
