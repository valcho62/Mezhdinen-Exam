using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Planet
    {
        public Planet()
        {
            this.Persons =new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TeleportAnomalies = new HashSet<Anomaly>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int SolarSystemId { get; set; }
        [ForeignKey("SolarSystemId")]
        public SolarSystem SolarSystem { get; set; }
        public int SunId { get; set; }
        [ForeignKey("SunId")]
        public Star Star { get; set; }

        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }
        public virtual ICollection<Anomaly> TeleportAnomalies { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }
}