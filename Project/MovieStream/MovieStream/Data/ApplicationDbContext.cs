using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStream.Models;

namespace MovieStream.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Movie>Movies { get; set; }
        public DbSet<OttPlatform> OttPlatforms { get; set; }
        public DbSet<MovieXOttPlatform>MovieXOttPlatforms { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
