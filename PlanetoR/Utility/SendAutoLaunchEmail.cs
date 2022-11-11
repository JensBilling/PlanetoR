using System.Text.Json;
using PlanetoR.Models;
using Quartz;

namespace PlanetoR.Utility;

public class SendAutoLaunchEmail : IJob
{
    private readonly IHttpClientFactory? _httpClientFactory;

    public SendAutoLaunchEmail(IHttpClientFactory? httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public Task Execute(IJobExecutionContext context)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var launchJsonFromApi = httpClient.GetStringAsync("https://fdo.rocketlaunch.live/json/launches/next/1").Result;

        
        var launchDescription = ExtractDataFromJson.extractString(launchJsonFromApi, "launch_description");
        var winOpen = ExtractDataFromJson.extractString(launchJsonFromApi, "win_open");
        var launchDate = DateTime.Parse(winOpen);

        Console.WriteLine("_____________");
        if (launchDate.Day.Equals(DateTime.Now.Day))
        {
            // execute "launch today"-email here
        }
        
        
        
        return Task.FromResult(true);
    }
}