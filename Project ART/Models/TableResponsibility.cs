using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableResponsibility
    {
        [Key]
        [DisplayName("Responsibility ID")]
        public int Responsibility_ID { get; set; }

        [ForeignKey("JobApplication")]
        [DisplayName("Job Application ID")]
        public int? Job_Application_ID { get; set; }
        public virtual TableJobApplication? JobApplication { get; set; }

        [DisplayName("Responsibility")]
        public string? Responsibility { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; } = " ";

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }
}
