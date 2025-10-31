// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Messaging.EventGrid;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.EventGrid.Models;
#region EventGrid
Uri endpoint = new Uri("uri");
AzureKeyCredential key = new AzureKeyCredential("key");
//apro la connessione con il topic Azure
var connection = new EventGridPublisherClient(endpoint, key);
//configuro l'evento custom
var primoEvento = new Azure.Messaging.EventGrid.EventGridEvent(
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
    var Evento = new Azure.Messaging.EventGrid.EventGridEvent(
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
var queueName = "myqueue";
var connectionBus = "connectionString";
var client = new ServiceBusClient(connectionBus);

//aggiunta messaggio
var sender = client.CreateSender(queueName);
var batch = await sender.CreateMessageBatchAsync();

batch.TryAddMessage(new ServiceBusMessage($"Messaggio"));
await sender.SendMessagesAsync(batch);

//ascolto stile web socket
var processor = client.CreateProcessor(queueName);

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


