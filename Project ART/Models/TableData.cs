using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableData
    {
        [Key]
        [DisplayName("Data ID")]
        public int Data_ID { get; set; }

        [DisplayName("Data")]
        public string? Data { get; set; }

        [ForeignKey("Resume")]
        public int Resume_ID { get; set; }
        public virtual TableResume? Resume { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;
    }
}
