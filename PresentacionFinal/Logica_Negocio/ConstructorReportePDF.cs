using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class ConstructorReportePDF: IConstructor
    {
        VistaPDF vistaPDF;

public int CalcularNumeroDePag()
        {
            throw new NotImplementedException();
        }

        public void ConstruirCuerpo(string[] estados, string[] sectores, List<SectorPorEstadosDuraciones> calculoReporte)
        {
            for (int i=0; i < sectores.Length; i++)
            {
                if (i==0)
                    vistaPDF.IniciarFila();
                if (i < 1)
                {
                    //vistaPDF.reporteCuerpo = new string[sectores.Length][];
                    //vistaPDF.AgregarFila(sectores[i], estados[i], calculoReporte[i], calculoReporte[i]);
                }
                //else Agregar una nueva pagina.
            }
            foreach (var sector in calculoReporte)
            {
                foreach (var estado in sector.estadoDuraciones)
                {
                    vistaPDF.AgregarFila(sector.sector, estado.estado, estado.duracionMax, estado.duracionMin, estado.promedio);
                }
            }
        }

        public void ConstruirEncabezado(string titulo, DateTime fechaDesde, DateTime fechaHasta)
        {
            //vistaPDF.reporteEncabezado = titulo + fechaDesde.ToString() + fechaHasta.ToString();
            vistaPDF.AgregarEncabezado(titulo, fechaDesde, fechaHasta);
        }

        public void ConstruirPieDePagina(string usuario, DateTime fechaHoraActual)
        {
             vistaPDF.SetPiePagina(usuario, fechaHoraActual);

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
