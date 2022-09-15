using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableKeyword
    {
        [Key]
        [DisplayName("Key Word ID")]
        public int Key_Word_ID { get; set; }


        [DisplayName("Word")]
        public string? Word { get; set; }

        [DisplayName("Time Stamp")]
        public TimeOnly? Time_Stamp { get; set; }

        //Foreign Key
        [DisplayName("Introduction ID")]
        [ForeignKey("TableIntroduction")]
        public int Introduction_ID { get; set; }
        public virtual TableIntroduction Introductions { get; set; }
    }
}
