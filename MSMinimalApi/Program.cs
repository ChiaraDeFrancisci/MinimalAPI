
#region Configuration

using MSMinimalAPI.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);
//metodo custom per gestire le registrazioni ai servizi Http generali per tutte le application
builder.Services.RegisterHttpServices();
//metodo custom per gestire le registrazioni di servizi local dell'application

builder.Services.RegisterServices(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //per prod posso creare un nuovo endpoint error per reindirizzare l'utente su pagine customizzate
    //app.UseExceptionHandler();
}
#endregion

app.RegisterTodoItemsEndpoints();
app.RegisterProductsEndpoints();
app.UseHttpsRedirection();
app.Run();