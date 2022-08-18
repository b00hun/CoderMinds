using System.ComponentModel.DataAnnotations;

namespace DeskBookingSystem.Models.DTOs
{
    public class ReservationDTO
    {
        
        public int ReservationId { get; set; }
        [Required]
        public int LocalUserId { get; set; }
        [Required]
        public int ReservedDeskId { get; set; }

        public DateTime ReservationDay { get; set; }

        public DateTime ReservationCreate { get; set; }
    }
}
