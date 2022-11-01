using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
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
        public string? Status { get; set; } = "Pending";

        [DisplayName("Date")]
        public string? Date { get; set; }

        [ForeignKey("User")]
        [DisplayName("Approved By")]
        public int? Approved_By { get; set; }
        public virtual TableUser? Approved_By_ID { get; set; }

        [ForeignKey("User")]
        [DisplayName("Assessed By")]
        public int? Assessed_By { get; set; }
        public virtual TableUser? Assessed_By_ID { get; set; }

        [ForeignKey("User")]
        [DisplayName("Hired By")]
        public int? Hired_By { get; set; }
        public virtual TableUser? Hired_By_ID { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }


}