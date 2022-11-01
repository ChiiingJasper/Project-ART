using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableExam
    {
        [Key]
        [DisplayName("Exam ID")]
        public int Exam_ID { get; set; }

        [DisplayName("Exam Score")]
        public int? Exam_Score { get; set; }

        [DisplayName("Exam Sheet")]
        public string? Exam_Sheet { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;


    }
}