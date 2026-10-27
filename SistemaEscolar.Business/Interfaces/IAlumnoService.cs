using SistemaEscolar.Shared.Models;

namespace SistemaEscolar.Business.Interfaces;

public interface IAlumnoService
{
    Task<IEnumerable<Alumno>> GetAllAsync();
    Task<Alumno?> GetByIdAsync(int id);
    Task AddAsync(Alumno obj);
}
