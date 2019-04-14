using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carsharing.Models;

namespace Carsharing.Services
{
    public interface IStorageService
    {
        Task<Car> FindCarAsync(string id);

        Task<List<Car>> FindAvailableCarsAsync(double latitude, double longitude, double radius);

        Task UpdateCarAsync(string id, Car car);
    }
}
