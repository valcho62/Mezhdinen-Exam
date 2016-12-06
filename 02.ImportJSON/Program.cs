using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;
using Models.DTOs;
using Newtonsoft.Json;

namespace _02.ImportJSON
{
    class Program
    {
        private const string StarPath = "../../../datasets/stars.json";
        private const string SolarSystemPath = "../../../datasets/solar-systems.json";
        private const string PersonPath = "../../../datasets/persons.json";
        private const string PlanetPath = "../../../datasets/planets.json";
        private const string AnomalyPath = "../../../datasets/anomalies.json";
        private const string AnomalyVictimsPath = "../../../datasets/anomaly-victims.json";

        static void Main()
        {
            //ImportSolarSystems();
            //ImportStars();
            //ImportPlanets();
            //ImportPersons();
            //ImportAnomaly();
            //ImportAnomalyVictims();
        }

        private static void ImportAnomaly()
        {
          var contex = new MassDefectContex();
            var json = File.ReadAllText(AnomalyPath);
            var allAnomalies = JsonConvert.DeserializeObject<ICollection<AnomalyDTO>>(json);

            foreach (var anomaly in allAnomalies)
            {
                if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
                else
                {
                    var tempOrigin = contex.Planets.Where(x => x.Name == anomaly.OriginPlanet).FirstOrDefault();
                    var tempTeleport = contex.Planets.Where(x => x.Name == anomaly.TeleportPlanet).FirstOrDefault();

                    if (tempTeleport == null || tempOrigin == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                    else
                    {
                        var tempAnomaly = new Anomaly()
                        {
                            OriginPlanet = tempOrigin,
                        TeleportPlanet = tempTeleport
                        };
                        contex.Anomalies.Add(tempAnomaly);
                        Console.WriteLine($"Successfully imported anomaly.");
                    }
                }
            }
            contex.SaveChanges();
        }

        private static void ImportPersons()
        {
          var contex = new MassDefectContex();
            var json = File.ReadAllText(PersonPath);
            var allPersons = JsonConvert.DeserializeObject<ICollection<PersonDTO>>(json);

            foreach (var person in allPersons)
            {
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
                else
                {
                    var tempPlanet = contex.Planets.Where(x => x.Name == person.HomePlanet).FirstOrDefault();
                    if (tempPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                    else
                    {
                        var tempPerson = new Person()
                        {
                            Name = person.Name,
                            HomePlanet = tempPlanet
                        };
                        Console.WriteLine($"Successfully imported Person {tempPerson.Name}.");
                        contex.Persons.Add(tempPerson);
                    }
                }
            }
            contex.SaveChanges();
        }

        private static void ImportPlanets()
        {
           var contex = new MassDefectContex();
            var json = File.ReadAllText(PlanetPath);
            var allPlanets = JsonConvert.DeserializeObject<ICollection<PlanetDTO>>(json);

            foreach (var planet in allPlanets)
            {
                if (planet.Name == null || planet.Sun == null || planet.SolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
                else
                {
                    var tempSun = contex.Stars.Where(x => x.Name == planet.Sun).First();
                    var tempSolar = contex.SolarSystems.Where(x => x.Name == planet.SolarSystem).First();

                    if (tempSun == null || tempSolar == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                    else
                    {
                         var tempPlanet = new Planet()
                         {
                             Name = planet.Name,
                             SolarSystem = tempSolar,
                             Star = tempSun
                         };
                        Console.WriteLine($"Successfully imported Planet {tempPlanet.Name}.");
                        contex.Planets.Add(tempPlanet);
                    }
                   
                }
            }
            contex.SaveChanges();
        }

        private static void ImportAnomalyVictims()
        {
           var contex = new MassDefectContex();
            var json = File.ReadAllText(AnomalyVictimsPath);
            var allAnomVict = JsonConvert.DeserializeObject<ICollection<AnomalyVictimsDTO>>(json);

            foreach (var anomVict in allAnomVict)
            {
                if (anomVict.Id == null || anomVict.Person == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
                else
                {
                    var tempAnomaly = contex.Anomalies.Where(x => x.Id == anomVict.Id).FirstOrDefault();
                    var tempPerson = contex.Persons.Where(x => x.Name == anomVict.Person).FirstOrDefault();

                    if (tempPerson == null || tempAnomaly == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                    else
                    {
                        tempAnomaly.Victims.Add(tempPerson);
                        Console.WriteLine($"Succesfuly added {tempPerson.Name} to anomaly.");
                    }
                }
            }
            contex.SaveChanges();

        }

        private static void ImportStars()
        {
            MassDefectContex contex = new MassDefectContex();
            var jsonStars = File.ReadAllText(StarPath);
            var allStars = JsonConvert.DeserializeObject<ICollection<StarDTO>>(jsonStars);

            foreach (var star in allStars)
            {
                if (star.Name == null || star.SolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
                var starSolarSystem = contex.SolarSystems.Where(x => x.Name == star.SolarSystem).First();

                if (starSolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
                else
                {
                    Star tempStar = new Star()
                    {
                        Name = star.Name,
                        SolarSystem = starSolarSystem
                    };
                    contex.Stars.Add(tempStar);
                    Console.WriteLine($"Successfully imported Star {tempStar.Name}.");
                }
                contex.SaveChanges();
            }
        }

        private static void ImportSolarSystems()
        {
            MassDefectContex contex = new MassDefectContex();
            var json = File.ReadAllText(SolarSystemPath);
            var allSolSystems = JsonConvert.DeserializeObject<ICollection<SolarSystemDTO>>(json);

            foreach (var solSystem in allSolSystems)
            {
                if (solSystem.Name == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
                else
                {
                    SolarSystem tempSolarSystem = new SolarSystem()
                    {
                        Name = solSystem.Name
                    };
                    contex.SolarSystems.Add(tempSolarSystem);
                    Console.WriteLine($"Successfully imported SolarSystem {tempSolarSystem.Name}.");
                }
            }
            contex.SaveChanges();
        }
    }
}
