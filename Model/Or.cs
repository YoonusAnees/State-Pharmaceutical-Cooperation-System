using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Pharmaceutical_Cooperation__System.Model
{
    class Or
    {

        [Key]
        public int OrderId { get; set; }


        public int Id { get; set; }

        public string DrugName { get; set; }
        //public Drug Drug { get; set; }

        public int PharmacyId { get; set; }


        public string PharmacyName { get; set; }

        //public Pharmacy pharmacy { get; set; }

        public int Stock { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
