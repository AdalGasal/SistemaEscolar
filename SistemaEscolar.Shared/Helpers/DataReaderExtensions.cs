using System.Data.Common;

namespace SistemaEscolar.Shared.Helpers;

public static class DataReaderExtensions
{
    public static string GetSafeString(this DbDataReader reader, string columnName, string defaultValue = "")
    {
        int ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? defaultValue : reader.GetString(ordinal);
    }

    public static string? GetSafeNullableString(this DbDataReader reader, string columnName)
    {
        int ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
    }

    public static DateTime GetSafeDateTime(this DbDataReader reader, string columnName, DateTime? defaultValue = null)
    {
        int ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? (defaultValue ?? DateTime.MinValue) : reader.GetDateTime(ordinal);
    }

    public static char GetSafeChar(this DbDataReader reader, string columnName, char defaultValue = ' ')
    {
        int ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
            return defaultValue;

        string value = reader.GetString(ordinal);
        return string.IsNullOrEmpty(value) ? defaultValue : value[0];
    }

    public static bool GetSafeBoolean(this DbDataReader reader, string columnName, bool defaultValue = false)
    {
        int ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? defaultValue : reader.GetBoolean(ordinal);
    }
}
