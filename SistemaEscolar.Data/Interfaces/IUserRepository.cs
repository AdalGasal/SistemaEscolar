using SistemaEscolar.Shared.Models;

namespace SistemaEscolar.Data.Interfaces;

public interface IUserRepository
{
    Task<int> RegisterAsync(Usuario obj);
    Task<Usuario?> GetByUserNameAsync(string userName);
}
