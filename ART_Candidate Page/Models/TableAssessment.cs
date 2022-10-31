using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
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


    }
}
