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

        [DisplayName("First Name")]
        public String? First_Name { get; set; }

        [DisplayName("Last Name")]
        public String? Last_Name { get; set; }

        [DisplayName("Middle Initital")]
        public String? Middle_Initital { get; set; }

        [DisplayName("Email")]
        public String? Email { get; set; }

        [DisplayName("Mobile Number")]
        public String? MobileNumber { get; set; }

        [DisplayName("Photo")]
        public String? Photo { get; set; }

        [DisplayName("Website")]
        public String? Website { get; set; }

        [DisplayName("Province")]
        public String? Province { get; set; }

        [DisplayName("City")]
        public String? City { get; set; }

        [DisplayName("Email Confirmed")]
        public Boolean? EmailConfirmed { get; set; } = false;


        [ForeignKey("Resume")]
        [DisplayName("Resume ID")]
        public int? Resume_ID { get; set; }
        public virtual TableResume? Resume { get; set; }

        [ForeignKey("Introduction")]
        [DisplayName("Introduction ID")]
        public int? Introduction_ID { get; set; }
        public virtual TableIntroduction? Introduction { get; set; }

        [ForeignKey("Assessment")]
        [DisplayName("Assessment ID")]
        public int? Assessment_ID { get; set; }
        public virtual TableAssessment? Assessment { get; set; }

        [ForeignKey("JobApplication")]
        [DisplayName("Job Application ID")]
        public int? Job_Application_ID { get; set; }
        public virtual TableJobApplication? JobApplication { get; set; }

        [DisplayName("Is Deleted")]
        public Boolean? Is_Deleted { get; set; } = false;

    }
}
