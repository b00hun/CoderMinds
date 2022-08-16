using System.ComponentModel.DataAnnotations;

namespace DeskBookingSystem.Models.DTOs
{
    public class LocationDTO
    {
        
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string LocationName { get; set; }
        [Required]
        [MaxLength(3)]
        public string Floor { get; set; }

    }
}
