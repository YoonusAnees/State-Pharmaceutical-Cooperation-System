using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Pharmaceutical_Cooperation__System
{
    public enum OrderStatus
    {
        NotPaid,
        HalfPaid,
        Complete
    }
    internal class Drug
    {
       public int Id { get; set; }
       public string Name { get; set; } = string.Empty;
       public decimal Price { get; set; }

        public OrderStatus Status { get; set; }

       public int Stock { get; set; }
       public string Manufacturer { get; set; } = string.Empty;
       public string Description { get; set; } = string.Empty;
    }
}
