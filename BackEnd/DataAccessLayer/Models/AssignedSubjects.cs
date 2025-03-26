using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AssignedSubjects
    {
        [Key]
        public int AssignedSubjectID { get; set; }
        [Required]
        public int ProfessorID { get; set; }
        [Required]
        public int SubjectID { get; set; }

    }
}
