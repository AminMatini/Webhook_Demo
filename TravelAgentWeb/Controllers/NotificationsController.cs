using Microsoft.AspNetCore.Mvc;
using TravelAgentWeb.DTOs;

namespace TravelAgentWeb.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NotificationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult FlightChanged(FlightDetailUpdateDto flightDetailUpdateDto)
    {
        Console.WriteLine($"Webhook Receieved from: {flightDetailUpdateDto.Publisher}");

        var secretModel = _context.SubscriptionSecrets.FirstOrDefault(s =>
            s.Publisher == flightDetailUpdateDto.Publisher &&
            s.Secret == flightDetailUpdateDto.Secret);

        if (secretModel == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Secret - Ignore Webwook");
            Console.ResetColor();
            return Ok();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Valid Webhook!");
            Console.WriteLine($"Old Price {flightDetailUpdateDto.OldPrice}, New Price {flightDetailUpdateDto.NewPrice}");
            Console.ResetColor();
            return Ok();
        }
    }
}
