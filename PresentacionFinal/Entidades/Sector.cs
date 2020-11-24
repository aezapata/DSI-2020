using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Sector
    {
        public Sector(string nombre, List<Seccion> seccion)
        {
            this.nombre = nombre;
            this.secciones = seccion;
        }

        int ancho { get; set; }
        int largo { get; set; }
        public string nombre { get; set; }
        int[] ubicacionPuerta { get; set; }
        int[] ubicacionVentana { get; set; }
        List<Seccion> secciones { get; set; }

        public List<object> buscarPedCumpFiltros(List<string> estadosSeleccionados, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<object> estadosDuracionSector = new List<object>();

            foreach (var seccion in secciones)
            {

                // Busca estados y duracion de cada mesa
                List<object> estadosConPromedioSeccion = seccion.buscarPedCumpFiltros(estadosSeleccionados, fechaDesde, fechaHasta);
                
                if (estadosConPromedioSeccion != null )
                {
                    estadosDuracionSector = estadosDuracionSector.Concat(estadosConPromedioSeccion).ToList();
                }

            }

            estadosDuracionSector.Add(new { sector = this.nombre, promedios = estadosDuracionSector });

            return estadosDuracionSector;
        }


    }
}
