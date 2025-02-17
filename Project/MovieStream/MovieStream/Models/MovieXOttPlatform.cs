using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MovieStream.Models
{
    public class MovieXOttPlatform
    {
        [Key]
        public  int MovieXOttPlatformId  {get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } // Navigation property

        [ForeignKey("OttPlatformId")]
        public int OttPlatformId { get; set; }
        public OttPlatform OttPlatform { get; set; } // Navigation property

        public DateTime AvailabilityDate { get; set; }
    }
    public class MovieXOttPlatformDto
    {
        public int MovieXOttPlatformId { get; set; }

        public int MovieId { get; set; }
       

        public int OttPlatformId { get; set; }
      

        public DateTime AvailabilityDate { get; set; }
    }
}
