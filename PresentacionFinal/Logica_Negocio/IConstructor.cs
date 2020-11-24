using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PresentacionFinal;

namespace PresentacionFinal
{
    public interface IConstructor
    {
        public abstract int CalcularNumeroDePag();


        public abstract void ConstruirCuerpo(string[] estados, string[] sectores, List<object> calculoReporte);
        public abstract void ConstruirEncabezado(string titulo, DateTime fechaDesde, DateTime fechaHasta);

        public abstract void ConstruirPieDePagina(string usuario, DateTime fechaHoraActual);

        public abstract void ConstruirProducto();

        public abstract IFormasVisualizacion ObtenerProducto();
    }
}
