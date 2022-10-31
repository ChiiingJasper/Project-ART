using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableIntroduction
    {
        [Key]
        [DisplayName("Introduction ID")]
        public int Introduction_ID { get; set; }

        [DisplayName("Introduction Video")]
        public string? Introduction_Video { get; set; }


        [DisplayName("DISC Trait")]
        public string? DISC_Trait { get; set; }

    }


}