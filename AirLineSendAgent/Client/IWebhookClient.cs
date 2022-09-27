using AirLineSendAgent.DTOs;

namespace AirLineSendAgent.Client;

public interface IWebhookClient
{
    Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayloadDto);
}
