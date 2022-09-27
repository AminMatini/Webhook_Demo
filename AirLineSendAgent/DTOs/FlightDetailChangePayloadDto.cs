namespace AirLineSendAgent.DTOs;

public class FlightDetailChangePayloadDto
{
    public string WebhookURI { get; set; } = null!;
    public string Publisher { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string FlightCode { get; set; } = null!;
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public string WebhookType { get; set; } = null!;
}