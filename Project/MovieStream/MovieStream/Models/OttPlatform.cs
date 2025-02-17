using MovieStream.Data.Migrations;
using System.ComponentModel.DataAnnotations;
namespace MovieStream.Models
{
    public class OttPlatform
    {
        [Key]
        public int OttPlatformId { get; set; }
        public string PlatformName { get; set; } 
        public string SubscriptionType { get; set; }
        public List<MovieXOttPlatform> MovieOttPlatforms { get; set; }
    }
    public class OttPlatformDto
    {
        public int OttPlatformId { get; set; }
        public string PlatformName { get; set; }
        public string SubscriptionType { get; set; }
    }
}
