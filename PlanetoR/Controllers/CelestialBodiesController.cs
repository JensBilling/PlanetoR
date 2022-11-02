using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanetoR.Data;
using PlanetoR.Models;

namespace PlanetoR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CelestialBodiesController : ControllerBase
{
    private readonly AppDbContext _context;

    private static List<CelestialBody> celestialBodies = new List<CelestialBody>()
    {
        new CelestialBody
        {
            Id = 1,
            Name = "Earth", 
            Long = "123", 
            Lat = "456", 
            AverageTempCelsius = 12
        },
        new CelestialBody
        {
            Id = 2,
            Name = "Mars", 
            Long = "536", 
            Lat = "732", 
            AverageTempCelsius = 30
        }
    };

    public CelestialBodiesController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CelestialBody>>> GetAll()
    {
        return Ok(await _context.CelestialBodies.ToListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CelestialBody>> GetById(int id)
    {
        var celestialBody = await _context.CelestialBodies.FindAsync(id);
        if (celestialBody == null)
        {
            return BadRequest("ID not found");
        }
        return Ok(celestialBody);
    }

    [HttpPost]
    public async Task<ActionResult<List<CelestialBody>>> AddCelestialBody(CelestialBody newCelestialBody)
    {
        _context.CelestialBodies.Add(newCelestialBody);
        await _context.SaveChangesAsync();

        return Ok(await _context.CelestialBodies.ToListAsync());
    }
    
    [HttpPut]
    public async Task<ActionResult<List<CelestialBody>>> UpdateCelestialBody(CelestialBody updateCelestialBodyRequest)
    {
        var celestialBody = celestialBodies.Find(cb => cb.Id == updateCelestialBodyRequest.Id);
        if (celestialBody == null)
        {
            return BadRequest("ID not found");
        }

        celestialBody.Name = updateCelestialBodyRequest.Name;
        celestialBody.Long = updateCelestialBodyRequest.Long;
        celestialBody.Lat = updateCelestialBodyRequest.Lat;
        celestialBody.AverageTempCelsius = updateCelestialBodyRequest.AverageTempCelsius;
        
        return Ok(celestialBodies);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<CelestialBody>>> DeleteCelestialBody(int id)
    {
        var celestialBody = celestialBodies.Find(cb => cb.Id == id);
        if (celestialBody == null)
        {
            return BadRequest("ID not found");
        }

        celestialBodies.Remove(celestialBody);
        return Ok(celestialBodies);
    }
}