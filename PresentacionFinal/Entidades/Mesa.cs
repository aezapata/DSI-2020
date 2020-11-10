using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Mesa
    {
        int capacidadComensales { get; set; }
        int espacioQueOcupa { get; set; }
        int filaEnPlano { get; set; }
        int numero { get; set; }
        int ordenEnPlano { get; set; }
        Pedido[] pedidos { get; set; }
        UnionDeMesa union { get; set; }
    }
}
