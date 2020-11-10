using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimaEntrega_TPI.Entidades
{
    public class Usuario
    {
        string contraseña { get; set; }
        DateTime fechaBaja { get; set; }
        DateTime fechaCreacion { get; set; }
        string nombre { get; set; }
    }
}
