﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskBookingSystem.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string LocationName { get; set; }
        
        public string Floor { get; set; }

        public List<Desk>? Desks { get; set; }

        
    }
}
