using System;
using System.ComponentModel.DataAnnotations;

namespace Carsharing.Models
{
    public class BookRequest
    {
        [Required]
        public string CarId { get; set; }
    }
}
