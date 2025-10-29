
namespace MyApp.ExtensionMethods;

public  static class ApplicationServices
{
    //creo un metodo statico custom da usare per gestire separatamente le registrazioni ai servizi
    //
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        services.AddScoped<ITodoItems, MockItemsService>();
        services.AddScoped<IProduct, ProductService>();
        //DbContext è scoped
        services.AddDbContext<NorthwindContext>(
            x => x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

    }
}
