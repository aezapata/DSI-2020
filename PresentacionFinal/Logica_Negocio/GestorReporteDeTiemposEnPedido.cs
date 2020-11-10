using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
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

        List<int> tiemposAbierto = new List<int>();
        List<int> tiemposCobrado = new List<int>();
        List<int> tiemposFacturado = new List<int>();

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

            for (int i=0; i < 5; i++)
            {
                HistorialEstado historialX = new HistorialEstado();

                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado(); historialX.estado.nombre = "Abierto";
                histoPedido[0] = historialX;
                tiemposAbierto.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado(); historialX.estado.nombre = "Cobrado";
                tiemposCobrado.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

                histoPedido[1] = historialX;
                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado(); historialX.estado.nombre = "Facturado";
                tiemposFacturado.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

                histoPedido[2] = historialX;

                Pedido pedidoX = new Pedido(); pedidoX.cantComensales = 4; pedidoX.fechaHoraPed = histoPedido[0].fechaHoraInicio; pedidoX.nroPedido = i; pedidoX.historial = histoPedido;
                mispedidos[i] = pedidoX;
            }
            pedidos = mispedidos;

        }
        public Object GenerarReporte()
        {
            HardCore();

            Object[] tiemposCalculados = new object[estadosSelec.Length*3];
            tiemposCalculados[0]= tiemposAbierto.ToArray().Max();
            tiemposCalculados[1] = tiemposAbierto.ToArray().Min();
            tiemposCalculados[2] = ((int)tiemposCalculados[0] + (int)tiemposCalculados[1]) / 2;

            IConstructor constructor = new ConstructorReportePDF();
            Director director = new Director(constructor);
            director.Construir(titulo, fechaHoraDesde, fechaHoraHasta, estadosSelec, pisosSelecc, sectoresSelecc,
                opcTotalizar, pedidos, tiemposCalculados, nombreUsuarioLog, fechaHoraActual);

            var formaVisualizacion = constructor.ObtenerProducto();
            var pdf = formaVisualizacion.VisualizarReporteGenerado();
            return pdf;
        }

        public void TomarFiltros(DateTime fechaIni, DateTime fechaFin)
        {
            fechaHoraDesde = fechaIni;
            fechaHoraHasta = fechaFin;
        }

    }
}
