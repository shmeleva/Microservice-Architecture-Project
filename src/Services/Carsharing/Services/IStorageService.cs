using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carsharing.Models;

namespace Carsharing.Services
{
    public interface IStorageService
    {
        Task<Car> GetCarAsync(Guid guid);

        Task<List<Car>> GetAvailableCarsAsync(double latitude, double longitude, double radius);

        Task UpdateCarAsync(Car car);
    }
}
