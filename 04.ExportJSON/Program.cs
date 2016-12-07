using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Newtonsoft.Json;

namespace _04.ExportJSON
{
    class Program
    {
        static void Main()
        {
            var contex = new MassDefectContex();
           // ExportPlanetsWhichAreNotAnomalyOrigin(contex);
            //ExportPersonsWhichAreNotVictims(contex);
            ExportTopAnomaly(contex);
        }

        private static void ExportTopAnomaly(MassDefectContex contex)
        {
            var result = contex.Anomalies
                .OrderByDescending(x => x.Victims.Count)
                .Select(sel => new
                {
                    id = sel.Id,
                    originPlanet = new
                    {
                        name = sel.OriginPlanet.Name
                    },
                    teleportPlanet = new
                    {
                        name = sel.TeleportPlanet.Name
                    },
                    victimsCount = sel.Victims.Count
                })
                .First();
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText("../../topAnomaly.json",json);
        }

        private static void ExportPersonsWhichAreNotVictims(MassDefectContex contex)
        {
            var result = contex.Persons
                .Where(x => !x.Anomalies.Any())
                .Select(sel => new
                {
                    name = sel.Name,
                    homePlanet = sel.HomePlanet.Name
                });

            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText("../../personsNotVictims.json",json);
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigin(MassDefectContex contex)
        {
            var result = contex.Planets
                .Where(x => !x.OriginAnomalies.Any())
                .Select(sel => sel.Name);
            var json = JsonConvert.SerializeObject(result,Formatting.Indented);
            File.WriteAllText("../../planetsNotOrigin.json",json);
        }
    }
}
