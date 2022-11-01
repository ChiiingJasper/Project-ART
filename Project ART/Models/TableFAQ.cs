using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableFAQ
    {
        [Key]
        [DisplayName("Question ID")]
        public int Question_ID { get; set; }

        [DisplayName("Question")]
        public string? Question { get; set; }

        [DisplayName("Answer")]
        public string? Answer { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Email Confirmed")]
        public bool? Email_Confirmed { get; set; } = false;

        [ForeignKey("User")]
        [DisplayName("Answered By")]
        public int User_ID { get; set; }
        public virtual TableUser? User { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }
}
