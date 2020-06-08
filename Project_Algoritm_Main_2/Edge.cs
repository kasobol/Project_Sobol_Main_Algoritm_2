using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }

        public double Weight { get; set; }

        public int[] Slots;

        public Edge(Vertex From, Vertex To, int[] Slots)
        {
            this.From = From;
            this.To = To;
            this.Weight = Math.Sqrt(Math.Pow(To.X - From.X, 2) + Math.Pow(To.Y - From.Y, 2));
            this.Slots = Slots;
        }

        public override string ToString()
        {
            return $"{From.Name} -> {To.Name} : {Weight.ToString("F4")}";
        }
    }
}
