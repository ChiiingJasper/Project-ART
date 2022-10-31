using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableStatus
    {
        [Key]
        [DisplayName("Status ID")]
        public int Status_ID { get; set; }

        [ForeignKey("Candidate")]
        [DisplayName("Candidate ID")]
        public int? Candidate_ID { get; set; }
        public virtual TableCandidate? Candidate { get; set; }


        [DisplayName("Status")]
        public string? Status { get; set; }

        [DisplayName("Date")]
        public string? Date { get; set; }


    }


}