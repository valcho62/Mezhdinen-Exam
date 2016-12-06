using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            MassDefectContex contex = new MassDefectContex();
            contex.Database.Initialize(true);
           // contex.SaveChanges();
        }
    }
}
