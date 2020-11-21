using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Sector
    {
        public Sector(string nombre, Seccion[] seccion)
        {
            this.nombre = nombre;
            this.seccion = seccion;
        }

        int ancho { get; set; }
        int largo { get; set; }
        public string nombre { get; set; }
        int[] ubicacionPuerta { get; set; }
        int[] ubicacionVentana { get; set; }
        Seccion[] seccion { get; set; }
    }
}
