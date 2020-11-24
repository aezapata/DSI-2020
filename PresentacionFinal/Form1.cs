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
        private bool errorFecha;
        private List<string> estadosSeleccionados = new List<string>();
        private List<string> pisosSeleccionados = new List<string>();
        private List<string> sectoresSeleccionados = new List<string>();

        public GestorReporteDeTiemposEnPedido gestor;
        public Form1()
        {
            InitializeComponent();
            gestor = new GestorReporteDeTiemposEnPedido();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gestor.TomarPeriodo(dateTimePicker1.Value, dateTimePicker2.Value);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //gestor.TomarPeriodo(dateTimePicker1.Value, dateTimePicker2.Value);
            //gestor.NuevoReporteTiemposP();
            //MessageBox.Show("PDF Creado con exito ;)", "Creacion de PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gestor.TomarPeriodo(dateTimePicker1.Value, dateTimePicker2.Value);
            if (gestor.tomarConfirmacion(estadosSeleccionados, pisosSeleccionados, sectoresSeleccionados))
            {
                MessageBox.Show("PDF Creado con exito ;)", "Creacion de PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Se detectaron errores en los filtros", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gestor.tomarOpcParaTotalizar(2);
        }

        private void GenerarEstadosList(List<Estado> estados)
        {
            System.Windows.Forms.CheckBox box;
            for (int i = 0; i < estados.Count; i++)
            {
                box = new System.Windows.Forms.CheckBox();
                box.Tag = estados[i].nombre;
                box.Text = estados[i].nombre;
                box.AutoSize = true;
                box.Checked = true;
                box.Location = new Point(86 + (i * 100), 75); //vertical
                box.CheckedChanged += new EventHandler(estado_event_handler);
                this.estadosSeleccionados.Add(box.Tag.ToString());
                this.Controls.Add(box);
            }
        }

        private void estado_event_handler(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox element = (System.Windows.Forms.CheckBox)sender;

            if (element.Checked)
            {
                this.estadosSeleccionados.Add(element.Tag.ToString());
            }
            else
            {
                this.estadosSeleccionados.Remove(element.Tag.ToString());
            }

        }

        private void pisos_event_handler(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox element = (System.Windows.Forms.CheckBox)sender;

            if (element.Checked)
            {
                this.pisosSeleccionados.Add(element.Tag.ToString());
            }
            else
            {
                this.pisosSeleccionados.Remove(element.Tag.ToString());
            }

            this.GenerarSectoresList(gestor.mostrarPisosSeleccionados(this.pisosSeleccionados));
        }

        private void sector_event_handler(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox element = (System.Windows.Forms.CheckBox)sender;

            if (element.Checked)
            {
                this.sectoresSeleccionados.Add(element.Tag.ToString());

            }
            else
            {
                this.sectoresSeleccionados.Remove(element.Tag.ToString());
            }
        }

        private void GenerarPisosList(List<Piso> pisos)
        {
            System.Windows.Forms.CheckBox box;
            for (int i = 0; i < pisos.Count; i++)
            {
                box = new System.Windows.Forms.CheckBox();
                box.Name = "Piso1";
                box.Tag = pisos[i].numero;
                box.Text = "Piso " + pisos[i].numero;
                box.AutoSize = true;
                box.Location = new Point(86 + (i * 100), 108); //vertical
                box.Checked = true;
                box.CheckedChanged += new EventHandler(pisos_event_handler);
                pisosSeleccionados.Add(box.Tag.ToString());
                this.Controls.Add(box);
            }
        }

        private void GenerarSectoresList(List<Piso> pisos)
        {

            // ELIMINAMOS LOS GROUP BOX, PARA GENERAR NUEVOS , EN CASO DE FILTRAR
            List<GroupBox> toBeRemoved = new List<GroupBox>();

            if (pisos.Count == 0)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is GroupBox)
                    {
                        toBeRemoved.Add((GroupBox)c);
                    }
                }
            }

            foreach (var piso in pisos)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is GroupBox && c.Text != "Piso Numero - " + piso.numero)
                    {
                        toBeRemoved.Add((GroupBox) c);
                    }
                }
            }

            foreach (GroupBox c in toBeRemoved)
            {
                this.Controls.Remove(c);
            }

            sectoresSeleccionados.Clear();
            // ELIMINAMOS LOS GROUP BOX, PARA GENERAR NUEVOS , EN CASO DE FILTRAR

            int x = 136;
            System.Windows.Forms.GroupBox gb;
            foreach (var piso in pisos)
            {

                gb = new GroupBox();
                gb.Size = new Size(200, 100);
                gb.Location = new Point(x, 193);
                gb.Text = "Piso Numero - " + piso.numero;

                int y = 20;
                System.Windows.Forms.CheckBox box;
                var sector = piso.sectores.ToArray();

                for (int i = 0; i < sector.Length; i++)
                {
                    box = new System.Windows.Forms.CheckBox();
                    box.Name = "Sector";
                    box.Tag = sector[i].nombre;
                    box.Text = "Piso " + sector[i].nombre;
                    box.AutoSize = true;
                    box.Location = new Point(5, y); //vertical
                    box.Checked = true;
                    box.CheckedChanged += new EventHandler(sector_event_handler);
                    sectoresSeleccionados.Add(box.Tag.ToString());
                    gb.Controls.Add(box);
                    y += 20;
                }
                x += 200;

                this.Controls.Add(gb);

            }


        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.GenerarEstadosList(gestor.mostrarEstadosPSelecc());
            this.GenerarPisosList(gestor.mostrarPisosPSelecc());
            this.GenerarSectoresList(gestor.mostrarPisosSeleccionados(this.pisosSeleccionados));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }



    }
}
