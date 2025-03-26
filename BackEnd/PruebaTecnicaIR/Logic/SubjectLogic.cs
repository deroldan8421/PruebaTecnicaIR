using DataAccessLayer.Models;
using Newtonsoft.Json;

namespace PruebaTecnicaIR.Logic
{
    public class SubjectLogic
    {
        public string getAllSubject(int idProfessor, ApplicationDbContext _context)
        {
            string result = "";
            try
            {
                var professor = (from p in _context.Subject
                                 join s in _context.AssignedSubjects on p.SubjectID equals s.SubjectID
                                 where s.ProfessorID == idProfessor
                                 select new
                                 {
                                     Value = p.SubjectID,
                                     Text = p.SubjectName
                                 });
                return JsonConvert.SerializeObject(professor).ToString();
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        public int addSubject(int idSubject, int idStudent, int idProfessor, ApplicationDbContext _context)
        {
            try
            {
                var validateProfessor = (from c in _context.Class
                                         join s in _context.AssignedSubjects on c.SubjectID equals s.SubjectID
                                         where c.StudentID == idStudent && s.ProfessorID == idProfessor
                                         select c).ToList();
                if (validateProfessor.Count > 0)
                {
                    return -2;
                }

                var validateSubject = (from c in _context.Class
                                       where c.StudentID == idStudent
                                       select c).ToList();
                if (validateSubject.Count > 3)
                {
                    return -4;
                }

                var creditSubject = _context.Subject.Where(x => x.SubjectID == idSubject).Select(x => x.CreditValue).FirstOrDefault();

                var validateCredit = (from c in _context.Student
                                      join cr in _context.Credits on c.CreditID equals cr.CreditID
                                      where c.StudentID == idStudent
                                      select new
                                      {
                                          credit =(cr.AvailableCredits == 0)? (cr.InitialCredits - creditSubject) : (cr.AvailableCredits - creditSubject),
                                          creditg = cr.SpentCredits + creditSubject
                                      }
                                      ).FirstOrDefault();

                if (validateCredit.creditg < 0) 
                {
                    return -3;
                }

                var UpdateCredit = _context.Credits.Find(idStudent);
                UpdateCredit.AvailableCredits = (int)validateCredit.credit;
                UpdateCredit.SpentCredits = (int)validateCredit.creditg;
                _context.SaveChanges();

                var idClass = _context.Class.Add(new DataAccessLayer.Models.Class
                {
                    SubjectID = idSubject,
                    StudentID = idStudent
                });

                _context.SaveChanges();


                return idClass.Entity.ClassID;
            }
            catch (Exception error)
            {
                return -1;
            }
        }
    }
}
