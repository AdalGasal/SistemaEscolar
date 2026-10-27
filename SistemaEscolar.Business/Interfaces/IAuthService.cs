using SistemaEscolar.Shared.Dtos.Auth;

namespace SistemaEscolar.Business.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto login);
    Task RegisterAsync(LoginDto register);
}
