using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schronisko.Models.Data;
using Schronisko.Models;

namespace Schronisko.Controllers;

[ApiController]
[Route("api/animals/{animalId}/[controller]")]
public class VisitController : ControllerBase
{
    private readonly Context _context;
    public VisitController(Context context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Visit>>> GetVisits(int animalId) =>
        await _context.Visits.Where(v => v.AnimalId == animalId).ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Visit>> AddVisit(int animalId, Visit visit)
    {
        if (!await _context.Animals.AnyAsync(a => a.Id == animalId)) return NotFound("Animal not found");
        visit.AnimalId = animalId;
        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVisits), new { animalId }, visit);
    }
}