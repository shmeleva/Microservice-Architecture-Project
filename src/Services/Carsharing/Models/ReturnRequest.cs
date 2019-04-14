using System;
using System.ComponentModel.DataAnnotations;

namespace Carsharing.Models
{
    public class ReturnRequest
    {
        [Required]
        public string CarId { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
