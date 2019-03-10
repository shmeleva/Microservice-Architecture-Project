using System;
namespace Carsharing.Models
{
    public class ReturnRequest
    {
        public Guid CarId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
