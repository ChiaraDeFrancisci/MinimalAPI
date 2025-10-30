

namespace MyApp.ExtensionMethods;

public  static class ApplicationServices
{
    //creo un metodo statico custom da usare per gestire separatamente le registrazioni ai servizi
    //
  
    
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen();

        services.AddScoped<ITodoItems, MockItemsService>();
        services.AddScoped<IProduct, ProductService>();
        //DbContext è scoped
        services.AddDbContext<NorthwindContext>(
            x => x.UseSqlServer(configuration["ConnectionString"]));
        
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

    }
}
