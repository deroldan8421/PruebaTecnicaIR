using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        [Required]
        public int SubjectID { get; set; }
        [Required]
        public int StudentID { get; set; }
    }
}
