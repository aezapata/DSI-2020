using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Piso
    {
        public Piso(int numero, List<Sector> sector)
        {
            this.numero = numero;
            this.sectores = sector;
        }

        public int numero { get; set; }
        public List<Sector> sectores { get; set; }

        public List<SectorPorEstadosDuraciones> buscarPedidosCumplenFiltros(List<string> estadosSeleccionados, List<string> sectoresSeleccionados, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Sector> sectoresFiltrados = new List<Sector>();
            
            foreach (var sector in sectoresSeleccionados)
            {
                Sector sectorSeleccionado = this.sectores.First(x => x.nombre == sector);
                
                if (sectorSeleccionado != null)
                {
                    sectoresFiltrados.Add(sectorSeleccionado);
                }
            }

            List<SectorPorEstadosDuraciones> estadosDuracionFiltrado = new List<SectorPorEstadosDuraciones>();

            foreach (var sector in sectoresFiltrados)
            {
                SectorPorEstadosDuraciones sectorConInfo = sector.buscarPedCumpFiltros(estadosSeleccionados, fechaDesde, fechaHasta);

                if (sectorConInfo != null )
                {
                    estadosDuracionFiltrado.Add(sectorConInfo);
                }
            }

            foreach (var sector in estadosDuracionFiltrado)
            {
                foreach (var estado in sector.estadoDuraciones)
                {
                    double duraciones = 0;
                    estado.duraciones.ForEach(x => duraciones += x);
                    estado.promedio = duraciones / estado.contEstado;
                    estado.duracionMax = estado.duraciones.OrderByDescending(x => x).First();
                    estado.duracionMin = estado.duraciones.OrderBy(x => x).First();
                }
            }

            return estadosDuracionFiltrado;
        }


    }
}
