using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PresentacionFinal;

namespace PresentacionFinal
{
    public class Director
    {
        IConstructor constructor;
        public void Construir(string titulo, DateTime fechaDesde, DateTime fechaHasta,
            List<string> estados, List<string> pisos, List<string> sectores, int totalizarPor,
            Pedido[] pedidos, List<SectorPorEstadosDuraciones> calculosReporte, string usuario,
            DateTime fechaHoraActual)
        {
            constructor.ConstruirProducto();
            constructor.ConstruirEncabezado(titulo, fechaDesde, fechaHasta);
            constructor.ConstruirCuerpo(estados.ToArray(), sectores.ToArray(), calculosReporte);
            constructor.ConstruirPieDePagina(usuario, fechaHoraActual);

        }

        public Director(IConstructor miconstructor)
        {
            constructor = miconstructor;
        }

        
    }
}
