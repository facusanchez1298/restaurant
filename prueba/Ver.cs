﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba
{
    public partial class Ver : Form
    {
        public int plantilla = 1;
        Conexion Conexion;
        public Ver()
        {
            Conexion = new Conexion();
            InitializeComponent();
            Conexion.crearBaseDeDatos();

            recargar();
            

        }

        /// <summary>
        /// actualiza todos los datos de la ventana
        /// </summary>
        public void recargar()
        {

            Controls.Cast<Control>().Where(q => q.GetType().Equals(typeof(Item))).ToList().ForEach(q =>
            {
                this.Controls.Remove(q);
            });
            Conexion.cargarMesas(this, plantilla);
            plano1.SendToBack();
            actualizarTabla();


        }

        /// <summary>
        /// actualiza los datos de la tabla
        /// </summary>
        public void actualizarTabla()
        {
            Conexion.cargarTablaMesas(dataGridView1, plantilla);
        }

        /// <summary>
        /// un temporizador para el relog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelHora.Text = DateTime.Now.ToLongTimeString();
            labelFecha.Text = DateTime.Now.ToShortDateString();
            labelDia.Text = DateTime.Now.ToString("dddd");
        }

        /// <summary>
        /// abre la ventana del editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Mozos>();
        }

        /// <summary>
        /// abre la ventana para cargar las ordenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void item_DoubleClick(object sender, EventArgs e)
        {
            Datos datos = new Datos();
            AbrirFormEnPanel<Datos>(sender as Item);
        }

        #region cambio de plantilla
        private void button3_Click(object sender, EventArgs e)
        {
            plantilla = 1;
            recargar();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            plantilla = 2;
            recargar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            plantilla = 3;
            recargar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            plantilla = 4;
            recargar();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            plantilla = 5;
            recargar();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            plantilla = 6;
            recargar();
        }


        #endregion

        internal void Mouse_hover(object sender, EventArgs e)
        {            
            (sender as Item).BackColor = (sender as Item).ocupado ? Color.Red : Color.Green;
        }

        internal void Mouse_Leave(object sender, EventArgs e)
        {
            (sender as Item).BackColor = SystemColors.ActiveCaption;
        }

        public void AbrirFormEnPanel<form>(Item item) where form : Form, Dar, new() 
        {
            
            form formulario;
            formulario = panelDatos.Controls.OfType<form>().FirstOrDefault();

            if (formulario != null)
            {
                cerrarPaneles();
            }

            //si el formulario/instancia no existe, creamos nueva instancia y mostramos
            if (formulario == null)
            {
                formulario = new form();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Left;
                
                panelDatos.Controls.Add(formulario);
                panelDatos.Tag = formulario;
                formulario.Show();
               

                formulario.darItem(item);
                formulario.darPadre(this);
                


                formulario.BringToFront();
            }
            else
            {
                //si la Formulario/instancia existe, lo traemos a frente
                formulario.BringToFront();

                //Si la instancia esta minimizada mostramos
                if (formulario.WindowState == FormWindowState.Minimized)
                {
                    formulario.WindowState = FormWindowState.Normal;
                }

            }
        }


        //public void AbrirFormEnPanelPedido<form>(Item item) where form : Form, Dar, new()
        //{

        //    form formulario;
        //    formulario = panelDatos.Controls.OfType<form>().FirstOrDefault();

        //    if (formulario != null)
        //    {
        //        cerrarPaneles();
        //    }

        //    //si el formulario/instancia no existe, creamos nueva instancia y mostramos
        //    if (formulario == null)
        //    {
        //        formulario = new form();
        //        formulario.TopLevel = false;
        //        formulario.FormBorderStyle = FormBorderStyle.None;
        //        formulario.Dock = DockStyle.Left;
        //        panelPedido.Show();
        //        panelPedido.Controls.Add(formulario);
        //        panelPedido.Tag = formulario;
        //        formulario.Show();
        //        formulario.FormClosed += closeForms;

        //        formulario.darItem(item);
        //        formulario.darPadre(this);



        //        formulario.BringToFront();
        //    }
        //    else
        //    {
        //        //si la Formulario/instancia existe, lo traemos a frente
        //        formulario.BringToFront();

        //        //Si la instancia esta minimizada mostramos
        //        if (formulario.WindowState == FormWindowState.Minimized)
        //        {
        //            formulario.WindowState = FormWindowState.Normal;
        //        }

        //    }
        //}

        internal void AbrirFormEnPanel<form>(Item item, Datos datos) where form : Pedido, new()
        {
            form formulario;
            formulario = panelDatos.Controls.OfType<form>().FirstOrDefault();

            //if (formulario != null)
            //{
            //    cerrarPaneles();
            //}

            //si el formulario/instancia no existe, creamos nueva instancia y mostramos
            if (formulario == null)
            {
                formulario = new form();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Left;
                
                panelDatos.Controls.Add(formulario);
                panelDatos.Tag = formulario;
                formulario.Show();
                formulario.FormClosed += Formulario_FormClosed;
                

                formulario.darItem(item);
                formulario.darPadre(datos);



                formulario.BringToFront();
            }
            else
            {
                //si la Formulario/instancia existe, lo traemos a frente
                formulario.BringToFront();

                //Si la instancia esta minimizada mostramos
                if (formulario.WindowState == FormWindowState.Minimized)
                {
                    formulario.WindowState = FormWindowState.Normal;
                }

            }
        }

        private void Formulario_FormClosed(object sender, FormClosedEventArgs e)
        {
            panelDatos.AutoSize = true;
        }

        internal void AbrirFormEnPanel<form>() where form : Form, new()
        {
            form formulario;
            formulario = panelDatos.Controls.OfType<form>().FirstOrDefault();

            Form otro = panelDatos.Controls.OfType<Form>().FirstOrDefault();

            //si hay formularios del otro tipo los cerramos

            if (otro != null)
            {
                cerrarPaneles();
            }

            //si el formulario/instancia no existe, creamos nueva instancia y mostramos
            if (formulario == null)
            {
                formulario = new form();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Left;

                panelDatos.Controls.Add(formulario);
                panelDatos.Tag = formulario;
                formulario.Show();
                
                formulario.BringToFront();
            }
            else
            {
                //si la Formulario/instancia existe, lo traemos a frente
                formulario.BringToFront();

                //Si la instancia esta minimizada mostramos
                if (formulario.WindowState == FormWindowState.Minimized)
                {
                    formulario.WindowState = FormWindowState.Normal;
                }

            }
        }

        private void closeForms(object sender, FormClosedEventArgs e)
        {
            //panelPedido.Hide();
        }

        public void cerrarPaneles()
        {
            panelDatos.Controls.Cast<Control>().Where(q => q.GetType().Equals(typeof(Datos)) || q.GetType().Equals(typeof(Pedido)) || q.GetType().Equals(typeof(Mozos)) || q.GetType().Equals(typeof(EditarMenu))).ToList().ForEach(q =>
            {
                (q as Form).Close();
            });
        }

        private void plano1_Click(object sender, EventArgs e)
        {
            cerrarPaneles();
            dataGridView1.BringToFront();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ponerColorDeFondo();


            if (e.RowIndex >= 0)
            {
                int mesa = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Mesa"].Value.ToString());


                this.Controls.Cast<Control>().Where(q => q.GetType().Equals(typeof(Item))).ToList().ForEach(q =>
                {
                    if ((q as Item).index == mesa)
                    {
                        q.BackColor = Color.Green;                       
                    }
                });
            }
        }

        public void ponerColorDeFondo()
        {
            this.Controls.Cast<Control>().Where(q => q.GetType().Equals(typeof(Item))).ToList().ForEach(q =>
            {   
                    q.BackColor = SystemColors.ActiveCaption;
            });
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            ponerColorDeFondo();

           
            if(e.RowIndex >= 0)
            {
                int mesa = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Mesa"].Value.ToString());


                this.Controls.Cast<Control>().Where(q => q.GetType().Equals(typeof(Item))).ToList().ForEach(q =>
                {
                    if((q as Item).index == mesa)
                    {                        
                        q.BackColor = Color.Green;
                        AbrirFormEnPanel<Datos>(q as Item);
                    }
                });
            }
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<EditarMenu>();
        }
    }
}

