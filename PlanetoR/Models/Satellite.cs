﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PlanetoR.Models;

public class Satellite
{
    public int id { get; set; }

    public string name { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;

    public string country { get; set; } = string.Empty;

     public double latitude { get; set; }

    public double longitude { get; set; } 
}