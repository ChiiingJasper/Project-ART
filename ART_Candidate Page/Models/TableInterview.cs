using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableInterview
    {
        [Key]
        [DisplayName("Interview ID")]
        public int Interview_ID { get; set; }

        [DisplayName("Interview Score")]
        public int? Interview_Score { get; set; }

        [DisplayName("Interview")]
        public string? Interview { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;


    }
}