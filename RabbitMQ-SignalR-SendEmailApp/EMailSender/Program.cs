// See https://aka.ms/new-console-template for more information
using EMailSender.Models;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("amqps://kdemspcl:7xeCM9CHITSRuBJI0nLc-RgrPFCMjbIN@cow.rmq2.cloudamqp.com/kdemspcl");
var connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

channel.QueueDeclare("messageQueue", durable: false, exclusive: false, autoDelete: false);


var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("messageQueue", false, consumer: consumer);



//signalR Operations

HubConnection hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7008/messageHub").Build();

await hubConnection.StartAsync();



consumer.Received += async (s, e) =>
{
    Console.WriteLine("Mesaj geldi");
    string serializedData = Encoding.UTF8.GetString(e.Body.Span);
    User data = JsonSerializer.Deserialize<User>(serializedData);

    //EMailSender.EMailSender.Send(data.Email, data.Message);

    await hubConnection.InvokeAsync("SendMessageAsync", $"Email : {data.Email} - Message : {data.Message}");

    Console.WriteLine("Email gönderilmiştir.");


    


};

Console.ReadKey();
