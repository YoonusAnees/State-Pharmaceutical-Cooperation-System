using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using State_Pharmaceutical_Cooperation__System;
namespace StatePharmaceuticalCooperation.Model
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        //[Required]
        //public string Type  { get; set; }


        public List<Tender> Tenders { get; set; } = new List<Tender>();

        //public List<Order> Orderss { get; set; } = new List<Order>(); // A supplier can place multiple orders


    }
}
