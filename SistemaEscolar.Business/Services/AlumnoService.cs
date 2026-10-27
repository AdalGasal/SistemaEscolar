using SistemaEscolar.Business.Interfaces;
using SistemaEscolar.Data.Interfaces;
using SistemaEscolar.Shared.Models;

namespace SistemaEscolar.Business.Services;

public class AlumnoService : IAlumnoService
{
    private readonly IAlumnoRepository _alumnoRepository;

    public AlumnoService(IAlumnoRepository alumnoRepository)
    {
        _alumnoRepository = alumnoRepository;
    }

    public async Task<IEnumerable<Alumno>> GetAllAsync()
    {
        return await _alumnoRepository.GetAllAsync();
    }

    public async Task<Alumno?> GetByIdAsync(int id)
    {
        var alumno = await _alumnoRepository.GetByIdAsync(id);
        // Validación de existencia de alumno proporcionado
        if (alumno == null)
        {
            throw new KeyNotFoundException($"Alumno with ID {id} not found.");
        }
        return alumno;
    }

    public async Task AddAsync(Alumno obj)
    {
        // Validación de datos del alumno antes de agregar
        if (string.IsNullOrEmpty(obj.Nombre))        
            throw new ArgumentException("Nombre is required.");

        // ToDo: agregar más validaciones

        await _alumnoRepository.AddAsync(obj);
    }
}
