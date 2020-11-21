using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Sesion
    {
        public Sesion(DateTime fechaInicio, DateTime horaInicio, Usuario usuario)
        {
            this.fechaInicio = fechaInicio;
            this.horaInicio = horaInicio;
            this.usuario = usuario;
        }

        DateTime fechaFin { get; set; }
        DateTime fechaInicio { get; set; }
        DateTime horaFin { get; set; }
        DateTime horaInicio { get; set; }
        Usuario usuario { get; set; }
    }
}
