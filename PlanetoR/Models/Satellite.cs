using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PlanetoR.Models;

public class Satellite
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("name")] 
    public string Name { get; set; } = string.Empty;
    [JsonProperty("description")] 
    public string Description { get; set; }
    [JsonProperty("country")] 
    public string Country { get; set; } = string.Empty;
    [JsonProperty("latitude")] 
    public string Latitude { get; set; } = string.Empty;
    [JsonProperty("longitude")] 
    public string Longitude { get; set; } = string.Empty;
}