
using System;

namespace StatePharmaceuticalCooperation.Model
{
    public enum TenderStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }

    public class Tender
    {


        public int Id { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }


        public decimal Budget { get; set; }

      
 

        public string SupplierName { get; set; }

        public TenderStatus Status { get; set; } = TenderStatus.Pending;

    }
}
