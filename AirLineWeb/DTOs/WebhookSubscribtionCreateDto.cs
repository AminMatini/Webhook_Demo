namespace AirLineWeb.DTOs;

public class WebhookSubscribtionCreateDto
{
    public string WebHookUrl { get; set; } = null!;
    public string WebhookType { get; set; } = null!;
}
