using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            List<Result_Test> res = new List<Result_Test>();

            int number_vertex = 30;
            int number_range = 60;
            //double[] number_of_lambda_request = new double[] { 10, 12.5, 15, 17.5, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 100, 150, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            double[] number_of_lambda_request = { 300 };
            int number_of_all_day = 10;
            int number_of_max_side_area = 1000;
            int number_of_graph = 15;

            foreach (var lambda in number_of_lambda_request)
            {
                for (int w = 0; w < number_of_graph; w++)
                {
                    Graph graph = new Graph();
                    int[] range = new int[number_range];
                    for (int i = 0; i < range.Length; i++)
                    {
                        range[i] = i;
                    }
                    List<Vertex> vertex_list = new List<Vertex>();

                    for (int i = 0; i < number_vertex; i++)
                    {
                        graph.Add_Vertex(new Vertex(number_of_max_side_area * rnd.NextDouble(), number_of_max_side_area * rnd.NextDouble(), $"v{i}", i));
                    }
                    graph.All_Slots = range;

                    graph.Graph_Delone();
                    graph.Graph_Gabriel();


                    graph.Add_OutEdge();

                    res.AddRange(graph.Start_Check(new TimeSpan(number_of_all_day, 0, 0, 0), lambda));
                }
            }
            var point_res = Mean_of_Utlization(res);

            StringBuilder str_all = new StringBuilder();

            int count = 0;
            foreach (var item in res)
            {
                Console.WriteLine(count);
                str_all.Append("Number:" + count + ", " + item.ToString() + "\n");
                count++;
            }
            Console.WriteLine(str_all);
            string path_str = @"C:\Users\kosti\Desktop\Test_Project.txt";

            using (FileStream fstream = new FileStream($"{path_str}", FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(str_all.ToString());
                fstream.Write(array, 0, array.Length);
            }

            string str_probability_utilization = "";

            foreach (var item in point_res)
            {
                str_probability_utilization += $"{item.Item1.ToString("F4")}  {item.Item2.ToString("F4")}\n";
            }

            string str_time_utilization = "";

            foreach (var item in point_res)
            {
                str_time_utilization += $"{item.Item1.ToString("F4")}  {item.Item5.ToString("F4")}\n";
            }

            string str_length_utilization = "";

            foreach (var item in point_res)
            {
                str_length_utilization += $"{item.Item1.ToString("F4")}  {item.Item3.ToString("F4")}\n";
            }

            string str_slice_utilization = "";

            foreach (var item in point_res)
            {
                str_slice_utilization += $"{item.Item1.ToString("F4")}  {item.Item4.ToString("F4")}\n";
            }

            using (FileStream fstream = new FileStream(@"C:\Users\kosti\Desktop\Probability_Utilization.txt", FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(str_probability_utilization);
                fstream.Write(array, 0, array.Length);
            }

            using (FileStream fstream = new FileStream(@"C:\Users\kosti\Desktop\Time_Utilization.txt", FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(str_time_utilization);
                fstream.Write(array, 0, array.Length);
            }

            using (FileStream fstream = new FileStream(@"C:\Users\kosti\Desktop\Length_Utilization.txt", FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(str_length_utilization);
                fstream.Write(array, 0, array.Length);
            }

            using (FileStream fstream = new FileStream(@"C:\Users\kosti\Desktop\Slice_Utilization.txt", FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(str_slice_utilization);
                fstream.Write(array, 0, array.Length);
            }
        }

        static public List<Tuple<double, double, double, double, double>> Mean_of_Utlization(List<Result_Test> list)
        {
            var res = new List<Tuple<double, double, double, double, double>>();
            double point = 0.005;
            double mean_probability;
            double mean_length;
            double mean_slice;
            double mean_time;

            int count;
            count = 0;
            mean_probability = 0;
            mean_length = 0;
            mean_slice = 0;
            mean_time = 0;

            while (point < 1)
            {
                foreach (var item in list)
                {
                    if (item.utilization < point && point - 0.005 <= item.utilization)
                    {
                        if (item.success == true)
                        {
                            mean_probability++;
                            mean_length += item.length;
                            mean_slice += item.range.Length;
                        }
                        mean_time += item.time;
                        count++;
                    }
                }
                if (count != 0)
                {
                    res.Add(Tuple.Create(point, mean_probability / count, mean_length / count, mean_slice / count, mean_time / count));
                    count = 0;
                    mean_probability = 0;
                    mean_length = 0;
                    mean_slice = 0;
                    mean_time = 0;
                }
                point += 0.005;
            }
            return res;
        }

        public static string ListEdge_To_String(List<Edge> list)
        {
            if (list == null)
            {
                return "null";
            }
            string str = "";
            if (list.Count == 0)
            {
                return null;
            }
            foreach (var item in list)
            {
                str += $"{item.From.Name} -> ";
            }
            str += $"{list.Last().To.Name}";
            return str;
        }

        public static string Mas_in_String(int[] mas)
        {
            if (mas == null)
            {
                return "null";
            }
            string str = null;
            foreach (var item in mas)
            {
                str += item + " ";
            }
            return $"( {str})";
        }

        static void Show_Vertex(Vertex[] vertexes)
        {
            foreach (var vertex in vertexes)
            {
                Console.WriteLine(vertex.ToString());
                foreach (var label in vertex.Labels)
                {
                    Console.WriteLine(label.ToString());
                }
            }
        }

        static public int Random_From_Array<T>(List<T> mas)
        {
            return rnd.Next(0, mas.Count);
        }
    }
}
