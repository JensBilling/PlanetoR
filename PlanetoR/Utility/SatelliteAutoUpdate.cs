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
        var responseJson = httpClient.GetStringAsync("https://api.wheretheiss.at/v1/satellites/25544");

        var sateliteFromJson = JsonSerializer.Deserialize<Satellite>(responseJson.Result);
        
        sateliteFromJson.description = "The International Space Station (ISS) is the largest modular space station currently in low Earth orbit. It is a multinational collaborative project involving five participating space agencies: NASA (United States), Roscosmos (Russia), JAXA (Japan), ESA (Europe), and CSA (Canada).";
        sateliteFromJson.country = "Sweden";

        var foundSatellite =  _context.Satellites.Find(sateliteFromJson.id);

        if (foundSatellite == null)
        {
            _context.Satellites.Add(sateliteFromJson);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        
        foundSatellite.country = sateliteFromJson.country;
        foundSatellite.latitude = sateliteFromJson.latitude;
        foundSatellite.longitude = sateliteFromJson.longitude;
        
        _context.SaveChanges();

        return Task.FromResult(true);
    }
}