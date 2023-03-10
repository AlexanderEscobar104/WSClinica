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
    
    public class CentroAtencionController : Controller
    {
       
        private readonly CentroAtencionService _centroAtencionService;

        public CentroAtencionController(CentroAtencionService centroAtencionService)
        {
            _centroAtencionService = centroAtencionService;
        }

        /// <summary>
        /// metodo para listar los centros de atencion
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listCentroAtencion")]
        public IActionResult listCentroAtencion([FromQuery] int opcion, int id, string nombre = "")
        {

            List<CentroAtencion> centroAtencionList = new List<CentroAtencion>();
            centroAtencionList = _centroAtencionService.GetAll(opcion, id, nombre);
            return Ok(centroAtencionList);
        }

        /// <summary>
        /// metodo para agregar centro de atencion
        /// </summary>
        /// <param name="centroAtencion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addCentroAtencion")]
        public IActionResult addCentroAtencion(CentroAtencion centroAtencion)
        {
            string resultado;
            resultado = _centroAtencionService.AddCentroAtencion(centroAtencion);
            return Ok(resultado);
        }


        /// <summary>
        /// metodo para actualizar centro de atencion
        /// </summary>
        /// <param name="centroAtencion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateCentroAtencion")]
        public IActionResult updateCentroAtencion(CentroAtencion centroAtencion)
        {
            string resultado;
            resultado = _centroAtencionService.UpdateCentroAtencion(centroAtencion);
            return Ok(resultado);
        }


        /// <summary>
        /// metodo para eliminar centro de atencion
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteCentroAtencion")]
        public IActionResult deleteCentroAtencion(int Id)
        {
            string resultado;
            resultado = _centroAtencionService.DeleteCentroAtencion(Id);
            return Ok(resultado);
        }


    }
}
