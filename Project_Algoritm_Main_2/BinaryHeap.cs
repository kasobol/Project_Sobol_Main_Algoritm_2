using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class BinaryHeap
    {
        public List<Edge_Distance> Edges_Sort = new List<Edge_Distance>();

        public Edge_Distance Get_Min()
        {
            Edge_Distance edge = Edges_Sort.First();
            Delete_Min();
            return edge;
        }

        public void Add_To_Heap(Edge_Distance edge)
        {
            if (Edges_Sort.Count == 0)
            {
                Edges_Sort.Add(edge);
                return;
            }

            Edges_Sort.Add(edge);
            Edge_Distance pointer = edge;

            int index_pointer = Edges_Sort.Count - 1;
            int index_prev_node;

            while (true)
            {
                index_prev_node = (index_pointer - 1) / 2;
                if (pointer.Distance < Edges_Sort[index_prev_node].Distance)
                {
                    Edges_Sort[index_pointer] = Edges_Sort[index_prev_node];
                    Edges_Sort[index_prev_node] = pointer;
                    index_pointer = index_prev_node;
                }
                else
                {
                    break;
                }
            }
        }

        public void Delete_Min()
        {
            if (Edges_Sort.Count == 1 || Edges_Sort.Count == 2)
            {
                Edges_Sort.RemoveAt(0);
                return;
            }

            Edges_Sort[0] = Edges_Sort.Last();
            Edges_Sort.RemoveAt(Edges_Sort.Count - 1);

            Edge_Distance pointer = Edges_Sort[0];
            Edge_Distance minimum_pointer;
            int index_pointer = 0;
            int index_minimum_pointer;
            int index_follow_left = 1;
            int index_follow_right = 2;

            while (true)
            {
                if (index_pointer != (Edges_Sort.Count - 1) / 2)
                {
                    if (index_pointer < (Edges_Sort.Count - 1) / 2)
                    {
                        minimum_pointer = Min_Of_Three(pointer, Edges_Sort[index_follow_left], Edges_Sort[index_follow_right]);
                        index_minimum_pointer = Edges_Sort.IndexOf(minimum_pointer);
                        if (pointer == minimum_pointer)
                        {
                            return;
                        }
                        //if (pointer != minimum_pointer)
                        //{
                        Edges_Sort[index_minimum_pointer] = pointer;
                        Edges_Sort[index_pointer] = minimum_pointer;
                        index_pointer = index_minimum_pointer;
                        index_follow_left = index_pointer * 2 + 1;
                        index_follow_right = index_follow_left + 1;
                        //}
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    minimum_pointer = pointer.Distance > Edges_Sort.Last().Distance ? Edges_Sort.Last() : pointer;
                    index_minimum_pointer = Edges_Sort.IndexOf(minimum_pointer);
                    if (pointer != minimum_pointer)
                    {
                        Edges_Sort[index_minimum_pointer] = pointer;
                        Edges_Sort[index_pointer] = minimum_pointer;
                    }
                    return;
                }
            }
        }

        Edge_Distance Min_Of_Three(Edge_Distance e1, Edge_Distance e2, Edge_Distance e3)
        {
            return e1.Distance > e2.Distance ? (e2.Distance > e3.Distance ? e3 : e2) : (e1.Distance > e3.Distance ? e3 : e1);
        }
    }
}
