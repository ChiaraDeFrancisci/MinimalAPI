namespace MyApp.Endpoints;

public static class ProductsEndpoint
{
    public static void RegisterProductsEndpoints(this WebApplication app)
    {
        var group2 = app.MapGroup("/products");//per memorizzare il path primario "/"
        group2.MapGet("/", async (NorthwindContext context) => {
            //a me non servono tutte le properties creo classi DTO con solo le cose che mi servono
            return Results.Ok(await context.Products.Select(P => new ProductDTO()
            {
                Id = P.ProductId,
                Nome = P.ProductName,
                Categoria = P.Category.CategoryName
            }).ToListAsync());
        });
        //codici http per OK (200 ok e restituisco valori, 204 OK non restituisco valori)
        //app.MapGet("/products/{id}", (ChiaveComplessa id) => { });
    }
}
