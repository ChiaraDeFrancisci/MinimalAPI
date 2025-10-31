namespace MSMinimalAPI.TodoItems;

public class MockItemsService(IConfiguration config, IOptions<AppSettings> appsetting) : ITodoItems
{
    //caso sincrono
    //public List<TodoItem> GetAllItems()
    //{
    //    //var a = new Product() { Nome = "cosa" };
    //    return new List<TodoItem>() {

    async Task<List<TodoItem>> ITodoItems.GetAllItems()
    {
        //esempio d'uso di configuration per le variabili di ambiente
        //chiave semplice
        _ = Configuration["MiaChiave"];
        //chiave complessa prendo valore A
        _ = Configuration["MiaChiaveComplessa:A"];
        //utilizzo tramite classe IOptions
        _ = appsetting.Value.A;


        await Task.Delay(1000);
        //var a = new Product() { Nome = "cosa" };
        return Items;
        //return new List<TodoItem>() {

        //    new TodoItem(1, "pilota elicotteri", false,"sport"),
        //    new TodoItem(2, "la vita prima della vita", true, "paleontologia")
        //};
    }
    //inizializzo gli items
    private static List<TodoItem> Items { get; set; } = [
            new TodoItem(1, "pilota elicotteri", false,"sport"),
            new TodoItem(2, "la vita prima della vita", true, "paleontologia")
        ];
    public IConfiguration Configuration { get; private set; } = config;
    public IOptions<AppSettings> Appsetting { get; private set;} = appsetting;

    async Task<TodoItem?> ITodoItems.GetItem(int Id)
    {
        await Task.Delay(1000);
        var item = Items.FirstOrDefault(x => x.Id == Id);
        return item;
    }
    public async Task<TodoItem> CreateItem(CreateTodoItem newItem)
    {
        
        int maxId = 1;
        await Task.Delay(1000);
        if (Items.Count != 0)
            maxId = Items.Max(x => x.Id) + 1;
        TodoItem item = new(maxId, newItem.Title, false, newItem.Category);

        Items.Add(item);
        return item;
    }
    public async Task UpdateItem(TodoItem itemModificato)
    {
        //ho ancora quell'item nel DB?
        var item = Items.FirstOrDefault(x => x.Id == itemModificato.Id);

        await Task.Delay(1000);
        if (item is not null)
        {
            var newitem = item with
            {
                //con ?? gestisco il problema del nullable in caso in cui mi passino dati nulli e rischierei di sovrascrivere dati not null
                Title = itemModificato.Title ?? item.Title,
                IsDone = itemModificato.IsDone,
                Category = itemModificato.Category ?? item.Category
            };
            Items.Remove(item);
            Items.Add(newitem);
        }
    }

    public async Task DeleteItem(int Id)
    {
        //ho ancora quell'item nel DB?
        var item = Items.FirstOrDefault(x => x.Id == Id);

        await Task.Delay(1000);
        if (item is not null)
        {
            Items.Remove(item);
        }
    }
}
//class ItemsFromDB : ITodoItems
//{
//    public List<TodoItem> GetAllItems()
//    {
//        throw new NotImplementedException();
//    }

//}