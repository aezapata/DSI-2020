using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltimaEntrega_TPI.Logica_Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UltimaEntrega_TPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestorController : ControllerBase
    {
        GestorReporteDeTiemposEnPedido gestor = new GestorReporteDeTiemposEnPedido();

        // GET: api/<GestorController>
        [HttpGet]
        public Object VisualizarReporte()
        {
            return gestor.GenerarReporte();
        }

        
        // POST api/<GestorController>
        [HttpPost]
        public void Post([FromBody] Object value)
        {
            gestor.TomarFormaVis(value);
        }
    }
}
