using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CapaDiseno;
using OrdenesDeCompra.CapaDatos;

namespace OrdenesDeCompra.CapaInterfaz
{
    public partial class InterfazSolicitudOrdenesDeCompra : PlantillaForms.Plantilla
    {
        Navegador nv = new Navegador();                                                     // CREACION DE INSTANCIA DEL NAVEGADOR
        public InterfazSolicitudOrdenesDeCompra(DataGridView dg1)
        {
            InitializeComponent();
            nv.dgv_datos(dg1);
        }

        private void InterfazSolicitudOrdenesDeCompra_Load(object sender, EventArgs e)
        {
            CapaDatosCompras cd = new CapaDatosCompras();                                                   // INTANCIA DE LA CAPA DE DATOS
            nv.ingresarTabla("TBL_OrdenDeCompraEncabezado");                                                // CARGAR DATOS DE LA TABLA AL FORM
            Txt_CodigoProveedor.Text = "";
            Cbo_Proveedores.Text = "";
            DataSet dt = cd.cargarCBBX("TBL_Provedor", "Nombre");                                           // CARGAR EL COMBOBOX
            Cbo_Proveedores.DataSource = dt.Tables[0].DefaultView;                                          
            Cbo_Proveedores.ValueMember = "Nombre";
        }

        private void navegador1_MouseHover(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String.Format("{0:yyyy-MM-dd}", Txt_FOrden.Text);
        }

        private void Txt_FEntrega_TextChanged(object sender, EventArgs e)
        {
            String.Format("{0:yyyy-MM-dd}", Txt_FEntrega.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InterfazSeleccionDetalleOrdenDeCompra form = new InterfazSeleccionDetalleOrdenDeCompra();       // ABRIR EL DETALLE
            form.NOrden = Convert.ToInt32(Txt_NOrden.Text);                                                 // DARLE A LA VARIABLE DE NORDEN DEL FORM DE DETALLE EL VALOR DEL NUMERO DE ORDEN A UTILIZAR
            form.Show();
        }

        private void Txt_NOrden_TextChanged(object sender, EventArgs e)
        {
            CapaDatosCompras cd = new CapaDatosCompras();                                   // INSTANCIA DE LA CAPA DE DATOS
            if (Txt_NOrden.Text != "")                                                      // HABILITACION O DESHABILITACION DEL BOTON DE DETALLE SEGUN EL NUMERO DE ORDEN
            {       
                button3.Enabled = true;

                DataSet ds;
                ds = cd.ConsultarDatos(Txt_NOrden.Text);                                    // CARGA DEL DATAGRIDVIEW CON LOS DATOS DE LOS DEMAS DETALLES DE LA ORDEN
                Dgv_detalle.DataSource = ds.Tables[0];
                double suma = 0;
                foreach (DataGridViewRow row in Dgv_detalle.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        suma += Convert.ToDouble(row.Cells[4].Value);                       // SUMA DE SUBTOTALES PARA GENERAR EL TOTAL
                    }
                }

                this.Txt_total.Text = Convert.ToString(suma);                               // CARGA DEL TOTAL AL TEXTBOX
            }
            else
            {
                button3.Enabled = false;                                                    // DESHABILITACION DEL DETALLE
                DataTable dt = (DataTable)Dgv_detalle.DataSource;                           // VACIADO DEL DATAGRID
                dt.Clear();
                this.Txt_total.Text = "0";                                                  // TOTAL EN CERO
            }

        }

        private void Cbo_Proveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapaDatosCompras cd = new CapaDatosCompras();                                                                       // CARGAR DATOS DEL PROVEEDOR
            Txt_CodigoProveedor.Text = cd.ExtraerCodigos(Cbo_Proveedores.Text, "PK_codProveedor", "TBL_Provedor", "Nombre");
        }

        private void Txt_CodigoProveedor_TextChanged(object sender, EventArgs e)
        {
            CapaDatosCompras cd = new CapaDatosCompras();
            Txt_CodigoProveedor.Text = cd.ExtraerCodigos(Cbo_Proveedores.Text, "PK_codProveedor", "TBL_Provedor", "Nombre");
        }

        private void Txt_Aprobacion_TextChanged(object sender, EventArgs e)
        {
            Txt_Aprobacion.Text = "0";                                                                                  // APROBACION DE LA ORDEN EN 0 = RECHAZADA, OTRO MODULO SE ENCARGARA DE ACEPTARLA O NO
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
