using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Piso
    {
        public Piso(int numero, Sector[] sector)
        {
            this.numero = numero;
            this.sector = sector;
        }

        public int numero { get; set; }
        public Sector[] sector { get; set; }
    }
}
