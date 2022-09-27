using AirLineWeb.DTOs;
using AirLineWeb.Entities;
using AutoMapper;

namespace AirLineWeb.Profiles;

public class FlightDetailProfile : Profile
{
    public FlightDetailProfile()
    {
        CreateMap<FlightDetail, FlightDetailReadDto>();
        CreateMap<FlightDetailCreateDto, FlightDetail>();
        CreateMap<FlightDetailUpdateDto, FlightDetail>();
    }
}