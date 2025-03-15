using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Pharmaceutical_Cooperation__System.Model
{
    internal class OderItem
    {
        public int OrderId { get; set; }


        public int DrugId { get; set; }

        public string DrugName { get; set; }
        //public Drug Drug { get; set; }

        public int Stock { get; set; }

        public OrderStatus Status { get; set; }
        //public decimal TotalPrice { get; set; }
    }
}
