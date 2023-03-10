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
    public class SalaController : Controller
    {
        private readonly SalaService _salaService;

        public SalaController(SalaService salaService)
        {
            _salaService = salaService;
        }

        /// <summary>
        /// metodo para listar Sala
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listSala")]
        public IActionResult listSala([FromQuery] int opcion, int id, string nombre = "")
        {

            List<Sala> salaList = new List<Sala>();
            salaList = _salaService.GetAll(opcion, id, nombre);
            return Ok(salaList);
        }

        /// <summary>
        /// metodo para agregar Sala
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addSala")]
        public IActionResult addSala(Sala sala)
        {
            string resultado;
            resultado = _salaService.AddSala(sala);
            return Ok(resultado);
        }

        /// <summary>
        /// metodo para actualizar Sala
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateSala")]
        public IActionResult updateSala(Sala sala)
        {
            string resultado;
            resultado = _salaService.UpdateSala(sala);
            return Ok(resultado);
        }

        /// <summary>
        /// metodo para eliminar Sala
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteSala")]
        public IActionResult deleteSala(int Id)
        {
            string resultado;
            resultado = _salaService.DeleteSala(Id);
            return Ok(resultado);
        }
    }
}
