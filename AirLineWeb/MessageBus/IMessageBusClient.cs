using AirLineWeb.DTOs;

namespace AirLineWeb.MessageBus;

public interface IMessageBusClient 
{
    void SendMessage(NotificationMessageDto notificationMessageDto);
}