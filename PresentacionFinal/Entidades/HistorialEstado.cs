using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class HistorialEstado
    {
        public HistorialEstado(DateTime fechaHoraInicio, DateTime fechaHoraFin , Estado estado)
        {
            this.fechaHoraFin = fechaHoraFin;
            this.fechaHoraInicio = fechaHoraInicio;
            this.estado = estado;
        }

        public DateTime fechaHoraFin { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public Estado estado { get; set; }

        public bool esEstadoSeleccionado(string estadoSel)
        {
            if(this.estado != null)
            {
                return estado.nombre == estadoSel;
            }
            return false;
        }
    }
}
