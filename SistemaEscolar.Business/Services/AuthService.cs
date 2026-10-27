using SistemaEscolar.Business.Helpers;
using SistemaEscolar.Business.Interfaces;
using SistemaEscolar.Data.Interfaces;
using SistemaEscolar.Shared.Dtos.Auth;
using SistemaEscolar.Shared.Helpers;
using SistemaEscolar.Shared.Models;

namespace SistemaEscolar.Business.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> LoginAsync(LoginDto login)
    {
        // 1. Obtener el usuario de la base de datos según el nombre de usuario proporcionado
        var user = await _userRepository.GetByUserNameAsync(login.UserName);

        // 2. Verificar si el usuario existe y si la contraseña es correcta
        // Desencriptando contraseña y comparando con login.Password
        if (user == null)
            throw new ValidationException("Usuario no encontrado");

        if (!user.Estatus)
            throw new ValidationException("Usuario inactivo");

        if (!PasswordHasher.Verify(login.Password, user.Password))
            throw new ValidationException("Contraseña incorrecta");

        // 3. Si la autenticación es exitosa, devolver true; de lo contrario, devolver false
        return true;
    }

    public async Task RegisterAsync(LoginDto register)
    {
        var usuario = new Usuario();
        usuario.UserName = register.UserName;
        usuario.Password = PasswordHasher.Hash(register.Password);
        usuario.Estatus = true;

        await _userRepository.RegisterAsync(usuario);
    }
}
