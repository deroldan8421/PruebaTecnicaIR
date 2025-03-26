using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PruebaTecnicaIR.Logic;
using System.Data;

namespace PruebaTecnicaIR.Controllers
{
    
    [AllowAnonymous]
    public class studentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public studentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("api/interrapidisimo/createStudent/{nameStudent}/{lastNameStudent}/{identification}")]
        public ActionResult addNewStudent(string nameStudent, string lastNameStudent, string identification)
        {
            ControllerAnswer _jAnswer = new ControllerAnswer();
            if (string.IsNullOrEmpty(nameStudent) ||
                string.IsNullOrEmpty(lastNameStudent) ||
                string.IsNullOrEmpty(identification))
            {
                _jAnswer.Message = "Los campos obligatorios son: nombre, apellido, identificacion.";
                _jAnswer.Id = 0;
                _jAnswer.UpdateModel = false;
                _jAnswer.ErrorStatus = true;
                return Ok(_jAnswer);
            }
            PruebaTecnicaIR.Logic.Student sLogic = new Logic.Student(); 
            
            int result = sLogic.addNewStudent(nameStudent,lastNameStudent,identification, _context);

            if (result > 0)
            {
                _jAnswer.Message = "Datos guardados correctamente";
                _jAnswer.Id = result;
                _jAnswer.UpdateModel = true;
                _jAnswer.ErrorStatus = false;
                return Ok(_jAnswer);
            }
            else
            {
                if (result == -2)
                {
                    _jAnswer.Message = "El estudiante ya existe en la base de datos";
                    _jAnswer.Id = result;
                    _jAnswer.UpdateModel = false;
                    _jAnswer.ErrorStatus = true;
                    return Ok(_jAnswer);
                }
                else
                {
                    _jAnswer.Message = "Error al guardar";
                    _jAnswer.Id = result;
                    _jAnswer.UpdateModel = false;
                    _jAnswer.ErrorStatus = true;
                    return Ok(_jAnswer);
                }
               
            }
        }

        [HttpGet("api/interrapidisimo/getStudent/{identification}")]
        public ActionResult getStudent(string identification)
        {
            ControllerAnswer _jAnswer = new ControllerAnswer();

            PruebaTecnicaIR.Logic.Student sLogic = new Logic.Student();
            string result = sLogic.getStudent(identification, _context);

            _jAnswer.Message = result;
            _jAnswer.Id = 0;
            _jAnswer.UpdateModel = false;
            _jAnswer.ErrorStatus = false;
            return Ok(_jAnswer);
            
        }
    }
}
