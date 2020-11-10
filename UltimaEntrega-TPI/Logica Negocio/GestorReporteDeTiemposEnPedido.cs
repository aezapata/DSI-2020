using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimaEntrega_TPI.Entidades;

namespace UltimaEntrega_TPI.Logica_Negocio
{
    public class GestorReporteDeTiemposEnPedido
    {
        Estado[] estados { get; set; }
        Estado[] estadosSeleccionados { get; set; }
        string[] estadosSelec { get; set; }
        DateTime fechaHoraActual { get; set; }
        DateTime fechaHoraDesde { get; set; }
        DateTime fechaHoraHasta { get; set; }
        public string nombreUsuarioLog { get; set; }
        public string titulo { get; set; }
        bool opcTotalizar { get; set; }
        Sector[] sectores { get; set; }
        Sector[] sectoresSeleccionados { get; set; }
        string[] sectoresSelecc { get; set; }
        int[] tiemposPerMax { get; set; }
        int[] tiemposPerMin { get; set; }
        int[] tiemposPerProm { get; set; }
        Piso[] pisos { get; set; }
        Piso[] pisosSeleccionados { get; set; }
        string[] pisosSelecc { get; set; }

        Pedido[] pedidos { get; set; }
        enum FormasVis {pdf, porPantalla, excel}
        FormasVis formaVis;
        
        public void TomarFormaVis(IConstructor constructor)
        {
            formaVis = FormasVis.pdf;
            fechaHoraActual = DateTime.Now;
        }

        private void HardCore()
        {
            //Hardcodeo de atributos
            string[] estadosSel = {"Abierto", "Cancelado", "Cobrado", "Facturado" };
            estadosSelec = estadosSel;
            titulo = "Reporte de Tiempos en pedidos";
            fechaHoraDesde = DateTime.Parse("13/09/2020");
            fechaHoraHasta = DateTime.Now;
            string[] pisosSel = { "PlantaBaja", "1erPsio", "2doPiso"};
            pisosSelecc = pisosSel;
            string[] sectoresSel = { "Bar", "Comedor", "Vereda", "Patio" };
            sectoresSelecc = sectoresSel;
            opcTotalizar = true;
            nombreUsuarioLog = "Lucas";
            fechaHoraActual = DateTime.Now;

            //Creacion de Pedidos con sus historiales aleatorios
            HistorialEstado[] histoPedido = new HistorialEstado[3];
            Pedido[] mispedidos = new Pedido[5];
            for(int i=0; i < 5; i++)
            {
                HistorialEstado historialX = new HistorialEstado();

                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado(); historialX.estado.nombre = "Abierto";
                histoPedido[0] = historialX;

                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado(); historialX.estado.nombre = "Cobrado";

                histoPedido[1] = historialX;
                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado(); historialX.estado.nombre = "Facturado";

                histoPedido[2] = historialX;

                Pedido pedidoX = new Pedido(); pedidoX.cantComensales = 4; pedidoX.fechaHoraPed = FechasAleatorias.GetAleatorio(); pedidoX.nroPedido = i; pedidoX.historial = histoPedido;
                mispedidos[i] = pedidoX;
            }
            pedidos = mispedidos;

        }
        public Object GenerarReporte()
        {
            IConstructor constructor = new ConstructorReportePDF();
            Director director = new Director(constructor);
            director.Construir(titulo, fechaHoraDesde, fechaHoraHasta, estadosSelec, pisosSelecc, sectoresSelecc,
                opcTotalizar, pedidos, null, nombreUsuarioLog, fechaHoraActual);

            var formaVisualizacion = constructor.ObtenerProducto();
            var pdf = formaVisualizacion.VisualizarReporteGenerado();
            return pdf;
        }

    }
}
