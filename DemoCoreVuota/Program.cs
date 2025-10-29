using DemoCoreVuota;

var builder = WebApplication.CreateBuilder(args);
//per far gestire a lui la creazione delle istanze: sto registrando le due interfacce
builder.Services.AddScoped<IMyNotification, EmailNotification>();
builder.Services.AddScoped<IMyNotification, TelegramNotification>();//se inietto l'interfaccia allora il secondo vince, altrimenti uso IEnumerable
builder.Services.AddSingleton<IMyClock, StaticClock>();
builder.Services.AddTransient<A>();
builder.Services.AddSingleton<B>();

//*********DEPENDENCY INJECTION*************
//Singleton: stessa istanza unica per tutto il ciclo di vita dell'app. per la cache per consultazione dati geografici o sola lettura invece di usare il db
//Scoped: ogni sessione http genera una nuova istanza. EF context va inizializzata con AddScoped
//Transient: ogni richiesta rigenera la dipendenza. problema se il codice deve gestire degli stati associati alla richiesta e usarli per dei calcoli ecc.



var app = builder.Build();

//per avere la pagina iniziale che ammazza tutte le request in attesa che venga sistemato x problemi
//app.UseWelcomePage();
//per usare file statici
app.UseStaticFiles();
//in questo modo devo gestire manualmente le dipendenze nell'ordine corretto
app.MapGet("/", (A a) =>
{ //Aggiungere il services.AddSingleton rende inutile queste righe di codice
    //una classe che implementa l'interfaccia
    //IMyClock myClock = new StaticClock();
    //IMyNotification myNotification= new TelegramNotification();//se devo gestire dei cambi di invio notifiche allora creo altre cose senza modificare

    //var b = new B();
    //var a = new A(b,myClock,myNotification);


    //throw new Exception("Grave");//la pagina delle eccezioni deve essere diversa per svilupp e utente
    return "ciao";
});
//quando facciamo MapPost potremmo avere problemi, vede se nel body della request è presente un'istanza della classe, se non c'è chiede al container Services

app.Run();
