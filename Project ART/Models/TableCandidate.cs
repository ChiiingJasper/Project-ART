using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableCandidate
    {
        [Key]
        [DisplayName("Candidate ID")]
        public int Candidate_ID { get; set; }

        [DisplayName("Is Hired")]
        public Boolean Is_Hired { get; set; }

        //Foreign Keys
        [ForeignKey("JobApplication")]
        public int? ApplicationID { get; set; }
        public virtual TableJobApplication JobApplication { get; set; }

        [ForeignKey("Assessments")]
        public int? AssessmentID { get; set; }
        public virtual TableAssessment Assessments { get; set; }

        [ForeignKey("Users")]
        [DisplayName("Hired By")]
        public int? CompanyID { get; set; }
        public virtual TableUser Users { get; set; }

    }
}
