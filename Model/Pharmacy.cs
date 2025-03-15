
using System.ComponentModel.DataAnnotations;

namespace StatePharmaceuticalCooperation.Model
{
    public class Pharmacy
    {

        [Key] // Ensures this is the primary key

        public int PharmacyId { get; set; }  // Ensure this matches the database column


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
