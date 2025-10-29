namespace MyApp.TodoItems;

public interface ITodoItems
{
    public Task<List<TodoItem>> GetAllItems();
    public Task<TodoItem?> GetItem(int Id);
    public Task<TodoItem> CreateItem(CreateTodoItem newItem);
    public Task UpdateItem(TodoItem itemModificato);
    public Task DeleteItem(int Id);
}

//il record ha un costruttore e crea internamente le proprietà immutabili come con l'init
public record TodoItem(int Id, string Title, bool IsDone, string Category);
public record CreateTodoItem(string Title, string Category);
