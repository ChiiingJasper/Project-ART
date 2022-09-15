using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableCandidate
    {
        [Key]
        [DisplayName("Candidate ID")]
        public int Candidate_ID { get; set; }

        [DisplayName("Application ID")]
        public int Application_ID { get; set; }

        [DisplayName("Assessment ID")]
        public int Assessment_ID { get; set; }


        [DisplayName("Hired By")]
        public int Hired_By { get; set; }


        [DisplayName("Is Hired")]
        public Boolean Is_Hired { get; set; }
        
    }
}
