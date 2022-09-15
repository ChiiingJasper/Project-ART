﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ART.Models
{
    public class TableJobApplication
    {
        [Key]
        [DisplayName("Application ID")]
        public int Application_ID { get; set; }

        [DisplayName("Data Sheet ID")]
        [Required]
        public string? DataSheet_ID { get; set; }

        [DisplayName("Introduction")]
        public string? Introduction { get; set; }

        [DisplayName("Is Approved")]
        public Boolean? Is_Approved { get; set; }

        [DisplayName("Approved By")]
        public int Approved_By { get; set; }

        [DisplayName("Date Received")]
        public DateOnly? Date_Received { get; set; }

        //Foreign Keys

    }
}
