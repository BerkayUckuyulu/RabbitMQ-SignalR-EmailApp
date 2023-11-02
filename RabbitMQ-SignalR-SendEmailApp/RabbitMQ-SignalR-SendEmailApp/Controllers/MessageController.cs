using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ_SignalR_SendEmailApp.Models;

namespace RabbitMQ_SignalR_SendEmailApp.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public  IActionResult Post([FromForm]User model)
    {

        ConnectionFactory connectionFactory = new();
        connectionFactory.Uri = new Uri("amqps://kdemspcl:7xeCM9CHITSRuBJI0nLc-RgrPFCMjbIN@cow.rmq2.cloudamqp.com/kdemspcl");
        using var connection = connectionFactory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.QueueDeclare("messageQueue", durable: false, exclusive: false, autoDelete: false);

        string serializedDate = JsonSerializer.Serialize(model);

        byte[] data = Encoding.UTF8.GetBytes(serializedDate);
        channel.BasicPublish(exchange:"", routingKey:"messageQueue", body: data);

        return Ok();
    }
}

