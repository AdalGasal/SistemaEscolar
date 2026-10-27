using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace SistemaEscolar.Data.Repositories;

public class BaseRepository<T> where T : class
{
    protected readonly string _connectionString;
    protected readonly DbProviderFactory _connectionFactory;

    public BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");

        _connectionFactory = SqlClientFactory.Instance;
    }

    private (DbConnection Connection, DbCommand Command) CreateConnectionAndCommand(string query, DbParameter[]? parameters = null)
    {
        var connection = _connectionFactory.CreateConnection();
        if (connection == null)
            throw new InvalidOperationException("Failed to create a database connection.");

        connection.ConnectionString = _connectionString;

        var command = connection.CreateCommand();
        command.CommandText = query;
        
        if (parameters != null)
            command.Parameters.AddRange(parameters);

        return (connection, command);
    }

    protected DbParameter CreateParameter(string name, object value)
    {
        var parameter = _connectionFactory.CreateParameter();
        if (parameter == null)
            throw new InvalidOperationException("Failed to create a database parameter.");

        parameter.ParameterName = name;
        parameter.Value = value ?? DBNull.Value;

        return parameter;
    }

    protected async Task<List<T>> QueryAsync(string query, Func<DbDataReader, T> mapFunction, DbParameter[]? parameters = null)
    {
        var results = new List<T>();

        // 1. Inicializamos los objetos de infraestructura de datos de forma agnóstica
        var (connection, command) = CreateConnectionAndCommand(query, parameters);

        // 2. 'await using' garantiza que tanto la conexión como el comando se cierren 
        // y liberen su memoria al terminar el método, pase lo que pase (incluso si hay errores).
        await using (connection)
        {
            await using (command)
            {
                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                    results.Add(mapFunction(reader));
            }
        } // <-- Aquí la conexión se cierra automáticamente y regresa al Pool de conexiones

        return results;
    }

    protected async Task<T?> QuerySingleAsync(string query, Func<DbDataReader, T> mapFunction, DbParameter[]? parameters = null)
    {
        // 1. Inicializamos los objetos de infraestructura de datos de forma agnóstica
        var (connection, command) = CreateConnectionAndCommand(query, parameters);

        // 2. 'await using' garantiza que tanto la conexión como el comando se cierren 
        // y liberen su memoria al terminar el método, pase lo que pase (incluso si hay errores).
        await using (connection)
        {
            await using (command)
            {
                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                    return mapFunction(reader);
            }
        } // <-- Aquí la conexión se cierra automáticamente y regresa al Pool de conexiones

        return null;
    }

    protected async Task<int> ExecuteAsync(string query, DbParameter[]? parameters = null)
    {
        // 1. Inicializamos los objetos de infraestructura de datos de forma agnóstica
        var (connection, command) = CreateConnectionAndCommand(query, parameters);

        // 2. 'await using' garantiza que tanto la conexión como el comando se cierren 
        // y liberen su memoria al terminar el método, pase lo que pase (incluso si hay errores).
        await using (connection)
        {
            await using (command)
            {
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        } // <-- Aquí la conexión se cierra automáticamente y regresa al Pool de conexiones
    }

    protected async Task<int> ExecuteScalarAsync(string query, DbParameter[]? parameters = null)
    {
        // 1. Inicializamos los objetos de infraestructura de datos de forma agnóstica
        var (connection, command) = CreateConnectionAndCommand(query, parameters);

        // 2. 'await using' garantiza que tanto la conexión como el comando se cierren 
        // y liberen su memoria al terminar el método, pase lo que pase (incluso si hay errores).
        await using (connection)
        {
            await using (command)
            {
                await connection.OpenAsync();
                return (int)await command.ExecuteScalarAsync();
            }
        } // <-- Aquí la conexión se cierra automáticamente y regresa al Pool de conexiones
    }
}
