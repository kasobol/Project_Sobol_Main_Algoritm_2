using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Algoritm_Main_2
{
    class Order_List
    {
        public int Name { get; set; }

        public DateTime Time_Open { get; set; }

        public bool true_false { get; set; }

        public Order_List(int Name, DateTime Time_Open, bool true_false)
        {
            this.Name = Name;
            this.Time_Open = Time_Open;
            this.true_false = true_false;
        }

        public int CompareTo(Order_List other)
        {
            if (other == null)
                return 1;

            else
                return this.Time_Open.CompareTo(other.Time_Open);
        }

        public int number_close;
        public Result_Test res_close { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Time_Exist: {Time_Open}, TrueOrFalse: {true_false}";
        }
    }
}
