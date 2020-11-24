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

        public List<Object> buscarPedidosCumplenFiltros(List<string> estadosSeleccionados, List<string> sectoresSeleccionados, DateTime fechaDesde, DateTime fechaHasta)
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

            List<Object> estadosDuracionFiltrado = new List<Object>();

            foreach (var sector in sectoresFiltrados)
            {
                List<object> sectorConInfo = sector.buscarPedCumpFiltros(estadosSeleccionados, fechaDesde, fechaHasta);

                if (sectorConInfo != null )
                {
                    estadosDuracionFiltrado = estadosDuracionFiltrado.Concat(sectorConInfo).ToList();
                }
            }

            

            return estadosDuracionFiltrado;
        }


    }
}
