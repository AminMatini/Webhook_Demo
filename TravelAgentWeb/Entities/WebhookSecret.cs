namespace TravelAgentWeb.Entities;

public class WebhookSecret
{
    public int Id { get; set; }
    public string Secret { get; set; } = null!;
    public string Publisher { get; set; } = null!;
}