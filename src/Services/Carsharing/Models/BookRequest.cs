using System;
using System.ComponentModel.DataAnnotations;

namespace Carsharing.Models
{
    public class BookRequest
    {
        [Required]
        public Guid CarId { get; set; }
    }
}
