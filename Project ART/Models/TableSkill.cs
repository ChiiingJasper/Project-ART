using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Project_ART.Models
{
    public class TableSkill
    {
        [Key]
        [DisplayName("Skill ID")]
        public int Skill_ID { get; set; }

        [Required]
        [DisplayName("Data Sheet ID")]
        public string? Data_Sheet_ID { get; set; }

        [DisplayName("Skill")]
        public string? Skill { get; set; }
    }
}
