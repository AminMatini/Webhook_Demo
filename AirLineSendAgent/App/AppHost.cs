using AirLineSendAgent.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using AirLineSendAgent.DTOs;

namespace AirLineSendAgent.App;

public class AppHost : IAppHost
{
    private readonly ApplicationDbContext _context;
    private readonly IWebhookClient _webHookClient;

    public AppHost(ApplicationDbContext context , IWebhookClient webHookClient)
    {
        _context = context;
        _webHookClient = webHookClient;
    }

    public void Run()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

        using (var connection = factory.CreateConnection()) 

        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(
                exchange : "trigger", 
                type : ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(
                queue :  queueName , 
                exchange : "trigger" , 
                routingKey: "");

            var consumer = new EventingBasicConsumer(model : channel);

            Console.WriteLine("Listening on the message bus...");

            consumer.Received += async (moduleHandle, ea) =>
            {
                Console.WriteLine("Event is trigged ...");

                var body = ea.Body;

                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
                var message = JsonSerializer.Deserialize<NotificationMessageDto>(notificationMessage);

                if (message is null or null) return;

                var webhookToSend = new FlightDetailChangePayloadDto()
                {
                    WebhookType = message.WebhookType,
                    WebhookURI = string.Empty,
                    Secret = string.Empty,
                    Publisher = string.Empty,
                    OldPrice = message.OldPrice,
                    NewPrice = message.NewPrice,
                    FlightCode = message.FlightCode
                };

                foreach (var whs in _context.WebhookSubscriptions.Where(subs => subs.WebhookType.Equals(message.WebhookType)))
                {
                    webhookToSend.WebhookURI = whs.WebHookUrl;
                    webhookToSend.Secret = whs.Secret;
                    webhookToSend.Publisher = whs.WebhookPublisher;

                    await _webHookClient.SendWebhookNotification(webhookToSend);
                }
            };
        }
    }
}