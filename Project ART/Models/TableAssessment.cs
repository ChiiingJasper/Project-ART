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

        [DisplayName("Exam ID")]
        public int Exam_ID { get; set; }

        [DisplayName("Interview ID")]
        public int Interview_ID { get; set; }


        [DisplayName("Assessed By")]
        public int? Assessed_By { get; set; }



        [DisplayName("Date Assessed")]
        public DateTime Date_Assessed { get; set; }
        
    }
}
