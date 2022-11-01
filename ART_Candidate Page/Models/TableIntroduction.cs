using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableIntroduction
    {
        [Key]
        [DisplayName("Introduction ID")]
        public int Introduction_ID { get; set; }

        [DisplayName("DISC Trait")]
        public string? DISC_Trait { get; set; }

        [DisplayName("Introduction Video")]
        public string? Introduction_Video { get; set; }

        [DisplayName("Introduction Score")]
        public int? Introduction_Score { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;


    }


}