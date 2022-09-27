namespace TravelAgentWeb.DTOs;

public class FlightDetailUpdateDto
{
    public string Publisher { get; set; } = null!;
    public string Secret { get; set; }  = null!;
    public string FlightCode { get; set; } = null!;
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public string WebhookType { get; set; } = null!;
}
