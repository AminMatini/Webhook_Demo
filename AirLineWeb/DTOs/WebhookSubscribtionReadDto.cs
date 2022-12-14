namespace AirLineWeb.DTOs;

public class WebhookSubscribtionReadDto
{
    public int Id { get; set; }
    public string WebHookUrl { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string WebhookType { get; set; } = null!;
    public string WebhookPublisher { get; set; } = null!;
}

