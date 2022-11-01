using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableJobApplication
    {
        [Key]
        [DisplayName("Job Application ID")]
        public int Job_Application_ID { get; set; }

        [DisplayName("Job")]
        public string? Job { get; set; }

        [DisplayName("Job Description")]
        public string? Job_Description { get; set; }

        [DisplayName("Icon")]
        public string? Icon { get; set; }

        [DisplayName("Date Published")]
        public string? Date_Published { get; set; }

        [DisplayName("Date End")]
        public string? Date_End { get; set; }

        [DisplayName("Vacancy")]
        public int? Vacancy { get; set; }

        [DisplayName("Salary")]
        public string? Salary { get; set; }

        [DisplayName("Job Nature")]
        public string? Job_Nature { get; set; }

        [DisplayName("Province")]
        public string? Province { get; set; }

        [DisplayName("City")]
        public string? City { get; set; }

        [DisplayName("Is Open")]
        public bool? Is_Open { get; set; }

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;









    }
}
