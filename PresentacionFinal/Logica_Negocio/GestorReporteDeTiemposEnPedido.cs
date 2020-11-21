using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class GestorReporteDeTiemposEnPedido
    {
        List<Estado> estados = new List<Estado>();
        Sesion sesionActual { get; set; }
        // Estado[] estados { get; set; }
        Estado[] estadosSeleccionados { get; set; }

        List<String> estadosPedidos = new List<String>();

        Object[] tiemposCalculados;

        List<Piso> pisos = new List<Piso>();
        DateTime fechaHoraActual { get; set; }
        DateTime fechaHoraDesde { get; set; }
        DateTime fechaHoraHasta { get; set; }
        public string nombreUsuarioLog { get; set; }
        public string titulo { get; set; }
        int opcTotalizar { get; set; }
        Sector[] sectores { get; set; }
        Sector[] sectoresSeleccionados { get; set; }
        string[] sectoresSelecc { get; set; }
        int[] tiemposPerMax { get; set; }
        int[] tiemposPerMin { get; set; }
        int[] tiemposPerProm { get; set; }
        //Piso[] pisos { get; set; }
        Piso[] pisosSeleccionados { get; set; }
        string[] pisosSelecc { get; set; }

        Pedido[] pedidos { get; set; }
        enum FormasVis { pdf, porPantalla, excel }
        FormasVis formaVis;

        List<int> tiemposAbierto = new List<int>();
        List<int> tiemposCobrado = new List<int>();
        List<int> tiemposFacturado = new List<int>();

        public GestorReporteDeTiemposEnPedido()
        {
            HardCore();
        }

        public void TomarFormaVis(IConstructor constructor)
        {
            formaVis = FormasVis.pdf;
      
        }

        public List<Estado> mostrarEstadosPSelecc()
        {
            return estados;
        }

        public List<Piso> mostrarPisosPSelecc()
        {
            return pisos;
        }

        public List<Piso> mostrarPisosSeleccionados(List<string> pisosSeleccionados)
        {
            List<Piso> pisosFiltrados = new List<Piso>();
            foreach (var pisoSeleccionado in pisosSeleccionados)
            {
                Piso pisoEncontrado = pisos.First(piso => piso.numero.ToString() == pisoSeleccionado);
                pisosFiltrados.Add(pisoEncontrado);
            }
            return pisosFiltrados;
        }

        public List<Sector> mostrarSectoresPSelecc(List<string> pisosSeleccionados)
        {
            List<Sector> sectoresFiltrados = new List<Sector>();
            foreach (var pisoSeleccionado in pisosSeleccionados)
            {
                var sectoresPisoSeleccionado = pisos.First(piso => piso.numero.ToString() == pisoSeleccionado).sector;
                sectoresFiltrados = sectoresFiltrados.Concat(sectoresPisoSeleccionado.ToList()).ToList();
            }
            return sectoresFiltrados;
        }
        private void HardCore()
        {
            //Hardcodeo de atributos
            //string[] estadosSel = { "Abierto", "Cancelado", "Cobrado", "Facturado" };

            estados.Add(new Estado("Pedido", "Abierto"));
            estados.Add(new Estado("Pedido", "Cancelado"));
            estados.Add(new Estado("Pedido", "Cobrado"));
            estados.Add(new Estado("Pedido", "Facturado"));

            this.sesionActual = new Sesion(FechasAleatorias.GetAleatorio(), FechasAleatorias.GetAleatorio(), new Usuario("Guadalupe"));


            titulo = "Reporte de Tiempos en pedidos";

            fechaHoraDesde = DateTime.Parse("13/09/2020");
            fechaHoraHasta = DateTime.Now;

            
            this.pisos.Add(new Piso(0, new Sector[] { new Sector("Bar", null), new Sector("Adultos Mayores", null), new Sector("Vereda", null), new Sector("Patio", null) }));
            this.pisos.Add(new Piso(1, new Sector[] { new Sector("Hall", null), new Sector("Comedor", null)}));
            this.pisos.Add(new Piso(2, new Sector[] { new Sector("Terraza", null), new Sector("Fumadores", null), new Sector("Niños", null) }));

          


            string[] pisosSel = { "PlantaBaja", "1erPsio", "2doPiso" };
            pisosSelecc = pisosSel;
            string[] sectoresSel = { "Bar", "Comedor", "Vereda", "Patio" };
            sectoresSelecc = sectoresSel;

            nombreUsuarioLog = "Lucas";
            fechaHoraActual = DateTime.Now;

            //Creacion de Pedidos con sus historiales aleatorios
            HistorialEstado[] histoPedido = new HistorialEstado[3];
            Pedido[] mispedidos = new Pedido[5];

            for (int i = 0; i < 5; i++)
            {
                HistorialEstado historialX = new HistorialEstado();

                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado("Pedido", "Abierto");
                histoPedido[0] = historialX;
                tiemposAbierto.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado("Pedido", "Cobrado");
                tiemposCobrado.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

                histoPedido[1] = historialX;
                historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
                historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
                historialX.estado = new Estado("Pedido", "Facturado");
                tiemposFacturado.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

                histoPedido[2] = historialX;

                Pedido pedidoX = new Pedido(); pedidoX.cantComensales = 4; pedidoX.fechaHoraPed = histoPedido[0].fechaHoraInicio; pedidoX.nroPedido = i; pedidoX.historial = histoPedido;
                mispedidos[i] = pedidoX;
            }

            tiemposCalculados = new object[4 * 3];
            tiemposCalculados[0] = tiemposAbierto.ToArray().Max();
            tiemposCalculados[1] = tiemposAbierto.ToArray().Min();
            tiemposCalculados[2] = ((int)tiemposCalculados[0] + (int)tiemposCalculados[1]) / 2;


            pedidos = mispedidos;

        }

        public void tomarOpcParaTotalizar(int id)
        {
            this.opcTotalizar = id;
        }
        private void TomarFechaHoraActual()
        {
            fechaHoraActual = DateTime.Now;
        }
        public bool NuevoReporteTiemposP(List<string> estadosSeleccionados, List<string> pisosSeleccionados, List<string> sectoresSeleccionados)
        {
            this.TomarFechaHoraActual();

            if ( ! this.ValidarPeriodo() ) {
                return false;
            }

            this.buscarEstadosPedido();


            IConstructor constructor = new ConstructorReportePDF();

            Director director = new Director(constructor);

            director.Construir(titulo, fechaHoraDesde, fechaHoraHasta, estadosSeleccionados, pisosSeleccionados, sectoresSeleccionados,
                opcTotalizar, pedidos, tiemposCalculados, nombreUsuarioLog, fechaHoraActual);

            var formaVisualizacion = constructor.ObtenerProducto();
            var pdf = formaVisualizacion.VisualizarReporteGenerado();
            return true;
        }


        private void buscarEstadosPedido()
        {
            foreach (var estado in this.estados)
            {
                if(estado.esAmbitoPedido())
                {
                    estadosPedidos.Add(estado.nombre);
                }
            }
        }

        public void TomarPeriodo(DateTime fechaIni, DateTime fechaFin)
        {
            fechaHoraDesde = fechaIni;
            fechaHoraHasta = fechaFin;
        }

        public Boolean ValidarPeriodo()
        {
            if(fechaHoraDesde <= fechaHoraHasta && fechaHoraDesde < fechaHoraActual && fechaHoraHasta < fechaHoraActual)
            {
                return true;
            }
            return false;
        }

    }
}
