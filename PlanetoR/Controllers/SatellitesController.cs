using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanetoR.Data;
using PlanetoR.Models;

namespace PlanetoR.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "ROLE_USER, ROLE_ADMIN")]
public class SatellitesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SatellitesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Satellite>>> GetAllSatellites()
    {
        return Ok(await _context.Satellites.ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Satellite>> GetSatelliteById(int id)
    {
        var foundSatellite = await _context.Satellites.FindAsync(id);
        if (foundSatellite == null) return BadRequest("ID not found");

        return Ok(foundSatellite);
    }

    [HttpPost, Authorize(Roles = "ROLE_ADMIN")]
    public async Task<ActionResult<Satellite>> AddSatellite(Satellite newSatellite)
    {
        _context.Satellites.Add(newSatellite);
        await _context.SaveChangesAsync();

        return Ok(await _context.Satellites.ToListAsync());
    }

    [HttpPut, Authorize(Roles = "ROLE_ADMIN")]
    public async Task<ActionResult<Satellite>> UpdateSatellite(Satellite updateSatelliteRequest)
    {
        var foundSatellite = await _context.Satellites.FindAsync(updateSatelliteRequest.Id);
        if (foundSatellite == null) return BadRequest("ID not found");

        foundSatellite.Name = updateSatelliteRequest.Name;
        foundSatellite.Country = updateSatelliteRequest.Country;
        foundSatellite.Description = updateSatelliteRequest.Description;
        foundSatellite.Latitude = updateSatelliteRequest.Latitude;
        foundSatellite.Longitude = updateSatelliteRequest.Longitude;

        await _context.SaveChangesAsync();

        return Ok(await _context.Satellites.ToListAsync());
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "ROLE_ADMIN")]
    public async Task<ActionResult<Satellite>> DeleteSatellite(int id)
    {
        var foundSatellite = await _context.Satellites.FindAsync(id);
        if (foundSatellite == null) return BadRequest("ID not found");

        _context.Satellites.Remove(foundSatellite);
        await _context.SaveChangesAsync();
        return Ok(await _context.CelestialBodies.ToListAsync());
    }
}