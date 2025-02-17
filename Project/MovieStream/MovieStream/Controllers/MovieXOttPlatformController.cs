using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStream.Data;
using MovieStream.Models;

namespace MovieStream.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieXOttPlatformController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieXOttPlatformController(ApplicationDbContext context)
        {
            _context = context;
        }

        // **CREATE**: POST: api/MovieXOttPlatform
        // Adds a new movie to an OTT platform
        [HttpPost]
        public async Task<ActionResult<MovieXOttPlatformDto>> PostMovieXOttPlatform(MovieXOttPlatformDto movieXOttPlatformDto)
        {
            var movieXOttPlatform = new MovieXOttPlatform
            {
                MovieId = movieXOttPlatformDto.MovieId,
                OttPlatformId = movieXOttPlatformDto.OttPlatformId,
                AvailabilityDate = movieXOttPlatformDto.AvailabilityDate
            };

            _context.MovieXOttPlatforms.Add(movieXOttPlatform);
            await _context.SaveChangesAsync();

            movieXOttPlatformDto.MovieXOttPlatformId = movieXOttPlatform.MovieXOttPlatformId; // Assign generated ID

            return CreatedAtAction(nameof(GetMovieXOttPlatform), new { id = movieXOttPlatform.MovieXOttPlatformId }, movieXOttPlatformDto);
        }

        // **READ**: GET: api/MovieXOttPlatform
        // Gets all movie-OTT platform relationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieXOttPlatformDto>>> GetMovieXOttPlatforms()
        {
            return await _context.MovieXOttPlatforms
                .Select(m => new MovieXOttPlatformDto
                {
                    MovieXOttPlatformId = m.MovieXOttPlatformId,
                    MovieId = m.MovieId,
                    OttPlatformId = m.OttPlatformId,
                    AvailabilityDate = m.AvailabilityDate
                })
                .ToListAsync();
        }

        // **READ**: GET: api/MovieXOttPlatform/5
        // Gets a specific movie-OTT platform relationship by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieXOttPlatformDto>> GetMovieXOttPlatform(int id)
        {
            var movieXOttPlatform = await _context.MovieXOttPlatforms.FindAsync(id);

            if (movieXOttPlatform == null)
            {
                return NotFound();
            }

            return new MovieXOttPlatformDto
            {
                MovieXOttPlatformId = movieXOttPlatform.MovieXOttPlatformId,
                MovieId = movieXOttPlatform.MovieId,
                OttPlatformId = movieXOttPlatform.OttPlatformId,
                AvailabilityDate = movieXOttPlatform.AvailabilityDate
            };
        }

        // **UPDATE**: PUT: api/MovieXOttPlatform/5
        // Updates an existing movie-OTT platform relationship
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieXOttPlatform(int id, MovieXOttPlatformDto movieXOttPlatformDto)
        {
            if (id != movieXOttPlatformDto.MovieXOttPlatformId)
            {
                return BadRequest();
            }

            var movieXOttPlatform = await _context.MovieXOttPlatforms.FindAsync(id);
            if (movieXOttPlatform == null)
            {
                return NotFound();
            }

            movieXOttPlatform.MovieId = movieXOttPlatformDto.MovieId;
            movieXOttPlatform.OttPlatformId = movieXOttPlatformDto.OttPlatformId;
            movieXOttPlatform.AvailabilityDate = movieXOttPlatformDto.AvailabilityDate;

            _context.Entry(movieXOttPlatform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MovieXOttPlatforms.Any(e => e.MovieXOttPlatformId == id))
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

        // **DELETE**: DELETE: api/MovieXOttPlatform/5
        // Deletes a movie-OTT platform relationship by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieXOttPlatform(int id)
        {
            var movieXOttPlatform = await _context.MovieXOttPlatforms.FindAsync(id);
            if (movieXOttPlatform == null)
            {
                return NotFound();
            }

            _context.MovieXOttPlatforms.Remove(movieXOttPlatform);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
