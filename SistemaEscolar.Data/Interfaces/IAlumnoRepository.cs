using SistemaEscolar.Shared.Models;

namespace SistemaEscolar.Data.Interfaces;

public interface IAlumnoRepository
{
    Task<List<Alumno>> GetAllAsync();
    Task<Alumno?> GetByIdAsync(int id);
    Task<bool> AddAsync(Alumno obj);
}
