using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Professor
    {
        [Key]
        public int ProfessorID { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProfessorName { get; set; }
    }
}
