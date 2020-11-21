using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Estado
    {
        public Estado(string ambito, string nombre)
        {
            this.ambito = ambito;
            this.nombre = nombre;
        }
        
        public string ambito { get; set; }
        public string nombre { get; set; }

        public bool esAmbitoPedido()
        {
            return ambito == "Pedido";
        }
    }
}
