using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Project_ART.Models
{
    public class TableRecruiter
    {
        [Key]
        [DisplayName("Recruiter ID")]
        public int Recruiter_ID { get; set; }

        [DisplayName("Company ID")]
        public int Company_ID { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Is Admin")]
        public Boolean? Is_Admin { get; set; }

        //Foreign Key
        

    }
}
