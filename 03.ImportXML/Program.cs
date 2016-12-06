using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Data;
using Models;

namespace _03.ImportXML
{
    class Program
    {
        private const string NewAnpmalyPath = "../../../datasets/new-anomalies.xml";

        static void Main()
        {
            var contex = new MassDefectContex();
            var xml = XDocument.Load(NewAnpmalyPath);
            var newAnomalies = xml.XPathSelectElements("anomalies/anomaly");

            foreach (var newanom in newAnomalies)
            {
                InsertAnomalyAndVictim(newanom, contex);
            }
            contex.SaveChanges();
        }

        private static void InsertAnomalyAndVictim(XElement newanom, MassDefectContex contex)
        {
            var originAtribute = newanom.Attribute("origin-planet");
            var teleportAttribte = newanom.Attribute("teleport-planet");
            if (originAtribute.Value == null || teleportAttribte.Value == null)
            {
                Console.WriteLine("Invalid value");
                return;
            }
            var tempOrigin = contex.Planets.Where(x => x.Name == originAtribute.Value).FirstOrDefault();
            var tempTeleport = contex.Planets.Where(x => x.Name == teleportAttribte.Value).FirstOrDefault();

            if (tempTeleport == null || tempOrigin == null)
            {
                Console.WriteLine("Error: Invalid data.");
                return;
            }
            else
            {
                var tempAnom = new Anomaly()
                {
                    OriginPlanet = tempOrigin,
                    TeleportPlanet = tempTeleport
                };
                contex.Anomalies.Add(tempAnom);
                Console.WriteLine("Succesfuly add anomaly");
            }

            //victims
            var allVictims = newanom.XPathSelectElements("victims/victim");
            foreach (var victim in allVictims)
            {
                var tempVictim = contex.Persons.Where(x => x.Name == victim.Value).FirstOrDefault();
                if (tempVictim == null)
                {
                    Console.WriteLine("Errrrrroooor");
                    continue;
                }
                else
                {
                    temp
                }
            }
        }
    }
}
