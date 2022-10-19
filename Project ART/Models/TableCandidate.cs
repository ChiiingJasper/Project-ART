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

        [DisplayName("First Name")]
        public String? FirstName { get; set; }

        [DisplayName("Last Name")]
        public String? LastName { get; set; }

        [DisplayName("MI")]
        public String? MI { get; set; }

        [DisplayName("Status")]
        public String? Status { get; set; }

        [DisplayName("Email")]
        public String? Email { get; set; }

        [DisplayName("Email Confirmed")]
        public Boolean? EmailConfirmed { get; set; }

        [DisplayName("Mobile Number")]
        public String? MobileNumber { get; set; }

        [DisplayName("Resume")]
        public String? Resume { get; set; }

        [DisplayName("Video")]
        public String? Video { get; set; }

        [DisplayName("Website")]
        public String? Website { get; set; }


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
