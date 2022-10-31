using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableResume
    {
        [Key]
        [DisplayName("Resume ID")]
        public int Resume_ID { get; set; }

        [DisplayName("Resume")]
        public String? Resume { get; set; }

        [DisplayName("Is Deleted")]
        public Boolean? Is_Deleted { get; set; } = false;

    }
}
