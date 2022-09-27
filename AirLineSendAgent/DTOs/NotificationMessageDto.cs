namespace AirLineSendAgent.DTOs;

public class NotificationMessageDto
{
    public string Id { get; } = null!;
    public string WebhookType { get; set; } = null!;
    public string FlightCode { get; set; } = null!;
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
}
