using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableWeighting
    {
        [Key]
        [DisplayName("Weighting ID")]
        public int weighting_ID { get; set; }

        [DisplayName("Personality")]
        public int? Personality { get; set; }

        [DisplayName("Resume")]
        public int? Resume { get; set; }

        [DisplayName("Exam")]
        public int? Exam { get; set; }

        [DisplayName("Interview")]
        public int? Interview { get; set; }


    }
}
