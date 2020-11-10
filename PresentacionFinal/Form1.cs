using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GestorReporteDeTiemposEnPedido gestor = new GestorReporteDeTiemposEnPedido();
            gestor.TomarFiltros(dateTimePicker1.Value, dateTimePicker2.Value);
            gestor.GenerarReporte();
            MessageBox.Show("PDF Creado con exito ;)", "Creacion de PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
