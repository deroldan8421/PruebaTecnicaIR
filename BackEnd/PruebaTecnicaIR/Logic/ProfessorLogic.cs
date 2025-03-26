using DataAccessLayer.Models;
using Newtonsoft.Json;

namespace PruebaTecnicaIR.Logic
{
    public class ProfessorLogic
    {
        public string getAllProfessor(ApplicationDbContext _context)
        {
            string result = "";
            try
            {
                var professor = (from p in _context.Professor
                                 select new
                                 {
                                   Value = p.ProfessorID,
                                   Text = p.ProfessorName 
                                  });
                return JsonConvert.SerializeObject(professor).ToString();
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
    }
}
