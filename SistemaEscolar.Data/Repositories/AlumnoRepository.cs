using Microsoft.Extensions.Configuration;
using SistemaEscolar.Data.Interfaces;
using SistemaEscolar.Shared.Mappers;
using SistemaEscolar.Shared.Models;
using System.Data.Common;

namespace SistemaEscolar.Data.Repositories;

public class AlumnoRepository : BaseRepository<Alumno>, IAlumnoRepository
{
    public AlumnoRepository(IConfiguration configuration) : 
        base(configuration) { }

    public async Task<List<Alumno>> GetAllAsync()
    {
        string query = "SELECT * FROM Alumnos";

        return await QueryAsync(query, AlumnoMapper.Map);
    }

    public async Task<Alumno?> GetByIdAsync(int id)
    {
        string query = "SELECT * FROM Alumnos WHERE Id = @id";
        var parameters = new DbParameter[]
        {
            CreateParameter("id", id)
        };
        var results = await QueryAsync(query, AlumnoMapper.Map, parameters);
        return results.FirstOrDefault();
    }

    public async Task<bool> AddAsync(Alumno obj)
    {
        string query = """
            INSERT INTO Alumnos 
            (matricula, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, fechaIngreso, sexo, estatus)
            VALUES 
            (@matricula, @nombre, @apellidoPaterno, @apellidoMaterno, @fechaNacimiento, @fechaIngreso, @sexo, @estatus)
            """;

        var parameters = new DbParameter[]
        {
            CreateParameter("matricula", obj.Matricula),
            CreateParameter("nombre", obj.Nombre),
            CreateParameter("apellidoPaterno", obj.ApellidoPaterno),
            CreateParameter("apellidoMaterno", obj.ApellidoMaterno),
            CreateParameter("fechaNacimiento", obj.FechaNacimiento),
            CreateParameter("fechaIngreso", obj.FechaIngreso),
            CreateParameter("sexo", obj.Sexo),
            CreateParameter("estatus", obj.Estatus),
        };

        var result = await ExecuteAsync(query, parameters) > 0;
        return result;
    }
}