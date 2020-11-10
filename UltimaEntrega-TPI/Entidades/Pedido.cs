using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimaEntrega_TPI.Entidades
{
    public class Pedido
    {
        public int cantComensales { get; set; }
        public DateTime fechaHoraPed { get; set; }
        public int nroPedido { get; set; }
        public HistorialEstado[] historial {get;set;}
    }
}
