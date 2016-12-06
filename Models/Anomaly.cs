using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Anomaly
    {
        public Anomaly()
        {
            this.Victims =new HashSet<Person>();
        }
        public int Id { get; set; }
        public int OriginPlanetId { get; set; }
        [ForeignKey("OriginPlanetId"),InverseProperty("OriginAnomalies")]
        public Planet OriginPlanet { get; set; }
        public int TeleportPlanetId { get; set; }
        [ForeignKey("TeleportPlanetId"),InverseProperty("TeleportAnomalies")]
        public Planet TeleportPlanet { get; set; }
        public virtual ICollection<Person> Victims { get; set; }
    }
}