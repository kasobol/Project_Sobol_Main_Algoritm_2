using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Edge_Distance
    {
        public Edge Edge { get; set; }

        public double Distance { get; set; }

        public Edge_Distance(Edge edge, double distance)
        {
            Edge = edge;
            Distance = distance;
        }
        public Edge_Distance() { }
        public override string ToString()
        {
            return $"{Distance}, {Edge.ToString()}";
        }
    }
}
