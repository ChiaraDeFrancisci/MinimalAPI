using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoCoreVuota;

public interface IMyClock
{
    //si possono mettere ora anche proprietà e implementazione delle interfacce
    //public int Id { get; set; }        
    DateTime MyNow();
}
public class StaticClock:IMyClock
{
    public DateTime MyNow()
    {
        return new DateTime(2000, 1, 1,8,0,0);
    }

}
//nomi univoci x le interfacce
public interface IMyNotification
{
    void SendMessage(string message);
}

public class EmailNotification : IMyNotification
{
    public void SendMessage(string message)
    {
        Console.WriteLine("Sending email message...");
    }
}
public class TelegramNotification : IMyNotification
{
    public void SendMessage(string message)
    {
        Console.WriteLine("Sending telegram message...");
    }
}
public class A(B b, IMyClock myClock, IEnumerable<IMyNotification> myNotification)
{
    //A ora dipende da classe B in modo indissolubile. non ho controllo di quello che succede se quella classe è costretta a creare degli oggetti
    //non è un codice testabile perché ho una dispendenza e non posso dare un test autonomo su A
   
    public void DoSomething()
    {
        //var data = DateTime.Now; //sto costruendo una data dall'orologio clock del server che gira l'app, quindi se faccio un test il risultato dipende dalla data. non testabile
        var data = myClock.MyNow();

       
       myNotification?.FirstOrDefault()?.SendMessage($"{data.ToLongDateString()}");
        

        //QUESTO E' IL MALE! cit.
        //IL CODICE VA MODIFICATO SOLO PER AGGIUNGERE ROBA NUOVA, non per modificare 
        //var b = new B();
        Console.WriteLine($"A sta facendo qualcosa");
    }
    //usando questo metodo tolgo la dipendenza da B, perché è il costruttore che crea B e lo mette a disposizione
    //iniezione dipendenza tramite costruttore
    private readonly B b = b;
    private readonly IMyClock myClock = myClock;
    private readonly IEnumerable<IMyNotification> myNotification = myNotification;
}

public class B
{
    public void DoSomething()
    {
        Console.WriteLine("B sta facendo qualcosa");
    }
}