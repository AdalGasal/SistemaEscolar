using SistemaEscolar.Business.Interfaces;
using SistemaEscolar.Data.Interfaces;

namespace SistemaEscolar.Business.Services;

public class CarreraService : ICarreraService
{
    private readonly ICarreraRepository _carreraRepository;

    public CarreraService(ICarreraRepository carreraRepository)
    {
        _carreraRepository = carreraRepository;
    }

    public async Task<int> GetCountActiveAsync() => 
        await _carreraRepository.GetCountActiveAsync();
}
