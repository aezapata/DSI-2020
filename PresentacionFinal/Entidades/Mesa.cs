using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class Mesa
    {
        public Mesa(int numero, List<Pedido> pedidos)
        {
            this.numero = numero;
            this.pedidos = pedidos;
        }

        int capacidadComensales { get; set; }
        int espacioQueOcupa { get; set; }
        int filaEnPlano { get; set; }
        int numero { get; set; }
        int ordenEnPlano { get; set; }
        List<Pedido> pedidos { get; set; }
        UnionDeMesa union { get; set; }

        public List<EstadosDuraciones> buscarPedCumplFiltros(List<string> estadosSeleccionados, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<EstadosDuraciones> estadoDuracion = new List<EstadosDuraciones>();
            
            var pedidos = this.pedidos.Where(x => x.fechaHoraPed >= fechaDesde && x.fechaHoraPed <= fechaHasta).ToList();

            foreach (var estado in estadosSeleccionados)
            {
                double promedioEstado;
                // Pedidos con periodo seleccionado
                int contadorEstado = 0;
                // double duracion = 0;
                List<double> duraciones = new List<double>();

                foreach (var pedidoFil in pedidos)
                {
                    var result = pedidoFil.estuvoEnEstadoSeleccionado(estado);

                    if (result != null)
                    {
                        contadorEstado++;
                        duraciones.Add(result.calcularDuracionEnEstado(estado));
                     //   duracion += result.calcularDuracionEnEstado(estado);
                    }
                }

                if (contadorEstado == 0)
                {
                    continue;
                }

                // promedioEstado = duracion / contadorEstado;
                
                if(duraciones.Count != 0)
                {
                    estadoDuracion.Add( new EstadosDuraciones { estado = estado, duraciones = duraciones, contEstado = contadorEstado });
                }

            }

            return estadoDuracion;
        }
    }
}
