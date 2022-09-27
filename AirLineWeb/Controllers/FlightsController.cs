using AirLineWeb.DTOs;
using AirLineWeb.Entities;
using AirLineWeb.MessageBus;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirLineWeb.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FlightsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBus;
    public FlightsController(ApplicationDbContext context, 
        IMapper mapper,
        IMessageBusClient messageBus)
    {
        _context = context;
        _mapper = mapper;
        _messageBus = messageBus;
    }

    [HttpGet("{code}", Name = "GetByCode")]
    public ActionResult<FlightDetailReadDto> GetByCode(string code)
    {
        var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == code);

        if (flight == null) return NotFound();

        //This mapping won't work as I have not done the Profiles section Duh!!!
        return Ok(_mapper.Map<FlightDetailReadDto>(flight));
    }

    [HttpPost]
    public ActionResult<FlightDetailReadDto> Create(FlightDetailCreateDto dto)
    {
        var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == dto.FlightCode);

        if (flight == null)
        {
            var flightDetailModel = _mapper.Map<FlightDetail>(dto);

            try
            {
                _context.FlightDetails.Add(flightDetailModel);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var flightDetailReadDto = _mapper.Map<FlightDetailReadDto>(flightDetailModel);

            return CreatedAtRoute(nameof(GetByCode), new { code = flightDetailReadDto.FlightCode }, flightDetailReadDto);

        }
        else
        {
            return NoContent();
        }
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, FlightDetailUpdateDto dto)
    {
        var flight = _context.FlightDetails.FirstOrDefault(f => f.Id == id);

        if (flight == null)
        {
            return NotFound();
        }

        decimal oldPrice = flight.Price;

        _mapper.Map(dto, flight);

        try
        {
            _context.SaveChanges();
            if (oldPrice != flight.Price)
            {
                Console.WriteLine("Price Changed - Place message on bus");

                var message = new NotificationMessageDto
                {
                    WebhookType = "pricechange",
                    OldPrice = oldPrice,
                    NewPrice = flight.Price,
                    FlightCode = flight.FlightCode
                };
                _messageBus.SendMessage(message);
            }
            else
            {
                Console.WriteLine("No Price change");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}