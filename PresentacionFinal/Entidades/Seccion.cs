using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Seccion
    {
        public Seccion(string nombre, Mesa mesa)
        {
            this.nombre = nombre;
            this.mesa = mesa;
        }

        int coordenadaX { get; set; }
        int coordenadaY { get; set; }
        int dimension { get; set; }
        string nombre { get; set; }
        Mesa mesa    { get; set; }

        public List<object> buscarPedCumpFiltros(List<string> estadosSeleccionados, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<object> estadosConDuracion = new List<object>();

            if(mesa != null)
            {
                estadosConDuracion = mesa.buscarPedCumplFiltros(estadosSeleccionados, fechaDesde, fechaHasta);
            }

            return estadosConDuracion;
        }
    }
}
