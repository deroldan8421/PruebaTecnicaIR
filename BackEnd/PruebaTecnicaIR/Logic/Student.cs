using DataAccessLayer.Models;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace PruebaTecnicaIR.Logic
{
    public class Student
    {
        public int addNewStudent(string nameStudent, string lastNameStudent, string identification, ApplicationDbContext _context)
        {
            try
            {
                var validateStudent = (from c in _context.Student
                                         where c.Identification == identification
                                         select c).ToList();
                if (validateStudent.Count > 0)
                {
                    return -2;
                }
                var idCredit = _context.Credits.Add(new DataAccessLayer.Models.Credits
                {
                    InitialCredits = 15,
                    SpentCredits = 0,
                    AvailableCredits = 0
                });
                _context.SaveChanges();

                var idUser = _context.Student.Add(new DataAccessLayer.Models.Student
                {
                    CreditID = idCredit.Entity.CreditID,
                    FirstName = nameStudent,
                    LastName = lastNameStudent,
                    Identification = identification
                });
                _context.SaveChanges();
                return idUser.Entity.StudentID;
            }
            catch (Exception error)
            {
                return -1;
            }
        }

        public string getStudent(string identificacion, ApplicationDbContext _context)
        {
            try
            {
                var student = (from s in _context.Student
                               where s.Identification == identificacion
                               select new
                               {
                                   s.StudentID,
                                   s.FirstName,
                                   s.LastName
                               }
                               );
                if (student != null)
                {
                    return JsonConvert.SerializeObject(student).ToString();
                }
                return "No se encontro el estudiante";
            }
            catch (Exception error)
            {
                return "Error al buscar el estudiante";
            }
        }
    }
}
