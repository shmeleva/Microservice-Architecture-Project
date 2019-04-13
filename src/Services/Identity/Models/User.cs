using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class User
    { 
        [Required]
        public string Username { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string Hash { get; set; }
    }
}
