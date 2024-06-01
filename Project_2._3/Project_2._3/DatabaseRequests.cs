using Npgsql;
namespace Project_2._3;

// методы для отправки запросов в БД
public class DatabaseRequests
{
    // авторизация
    public static int? Authorization(string username, string password)
    {
        var querySql = $"SELECT Id FROM Authorize WHERE Use = '{username}' AND Password = '{password}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        if (reader.Read()) // успешный запрос
        {
            return reader.GetInt32(0); // получения id
        }
        else
        {
            return null;
        }
    }
    
    // регистрация
    public static void Registration(string Use,string Password)
    {
        var querySql = $"INSERT INTO Authorize(Use, Password) VALUES ('{Use}', '{Password}')"; // БД запрос
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); // команда с запросом в БД
        cmd.ExecuteNonQuery();
    }
    
    // добавить задачу
    public static void AddNewTask(string title, string description, DateTime dueDate, int? UserId)
    {
        var querySql = $"INSERT INTO Tasks(Title, Description, DueDate, UserId) VALUES ('{title}', '{description}', '{dueDate}', '{UserId}')"; // БД запрос
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); // команда с запросом в БД
        cmd.ExecuteNonQuery();
    }

    // удалить задачу
    public static void DeleteTask(int taskId, int? UserId)
    {
        var querySql = $"DELETE FROM Tasks WHERE Id = '{taskId}'  AND UseId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }

    // редактировать задачу
    public static void EditTask(string title, string description, DateTime dueDate, int taskId, int? UserId)
    {
        var querySql = $"UPDATE Tasks SET Title =  '{title}', Description = '{description}', DueDate = '{dueDate}' WHERE id = '{taskId}' AND UserId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }

    // просмотр всех задач
    public static void GetAllTasks(int? UserId)
    {
        var querySql = $"SELECT Id, Title FROM Tasks WHERE UserId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"№: {reader[0]} Название: {reader[1]}");
        }
    }

    // просмотр конкретной задачи
    public static void GetОneTasks(int taskId, int? UserId)
    {
        var querySql = $"SELECT Title, Description, DueDate FROM Tasks WHERE id = '{taskId}' AND UserId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Название: {reader[0]} \nОписание: {reader[1]} \nДата окончания: {reader[2]}");
        }
    }
    
    // просмотр предстоящих задач
    public static void GetUpcomingTasks(int? UserId)
    {
        var querySql = $"SELECT Id, Title FROM Tasks WHERE DueDate > current_date AND UserId = '{UserId}' "; // current_date - текущая дата
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"№: {reader[0]} Название: {reader[1]}");
        }
    }
    
    // просмотр выполненных задач
    public static void GetСompletedTasks(int? UserId)
    {
        var querySql = $"SELECT Id, Title FROM Tasks WHERE DueDate < current_date AND UserId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"№: {reader[0]} Название: {reader[1]}");
        }
    }
    
    // просмотр задач на сегодня
    public static void GetTodayTasks(int? UserId)
    {
        var querySql = $"SELECT Id, Title FROM Tasks WHERE DueDate = current_date AND UserId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"№: {reader[0]} Название: {reader[1]}");
        }
    }
    
    // просмотр задач на завтра
    public static void GetTomorrowTasks(int? UserId)
    {
        var querySql = $"SELECT Id, Title FROM Tasks WHERE DueDate = current_date + INTERVAL '1 day' AND UserId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"№: {reader[0]} Название: {reader[1]}");
        }
    }
    
    // просмотр задач на неделю
    public static void GetWeekTasks(int? UserId)
    {
        var querySql = $"SELECT Id, Title FROM Tasks WHERE DueDate = current_date + INTERVAL '7 day' AND UserId = '{UserId}' ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"№: {reader[0]} Название: {reader[1]}");
        }
    }
}