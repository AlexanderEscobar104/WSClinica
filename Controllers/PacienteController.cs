using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WSClinica.Connection;
using WSClinica.Models;
using WSClinica.Services;

namespace WSClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        /// <summary>
        /// metodo para listar Paciente
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listPaciente")]
        public IActionResult listPaciente([FromQuery] int opcion, int id, string nombre = "", string apellido = "", string identificacion = "")
        {

            List<Paciente> pacienteList = new List<Paciente>();
            pacienteList = _pacienteService.GetAll(opcion, id, nombre, apellido, identificacion );
            return Ok(pacienteList);
        }

        /// <summary>
        /// metodo para agregar Paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addPaciente")]
        public IActionResult addPaciente(Paciente paciente)
        {
            string resultado;
            resultado = _pacienteService.AddPaciente(paciente);
            return Ok(resultado);
        }

        /// <summary>
        /// metodo para actualizar Paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatePaciente")]
        public IActionResult updatePaciente(Paciente paciente)
        {
            string resultado;
            resultado = _pacienteService.UpdatePaciente(paciente);
            return Ok(resultado);
        }

        /// <summary>
        /// metodo para eliminar Paciente
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletePaciente")]
        public IActionResult deletePaciente(int Id)
        {
            string resultado;
            resultado = _pacienteService.DeletePaciente(Id);
            return Ok(resultado);
        }
    }
}
