using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Carsharing.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Latitude")]
        public double Latitude { get; set; }

        [BsonElement("Longitude")]
        public double Longitude { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }
    }
}