using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskBookingSystem.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }
        [ForeignKey("User")]
        public int LocalUserId { get; set; }
        public LocalUser User { get; set; }

        [ForeignKey("ReservedDesk")]
        public int ReservedDeskId { get; set; }
        public Desk ReservedDesk { get; set;}

        public DateTime ReservationDay { get; set; }

        public DateTime ReservationCreate { get; set; }
    }
}
