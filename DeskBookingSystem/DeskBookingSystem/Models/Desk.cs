using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskBookingSystem.Models
{
    public class Desk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public string DeskName { get; set; }

        public bool Avaible { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        public Location Location { get; set; }
    }
}
