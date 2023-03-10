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
    public class CitasController : Controller
    {
        private readonly CitasService _citasService;

        public CitasController(CitasService citasService)
        {
            _citasService = citasService;
        }

        [HttpGet]
        [Route("listCita")]
        public IActionResult listCita([FromQuery] int opcion, int id, string estado = "")
        {

            List<Citas> citasList = new List<Citas>();
            citasList = _citasService.GetAll(opcion, id, estado);
            return Ok(citasList);
        }

        [HttpGet]
        [Route("listCitaPaciente")]
        public IActionResult listCitaPaciente([FromQuery]  string Identificacion, int Id_centro_atencion = 0)
        {

            List<CitasPaciente> citaspacienteList = new List<CitasPaciente>();
            citaspacienteList = _citasService.GetAllCitasPaciente(Identificacion, Id_centro_atencion);
            return Ok(citaspacienteList);
        }


        [HttpPost]
        [Route("addCita")]
        public IActionResult addCita(Citas citas)
        {
            string resultado;
            resultado = _citasService.AddCitas(citas);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("updateCita")]
        public IActionResult updateCita(Citas citas)
        {
            string resultado;
            resultado = _citasService.UpdateCitas(citas);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("cancelCita")]
        public IActionResult cancelCita(int Id)
        {
            string resultado;
            resultado = _citasService.CancelCitas(Id);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("deleteCita")]
        public IActionResult deleteCita(int Id)
        {
            string resultado;
            resultado = _citasService.DeleteCitas(Id);
            return Ok(resultado);
        }
    }
}
