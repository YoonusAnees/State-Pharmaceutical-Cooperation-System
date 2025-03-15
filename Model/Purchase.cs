using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Pharmaceutical_Cooperation__System.Model
{
    internal class Purchase
    {
        public int OrderId { get; set; }

        public int DrugId { get; set; }   // Foreign Key to Drug

        public string DrugName { get; set; }

        //public Drug Drug { get; set; }

        //public string PharmacyName { get; set; }

        public decimal TotalPrice { get; set; }





        //public Drug Drug { get; set; }    // Navigation property to Drug
        public int Stock { get; set; }
        public DateTime OrderDate { get; set; } // This should match the database column name


        public List<Drug> Drugs { get; set; } = new List<Drug>();
        public List<PharmacyInforation> pharmacies { get; set; } = new List<PharmacyInforation>();


    }
}
