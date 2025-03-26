using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIR.Logic;

namespace PruebaTecnicaIR.Controllers
{
    [AllowAnonymous]
    public class ProfessorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProfessorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/interrapidisimo/getAllProfessor")]
        public ActionResult getAllProfessor()
        {
            ControllerAnswer _jAnswer = new ControllerAnswer();

            PruebaTecnicaIR.Logic.ProfessorLogic sLogic = new Logic.ProfessorLogic();
            string result = sLogic.getAllProfessor(_context);

            _jAnswer.Message = result;
            _jAnswer.Id = 0;
            _jAnswer.UpdateModel = false;
            _jAnswer.ErrorStatus = false;
            return Ok(_jAnswer);

        }
    }
}
