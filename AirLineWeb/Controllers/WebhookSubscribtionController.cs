using AirLineWeb.DTOs;
using AirLineWeb.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirLineWeb.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class WebhookSubscribtionController : ControllerBase
{
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;
	public WebhookSubscribtionController(ApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet("{secret}" , Name = "GetBySecret")]
	public ActionResult<WebhookSubscribtionReadDto> GetBySecret(string secret)
	{
		var subscribtion = _context.WebhookSubscribtions.FirstOrDefault(x => x.Secret == secret);

		if (subscribtion is null or null) return NotFound();

		return Ok(_mapper.Map<WebhookSubscribtionReadDto>(subscribtion));
	}

    [HttpPost]
	public ActionResult<WebhookSubscribtionReadDto> Create(WebhookSubscribtionCreateDto dto)
	{
		var subscribtion = _context.WebhookSubscribtions.FirstOrDefault(x=> x.WebHookUrl == dto.WebHookUrl);

		if(subscribtion == null)
		{
			subscribtion = _mapper.Map<WebhookSubscribtion>(dto);

			subscribtion.Secret = Guid.NewGuid().ToString();
			subscribtion.WebhookPublisher = "PanAus";

			try
			{
                _context.Add(subscribtion);

                _context.SaveChanges();
            }catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}


			var result = _mapper.Map<WebhookSubscribtionReadDto>(subscribtion);

			return CreatedAtRoute(nameof(GetBySecret)  , new {secret = result.Secret} , result);
		}
		else
		{
			return NoContent();
		}
	}
}
