using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public List<Edge> OutEdge = new List<Edge>();

        public List<Label> Labels = new List<Label>();

        public Vertex(double X, double Y, string Name, int Number)
        {
            this.X = X;
            this.Y = Y;
            this.Name = Name;
            this.Number = Number;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
