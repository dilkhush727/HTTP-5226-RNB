using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStream.Data;
using MovieStream.Models;


namespace MovieStream.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OttPlatformController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OttPlatformController(ApplicationDbContext context)
        {
            _context = context;
        }

        // **CREATE**: POST: api/OttPlatform
        // Adds a new OTT platform
        [HttpPost]
        public async Task<ActionResult<OttPlatformDto>> PostOttPlatform(OttPlatformDto ottPlatformDto)
        {
            var ottPlatform = new OttPlatform
            {
                PlatformName = ottPlatformDto.PlatformName,
                SubscriptionType = ottPlatformDto.SubscriptionType
            };

            _context.OttPlatforms.Add(ottPlatform);
            await _context.SaveChangesAsync();

            ottPlatformDto.OttPlatformId = ottPlatform.OttPlatformId; // Assign the generated ID

            return CreatedAtAction(nameof(GetOttPlatform), new { id = ottPlatform.OttPlatformId }, ottPlatformDto);
        }

        // **READ**: GET: api/OttPlatform
        // Gets all OTT platforms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OttPlatformDto>>> GetOttPlatforms()
        {
            return await _context.OttPlatforms
                .Select(o => new OttPlatformDto
                {
                    OttPlatformId = o.OttPlatformId,
                    PlatformName = o.PlatformName,
                    SubscriptionType = o.SubscriptionType
                })
                .ToListAsync();
        }

        // **READ**: GET: api/OttPlatform/5
        // Gets a specific OTT platform by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OttPlatformDto>> GetOttPlatform(int id)
        {
            var ottPlatform = await _context.OttPlatforms.FindAsync(id);

            if (ottPlatform == null)
            {
                return NotFound();
            }

            return new OttPlatformDto
            {
                OttPlatformId = ottPlatform.OttPlatformId,
                PlatformName = ottPlatform.PlatformName,
                SubscriptionType = ottPlatform.SubscriptionType
            };
        }

        // **UPDATE**: PUT: api/OttPlatform/5
        // Updates an existing OTT platform
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOttPlatform(int id, OttPlatformDto ottPlatformDto)
        {
            if (id != ottPlatformDto.OttPlatformId)
            {
                return BadRequest();
            }

            var ottPlatform = await _context.OttPlatforms.FindAsync(id);
            if (ottPlatform == null)
            {
                return NotFound();
            }

            ottPlatform.PlatformName = ottPlatformDto.PlatformName;
            ottPlatform.SubscriptionType = ottPlatformDto.SubscriptionType;

            _context.Entry(ottPlatform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.OttPlatforms.Any(e => e.OttPlatformId == id))
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

        // **DELETE**: DELETE: api/OttPlatform/5
        // Deletes a specific OTT platform by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOttPlatform(int id)
        {
            var ottPlatform = await _context.OttPlatforms.FindAsync(id);
            if (ottPlatform == null)
            {
                return NotFound();
            }

            _context.OttPlatforms.Remove(ottPlatform);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
