using DeskBookingSystem.Models.DTOs;

namespace DeskBookingSystem.Data
{
    public class LocationStore
    {
        public static List<LocationDTO> locationList = new List<LocationDTO>()
        {
            new LocationDTO { Id = 1,LocationName = "OpenSpace 1", Floor = "1"},
            new LocationDTO { Id = 2,LocationName = "Green Field", Floor = "Parter"},
            new LocationDTO { Id = 3,LocationName = "Chill zone", Floor = "2"},
        };
    }
}
