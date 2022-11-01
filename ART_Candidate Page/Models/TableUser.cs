using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableUser
    {
        [Key]
        [DisplayName("Company ID")]
        public int Company_ID { get; set; }

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
        public string? Password { get; set; }

        [DisplayName("Is Admin")]
        public bool Is_Admin { get; set; }

        [DisplayName("Profile Picture")]
        public string? Profile_Pic { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }
}