using System;
using System.ComponentModel.DataAnnotations;

namespace Carsharing.Models
{
    public class ReturnRequest
    {
        [Required]
        public Guid CarId { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
