using System;
using Microsoft.AspNetCore.SignalR;

namespace RabbitMQ_SignalR_SendEmailApp.Hubs
{
	public class MessageHub:Hub
	{
		public async Task SendMessageAsync(string message)
		{
			await Clients.All.SendAsync("receiveMessage", message);
		}
	}
}

