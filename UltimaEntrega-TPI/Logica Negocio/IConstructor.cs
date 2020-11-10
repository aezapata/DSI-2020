using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using UltimaEntrega_TPI.Entidades;

namespace UltimaEntrega_TPI.Logica_Negocio
{
    public interface IConstructor
    {
        public abstract int CalcularNumeroDePag();


        public abstract void ConstruirCuerpo(string[] estados, string[] sectores, object[] calculoReporte);
        public abstract void ConstruirEncabezado(string titulo, DateTime fechaDesde, DateTime fechaHasta);

        public abstract void ConstruirPieDePagina(string usuario, DateTime fechaHoraActual);

        public abstract void ConstruirProducto();

        public abstract IFormasVisualizacion ObtenerProducto();
    }
}
