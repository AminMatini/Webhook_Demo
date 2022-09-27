namespace AirLineWeb.Entities;

public class FlightDetail
{
    public int Id { get; set; }
    public string FlightCode { get; set; } = null!;
    public long Price { get; set; }
}
