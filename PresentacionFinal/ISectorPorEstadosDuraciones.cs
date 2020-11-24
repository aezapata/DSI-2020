using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class SectorPorEstadosDuraciones
    {
        public string sector;
        public List<EstadosDuraciones> estadoDuraciones;
    }
    public class EstadosDuraciones
    {
        public string estado;
        public List<double> duraciones;
        public int contEstado;

        public double promedio;
        public double duracionMax;
        public double duracionMin;
    }
}
