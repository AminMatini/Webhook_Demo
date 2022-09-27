namespace AirLineWeb.DTOs;

public class NotificationMessageDto
{
    public NotificationMessageDto()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; } = null!;
    public string WebhookType { get; set; } = null!;
    public string FlightCode { get; set; } = null!;
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
}