namespace PlanetoR.Models;

public class CelestialBody
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Long { get; set; }
    public string Lat { get; set; }
    public int AverageTempCelsius { get; set; }
}