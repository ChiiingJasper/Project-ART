using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_ART.Models
{
    public class TableIntroduction
    {
        [Key]
        [DisplayName("Introduction ID")]
        public int Introduction_ID { get; set; }

        [DisplayName("B5 Trait")]
        [Required]
        public string? B5_Trait { get; set; }

        [DisplayName("DISC Trait")]
        public string? DISC_Trait { get; set; }

    }


}