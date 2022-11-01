using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableCandidate
    {
        [Key]
        [DisplayName("Candidate ID")]
        public int Candidate_ID { get; set; }

        [DisplayName("First Name")]
        public string? First_Name { get; set; }

        [DisplayName("Last Name")]
        public string? Last_Name { get; set; }

        [DisplayName("Middle Initital")]
        public char? Middle_Initital { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Mobile Number")]
        public string? Mobile_Number { get; set; }

        [DisplayName("Photo")]
        public string? Photo { get; set; }

        [DisplayName("Website")]
        public string? Website { get; set; }

        [DisplayName("Province")]
        public string? Province { get; set; }

        [DisplayName("City")]
        public string? City { get; set; }

        [DisplayName("Email Confirmed")]
        public bool? Email_Confirmed { get; set; } = false;


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
        public bool? Is_Deleted { get; set; } = false;

    }
}
