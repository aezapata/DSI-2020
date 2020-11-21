using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Usuario
    {
        public Usuario( string nombre)
        {
            this.nombre = nombre;
        }

        string contraseña { get; set; }
        DateTime fechaBaja { get; set; }
        DateTime fechaCreacion { get; set; }
        string nombre { get; set; }
    }
}
