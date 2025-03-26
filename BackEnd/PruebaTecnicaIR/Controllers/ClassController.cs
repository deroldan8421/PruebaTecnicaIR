using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIR.Logic;

namespace PruebaTecnicaIR.Controllers
{
    [AllowAnonymous]
    public class ClassController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("api/interrapidisimo/getClass/{identification}")]
        public ActionResult getStudent(string identification)
        {
            ControllerAnswer _jAnswer = new ControllerAnswer();

            PruebaTecnicaIR.Logic.ClassLogic sLogic = new Logic.ClassLogic();
            string result = sLogic.getClass(identification, _context);

            _jAnswer.Message = result;
            _jAnswer.Id = 0;
            _jAnswer.UpdateModel = false;
            _jAnswer.ErrorStatus = false;
            return Ok(_jAnswer);

        }

        [HttpGet("api/interrapidisimo/getClassAll/{idClass}")]
        public ActionResult getClassAll(int idClass)
        {
            ControllerAnswer _jAnswer = new ControllerAnswer();

            PruebaTecnicaIR.Logic.ClassLogic sLogic = new Logic.ClassLogic();
            string result = sLogic.getClassAll(idClass, _context);

            _jAnswer.Message = result;
            _jAnswer.Id = 0;
            _jAnswer.UpdateModel = false;
            _jAnswer.ErrorStatus = false;
            return Ok(_jAnswer);

        }
    }
}
