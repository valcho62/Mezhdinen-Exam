﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Person
    {
        public Person()
        {
            this.Anomalies =new HashSet<Anomaly>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int HomePlanetId { get; set; }
        public Planet HomePlanet { get; set; }
        public virtual ICollection<Anomaly> Anomalies { get; set; }
    }
}