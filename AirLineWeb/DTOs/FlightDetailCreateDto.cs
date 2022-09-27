namespace AirLineWeb.DTOs;

public class FlightDetailCreateDto
{
    public string FlightCode { get; set; } = null!;
    public long Price { get; set; }
}