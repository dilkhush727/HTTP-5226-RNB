using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStream.Data;
using MovieStream.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace MovieStream.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // **READ**: List all movies
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Select(m => new MovieDto
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate
                })
                .ToListAsync();

            return View(movies); // Renders Views/Movie/Index.cshtml
        }

        // **CREATE**: Display form
        public IActionResult Create()
        {
            return View(); // Renders Views/Movie/Create.cshtml
        }

        // **CREATE**: Save new movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    ReleaseDate = movieDto.ReleaseDate
                };

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieDto);
        }

        // **EDIT**: Display form
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };

            return View(movieDto); // Renders Views/Movie/Edit.cshtml
        }

        // **EDIT**: Save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieDto movieDto)
        {
            if (id != movieDto.MovieId)
            {
                // If the ids don't match, return a BadRequest response
                return BadRequest("MovieId mismatch");
            }

            if (!ModelState.IsValid)
            {
                // If ModelState is not valid, return the view with validation errors
                // Log errors for debugging purposes
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Log validation errors
                }
                return View(movieDto);
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            // Update the movie entity with the values from movieDto
            movie.Title = movieDto.Title;
            movie.Genre = movieDto.Genre;
            movie.ReleaseDate = movieDto.ReleaseDate;

            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // **DELETE**: Confirm delete page
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };

            return View(movieDto); // Renders Views/Movie/Delete.cshtml
        }

        // **DELETE**: Remove from database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // **DETAILS**: Display details of a specific movie
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Where(m => m.MovieId == id)
                .Select(m => new MovieDto
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate
                })
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie); // Renders Views/Movie/Details.cshtml
        }
    }
}
