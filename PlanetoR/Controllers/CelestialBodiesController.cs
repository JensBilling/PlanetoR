using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanetoR.Data;
using PlanetoR.Models;

namespace PlanetoR.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "ROLE_USER, ROLE_ADMIN")]
public class CelestialBodiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CelestialBodiesController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CelestialBody>>> GetAllCelestialBodies()
    {
        return Ok(await _context.CelestialBodies.ToListAsync());
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CelestialBody>> GetCelestialBodyById(int id)
    {
        var foundCelestialBody = await _context.CelestialBodies.FindAsync(id);
        if (foundCelestialBody == null)
        {
            return BadRequest("Celestial body not found");
        }
        return Ok(foundCelestialBody);
    }

    [HttpPost, Authorize(Roles = "ROLE_ADMIN")]
    public async Task<ActionResult<List<CelestialBody>>> AddCelestialBody(CelestialBody newCelestialBody)
    {
        _context.CelestialBodies.Add(newCelestialBody);
        await _context.SaveChangesAsync();

        return Ok(await _context.CelestialBodies.ToListAsync());
    }
    
    [HttpPut, Authorize(Roles = "ROLE_ADMIN")]
    public async Task<ActionResult<List<CelestialBody>>> UpdateCelestialBody(CelestialBody updateCelestialBodyRequest)
    {
        var foundCelestialBody = await _context.CelestialBodies.FindAsync(updateCelestialBodyRequest.Id);
        if (foundCelestialBody == null) return BadRequest("Celestial body not found");
     
        foundCelestialBody.Name = updateCelestialBodyRequest.Name;
        foundCelestialBody.Mass = updateCelestialBodyRequest.Mass;
        foundCelestialBody.Density = updateCelestialBodyRequest.Density;
        foundCelestialBody.Diameter = updateCelestialBodyRequest.Diameter;
        foundCelestialBody.Gravity = updateCelestialBodyRequest.Gravity;
        foundCelestialBody.DayInEarthHours = updateCelestialBodyRequest.DayInEarthHours;
        foundCelestialBody.YearInEarthDays = updateCelestialBodyRequest.YearInEarthDays;
        foundCelestialBody.AverageTemperature = updateCelestialBodyRequest.AverageTemperature;
        foundCelestialBody.NumberOfMoons = updateCelestialBodyRequest.NumberOfMoons;
        

        await _context.SaveChangesAsync();
        
        return Ok(await _context.CelestialBodies.ToListAsync());
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "ROLE_ADMIN")]
    public async Task<ActionResult<List<CelestialBody>>> DeleteCelestialBody(int id)
    {
        var foundCelestialBody = await _context.CelestialBodies.FindAsync(id);
        if (foundCelestialBody == null) return BadRequest("Celestial body not found");
       

        _context.CelestialBodies.Remove(foundCelestialBody);
        await _context.SaveChangesAsync();
        
        return Ok(await _context.CelestialBodies.ToListAsync());
    }
}