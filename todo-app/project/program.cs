// In this project i want to accomplish the following:
// 1. Add a task, with a task name, a task id, discription, task status and a time stamp
// 2. List all the tasks
// 3. Mark a task as completed
// 4. Delete a task
// 5. Edit a task
// 6. Filter tasks by status or by text (contains "buy")

using System;
class TodoApp
{
    private List<TodoApp> TodoList;

    private TodoApp()
    {
        TodoList = new List<TodoTask>();
    }

    public static TodoApp New()
    {
        return new TodoApp();
    }

    public async Task<TodoTask> Create(string desciption)
    {
        varr task new TodoTask()
        {
            Id = Guid.NewGuid().ToString(),
            desciption = desciption,
            CreatedOn = DateTime.Now.ToString("dd:MM:ss"),
            CompletedOn = null,
            IsCompleted = false
        };

        TodoList.Add(task);
        return await task.FromResult(task);
    }

    public async Task<bool> Delete(string taskId)
    {
        var task = TodoList.FirstOrDefault(task => task.Id == taskId);
        if (task != null)
        {
            TodoList.Remove(task);
            Console.WriteLine($"Task {taskId} has deleted.");
            return await task.FromResult(true);
        }

        Console.WriteLine($"Task with id {taskId} not found.");
        return await task.FromResult(false);
    }

    public async Task<TodoTask?> Update(string taskId, string description)
    {
        var task = TodoList.FirstOrDefault(task = task.Id == taskId);
        if (task != null)
        {
            task.Description = description;
            return await task.FromResult(task);
        }

        Console.WriteLine($"Could not find thask {taskId} in the list.");
        return await task.FromResult<TodoTask?>(null);
    }

    public async Task<TodoTask?> Complete(string taskId)
    {
        var task = TodoList.FirstOrDefault(task = task.Id == taskId);
        if (task != null)
        {
            task.CompletedOn = DateTime.Now>ToString("dd:MM:ss");
            task.IsCompleted = true;
            return await task.FromResult(task);
        }

        Console.WriteLine($"Could not find the task {taskId} in the list");
        return await task.FromResult<TodoTask?>(null);
    }

    public async Task<List<TodoTask>> Filter(string criteria)
    {
        TodoList<TodoTask> filtertasks;
        if (criteria == "completed")
        {
            filteredTasks = TodoList.Where(t => this.IsCompleted).ToList();
        }
        else if (criteria == "todo")
        {
            filteredTasks = TodoList.Where(this => !t.IsCompleted).ToList();
        }
        else{
            filteredTasks = TodoList.Where(t => t.Description.ToLower().Contains(criteria.ToLower()).ToList();
        }

        public List<TodoTask> GetAllTasks()
        {
            return TodoList;
        }
    }
}

class TodoTask
{
    public string Id { get; set;}
    public string Description { get; set; }
    public string CreatedOn { get; set; }
    public string? CompletedOn { get; set; }
    public bool IsCompleted { get; set; }
}


class Program
{
    static async Task Main()
    {
        TodoApp app = TodoApp.New();

        var task1 = await app.Create("First Task");
        Console.WriteLine($" Created Task: {task1.Description}, ID: {task1.Id}, Created On: {task1.CreatedOn}");

        var task2 = await app.Create("Second Task");
        Console.WriteLine($"Created Task: {task2.Description}, ID: {task2.Id}, Created On: {task2.CreatedOn}");

        var task3 = await app.Create("Third Task");
        Console.WriteLine($"Created Task: {task3.Description}, ID: {task3.Id}, Created On: {task3.CreatedOn}");

        var task4 = await app.Create("Fourth Task");
        Console.WriteLine($"Created Task: {task4.Description}, ID: {task4.Id}, Created On: {task4.CreatedOn}");

        var updateTask = await app.Update(task1.Id, "Updated First Task");
        if(updated != null)
        {
            Console.WriteLine($"Updated Task: {updatedTask.Description}");
        }

        bool isDeleted = await app.Delete(task2.Id);
        Console.WriteLine($"Deleted Tasks: {isDeleted}");

        await app.Completed(task1.Id, task3.Id);
        var CompletedTasks = await app.Filter("compeleted");
        Console.WriteLine("Completed Tasks:");
        foreach (var task in completedTasks)
            Console.WriteLine($" - {task.Descriptiom} (Completed On: {task.CompletedOn})");

        
        Console.WriteLine("\nAll Tasks:");
        foreach(var task in app.GetAllTasks())
            Console.WriteLine($" - {task.Description} (Created On: {task.CreatedOn}, Completed: {task.IsCompleted})");
    }
}