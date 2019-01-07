using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;

namespace CJEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairingsController : ControllerBase
    {
        private readonly CJEngineContext _context;

        public PairingsController(CJEngineContext context)
        {
            _context = context;
        }

        // GET: api/Pairings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pairing>>> GetPairing()
        {
            return await _context.Pairing.ToListAsync();
        }

        // GET: api/Pairings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pairing>> GetPairing(int id)
        {
            var pairing = await _context.Pairing.FindAsync(id);

            if (pairing == null)
            {
                return NotFound();
            }

            return pairing;
        }

        // PUT: api/Pairings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPairing(int id, Pairing pairing)
        {
            if (id != pairing.Id)
            {
                return BadRequest();
            }

            _context.Entry(pairing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PairingExists(id))
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

        // POST: api/Pairings
        [HttpPost]
        public async Task<ActionResult<Pairing>> PostPairing(Pairing pairing)
        {
            _context.Pairing.Add(pairing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPairing", new { id = pairing.Id }, pairing);
        }

        // DELETE: api/Pairings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pairing>> DeletePairing(int id)
        {
            var pairing = await _context.Pairing.FindAsync(id);
            if (pairing == null)
            {
                return NotFound();
            }

            _context.Pairing.Remove(pairing);
            await _context.SaveChangesAsync();

            return pairing;
        }

        private bool PairingExists(int id)
        {
            return _context.Pairing.Any(e => e.Id == id);
        }
    }
}
