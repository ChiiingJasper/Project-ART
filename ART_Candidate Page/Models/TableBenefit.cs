﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ART_Candidate_Page.Models
{
    public class TableBenefit
    {
        [Key]
        [DisplayName("Benefit ID")]
        public int Benefit_ID { get; set; }

        [ForeignKey("Job Application")]
        public int Job_Application_ID { get; set; }
        public virtual TableJobApplication? JobApplication { get; set; }

        [DisplayName("Benefit")]
        public string? Benefit { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; } = " ";

        [DisplayName("Is Deleted")]
        public bool? Is_Deleted { get; set; } = false;

    }
}
