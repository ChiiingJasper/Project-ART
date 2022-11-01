using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableKeyword
    {
        [Key]
        [DisplayName("Key Word ID")]
        public int Key_Word_ID { get; set; }

        [ForeignKey("Introduction")]
        public int Introduction_ID { get; set; }
        public virtual TableIntroduction? Introduction { get; set; }

        [DisplayName("Word")]
        public string? Word { get; set; }

        [DisplayName("Time Stamp")]
        public string? Time_Stamp { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;
    }
}
