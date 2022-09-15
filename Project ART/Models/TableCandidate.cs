using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableCandidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Candidate ID")]
        public int Candidate_ID { get; set; }

        [DisplayName("Application ID")]
        [ForeignKey("TableJobApplication")]
        public int Application_ID { get; set; }
        public virtual TableJobApplication TableJobApplication { get; set; }

        [DisplayName("Assessment ID")]
        [ForeignKey("TableAssessment")]
        public int Assessment_ID { get; set; }
        public virtual TableAssessment TableAssessment { get; set; }

        [DisplayName("Hired By")]
        [ForeignKey("TableRecruiter")]
        public int Hired_By { get; set; }
        public virtual TableRecruiter TableRecruiter { get; set; }

        [DisplayName("Is Hired")]
        public Boolean Is_Hired { get; set; }
        
    }
}
