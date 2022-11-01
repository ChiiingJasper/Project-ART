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

        [ForeignKey("Exam")]
        [DisplayName("Exam ID")]
        public int Exam_ID { get; set; }
        public virtual TableExam? Exam { get; set; }

        [ForeignKey("Interview")]
        [DisplayName("Interview ID")]
        public int Interview_ID { get; set; }
        public virtual TableInterview? Interview { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }
}
