using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TablePersonalityPrediction
    {
        [Key]
        [DisplayName("CandidateDetails ID")]
        public int Candidate_Details_ID { get; set; }

        [DisplayName("Introduction Video Data")]
        public string? Introduction_Video_Data { get; set; }

        [DisplayName("DISC Personality")]
        public string? DISC_Personality { get; set; }


    }
}
