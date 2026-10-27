namespace SistemaEscolar.Business.Interfaces;

public interface ICarreraService
{
    Task<int> GetCountActiveAsync();
}
