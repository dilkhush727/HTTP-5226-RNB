using MovieStream.Data.Migrations;
using System.ComponentModel.DataAnnotations;

namespace MovieStream.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; } 
        public string Genre { get; set; } 
        public DateTime ReleaseDate { get; set; }
        public List<MovieXOttPlatform> MovieOttPlatforms { get; set; }
    }
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
