using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableResume
    {
        [Key]
        [DisplayName("Resume ID")]
        public int Resume_ID { get; set; }

        [DisplayName("Resume")]
        public string? Resume { get; set; }

        [DisplayName("Resume Score")]
        public int? Resume_Score { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }
}
