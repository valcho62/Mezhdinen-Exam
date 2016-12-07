using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Data;

namespace _05.ExportXML
{
    class Program
    {
        static void Main()
        {
            var contex = new MassDefectContex();
            var allAnomalies = contex.Anomalies
                .Select(sel => new
                {
                    id = sel.Id,
                    originPlanet = sel.OriginPlanet.Name,
                    teleportPlanet = sel.TeleportPlanet.Name,
                    victims = sel.Victims
                })
                .OrderBy(x => x.id);

            var xmlDoc = new XElement("anomalies");
            foreach (var anomaly in allAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", anomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", anomaly.originPlanet));
                anomalyNode.Add(new XAttribute("teleport-planet", anomaly.teleportPlanet));

                var victimsNode = new XElement("victims");
                foreach (var victim in anomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim.Name));
                    victimsNode.Add(victimNode);
                }

                anomalyNode.Add(victimsNode);
                xmlDoc.Add(anomalyNode);
            }

            Console.WriteLine(xmlDoc);

        }
    }
}
