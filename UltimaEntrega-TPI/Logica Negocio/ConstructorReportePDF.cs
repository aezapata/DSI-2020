using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimaEntrega_TPI.Logica_Negocio
{
    public class ConstructorReportePDF:IConstructor
    {
        VistaPDF vistaPDF;

public int CalcularNumeroDePag()
        {
            throw new NotImplementedException();
        }

        public void ConstruirCuerpo(string[] estados, string[] sectores, object[] calculoReporte)
        {
            for (int i=0; i < sectores.Length; i++)
            {
                if (i==0)
                    vistaPDF.IniciarFila();
                if (i < 20)
                {
                    //vistaPDF.reporteCuerpo = new string[sectores.Length][];
                    vistaPDF.AgregarFila(sectores[i], estados[i], calculoReporte[i], calculoReporte[i+1], calculoReporte[i+2]);
                }
                //else Agregar una nueva pagina.

            }
        }

        public void ConstruirEncabezado(string titulo, DateTime fechaDesde, DateTime fechaHasta)
        {
            //vistaPDF.reporteEncabezado = titulo + fechaDesde.ToString() + fechaHasta.ToString();
            vistaPDF.AgregarEncabezado(titulo, fechaDesde, fechaHasta);
        }

        public void ConstruirPieDePagina(string usuario, DateTime fechaHoraActual)
        {
            for (int i=1; i <= vistaPDF.reporteCuerpo.Length; i++)
            {
                vistaPDF.SetPiePagina(usuario, fechaHoraActual, i);
            }

        }

        public void ConstruirProducto()
        {
            vistaPDF = new VistaPDF();
        }

        public IFormasVisualizacion ObtenerProducto()
        {
            return vistaPDF;
        }
    }
}
