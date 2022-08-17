using System.ComponentModel.DataAnnotations;

namespace DeskBookingSystem.Models.DTOs
{
    public class DeskDTO
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string DeskName { get; set; }
        [Required]
        public int LocationID { get; set; }

        public bool Avaible { get; set; } = true;
    }
}
