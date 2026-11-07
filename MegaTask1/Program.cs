using MegaTask1.Models;

namespace MegaTask1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new();
            bool running = true;
            TaskManager.SuccessMessage("Welcome To Task Manager");
            while (running)
            {
                ShowMenu();
                int Op = ReadOption(1, 5);
                switch(Op)
                {
                    case 1:
                        taskManager.AddTask();
                        break;
                    case 2:
                        taskManager.ShowTasks();
                        break;
                    case 3:
                        var taskToUpdate = GetTaskBySearch(taskManager);
                        if (taskToUpdate != null)
                            taskManager.UpdateTask(taskToUpdate);
                        break;

                    case 4:
                        var taskToDelete = GetTaskBySearch(taskManager);
                        if (taskToDelete != null)
                            taskManager.DeleteTask(taskToDelete);
                        break;
                    case 5:
                        running = false;
                        TaskManager.SuccessMessage("Exiting application, Goodbye!");
                        break;

                }

            }
        }
        public static void ShowMenu()
        {
            TaskManager.InfoMessage("\nSelect an option:\n");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Update Task Status");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
        }
        public static string ReadStrvals(string prop)
        {
            while (true)
            {
                TaskManager.InfoMessage($"Enter the {prop}: ");
                string Value = Console.ReadLine() ?? string.Empty;
                if (!(Value == String.Empty))
                {
                    return Value;
                }
                else
                {
                    TaskManager.FailureMessage($"Invalid input. Please enter a valid {prop}.");
                    continue;
                }
            }
        }
        public static DateTime ReadDate(string prop)
        {
            while (true)
            {
                TaskManager.InfoMessage($"Enter {prop} (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    return date;
                }
                TaskManager.FailureMessage("Invalid date format. Please try again (e.g., 2026-11-15).");
            }
        }
        public static int ReadOption(int min, int max)
        {
            while (true)
            {
                TaskManager.InfoMessage($"Choose an option ({min}-{max}): ");
                if (int.TryParse(Console.ReadLine(), out int option) && option >= min && option <= max)
                {
                    return option;
                }
                TaskManager.FailureMessage($"Invalid choice. Enter a number between {min} and {max}.");
            }
        }
        public static int ReadInt(string prop)
        {
            while (true)
            {
                TaskManager.InfoMessage($"Enter {prop}: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }
                TaskManager.FailureMessage("Invalid input. Please enter a valid integer.");
            }
        }
        public static void SearchMenu()
        {
            Console.WriteLine("Search Options:");
            Console.WriteLine("1. By ID");
            Console.WriteLine("2. By Title");
        }
        public static TaskItem? GetTaskBySearch(TaskManager taskManager)
        {
            SearchMenu();
            int searchOption = ReadOption(1, 2);
            return searchOption switch
            {
                1 => SearchIDHandler(taskManager),
                2 => SearchTitleHandler(taskManager),
                _ => null
            };
        }
        public static TaskItem? SearchIDHandler(TaskManager taskManager)
        {
            while (true)
            {
                int taskId = ReadInt("Task ID");
                var task = taskManager.FindTask(taskId);

                if (task != null)
                {
                    return task;
                }

                TaskManager.InfoMessage("Try again? (y/n): ");

                while (true)
                {
                    string response = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

                    if (response == "n")
                    {
                        return null; // يخرج مباشرة ويرجع null
                    }
                    if (response == "y")
                    {
                        break; // يكسر حلقة الـ (y/n) ويعيد البحث من جديد
                    }

                    TaskManager.FailureMessage("Invalid input. Please enter 'y' or 'n': ");
                }
            }
        }
        public static TaskItem? SearchTitleHandler(TaskManager taskManager)
        {
            while (true)
            {
                string title = ReadStrvals("Title");
                var task = taskManager.FindTask(title);

                if (task != null)
                {
                    return task;
                }
                TaskManager.InfoMessage("Try again? (y/n): ");
                while (true)
                {
                    string response = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

                    if (response == "n")
                    {
                        return null;
                    }
                    if (response == "y")
                    {
                        break; 
                    }

                    TaskManager.FailureMessage("Invalid input. Please enter 'y' or 'n': ");
                }
            }
        }
    }
}
