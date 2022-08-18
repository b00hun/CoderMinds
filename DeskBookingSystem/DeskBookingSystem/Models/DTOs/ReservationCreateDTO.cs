using System.ComponentModel.DataAnnotations;

namespace DeskBookingSystem.Models.DTOs
{
    public class ReservationCreateDTO
    {
        [Required]
        public int LocalUserId { get; set; }
        [Required]
        public int ReservedDeskId { get; set; }
        [Required]
        public DateTime ReservationDay { get; set; }
        [Required]
        public DateTime ReservationCreate { get; set; }
    }
}
