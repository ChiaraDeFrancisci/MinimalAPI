namespace MyApp.ExtensionMethods;

public static class GeneralHttpExtensions
{
    //creo un metodo statico custom da usare per gestire separatamente le registrazioni ai servizi
    //
    public static void RegisterHttpServices(this IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        //gestione del JSON personalizzata
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //non inserisce cose null
            options.SerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
    }
}
