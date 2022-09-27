using AirLineWeb.DTOs;
using AirLineWeb.Entities;
using AutoMapper;

namespace AirLineWeb.Profiles;

public class WebhookSubscribtionProfile : Profile
{
	public WebhookSubscribtionProfile()
	{
		CreateMap<WebhookSubscribtionCreateDto, WebhookSubscribtion>();
		CreateMap<WebhookSubscribtion, WebhookSubscribtionReadDto>();
	}
}