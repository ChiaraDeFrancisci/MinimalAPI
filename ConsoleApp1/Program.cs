// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Messaging.EventGrid;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.EventGrid.Models;
#region EventGrid
Uri endpoint = new("uri");
AzureKeyCredential key = new("key");
//apro la connessione con il topic Azure
EventGridPublisherClient connection = new (endpoint, key);
//configuro l'evento custom
Azure.Messaging.EventGrid.EventGridEvent primoEvento = new(
    subject: "Ordine accettato: 1736",
    eventType: "OrdineAccettato",
    dataVersion: "1.0",
    data: new { IDCliente = 123, IDOrder = 1736 }

);
//trasmissione evento al topic
await connection.SendEventAsync(primoEvento);
Console.WriteLine("Evento spedito");


////genero x eventi
for (int i = 0; i < 10; i++)
{
    Azure.Messaging.EventGrid.EventGridEvent Evento = new(
    subject: $"Ordine accettato: {i}",
    eventType: "OrdineAccettato",
    dataVersion: "1.0",
    data: new { IDCliente = 123, IDOrder = i }

    );
    //trasmissione evento al topic
    await connection.SendEventAsync(Evento);
    Console.WriteLine($"Evento spedito {i}");
}
Console.WriteLine($"Fine trasmissione");
#endregion
#region ServiceBus
//Sender messaggi per il servicebus configurato su Azure - RabbinMQ molto simile
string queueName = "myqueue";
string connectionBus = "connectionString";
ServiceBusClient client = new(connectionBus);

//aggiunta messaggio
ServiceBusSender sender = client.CreateSender(queueName);
ServiceBusMessageBatch batch = await sender.CreateMessageBatchAsync();

batch.TryAddMessage(new ServiceBusMessage($"Messaggio"));
await sender.SendMessagesAsync(batch);

//ascolto stile web socket
ServiceBusProcessor processor = client.CreateProcessor(queueName);

//devo definire 2 delegati handler: quando ricevo messaggio quando non c'è piu
processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

await processor.StartProcessingAsync();



//trucco per evitare che venga sospesa l'esecuzione
Console.ReadLine();
//fermo l'ascolto
await processor.StopProcessingAsync();
//il messaggio ricevuto dagli handler è nell'args
async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine(body);
    await args.CompleteMessageAsync(args.Message);
}

async Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.Message);
    await Task.CompletedTask;
}
#endregion


