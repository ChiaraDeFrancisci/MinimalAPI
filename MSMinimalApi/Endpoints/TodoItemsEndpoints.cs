using Microsoft.AspNetCore.Mvc;

namespace MyApp.Endpoints;

public static class TodoItemsEndpoints
{
    //scrittura intermedia tra le minimal API (separate dal program) e i controller
    private static async Task<IResult> GetAll(ITodoItems service)
    {
        return Results.Ok(await service.GetAllItems());
    }
    private static async Task<IResult> GetItem(int id, ITodoItems service)
    {
        return await service.GetItem(id) == null ?
                                    Results.NotFound() :
                                    Results.Ok(await service.GetItem(id));
    }
    private static async Task<IResult> CreateItem(CreateTodoItem newItem, ITodoItems service)
    {
        if (newItem.Category is null || newItem.Title is null) return Results.BadRequest();
        var item = await service.CreateItem(newItem);
        //return Results.NoContent();
        //se serve l'id mi serve il path dell'oggetto creato
        return Results.Created($"/todoitems/{item.Id}", item);
    }
    private static async Task<IResult> UpdatePutItem(int id, TodoItem item, ITodoItems service)
    {
        //il tizio si è sbagliato? ha indicato due id diversi???
        if (id != item.Id) return Results.BadRequest();
        await service.UpdateItem(item);
        return Results.NoContent();
    }
    private static async Task<IResult> UpdatePatchItem(int id, TodoItem item, ITodoItems service)
    {
        //il tizio si è sbagliato? ha indicato due id diversi???
        if (id != item.Id) return Results.BadRequest();
        await service.UpdateItem(item);
        return Results.NoContent();
    }
    private static async Task<IResult> DeleteItem(int id, ITodoItems service)
    {
        await service.DeleteItem(id);
        return Results.NoContent();
    }
    public static void RegisterTodoItemsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/todoitems");//per memorizzare il path primario "/"

        //GetAll potrebbe potenzialmente avere una response grandissima. Valutare
        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetItem);
        group.MapPost("/", CreateItem);
        group.MapPut("{id}", UpdatePutItem);
        //il patch fa l'update solo dei dati diversi da null (quindi non serve la gestione con i coalesce
        group.MapPatch("{id}", UpdatePatchItem);
        group.MapDelete("{id}", DeleteItem);

        //va gestito la visualizzazione della pagina
        app.MapGet("/boom", () =>
        {
            throw new InvalidProgramException("Problema serio!!!");
        });
    }
}
