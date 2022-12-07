using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableSkill
    {
        [Key]
        [DisplayName("Skill ID")]
        public int Skill_ID { get; set; }

        [DisplayName("Skill")]
        public string? Data { get; set; }

        [DisplayName("Skill Matched")]
        public bool? Skill_Matched { get; set; } = false;

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;
    }
}
