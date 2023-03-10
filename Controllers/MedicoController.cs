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
    public class MedicoController : Controller
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        /// <summary>
        /// metodo para listar medicos
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="especialidad"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listMedico")]
        public IActionResult listMedico([FromQuery] int opcion, int id, string nombre = "", string apellido = "", string especialidad = "")
        {

            List<Medico> medicoList = new List<Medico>();
            medicoList = _medicoService.GetAll(opcion, id, nombre, apellido, especialidad);
            return Ok(medicoList);
        }


        /// <summary>
        /// metodo para agregar medicos
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addMedico")]
        public IActionResult addMedico(Medico medico)
        {
            string resultado;
            resultado = _medicoService.AddMedico(medico);
            return Ok(resultado);
        }

        /// <summary>
        /// metodo para actualizar medicos
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateMedico")]
        public IActionResult updateMedico(Medico medico)
        {
            string resultado;
            resultado = _medicoService.UpdateMedico(medico);
            return Ok(resultado);
        }


        /// <summary>
        /// metodo para eliminar medicos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteMedico")]
        public IActionResult deleteMedico(int Id)
        {
            string resultado;
            resultado = _medicoService.DeleteMedico(Id);
            return Ok(resultado);
        }
    }
}
