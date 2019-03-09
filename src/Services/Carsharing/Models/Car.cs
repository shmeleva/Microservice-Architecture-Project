using System;

namespace Carsharing.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Guid UserId { get; set; }
    }
}