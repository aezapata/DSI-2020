using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Pedido
    {
        public Pedido(int cantComensales, DateTime fechaHoraPed, int nroPedido, List<HistorialEstado> historial)
        {
            this.cantComensales = cantComensales;
            this.fechaHoraPed = fechaHoraPed;
            this.nroPedido = nroPedido;
            this.historial = historial;
        }

        public int cantComensales { get; set; }
        public DateTime fechaHoraPed { get; set; }
        public int nroPedido { get; set; }
        public List<HistorialEstado> historial {get;set;}

        public Pedido estuvoEnEstadoSeleccionado(string estado)
        {
            var historialExiste = historial.SingleOrDefault(x => x.esEstadoSeleccionado(estado));
            if(historialExiste != null )
            {
                return this;
            }
            return null;
        }

        public double calcularDuracionEnEstado(string estado)
        {
            var historialExiste = historial.SingleOrDefault(x => x.esEstadoSeleccionado(estado));

            double duracion = 0;

            if (historialExiste != null)
            {
               duracion = historialExiste.fechaHoraFin.Subtract(historialExiste.fechaHoraInicio).TotalHours;
            }

            return duracion;

        }
    }
}
