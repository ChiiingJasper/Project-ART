using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableAssessment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Assessment ID")]
        public int Assessment_ID { get; set; }

        [DisplayName("Exam ID")]
        [ForeignKey("TableExam")]
        public int Exam_ID { get; set; }
        public virtual TableExam TableExam { get; set; }



        [DisplayName("Interview ID")]
        [ForeignKey("TableInterview")]
        public int Interview_ID { get; set; }
        public virtual TableInterview TableInterview { get; set; }


        [DisplayName("Assessed By")]
        [ForeignKey("TableRecruiter")]
        public int? Assessed_By { get; set; }
        public virtual TableRecruiter TableRecruiter { get; set; }


        [DisplayName("Date Assessed")]
        public DateOnly? Date_Assessed { get; set; }
        
    }
}
