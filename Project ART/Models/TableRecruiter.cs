using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_ART.Models
{
    public class TableRecruiter
    {
        [Key]
        [DisplayName("Recruiter ID")]
        public int Recruiter_ID { get; set; }

        [DisplayName("Company ID")]
        public string? Company_ID { get; set; }

        [DisplayName("Password")]
        public PasswordPropertyTextAttribute? Password { get; set; }

        [DisplayName("Is Admin")]
        public Boolean? Is_Admin { get; set; }

    }
}
