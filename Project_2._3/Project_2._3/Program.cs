using System.Threading.Channels;
using Project_2._3;

// работа программы
public class Program
{
    public static void Main()
    {
        const int Exit = 0;
        string title; // название
        string description; // содержание
        DateTime dueDate; // дата до окончания
        int taskId; // номер задачи
		
        // Авторизация
        int? UserId = null; // айди пользователя
        Console.Write("Вход - 1; \nРегистрация - 2 \nВвод: ");
        int systemLogin = int.Parse(Console.ReadLine());
        switch (systemLogin)
        {
            case 1: // вход
                do
                {
                    Console.WriteLine("Введите логин: ");
                    string username = Console.ReadLine();

                    Console.WriteLine("Введите пароль: ");
                    string password = Console.ReadLine();

                    UserId = DatabaseRequests.Authorization(username, password);
                    if (UserId == null)
                    {
                        Console.WriteLine("Неверный логин или пароль");
                        Console.WriteLine(); // пробел
                    }
                } while (UserId == null);
                break;
            case 2: // регистрация
                Console.WriteLine("Введите логин: ");
                string Use = Console.ReadLine();

                Console.WriteLine("Введите пароль: ");
                string Password = Console.ReadLine();
                
                DatabaseRequests.Registration(Use, Password);
                break;
            default:
                Console.WriteLine("Неверный ввод");
                break;
        }
        
        Console.Write("Выход - 0 \nДобавить задачу - 1 \nУдалить задачу - 2 \nРедактировать задачу - 3" +
                      "\nПросмотр всех задач - 4 \nПросмотр задачи - 5 \nПросмотр предстоящих задач - 6" +
                      "\nПросмотр выполненных задач - 7 \nЗадачи на сегодня - 8 \nЗадачи на завтра - 9" +
                      "\nЗадачи на неделю - 10");
        while (true)
        {
            Console.WriteLine(); // пробел
            Console.Write("Выполнить: ");
            int task = int.Parse(Console.ReadLine());
            
            if (task == Exit)
            {
                break;
            }
                
            switch (task)
            {
                case 1: // добавить задачу
                    Console.Write("Название задачи: ");
                    title = Console.ReadLine();
                    
                    Console.Write("Содержание: ");
                    description = Console.ReadLine();
                    
                    Console.Write("Дата окончания(ГГГГ-ММ-ДД): ");
                    dueDate = DateTime.Parse(Console.ReadLine());
                    
                    DatabaseRequests.AddNewTask(title, description, dueDate, UserId); 
                    break;
                
                case 2: // удалить задачу
                    Console.Write("Введите номер задачи: ");
                    taskId = int.Parse(Console.ReadLine());
                    
                    DatabaseRequests.DeleteTask(taskId, UserId);
                    break;
                
                case 3: // редактировать задачу
                    Console.Write("Номер задачи: ");
                    taskId = int.Parse(Console.ReadLine());
                    
                    Console.Write("Название задачи: ");
                    title = Console.ReadLine();
                    
                    Console.Write("Содержание: ");
                    description = Console.ReadLine();
                    
                    Console.Write("Дата окончания(ДД-ММ-ГГГГ): ");
                    dueDate = DateTime.Parse(Console.ReadLine());
                    
                    DatabaseRequests.EditTask(title, description, dueDate, taskId, UserId);
                    break;
                
                case 4: // просмотр всех задач
                    DatabaseRequests.GetAllTasks(UserId);
                    break;
                
                case 5: // просмотр задачи
                    Console.Write("Номер задачи: ");
                    taskId = int.Parse(Console.ReadLine());
                    
                    DatabaseRequests.GetОneTasks(taskId, UserId);
                    break;
                
                case 6: // просмотр предстоящих задач 
                    DatabaseRequests.GetUpcomingTasks(UserId);
                    break;
                case 7: // просмотр выполненных задач
                    DatabaseRequests.GetСompletedTasks(UserId);
                    break;
                case 8: // задачи на сегодня
                    DatabaseRequests.GetTodayTasks(UserId);
                    break;
                case 9: // задачи на завтра
                    DatabaseRequests.GetTomorrowTasks(UserId);
                    break;
                case 10: // задачи на неделю
                    DatabaseRequests.GetWeekTasks(UserId);
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
    }
}