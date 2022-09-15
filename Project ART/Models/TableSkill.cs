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

        [DisplayName("Datasheet ID")]
        public int Data_Sheet_ID { get; set; }

        [DisplayName("Skill")]
        public string? Skill { get; set; }

        //Foreign Key

    }
}
