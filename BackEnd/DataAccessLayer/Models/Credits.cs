using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Credits
    {
        [Key]
        public int CreditID { get; set; }
        [Required]
        public int InitialCredits { get; set; }
        [Required]
        public int AvailableCredits { get; set; }
        [Required]
        public int SpentCredits { get; set; }
    }
}
