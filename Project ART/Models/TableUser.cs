using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableUser
    {
        [Key]
        [DisplayName("Company ID")]
        public int Company_ID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string? First_Name { get; set; }

        [DisplayName("Last Name")]
        public string? Last_Name { get; set; }

        [DisplayName("Middle Initial")]
        public char? Middle_Initial { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Mobile Number")]
        public string? Mobile_Number { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Is Admin")]
        public Boolean Is_Admin { get; set; }
        [DisplayName("Profile Picture")]
        public string Profile_Pic { get; set; }

    }
}