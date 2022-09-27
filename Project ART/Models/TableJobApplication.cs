using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableJobApplication
    {
        [Key]
        [DisplayName("Application ID")]
        public int Application_ID { get; set; }

        [DisplayName("Is Approved")]
        public Boolean? Is_Approved { get; set; }

        [DisplayName("Date Received")]
        public DateTime Date_Received { get; set; }

        //Foreign Keys
        [Required]
        [ForeignKey("Introductions")]
        public int IntroductionID { get; set; }
        public virtual TableIntroduction Introductions { get; set; }

        [Required]
        [ForeignKey("Datasheets")]
        public int DatasheetID { get; set; }
        public virtual TableDatasheet Datasheets { get; set; }

        [Required]
        [ForeignKey("Users")]
        [DisplayName("Approved By")]
        public int CompanyID { get; set; }
        public virtual TableUser Users { get; set; }


    }
}
