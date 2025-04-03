using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class TodoApp
{
    private List<TodoTask> TodoList;

    private TodoApp()
    {
        TodoList = new List<TodoTask>();
    }

    public static TodoApp New()
    {
        return new TodoApp();
    }

    public async Task<TodoTask> Create(string description)
    {
        var task = new TodoTask()
        {
            Id = Guid.NewGuid().ToString(),
            Description = description,
            CreatedOn = DateTime.Now.ToString("dd:MM:ss"),
            CompletedOn = null,
            IsCompleted = false
        };

        TodoList.Add(task);
        return await Task.FromResult(task);
    }

    public async Task<bool> Delete(string taskId)
    {
        var task = TodoList.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            TodoList.Remove(task);
            Console.WriteLine($"Task {taskId} has been deleted.");
            return await Task.FromResult(true);
        }

        Console.WriteLine($"Task with id {taskId} not found.");
        return await Task.FromResult(false);
    }

    public async Task<TodoTask?> Update(string taskId, string description)
    {
        var task = TodoList.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            task.Description = description;
            return await Task.FromResult(task);
        }

        Console.WriteLine($"Could not find task {taskId} in the list.");
        return await Task.FromResult<TodoTask?>(null);
    }

    public async Task<TodoTask?> Complete(string taskId)
    {
        var task = TodoList.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            task.CompletedOn = DateTime.Now.ToString("dd:MM:ss");
            task.IsCompleted = true;
            return await Task.FromResult(task);
        }

        Console.WriteLine($"Could not find task {taskId} in the list");
        return await Task.FromResult<TodoTask?>(null);
    }

    public async Task<List<TodoTask>> Filter(string criteria)
    {
        List<TodoTask> filteredTasks;
        if (criteria == "completed")
        {
            filteredTasks = TodoList.Where(t => t.IsCompleted).ToList();
        }
        else if (criteria == "todo")
        {
            filteredTasks = TodoList.Where(t => !t.IsCompleted).ToList();
        }
        else
        {
            filteredTasks = TodoList.Where(t => t.Description.ToLower().Contains(criteria.ToLower())).ToList();
        }

        return await Task.FromResult(filteredTasks);
    }

    public List<TodoTask> GetAllTasks()
    {
        return TodoList;
    }
}

class TodoTask
{
    public required string Id { get; set; }
    public required string Description { get; set; }
    public required string CreatedOn { get; set; }
    public string? CompletedOn { get; set; }
    public bool IsCompleted { get; set; }
}


class Program
{
    static async Task Main()
    {
        TodoApp app = TodoApp.New();

        Console.WriteLine("\n");
        var task1 = await app.Create("First Task");
        Console.WriteLine($"Created Task: {task1.Description}, ID: {task1.Id}, Created On: {task1.CreatedOn}");
        Console.WriteLine("\n");

        var task2 = await app.Create("Second Task");
        Console.WriteLine($"Created Task: {task2.Description}, ID: {task2.Id}, Created On: {task2.CreatedOn}");
        Console.WriteLine("\n");

        var task3 = await app.Create("Third Task");
        Console.WriteLine($"Created Task: {task3.Description}, ID: {task3.Id}, Created On: {task3.CreatedOn}");
        Console.WriteLine("\n");

        var task4 = await app.Create("Fourth Task");
        Console.WriteLine($"Created Task: {task4.Description}, ID: {task4.Id}, Created On: {task4.CreatedOn}");
        Console.WriteLine("\n");

        var updatedTask = await app.Update(task1.Id, "Updated First Task");
        if (updatedTask != null)
        {
            Console.WriteLine($"Updated Task: {updatedTask.Description}, {updatedTask.Id}");
        }
        Console.WriteLine("\n");

        bool isDeleted = await app.Delete(task2.Id);
        Console.WriteLine($"Deleted Task: {isDeleted}");
        Console.WriteLine("\n");

        await app.Complete(task1.Id);
        var completedTasks = await app.Filter("completed");
        Console.WriteLine("Completed Tasks:");
        foreach (var task in completedTasks)
        {
            Console.WriteLine($" - {task.Description} (Completed On: {task.CompletedOn})");
        }
        Console.WriteLine("\n");

        Console.WriteLine("\nAll Tasks:");
        foreach (var task in app.GetAllTasks())
        {
            Console.WriteLine($" - {task.Description} (Created On: {task.CreatedOn}, Completed: {task.IsCompleted})");
        }
        Console.WriteLine("\n");
    }
}
