using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimaEntrega_TPI.Entidades
{
    public class HistorialEstado
    {
        public DateTime fechaHoraFin { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public Estado estado { get; set; }
    }
}
