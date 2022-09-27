using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableSkill
    {
        [Key]
        public int Skill_ID { get; set; }
        [DisplayName("Skill Name")]
        public string? Skill_Name { get; set; }

        //Foreign Key
        [Required]
        [ForeignKey("Datasheets")]
        public int DatasheetID { get; set; }
        public virtual TableDatasheet Datasheets { get; set; }


    }
}
