namespace AirLineWeb.DTOs;

public class FlightDetailReadDto
{
    public int Id { get; set; }
    public string FlightCode { get; set; } = null!;
    public long Price { get; set; }
}
