using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimaEntrega_TPI.Entidades
{
    public class UnionDeMesa
    {
        DateTime fechaHoraSeparac { get; set; }
        DateTime fechaHoraUnion { get; set; }
        Mesa[] mesas { get; set; }
    }
}
