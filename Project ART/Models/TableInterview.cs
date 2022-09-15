using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableInterview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Interview ID")]
        public int Interview_ID { get; set; }

        [DisplayName("Interview Score")]
        [Required]
        public double Interview_Score { get; set; }


    }
}