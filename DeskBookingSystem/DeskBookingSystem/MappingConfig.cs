using AutoMapper;
using DeskBookingSystem.Models;
using DeskBookingSystem.Models.DTOs;

namespace DeskBookingSystem
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();

            CreateMap<Location, LocationCreateDTO>().ReverseMap();
            

        }
    }
}
