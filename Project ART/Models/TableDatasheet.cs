using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableDatasheet
    {
        [Key]
        [DisplayName("Data Sheet ID")]
        public int Data_Sheet_ID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string? First_Name { get; set; }

        [DisplayName("Last Name")]
        public string? Last_Name { get; set; }

        [DisplayName("Middle Initial")]
        public char Middle_Initial { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Mobile Number")]
        public string Mobile_Number { get; set; }

        [DisplayName("LinkedIn URL")]
        public string LinkedIn { get; set; }

        [DisplayName("Position")]
        public string? Position { get; set; }


    }
}
