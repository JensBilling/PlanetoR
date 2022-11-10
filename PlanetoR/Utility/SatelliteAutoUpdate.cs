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
        var client = _httpClientFactory.CreateClient();
        var response = client.GetStringAsync("https://api.wheretheiss.at/v1/satellites/25544");

        var updatedSatellite = JsonSerializer.Deserialize<Satellite>(response.Result);
        
        
        updatedSatellite.description = "very nice satellite";
        updatedSatellite.country = "SE";
        var foundSatellite =  _context.Satellites.Find(updatedSatellite.id);
        
        foundSatellite.name = updatedSatellite.name;
        foundSatellite.country = updatedSatellite.country;
        foundSatellite.description = updatedSatellite.description;
        foundSatellite.latitude = updatedSatellite.latitude;
        foundSatellite.longitude = updatedSatellite.longitude;

        
        

        _context.SaveChanges();
      
        

        return Task.FromResult(true);
    }
}