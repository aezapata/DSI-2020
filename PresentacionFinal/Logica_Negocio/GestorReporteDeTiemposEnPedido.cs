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
        private List<object> sectoresConDuracion;

        public GestorReporteDeTiemposEnPedido()
        {
            HardCore();
            this.buscarEstadosPedido();
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
                var sectoresPisoSeleccionado = pisos.First(piso => piso.numero.ToString() == pisoSeleccionado).sectores;
                sectoresFiltrados = sectoresFiltrados.Concat(sectoresPisoSeleccionado.ToList()).ToList();
            }
            return sectoresFiltrados;
        }
        private void HardCore()
        {
            //Hardcodeo de atributos
            //string[] estadosSel = { "Abierto", "Cancelado", "Cobrado", "Facturado" };
            Estado EstadoAbierto = new Estado("Pedido", "Abierto");
            estados.Add(EstadoAbierto);
            Estado EstadoCancelado = new Estado("Pedido", "Cancelado");
            estados.Add(EstadoCancelado);
            Estado EstadoCobrado = new Estado("Pedido", "Cobrado");
            estados.Add(EstadoCobrado);
            Estado EstadoFacturado = new Estado("Pedido", "Facturado");
            estados.Add(EstadoFacturado);

            this.sesionActual = new Sesion(FechasAleatorias.GetAleatorio(), FechasAleatorias.GetAleatorio(), new Usuario("Guadalupe"));


            titulo = "Reporte de Tiempos en pedidos";

            //fechaHoraDesde = DateTime.Parse("13/09/2020");
            //fechaHoraHasta = DateTime.Now;

            
            HistorialEstado hp1 = new HistorialEstado(new DateTime(2020,11,10,10,30,15), new DateTime(2020, 11, 10, 10, 40, 15), EstadoAbierto);
            HistorialEstado hp2 = new HistorialEstado(new DateTime(2020, 11, 10, 10, 40, 15), new DateTime(2020, 11, 10, 10, 41, 15), EstadoCancelado);
            List<HistorialEstado> he1 = new List<HistorialEstado>();
            he1.Add(hp1);
            he1.Add(hp2);

            Pedido pedido1 = new Pedido(3, new DateTime(2020, 11, 10, 10, 30, 15), 1, he1);


            HistorialEstado hp21 = new HistorialEstado(new DateTime(2020, 11, 9, 10, 30, 15), new DateTime(2020, 11, 9, 10, 40, 15), EstadoAbierto);
            HistorialEstado hp22 = new HistorialEstado(new DateTime(2020, 11, 9, 10, 40, 15), new DateTime(2020, 11, 9, 10, 45, 15), EstadoCobrado);
            List<HistorialEstado> he2 = new List<HistorialEstado>();
            he2.Add(hp21);
            he2.Add(hp22);

            Pedido pedido2 = new Pedido(2, new DateTime(2020, 11, 10, 10, 30, 15), 1, he2);


            HistorialEstado hp31 = new HistorialEstado(new DateTime(2020, 11, 23, 10, 30, 15), new DateTime(2020, 11, 23, 10, 40, 15), EstadoAbierto);
            HistorialEstado hp32 = new HistorialEstado(new DateTime(2020, 11, 23, 10, 40, 15), new DateTime(2020, 11, 23, 10, 45, 15), EstadoCobrado);
            HistorialEstado hp33 = new HistorialEstado(new DateTime(2020, 11, 23, 10, 46, 15), new DateTime(2020, 11, 23, 10, 54, 15), EstadoFacturado);
            List<HistorialEstado> he3 = new List<HistorialEstado>();
            he3.Add(hp31);
            he3.Add(hp32);
            he3.Add(hp33);

            Pedido pedido3 = new Pedido(2, new DateTime(2020, 11, 23, 10, 30, 15), 1, he3);

            List<Pedido> pedidosMesa1 = new List<Pedido>();
            pedidosMesa1.Add(pedido1);
            pedidosMesa1.Add(pedido2);

            List<Pedido> pedidosMesa2 = new List<Pedido>();
            pedidosMesa2.Add(pedido3);

            Seccion seccionBar = new Seccion("principal",new Mesa(1, pedidosMesa1));
            Seccion seccionBar2 = new Seccion("principal2", new Mesa(2, pedidosMesa2));

            List<Seccion> seccionesBar = new List<Seccion>();

            seccionesBar.Add(seccionBar);
            seccionesBar.Add(seccionBar2);

            Sector sectorBar =  new Sector("Bar", seccionesBar);
            Sector sectorAdultosMayores = new Sector("Adultos Mayores", null);
            Sector sectorVereda = new Sector("Vereda", null);
            Sector sectorPatio = new Sector("Patio", null);

            List<Sector> sectoresPiso1 = new List<Sector>();

            sectoresPiso1.Add(sectorBar);
            sectoresPiso1.Add(sectorAdultosMayores);
            sectoresPiso1.Add(sectorVereda);
            sectoresPiso1.Add(sectorPatio);

            this.pisos.Add(new Piso(0, sectoresPiso1));


            /////////////////////////


            Sector sectorHall = new Sector("hall", seccionesBar);
            Sector sectorComedor = new Sector("Comedor", null);
           

            List<Sector> sectoresPiso2 = new List<Sector>();

            sectoresPiso2.Add(sectorHall);
            sectoresPiso2.Add(sectorComedor);

            this.pisos.Add(new Piso(1, sectoresPiso2));

            /////////////////////////

            Sector sectorTerraza = new Sector("Terraza", seccionesBar);
            Sector sectorFumadores = new Sector("Fumadores", null);
            Sector sectorNinios = new Sector("Niños", null);


            List<Sector> sectoresPiso3 = new List<Sector>();

            sectoresPiso3.Add(sectorTerraza);
            sectoresPiso3.Add(sectorFumadores);
            sectoresPiso3.Add(sectorNinios);



            this.pisos.Add(new Piso(2, sectoresPiso3));

            //string[] pisosSel = { "PlantaBaja", "1erPsio", "2doPiso" };
            //pisosSelecc = pisosSel;
            //string[] sectoresSel = { "Bar", "Comedor", "Vereda", "Patio" };
            //sectoresSelecc = sectoresSel;

            nombreUsuarioLog = "Lucas";
            fechaHoraActual = DateTime.Now;

            //Creacion de Pedidos con sus historiales aleatorios
            HistorialEstado[] histoPedido = new HistorialEstado[3];
            Pedido[] mispedidos = new Pedido[5];

            //for (int i = 0; i < 5; i++)
            //{
            //    HistorialEstado historialX = new HistorialEstado();

            //    historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
            //    historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
            //    historialX.estado = new Estado("Pedido", "Abierto");
            //    histoPedido[0] = historialX;
            //    tiemposAbierto.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

            //    historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
            //    historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
            //    historialX.estado = new Estado("Pedido", "Cobrado");
            //    tiemposCobrado.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

            //    histoPedido[1] = historialX;
            //    historialX.fechaHoraFin = FechasAleatorias.GetAleatorio();
            //    historialX.fechaHoraInicio = FechasAleatorias.GetAleatorio(historialX.fechaHoraFin);
            //    historialX.estado = new Estado("Pedido", "Facturado");
            //    tiemposFacturado.Add((int)(historialX.fechaHoraFin - historialX.fechaHoraInicio).TotalHours);

            //    histoPedido[2] = historialX;

            //    Pedido pedidoX = new Pedido(); pedidoX.cantComensales = 4; pedidoX.fechaHoraPed = histoPedido[0].fechaHoraInicio; pedidoX.nroPedido = i; pedidoX.historial = histoPedido;
            //    mispedidos[i] = pedidoX;
            //}

            //tiemposCalculados = new object[4 * 3];
            //tiemposCalculados[0] = tiemposAbierto.ToArray().Max();
            //tiemposCalculados[1] = tiemposAbierto.ToArray().Min();
            //tiemposCalculados[2] = ((int)tiemposCalculados[0] + (int)tiemposCalculados[1]) / 2;


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
            IConstructor constructor = new ConstructorReportePDF();

            Director director = new Director(constructor);

            director.Construir(titulo, fechaHoraDesde, fechaHoraHasta, estadosSeleccionados, pisosSeleccionados, sectoresSeleccionados,
                opcTotalizar, pedidos, sectoresConDuracion, nombreUsuarioLog, fechaHoraActual);

            var formaVisualizacion = constructor.ObtenerProducto();
            var pdf = formaVisualizacion.VisualizarReporteGenerado();
            return true;
        }

        public bool tomarConfirmacion(List<string> estadosSeleccionados, List<string> pisosSeleccionados, List<string> sectoresSeleccionados)
        {
            this.TomarFechaHoraActual();

            if (!this.ValidarPeriodo())
            {
                return false;
            }

            this.buscarPedidosCumplenFiltros(estadosSeleccionados, pisosSeleccionados,  sectoresSeleccionados);

            if (this.NuevoReporteTiemposP(estadosSeleccionados, pisosSeleccionados, sectoresSeleccionados))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buscarPedidosCumplenFiltros( List<string> estadosSeleccionados, List<string> pisosSeleccionados, List<string> sectoresSeleccionados)
        {
            List<Piso> pisosSeleccionadosObjetos = new List<Piso>();
            
            foreach (var piso in pisosSeleccionados)
            {
                Piso pisoSeleccionado = this.pisos.First(x => x.numero.ToString() == piso);

                if (pisoSeleccionado != null )
                {
                    pisosSeleccionadosObjetos.Add(pisoSeleccionado);
                }
            }

            List<Object> duracionesPorEstado = new List<object>();

            foreach (var pisoSel in pisosSeleccionadosObjetos)
            {
                var sectorConDuraciones = pisoSel.buscarPedidosCumplenFiltros(estadosSeleccionados, sectoresSeleccionados, this.fechaHoraDesde, this.fechaHoraHasta);
                duracionesPorEstado = duracionesPorEstado.Concat(sectorConDuraciones).ToList();
            }

            this.sectoresConDuracion = duracionesPorEstado;

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
