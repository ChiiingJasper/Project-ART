using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableJobApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Application ID")]
        public int Application_ID { get; set; }

        [DisplayName("Data Sheet ID")]
        [ForeignKey("TableDatasheet")]
        public string? DataSheet_ID { get; set; }
        public virtual TableDatasheet TableDatasheet { get; set; }

        [DisplayName("Introduction")]
        [ForeignKey("TableIntroduction")]
        public string? Introduction_ID { get; set; }
        public virtual TableIntroduction TableIntroduction { get; set; }

        [DisplayName("Is Approved")]
        public Boolean? Is_Approved { get; set; }

        [DisplayName("Approved By")]
        [ForeignKey("TableEmployee")]
        public int Approved_By { get; set; }
        public virtual TableEmployee TableEmployee { get; set; }

        [DisplayName("Date Received")]
        public DateOnly? Date_Received { get; set; }

        //Foreign Keys


    }
}
