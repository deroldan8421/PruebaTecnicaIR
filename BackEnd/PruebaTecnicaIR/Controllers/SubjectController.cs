using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaIR.Logic;

namespace PruebaTecnicaIR.Controllers
{
    [AllowAnonymous]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/interrapidisimo/getSubject/{idProfessor}")]
        public ActionResult getAllSubject(int idProfessor)
        {
            ControllerAnswer _jAnswer = new ControllerAnswer();

            PruebaTecnicaIR.Logic.SubjectLogic sLogic = new Logic.SubjectLogic();
            string result = sLogic.getAllSubject(idProfessor,_context);

            _jAnswer.Message = result;
            _jAnswer.Id = 0;
            _jAnswer.UpdateModel = false;
            _jAnswer.ErrorStatus = false;
            return Ok(_jAnswer);

        }

        [HttpPost("api/interrapidisimo/addSubject/{idSubject}/{idStudent}/{idProfessor}")]
        public ActionResult addSubject(int idSubject, int idStudent, int idProfessor)
        {
            ControllerAnswer _jAnswer = new ControllerAnswer();
            if (idSubject == 0 ||
                idStudent == 0 ||
                idProfessor == 0)
            {
                _jAnswer.Message = "Los campos obligatorios son: Materia, estudiante, profesor.";
                _jAnswer.Id = 0;
                _jAnswer.UpdateModel = false;
                _jAnswer.ErrorStatus = true;
                return Ok(_jAnswer);
            }
            PruebaTecnicaIR.Logic.SubjectLogic sLogic = new Logic.SubjectLogic();

            int result = sLogic.addSubject(idSubject, idStudent, idProfessor, _context);

            if (result > 0)
            {
                _jAnswer.Message = "Se inscribio a la materia correctamente";
                _jAnswer.Id = result;
                _jAnswer.UpdateModel = true;
                _jAnswer.ErrorStatus = false;
                return Ok(_jAnswer);
            }
            else
            {
                if (result == -1)
                {
                    _jAnswer.Message = "Error al guardar";
                    _jAnswer.Id = result;
                    _jAnswer.UpdateModel = false;
                    _jAnswer.ErrorStatus = true;
                    return Ok(_jAnswer);
                }
                else if (result == -2)
                {
                    _jAnswer.Message = "El estudiante ya tiene clase con ese profesor";
                    _jAnswer.Id = result;
                    _jAnswer.UpdateModel = false;
                    _jAnswer.ErrorStatus = true;
                    return Ok(_jAnswer);
                }
                else if (result == -3)
                {
                    
                    _jAnswer.Message = "no tiene creditos disponibles";
                    _jAnswer.Id = result;
                    _jAnswer.UpdateModel = false;
                    _jAnswer.ErrorStatus = true;
                    return Ok(_jAnswer);
                    
                }
                else 
                {

                    _jAnswer.Message = "no puede inscribir mas de 3 materias";
                    _jAnswer.Id = result;
                    _jAnswer.UpdateModel = false;
                    _jAnswer.ErrorStatus = true;
                    return Ok(_jAnswer);

                }

            }
        }

    }
}
