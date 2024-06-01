using Npgsql;
namespace Project_2._3;

// подключение и открытие БД
public class DatabaseService
{
    private static NpgsqlConnection? _connection; // открытое соежиненеи с бд
    // возврат строки подключения к БД
    private static string GetConnectionString()
    {
        return @"Host=10.30.0.137;Port=5432;Database=gr622_ofaan;Username=gr622_ofaan;Password=QWEasd123!@#";
    }
    
    // октрывает соединение с БД елси закрыто
    public static NpgsqlConnection GetSqlConnection()
    {
        if (_connection is null)
        {
            _connection = new NpgsqlConnection(GetConnectionString());
            _connection.Open();
        }
        
        return _connection;
    }
}