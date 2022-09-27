using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableAssessment
    {
        [Key]
        [DisplayName("Assessment ID")]
        public int Assessment_ID { get; set; }

        [DisplayName("Date Assessed")]
        public DateTime Date_Assessed { get; set; }

        //Foreign Keys
        [Required]
        [ForeignKey("Exams")]
        public int ExamID { get; set; }
        public virtual TableExam Exams { get; set; }

        [Required]
        [ForeignKey("Interviews")]
        public int InterviewID { get; set; }
        public virtual TableInterview Interviews { get; set; }

        [Required]
        [ForeignKey("Users")]
        [DisplayName("Assessed By")]
        public int CompanyID { get; set; }
        public virtual TableUser Users { get; set; }

    }
}
