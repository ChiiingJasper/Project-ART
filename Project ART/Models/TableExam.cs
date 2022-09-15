using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableExam
    {
        [Key]
        [DisplayName("Exam ID")]
        public int Exam_ID { get; set; }

        [DisplayName("Exam Score")]
        [Required]
        public double Exam_Score { get; set; }
        

    }
}