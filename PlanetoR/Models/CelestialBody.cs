namespace PlanetoR.Models;

public class CelestialBody
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Mass { get; set; }
    public double Density { get; set; }
    public double Diameter { get; set; }
    public double Gravity { get; set; }
    public double DayInEarthHours { get; set; }
    public double YearInEarthDays { get; set; }
    public double AverageTemperature { get; set; }
    public double NumberOfMoons { get; set; }
}