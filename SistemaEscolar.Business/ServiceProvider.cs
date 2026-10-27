using Microsoft.Extensions.DependencyInjection;
using SistemaEscolar.Business.Interfaces;
using SistemaEscolar.Business.Services;
using SistemaEscolar.Data.Interfaces;
using SistemaEscolar.Data.Repositories;

namespace SistemaEscolar.Business;

public static class ServiceProvider
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // Register repostories
        services.AddScoped<IAlumnoRepository, AlumnoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICarreraRepository, CarreraRepository>();

        // Aquí puedes registrar tus servicios de negocio
        services.AddScoped<IAlumnoService, AlumnoService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICarreraService, CarreraService>();


        return services;
    }
}
