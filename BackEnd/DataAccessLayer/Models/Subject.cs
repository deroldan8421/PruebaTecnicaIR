﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }
        public decimal CreditValue { get; set; }
    }
}
