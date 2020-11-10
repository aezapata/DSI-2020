using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Sesion
    {
        DateTime fechaFin { get; set; }
        DateTime fechaInicio { get; set; }
        DateTime horaFin { get; set; }
        DateTime horaInicio { get; set; }
        Usuario usuario { get; set; }
    }
}
