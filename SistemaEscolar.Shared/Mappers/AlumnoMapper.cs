using SistemaEscolar.Shared.Helpers;
using SistemaEscolar.Shared.Models;
using System.Data.Common;

namespace SistemaEscolar.Shared.Mappers;

public static class AlumnoMapper
{
    public static Alumno Map(DbDataReader reader) => new()
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        Nombre = reader.GetSafeString("Nombre"),
        ApellidoPaterno = reader.GetSafeString("ApellidoPaterno"),
        ApellidoMaterno = reader.GetSafeString("ApellidoMaterno"),
        FechaNacimiento = reader.GetSafeDateTime("FechaNacimiento"),
        FechaIngreso = reader.GetSafeDateTime("FechaIngreso"),
        Matricula = reader.GetSafeString("Matricula"),
        Sexo = reader.GetSafeChar("Sexo"),
        Estatus = reader.GetSafeBoolean("Estatus")
    };

    // Conversión de modelo => dto

    // Conversión de dto => modelo

}
