using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schronisko.Models.Data;
using Schronisko.Models;

namespace Schronisko.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly Context _context;
    public AnimalController(Context context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals() =>
        await _context.Animals.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        var animal = await _context.Animals.Include(a => a.Visits).FirstOrDefaultAsync(a => a.Id == id);
        return animal == null ? NotFound() : animal;
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Animal>>> SearchByName(string name) =>
        await _context.Animals.Where(a => a.Name.Contains(name)).ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Animal>> CreateAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnimal(int id, Animal updated)
    {
        if (id != updated.Id) return BadRequest();
        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null) return NotFound();
        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}