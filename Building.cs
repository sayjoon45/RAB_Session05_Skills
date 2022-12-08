using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAB_Session05_Skills
{
    public class Building
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumFloors { get; set; }
        public double Area { get; set; }

        public Building(string name, string address, int numFloors, double area)
        {
            Name = name;
            Address = address;
            NumFloors = numFloors;
            Area = area;
        }
        public double GetBuildingArea()
        {
            return Area * NumFloors;
        }
    }
}
