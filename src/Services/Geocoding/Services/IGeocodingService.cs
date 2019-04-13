using System.Threading.Tasks;
using Geocoding.Models;

namespace Geocoding.Services
{
    public interface IGeocodingService
    {
        Task<Location> GetLocationByAddressAsync(string address);

        Task<Location> GetLocationByCoordinatesAsync(double latitude, double longitude);
    }
}
