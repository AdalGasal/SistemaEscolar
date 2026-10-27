
namespace SistemaEscolar.Data.Interfaces;

public interface ICarreraRepository
{
    Task<int> GetCountActiveAsync();
}
