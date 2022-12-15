using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TablePendingCandidatePrediction
    {
        [Key]
        [DisplayName("CandidateDetails ID")]
        public int Candidate_Details_ID { get; set; }

        [DisplayName("Candidate_ID")]
        public int Candidate_ID { get; set; }

        [DisplayName("DISC Personality")]
        public string? DISC_Personality { get; set; }

        [DisplayName("Resume Score")]
        public int? Resume_Score { get; set; }

        [DisplayName("Approved")]
        public bool? Approved { get; set; }
    }
}
