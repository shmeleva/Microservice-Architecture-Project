using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carsharing.Models;

namespace Carsharing.Services
{
    public class MongoDbStorageService : IStorageService
    {
        public MongoDbStorageService()
        {
        }

        public Task<List<Car>> GetAvailableCarsAsync(double latitude, double longitude, double radius)
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetCarAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCarAsync(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
