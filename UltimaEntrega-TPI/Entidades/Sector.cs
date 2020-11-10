using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimaEntrega_TPI.Entidades
{
    public class Sector
    {
        int ancho { get; set; }
        int largo { get; set; }
        string nombre { get; set; }
        int[] ubicacionPuerta { get; set; }
        int[] ubicacionVentana { get; set; }
        Seccion[] seccion { get; set; }
    }
}
