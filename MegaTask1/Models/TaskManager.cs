using MegaTask1;

namespace MegaTask1.Models
{
    public class TaskManager
    {
        public List<TaskItem> Tasks = [];
        private int _nextId = 1;
        
        public static void PriorityMenue()
        {
            InfoMessage("Priority Options: \n");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. High");
        }
        public static void StatusMenue()
        {
            InfoMessage("Status Options: \n");
            Console.WriteLine("1. Pending");
            Console.WriteLine("2. InProgress");
            Console.WriteLine("3. Completed");
        }
        public void AddTask()
        {
            TaskItem task = new();
            task.Id = _nextId++;
            task.Title = Program.ReadStrvals("Title");
            task.Description = Program.ReadStrvals("Description");
            task.DueDate = Program.ReadDate("DueDate");

            PriorityMenue();
            task.Priority = (Priority)Program.ReadOption(1, 3);

            StatusMenue();
            task.Status = (Status)Program.ReadOption(1, 3);

            Tasks.Add(task);
            SuccessMessage("Task added successfully!\n");
        }
        public void ShowTasks()
        {
            if (Tasks.Count == 0)
            {
               FailureMessage("No tasks available.");
                return;
            }
            /// شوية linq ميضرشش
            var completedTasks = Tasks.Where(t => t.Status == Status.Completed).ToList();
            var overdueTasks = Tasks.Where(t => t.Status != Status.Completed && t.DueDate < DateTime.Now).ToList();
            var activeTasks = Tasks.Where(t => t.Status != Status.Completed && t.DueDate >= DateTime.Now).ToList();

            PrintTaskSection("Active Tasks", activeTasks);
            PrintTaskSection("Overdue Tasks", overdueTasks);
            PrintTaskSection("Completed Tasks", completedTasks);
        }
        private static void PrintTaskSection(string header, List<TaskItem> tasks)
        {
            SuccessMessage("\n" + header);
            Console.WriteLine("------------");
            if (tasks.Count == 0)
            {
                Console.WriteLine($"No {header} Found");
            }
            else {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"[{task.Id}] {task.Title} | Due: {task.DueDate:yyyy-MM-dd} | Priority: {task.Priority} | Status: {task.Status}");
                    Console.WriteLine($"    Desc: {task.Description} \n");
                }
            }
            Console.WriteLine("------------");
        }
        public TaskItem? FindTask(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                return task;
            }

            FailureMessage($"Task With ID ({id}) was not found.");
            return null;
        }
        public TaskItem? FindTask(string title)
        {
            var task = Tasks.FirstOrDefault(t => string.Equals(t.Title.Trim(), title.Trim(), StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                return task;
            }

            FailureMessage($"Task With Title ({title}) was not found.");
            return null;
        }
        public static void SuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void FailureMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void InfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(message);
            Console.ResetColor();
        }
        public void UpdateTask(TaskItem task)
        {

            if (task is not null)
            {
                SuccessMessage("Task found. You can now update the task details.");
                Console.WriteLine("Select the new status:");
                StatusMenue();
                task.Status = (Status)Program.ReadOption(1, 3);
                SuccessMessage("Task updated successfully!");
            }
            else
            {
                FailureMessage("Task not found.");
            }
            
        }
        public void DeleteTask(TaskItem task)
        {
            if (task is not null)
            {
                while (true)
                {
                    TaskManager.InfoMessage($"Are you sure you want to delete '{task.Title}'? (y/n): ");
                    string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
                    if (confirm == "y")
                    {
                        Tasks.Remove(task);
                        SuccessMessage("Task deleted successfully!");
                        break;
                    }
                    else if (confirm == "n")
                    {
                        InfoMessage("Delete operation cancelled.\n");
                        break;
                    }
                    else
                    {
                        FailureMessage("Invalid input. Please enter 'y' or 'n'.");
                    }
                }
            }
            else
            {
                FailureMessage("Task not found.");
            }
        }   
    }
}
