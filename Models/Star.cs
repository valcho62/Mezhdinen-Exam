using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Models
{
    public class Star
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SolarSystemId { get; set; }
        public SolarSystem SolarSystem { get; set; }
    }
}