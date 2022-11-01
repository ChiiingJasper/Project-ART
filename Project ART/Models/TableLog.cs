using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableLog
    {
        [Key]
        [DisplayName("Log ID")]
        public int Log_ID { get; set; }

        [DisplayName("Table ID")]
        public int? Table_ID { get; set; }

        [DisplayName("Table")]
        public string? Table { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Date Time")]
        public string? Date_Time { get; set; }

        [ForeignKey("User")]
        [DisplayName("Updated By")]
        public int User_ID { get; set; }
        public virtual TableUser? User { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }
}
