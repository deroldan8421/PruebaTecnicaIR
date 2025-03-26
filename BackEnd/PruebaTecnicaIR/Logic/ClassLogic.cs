using DataAccessLayer.Models;
using Newtonsoft.Json;

namespace PruebaTecnicaIR.Logic
{
    public class ClassLogic
    {
        public string getClass(string identificacion, ApplicationDbContext _context)
        {
            try
            {
                var classT = (from c in _context.Class
                              join st in _context.Student on c.StudentID equals st.StudentID
                              join s in _context.Subject on c.SubjectID equals s.SubjectID
                              join sa in _context.AssignedSubjects on s.SubjectID equals sa.SubjectID
                              join p in _context.Professor on sa.ProfessorID equals p.ProfessorID
                               where st.Identification == identificacion
                               select new
                               {
                                   p.ProfessorName,
                                   Value = s.SubjectID,
                                   Text = s.SubjectName
                               }
                               );
                if (classT != null)
                {
                    return JsonConvert.SerializeObject(classT).ToString();
                }
                return "No se encontro el estudiante";
            }
            catch (Exception error)
            {
                return "Error al buscar el estudiante";
            }
        }
        public string getClassAll(int idClass, ApplicationDbContext _context)
        {
            try
            {
                var classT = (from c in _context.Class
                              join st in _context.Student on c.StudentID equals st.StudentID
                              join s in _context.Subject on c.SubjectID equals s.SubjectID
                              where s.SubjectID == idClass
                              select new
                              {
                                  Student = st.FirstName + " " + st.LastName,
                                  
                              }).ToList();
                if (classT != null)
                {
                    return JsonConvert.SerializeObject(classT).ToString();
                }
                return "No se encontro Clase";
            }
            catch (Exception error)
            {
                return "Error al buscar el clase";
            }
        }
    }
}
