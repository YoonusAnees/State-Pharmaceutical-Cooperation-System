using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Pharmaceutical_Cooperation__System.Model
{
    internal class PharmacyInforation
    {
        [Key]
        public int PharmacyId { get; set; }  // Ensure this matches the database column


        [Required]
        public string PharmacyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
