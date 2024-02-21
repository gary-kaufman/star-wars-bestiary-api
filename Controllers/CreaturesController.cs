using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsBestiaryApi;

namespace StarWarsBestiaryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreaturesController : ControllerBase
    {
        private readonly CreatureContext _context;

        public CreaturesController(CreatureContext context)
        {
            _context = context;
        }

        // GET: api/Creatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creature>>> GetCreatures()
        {
            return await _context.Creatures.ToListAsync();
        }

        // GET: api/Creatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Creature>> GetCreature(Guid id)
        {
            var creature = await _context.Creatures.FindAsync(id);

            if (creature == null)
            {
                return NotFound();
            }

            return creature;
        }

        // GET: api/Creatures/GetByHeightRange=0.5,2
        [HttpGet("GetByHeightRange={heightMin},{heightMax}")]
        public async Task<ActionResult<IEnumerable<Creature>>> GetCreaturesByHeightRange(double heightMin, double heightMax)
        {
            var creatures = await _context.Creatures
            .Where(c => c.Height < heightMax && 
            c.Height > heightMin || 
            c.Height == heightMax || 
            c.Height == heightMin)
            .ToListAsync();

            if (creatures == null)
            {
                return NotFound();
            }

            return creatures;
        }

        // GET: api/Creatures/GetByWeightRange=0.5,2
        [HttpGet("GetByWeightRange={weightMin},{weightMax}")]
        public async Task<ActionResult<IEnumerable<Creature>>> GetCreaturesByWeightRange(double weightMin, double weightMax)
        {
            var creatures = await _context.Creatures
            .Where(c => c.Weight < weightMax && 
            c.Weight > weightMin || 
            c.Weight == weightMax || 
            c.Weight == weightMin)
            .ToListAsync();

            if (creatures == null)
            {
                return NotFound();
            }

            return creatures;
        }


        // GET: api/Creatures/GetByPlanetOfOrigin=Tatooine
        [HttpGet("GetByPlanetOfOrigin={planet}")]
        public async Task<ActionResult<IEnumerable<Creature>>> GetCreaturesByHeightRange(string planet)
        {
            var creatures = await _context.Creatures
            .Where(c => c.PlanetOfOrigin.Contains(planet))
            .ToListAsync();

            if (creatures == null)
            {
                return NotFound();
            }

            return creatures;
        }

        // GET: api/Creatures/GetByDiet=1
        [HttpGet("GetByDiet={diet}")]
        public async Task<ActionResult<IEnumerable<Creature>>> GetCreaturesByDiet(int diet)
        {

            var creatures = await _context.Creatures
            .Where(c => c.Diet == Creature.IntToDiet(diet))
            .ToListAsync();

            if (creatures == null)
            {
                return NotFound();
            }

            return creatures;
        }

        // PUT: api/Creatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreature(Guid id, Creature creature)
        {
            if (id != creature.CId)
            {
                return BadRequest();
            }

            _context.Entry(creature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreatureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Creatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Creature>> PostCreature(Creature creature)
        {
            _context.Creatures.Add(creature);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCreature), new { id = creature.CId }, creature);
        }

        // DELETE: api/Creatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreature(Guid id)
        {
            var creature = await _context.Creatures.FindAsync(id);
            if (creature == null)
            {
                return NotFound();
            }

            _context.Creatures.Remove(creature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreatureExists(Guid id)
        {
            return _context.Creatures.Any(e => e.CId == id);
        }
    }
}
