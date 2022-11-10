using System.Text.Json;
using PlanetoR.Controllers;
using PlanetoR.Data;
using PlanetoR.Models;
using Quartz;

namespace PlanetoR.Utility;

public class SatelliteAutoUpdate : IJob
{
    private static IHttpClientFactory? _httpClientFactory;
    private readonly AppDbContext _context;
    

    public SatelliteAutoUpdate(IHttpClientFactory? httpClientFactory, AppDbContext context)
    {
        _httpClientFactory = httpClientFactory;
        _context = context;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var satelliteJsonFromApi = httpClient.GetStringAsync("https://api.wheretheiss.at/v1/satellites/25544");
        
        var satelliteObjectFromApi = JsonSerializer.Deserialize<Satellite>(satelliteJsonFromApi.Result);
        var foundSatellite =  _context.Satellites.Find(satelliteObjectFromApi.id);

        if (foundSatellite == null)
        {
            _context.Satellites.Add(satelliteObjectFromApi);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        var countryNameFromCoordsFromApi = httpClient.GetStringAsync($"http://api.geonames.org/countryCodeJSON?lat={foundSatellite.latitude}&lng={foundSatellite.longitude}&username=jens.b");
        var positionFromJson = JsonSerializer.Deserialize<SatelliteCountryDto>(countryNameFromCoordsFromApi.Result);
        foundSatellite.UpdateCountry(positionFromJson);
        
        
      
        foundSatellite.latitude = satelliteObjectFromApi.latitude;
        foundSatellite.longitude = satelliteObjectFromApi.longitude;
        
        _context.SaveChanges();

        return Task.FromResult(true);
    }
}