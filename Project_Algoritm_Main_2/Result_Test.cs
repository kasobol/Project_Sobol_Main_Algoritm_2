using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Result_Test
    {
        public int name { get; set; }

        public double time { get; set; }

        public double utilization { get; set; }

        public bool success { get; set; }

        public double length { get; set; }

        public int[] range { get; set; }

        public List<Edge> way { get; set; }

        public Result_Test(int name, double time, double utilization, bool success, double length, int[] range, List<Edge> way)
        {
            this.name = name;
            this.time = time;
            this.utilization = utilization;
            this.success = success;
            this.length = length;
            this.range = range;
            this.way = way;
        }

        public void Cancel_Order()
        {
            if (success == true)
            {
                foreach (var edge in way)
                {
                    int i = edge.Slots.Length;
                    Array.Resize(ref edge.Slots, edge.Slots.Length + range.Length);
                    foreach (int item in range)
                    {
                        edge.Slots[i] = item;
                        i++;
                    }
                    Array.Sort(edge.Slots);
                }
            }
        }

        public override string ToString()
        {
            return $"Name: {name}, Time: {time.ToString("F7")}, Utilization: {utilization.ToString("F4")}, Success: {success}, Length: {length}, Range: {Program.Mas_in_String(range)}, Way: {Program.ListEdge_To_String(way)}";
        }

        public string Length_Utilization()
        {
            int k;
            if (range == null)
            {
                k = 0;
            }
            else
            {
                k = range.Length;
            }
            return $"{utilization.ToString("F4")} {k}";
        }
    }
}
