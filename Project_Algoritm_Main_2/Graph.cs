using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Graph
    {
        public Graph()
        {
            Method_Help();
        }
        static Random rnd = new Random();

        public List<Vertex> Vertexes = new List<Vertex>();
        public List<Edge> Edges = new List<Edge>();

        public int[] All_Slots;

        public void Add_Vertex(Vertex vertex)
        {
            Vertexes.Add(vertex);
        }

        public void Add_Edge(Edge edge)
        {
            Edges.Add(edge);
            //edge.From.OutEdge.Add(edge);
        }

        public void Add_OutEdge()
        {
            foreach (var edge in Edges)
            {
                edge.From.OutEdge.Add(edge);
            }
        }

        public void Reset_Label()
        {
            foreach (var vertex in Vertexes)
            {
                vertex.Labels = new List<Label>();
            }
        }

        public double Sum(List<Edge> list)
        {
            double sum = 0;
            foreach (var edge in list)
            {
                sum += edge.Weight;
            }
            return sum;
        }

        public int[] Cross_Range(int[] mas1, int[] mas2)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < mas1.Length; i++)
            {
                for (int j = 0; j < mas2.Length; j++)
                {
                    if (mas1[i] == mas2[j])
                    {
                        res.Add(mas1[i]);
                        break;
                    }
                }
            }
            var help_res = new int[res.Count];
            int k = 0;
            foreach (var item in res)
            {
                help_res[k] = item;
                k++;
            }
            return help_res;
        }

        public int[] Max_Diaposon(int[] mas)
        {
            if (mas.Length == 0 || mas.Length == 1)
            {
                return mas;
            }
            int l_res = 0;
            int k_res = 0;
            int l_point = 0;
            int k_point = 0;
            int max_length = 0;

            for (int i = 0; i < mas.Length - 1; i++)
            {
                if (mas[i] + 1 != mas[i + 1])
                {
                    if (k_point - l_point + 1 > max_length)
                    {
                        max_length = k_point - l_point + 1;
                        l_res = l_point;
                        k_res = k_point;
                    }
                    l_point = i + 1;
                    k_point = i + 1;
                    continue;
                }
                k_point++;
            }
            if (k_point - l_point + 1 > max_length)
            {
                max_length = k_point - l_point + 1;
                l_res = l_point;
                k_res = k_point;
            }

            int[] res_mas = new int[max_length];
            for (int i = l_res, j = 0; i <= k_res; i++, j++)
            {
                res_mas[j] = mas[i];
            }

            return res_mas;
        }

        public int[] Min_Diaposon(int[] mas, int n)
        {
            if (mas.Length == 0 || mas.Length == 1)
            {
                return mas;
            }
            int l_res = 0;
            int k_res = 0;
            int l_point = 0;
            int k_point = 0;
            int min_length = int.MaxValue;

            for (int i = 0; i < mas.Length - 1; i++)
            {
                if (mas[i] + 1 != mas[i + 1])
                {
                    int m = k_point - l_point + 1;
                    if (m < min_length && m >= n)
                    {
                        min_length = k_point - l_point + 1;
                        l_res = l_point;
                        k_res = k_point;
                    }
                    l_point = i + 1;
                    k_point = i + 1;
                    continue;
                }
                k_point++;
            }
            if (k_point - l_point + 1 < min_length && k_point - l_point + 1 >= n)
            {
                min_length = k_point - l_point + 1;
                l_res = l_point;
                k_res = k_point;
            }

            int[] res_mas = new int[min_length];
            for (int i = l_res, j = 0; i <= k_res; i++, j++)
            {
                res_mas[j] = mas[i];
            }

            return res_mas;
        }

        public Tuple<int[], Label> Min_Diaposon_Of_Labels(List<Label> list_label, int n)
        {
            Label pointer = list_label.First();
            var min_diaposon = list_label.First().Slots;

            int[] min = min_diaposon;
            foreach (var label in list_label)
            {
                min = Min_Diaposon(label.Slots, n);
                if (min.Length < min_diaposon.Length)
                {
                    min_diaposon = min;
                    pointer = label;
                }
            }

            return Tuple.Create(min_diaposon, pointer);
        }

        public int[] Delete_Slots(Edge edge, int[] slots)
        {
            if (slots.Length == 0)
            {
                return edge.Slots;
            }
            int[] mas = new int[edge.Slots.Length - slots.Length];

            int k = 0;
            for (int i = 0; i < edge.Slots.Length; i++)
            {
                if (edge.Slots[i] == slots[0])
                {
                    k = i;
                    break;
                }
            }

            int l = 0;
            for (; l < k; l++)
            {
                mas[l] = edge.Slots[l];
            }
            for (int i = k + slots.Length; i < edge.Slots.Length; i++, l++)
            {
                mas[l] = edge.Slots[i];
            }
            return mas;
        }

        public bool Comparison_Mas(int[] mas1, int[] mas2)
        {
            if (mas1.Length != mas2.Length)
            {
                return false;
            }
            return true;
        }

        public double Utilization()
        {
            double a = 0;
            foreach (var edge in Edges)
            {
                a += edge.Slots.Length;
            }

            return 1 - a / (Edges.Count * All_Slots.Length);
        }

        public double Probably_Exp(double lambda)
        {
            int rand = rnd.Next();
            return -1 * (Math.Log(1 - (rand / (double)int.MaxValue)) / lambda);
        }

        int beta = 10;
        List<double> mas = new List<double>();
        delegate double Met(double prev, int n);
        delegate double Fac(int n);
        public void Method_Help()
        {
            List<double> arr = new List<double>();
            Fac Factrial = delegate (int n)
            {
                if (n == 0)
                {
                    return 1;
                }
                double res = 1;
                for (int i = 1; i <= n; i++)
                {
                    res *= i;
                }
                return res;
            };
            Met P = delegate (double prev, int n)
            {
                double res = 0;
                double add = Math.Pow(Math.E, -1 * beta) * Math.Pow(beta, n) / Factrial(n);
                res = prev + add;
                return res;
            };
            arr.Add(P(0, 0));
            for (int i = 1; true; i++)
            {
                arr.Add(P(arr[i - 1], i));
                if (arr[i] >= 1)
                {
                    arr[i] = 1;
                    break;
                }
            }
            mas.Add(-1);
            mas.Add((int)(arr[0] * int.MaxValue));
            for (int i = 1; true; i++)
            {
                if (mas.Last() == int.MaxValue)
                {
                    break;
                }
                int t = (int)(arr[i] * int.MaxValue);
                mas.Add(t);
            }
        }

        public double Probably_Puason(double beta)
        {
            int rand = rnd.Next();
            for (int i = 0; true; i++)
            {
                if (rand > mas[i] && rand <= mas[i + 1])
                {
                    return i;
                }
            }
        }

        public void Algo(Vertex start, Vertex end, double max_distance, int min_diaposon)
        {
            Reset_Label();

            bool check;


            BinaryHeap Heap_Priority = new BinaryHeap();
            Vertex pointer_vertex = start;
            var edge_start = new Edge(start, start, All_Slots);
            var pointer_heap = new Edge_Distance(edge_start, 0);
            Heap_Priority.Add_To_Heap(pointer_heap);
            List<int[]> list_range = new List<int[]>();

            start.Labels.Add(new Label(0, edge_start, All_Slots));
            while (Heap_Priority.Edges_Sort.Count != 0)
            {
                pointer_heap = Heap_Priority.Get_Min();
                pointer_vertex = pointer_heap.Edge.To;

                list_range = new List<int[]>();

                foreach (var label in pointer_vertex.Labels)
                {
                    if (label.Distance == pointer_heap.Distance && label.Previous_Edge.From == pointer_heap.Edge.From && label.Previous_Edge.To == pointer_heap.Edge.To)
                    {
                        list_range.Add(label.Slots);
                    }
                }

                foreach (var range in list_range)
                {
                    foreach (var outedge in pointer_vertex.OutEdge)
                    {
                        if (outedge.Slots.Length == 0)
                        {
                            continue;
                        }
                        int[] cross = Cross_Range(range, outedge.Slots);
                        double distance = pointer_heap.Distance + outedge.Weight;

                        if (distance < max_distance && Max_Diaposon(cross).Length >= min_diaposon)
                        {
                            var vertex = outedge.To;
                            var label_add = new Label(distance, outedge, cross);

                            var remove_label = new List<Label>();

                            check = true;
                            foreach (var label_vertex in vertex.Labels)
                            {
                                if (distance >= label_vertex.Distance && Comparison_Mas(cross, Cross_Range(cross, label_vertex.Slots)) == true)
                                {
                                    check = false;
                                    break;
                                }
                                if (distance <= label_vertex.Distance && Comparison_Mas(label_vertex.Slots, Cross_Range(cross, label_vertex.Slots)) == true)
                                {
                                    remove_label.Add(label_vertex);
                                }
                            }

                            if (check)
                            {
                                foreach (var label_del in remove_label)
                                {
                                    vertex.Labels.Remove(label_del);
                                }
                                vertex.Labels.Add(label_add);
                                Heap_Priority.Add_To_Heap(new Edge_Distance(outedge, distance));
                            }
                        }
                    }
                }
            }
        }

        public Tuple<List<Edge>, int[]> Trace(Vertex start, Vertex end, int diaposon)
        {
            if (end.Labels.Count == 0)
            {
                return null;
            }

            var result_way = new List<Edge>();
            var result_slots = new int[diaposon];

            var de = Min_Diaposon_Of_Labels(end.Labels, diaposon);
            var pointer = de.Item2;
            var min_diaposon = de.Item1;
            for (int i = 0; i < result_slots.Length; i++)
            {
                result_slots[i] = min_diaposon[i];
            }

            result_way.Add(pointer.Previous_Edge);
            while (true)
            {
                if (pointer.Previous_Edge.From == start)
                {
                    break;
                }
                foreach (var label in pointer.Previous_Edge.From.Labels)
                {
                    double k = pointer.Distance - label.Distance - pointer.Previous_Edge.Weight;
                    if (k < 0.00001 && Comparison_Mas(pointer.Slots, Cross_Range(label.Slots, pointer.Slots)))
                    {
                        pointer = label;
                        break;
                    }
                }
                result_way.Add(pointer.Previous_Edge);
            }

            foreach (var edge in result_way)
            {
                edge.Slots = Delete_Slots(edge, result_slots);
            }
            result_way.Reverse();
            return Tuple.Create(result_way, result_slots);
        }

        List<Result_Test> list_res = new List<Result_Test>();

        public List<Result_Test> Start_Check(TimeSpan all_time, double lambda)
        {
            var start_time = new DateTime(2019, 01, 01, 12, 00, 00);
            var finish_time = start_time + all_time;
            var list_interval = Intervals(all_time, lambda);
            var list_order = new List<Order_List>();

            int i = 0;
            foreach (var item in list_interval)
            {
                list_order.Add(new Order_List(i, start_time + item, true));
                list_order.Add(new Order_List(i, start_time + item + new TimeSpan((long)(new TimeSpan(1, 0, 0, 0).Ticks * Probably_Exp(10))), false)); // Реализовать бинарную кучу по времени
                i++;
            }
            list_order.Sort(delegate (Order_List x, Order_List y)
            {
                if (x.Time_Open == y.Time_Open)
                {
                    return 0;
                }
                else if (x.Time_Open > y.Time_Open)
                {
                    return 1;
                }
                else if (x.Time_Open < y.Time_Open)
                {
                    return -1;
                }
                else return x.Time_Open.CompareTo(y.Time_Open);
            });

            for (int k = 0; k < list_order.Count; k++)
            {
                if (list_order[k].true_false == true)
                {
                    for (int l = k + 1; l < list_order.Count; l++)
                    {
                        if (list_order[k].Name == list_order[l].Name)
                        {
                            list_order[k].number_close = l;
                            break;
                        }
                    }
                }
            }
            //int t = 0;
            for (int p = 0; p < list_order.Count; p++)
            {
                if (list_order[p].true_false == true)
                {
                    //Console.WriteLine(t++);
                    var add = Open_Order(list_order[p]);
                    list_res.Add(add);
                    list_order[list_order[p].number_close].res_close = add;
                }
                else
                {
                    Close_Order(list_order[p]);
                }
            }
            //int t = 0;
            //foreach (var order in list_order)
            //{
            //    if (order.true_false == true)
            //    {
            //        Console.WriteLine(t++);
            //        list_res.Add(Open_Order(order));

            //    }
            //    else
            //    {
            //        Close_Order(order);
            //    }
            //}
            return list_res;
        }

        public List<TimeSpan> Intervals(TimeSpan all_time, double lambda)
        {
            var list_interval = new List<TimeSpan>();
            list_interval.Add(new TimeSpan(0, 0, 0, 0));
            for (int i = 1; true; i++)
            {
                if (list_interval[i - 1] > all_time)
                {
                    return list_interval;
                }
                list_interval.Add(list_interval[i - 1] + new TimeSpan((long)(new TimeSpan(1, 0, 0, 0).Ticks * Probably_Exp(lambda))));
            }
        }
        int count = 0;
        public Result_Test Open_Order(Order_List order)
        {
            List<Edge> way = new List<Edge>();
            int diaposon = (int)Probably_Puason(10);
            var v_from = Vertexes[rnd.Next(1, Vertexes.Count)];

            List<Vertex> mas = new List<Vertex>();
            foreach (var item in Vertexes)
            {
                if (item != v_from)
                {
                    mas.Add(item);
                }
            }

            var v_to = mas[Program.Random_From_Array(mas)];

            double utiliz = Utilization();

            var prev_time = DateTime.Now;
            Algo(v_from, v_to, 2000, diaposon);
            var list = Trace(v_from, v_to, diaposon);
            var time = DateTime.Now - prev_time;
            //Console.WriteLine(count++);
            if (list != null)
            {
                return new Result_Test(order.Name, time.TotalSeconds, utiliz, true, Sum(list.Item1), list.Item2, list.Item1);
            }
            else
            {
                return new Result_Test(order.Name, time.TotalSeconds, utiliz, false, 0, null, null);
            }
        }
        public void Close_Order(Order_List order)
        {
            order.res_close.Cancel_Order();
            //foreach (var item in list_res)
            //{
            //    if (item.name == order.Name)
            //    {
            //        item.Cancel_Order();
            //    }
            //}
        }

        public void Graph_Delone()
        {
            List<Edge> All_Edges = new List<Edge>();


            for (int i = 0; i < Vertexes.Count - 1; i++)
            {
                for (int j = i + 1; j < Vertexes.Count; j++)
                {
                    var v_from = Vertexes[i];
                    var v_to = Vertexes[j];

                    All_Edges.Add(new Edge(v_from, v_to, All_Slots));
                    All_Edges.Add(new Edge(v_to, v_from, All_Slots));
                }
            }

            var Heap_Priority = new BinaryHeap();
            foreach (var edge in All_Edges)
            {
                Heap_Priority.Add_To_Heap(new Edge_Distance(edge, edge.Weight));
            }

            while (true)
            {
                Edge pointer = Heap_Priority.Get_Min().Edge;
                bool check_all_cross = false;
                foreach (var edge in Edges)
                {
                    check_all_cross = Check_Cross(edge, pointer);
                    if (check_all_cross == true)
                    {
                        break;
                    }
                }

                if (check_all_cross == false)
                {
                    Edges.Add(pointer);
                }

                if (Heap_Priority.Edges_Sort.Count == 0)
                {
                    break;
                }
            }
        }

        public void Graph_Gabriel()
        {
            List<Edge> edges_remove = new List<Edge>();
            foreach (var edge in Edges)
            {
                double radius = edge.Weight / 2;
                foreach (var vertex in Vertexes)
                {
                    if (vertex != edge.From && vertex != edge.To)
                    {
                        double length = Math.Sqrt(Math.Pow((edge.From.X + edge.To.X) / 2 - vertex.X, 2) + Math.Pow((edge.From.Y + edge.To.Y) / 2 - vertex.Y, 2));
                        if (length < radius)
                        {
                            edges_remove.Add(edge);
                            break;
                        }
                    }
                }
            }
            foreach (var item in edges_remove)
            {
                Edges.Remove(item);
            }
        }

        public bool Check_Cross(Edge e1, Edge e2)
        {
            Tuple<double, double> vector1 = new Tuple<double, double>(e1.To.X - e1.From.X, e1.To.Y - e1.From.Y);
            Tuple<double, double> vector2 = new Tuple<double, double>(e2.From.X - e1.From.X, e2.From.Y - e1.From.Y);
            Tuple<double, double> vector3 = new Tuple<double, double>(e2.To.X - e1.From.X, e2.To.Y - e1.From.Y);
            Tuple<double, double> vector4 = new Tuple<double, double>(e2.To.X - e2.From.X, e2.To.Y - e2.From.Y);
            Tuple<double, double> vector5 = new Tuple<double, double>(e1.From.X - e2.From.X, e1.From.Y - e2.From.Y);
            Tuple<double, double> vector6 = new Tuple<double, double>(e1.To.X - e2.From.X, e1.To.Y - e2.From.Y);

            double multy1 = vector1.Item1 * vector2.Item2 - vector1.Item2 * vector2.Item1;
            double multy2 = vector1.Item1 * vector3.Item2 - vector1.Item2 * vector3.Item1;
            double multy3 = vector4.Item1 * vector5.Item2 - vector4.Item2 * vector5.Item1;
            double multy4 = vector4.Item1 * vector6.Item2 - vector4.Item2 * vector6.Item1;

            if ((multy1 <= 0 && multy2 <= 0) || (multy1 >= 0 && multy2 >= 0))
            {
                return false;
            }
            if ((multy3 <= 0 && multy4 <= 0) || (multy3 >= 0 && multy4 >= 0))
            {
                return false;
            }


            return true;
        }
    }
}
