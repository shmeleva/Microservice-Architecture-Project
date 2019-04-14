using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Geocoding.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geocoding.Services
{
    public class HereGeocodingService : IGeocodingService
    {
        // TODO: Move to config.
        // TODO: Add secrets.
        private readonly string geocodingApiEndpoint = "https://geocoder.api.here.com/6.2/geocode.json";
        private readonly string reverseGeocodingApiEndpoint = "https://reverse.geocoder.api.here.com/6.2/reversegeocode.json";
        private readonly string geocodingApiAppId = "";
        private readonly string geocodingApiAppKey = "";


        public async Task<Location> GetLocationByAddressAsync(string address)
        {
            var source = QueryHelpers.AddQueryString(geocodingApiEndpoint, new Dictionary<string, string>
            {
                { "app_id", geocodingApiAppId },
                { "app_code", geocodingApiAppKey },
                { "searchtext", address },
                { "maxresults", "1" },
                { "locationattributes", "none,address" },
                { "county", "FIN: Uusimaa" },
                { "gen", "9" },
            });

            return await GetLocationAsync(source);
        }

        public async Task<Location> GetLocationByCoordinatesAsync(double latitude, double longitude)
        {
            var source = QueryHelpers.AddQueryString(reverseGeocodingApiEndpoint, new Dictionary<string, string>
            {
                { "app_id", geocodingApiAppId },
                { "app_code", geocodingApiAppKey },
                { "prox", $"{latitude},{longitude},50" },
                { "mode", "retrieveAddresses" },
                { "level", "postalCode" },
                { "maxresults", "1" },
                { "locationattributes", "none,address" },
                { "gen", "9" },
            });

            return await GetLocationAsync(source);
        }

        private async Task<Location> GetLocationAsync(string source)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync(source);
                httpResponse.EnsureSuccessStatusCode();

                var jObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

                var jLocation = jObject.SelectToken("Response.View[0].Result[0].Location", false)?.ToString();
                if (jLocation == null)
                {
                    return null;
                }

                var location = JsonConvert.DeserializeAnonymousType(jLocation, new
                {
                    DisplayPosition = new
                    {
                        Latitude = default(double),
                        Longitude = default(double),
                    },
                    Address = new
                    {
                        Label = default(string),
                    }
                });

                return new Location
                {
                    Address = location.Address.Label,
                    Latitude = location.DisplayPosition.Latitude,
                    Longitude = location.DisplayPosition.Longitude,
                };
            }
        }
    }
}
