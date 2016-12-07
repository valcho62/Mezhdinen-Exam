using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace Models
{
    public class Star
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SolarSystemId { get; set; }
        [ForeignKey("SolarSystemId")]
        public SolarSystem SolarSystem { get; set; }
    }
}