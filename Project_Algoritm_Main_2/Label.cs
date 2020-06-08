using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Label
    {
        public double Distance { get; set; }

        public Edge Previous_Edge { get; set; }

        public int[] Slots { get; set; }

        public Label(double Distance, Edge Previous_Edge, int[] Slots)
        {
            this.Distance = Distance;
            this.Previous_Edge = Previous_Edge;
            this.Slots = Slots;
        }

        public string Slots_To_String(int[] slots)
        {
            string str = "";
            foreach (var item in slots)
            {
                str += item + " ";
            }
            return str;
        }

        public override string ToString()
        {
            return $"({Distance}, {Previous_Edge.ToString()}, {Slots_To_String(Slots)})";
        }
    }
}
