using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Skill ID")]
        public int Skill_ID { get; set; }

        [DisplayName("Datasheet ID")]
        [ForeignKey("TableDatasheet")]
        public int Data_Sheet_ID { get; set; }
        public virtual TableDatasheet TableDatasheet { get; set; }


        [DisplayName("Skill")]
        public string? Skill { get; set; }

        //Foreign Key

    }
}
