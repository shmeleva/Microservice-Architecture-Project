using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carsharing.Models;

namespace Carsharing.Services
{
    public class StorageService : IStorageService
    {
        public Task<Car> GetCarAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Car>> GetAvailableCarsAsync(double latitude, double longitude, double radius)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCarAsync(Car car)
        {
            if (car == null)
            {
                throw new NullReferenceException();
            }

            throw new NotImplementedException();
        }
    }
}
