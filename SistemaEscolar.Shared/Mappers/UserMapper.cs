using SistemaEscolar.Shared.Helpers;
using SistemaEscolar.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace SistemaEscolar.Shared.Mappers;

public class UserMapper
{
    public static Usuario Map(DbDataReader reader) => new()
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        Password = reader.GetSafeString("Password"),
        UserName = reader.GetSafeString("UserName"),
        CreatedAt = reader.GetSafeDateTime("CreatedAt"),
        Estatus = reader.GetSafeBoolean("Estatus")
    };
}
