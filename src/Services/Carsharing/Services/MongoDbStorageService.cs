using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carsharing.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Carsharing.Services
{
    public class MongoDbStorageService : IStorageService
    {
        private readonly IMongoCollection<Car> cars;

        public MongoDbStorageService(IConfiguration config)
        {
            var client = new MongoClient("mongodb://mongo-carsharing:27017");
            var database = client.GetDatabase("carsharing");
            cars = database.GetCollection<Car>("Cars");
        }

        // NOTE: Mock functionality: ignores coordinates and simply returns all available cars.
        public async Task<List<Car>> FindAvailableCarsAsync(double latitude, double longitude, double radius) =>
            await cars.Find(x => x.Username == null).ToListAsync().ConfigureAwait(false);

        public async Task<Car> FindCarAsync(string id) =>
            await cars.Find(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);

        // NOTE: Not thread-safe.
        public async Task UpdateCarAsync(string id, Car car) =>
            await cars.ReplaceOneAsync(x => x.Id == id, car).ConfigureAwait(false);
    }
}
