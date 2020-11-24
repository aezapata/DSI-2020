using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Sector
    {
        public Sector(string nombre, List<Seccion> seccion)
        {
            this.nombre = nombre;
            this.secciones = seccion;
        }

        int ancho { get; set; }
        int largo { get; set; }
        public string nombre { get; set; }
        int[] ubicacionPuerta { get; set; }
        int[] ubicacionVentana { get; set; }
        List<Seccion> secciones { get; set; }

        public SectorPorEstadosDuraciones buscarPedCumpFiltros(List<string> estadosSeleccionados, DateTime fechaDesde, DateTime fechaHasta)
        {
            
            List<EstadosDuraciones> estadosDuracionSector = new List<EstadosDuraciones>();

            foreach (var seccion in secciones)
            {
                // Busca estados y duracion de cada mesa
                List<EstadosDuraciones> estadosConPromedioSeccion = seccion.buscarPedCumpFiltros(estadosSeleccionados, fechaDesde, fechaHasta);
                
                if (estadosConPromedioSeccion != null )
                {
                    estadosDuracionSector = estadosDuracionSector.Concat(estadosConPromedioSeccion).ToList();
                }

            }

            var estadosDuplicados = estadosDuracionSector.GroupBy(i => i.estado).Where(g => g.Count() > 1).Select(g => g.Key);

            foreach (var estado in estadosDuplicados)
            {
                var estados = estadosDuracionSector.Where(x => x.estado == estado).ToList();
                List<double> duraciones = new List<double>();
                int contEstados = 1;
                foreach (var item in estados)
                {
                    duraciones = duraciones.Concat(item.duraciones).ToList();
                    contEstados += item.contEstado;
                }

                estadosDuracionSector.RemoveAll(x => x.estado == estado);
                estadosDuracionSector.Add(new EstadosDuraciones { estado = estado, duraciones = duraciones, contEstado = contEstados });
            }


            return new SectorPorEstadosDuraciones{ sector = this.nombre, estadoDuraciones = estadosDuracionSector };
        }


    }
}
