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
    public partial class InterfazSolicitudDeDevoluciones : PlantillaForms.Plantilla
    {
        CapaDatosCompras cd = new CapaDatosCompras();                                                                   // INSTANCIA A LA CAPA DE DATOS
        Navegador nv = new Navegador();                                                                                 // INTANCIA AL NAVEGADOR
        public InterfazSolicitudDeDevoluciones(DataGridView dg1)
        {
            InitializeComponent();
            nv.dgv_datos(dg1);
        }

        private void InterfazSolicitudDeDevoluciones_Load(object sender, EventArgs e)
        {
            nv.ingresarTabla("TBL_OrdenDeDevolucionEncabezado");                                                        // CARGA DE DATOS DE LA TABLA DE LA ORDEN
            Txt_CodigoProveedor.Text = "";
            Cbo_Proveedores.Text = "";
            DataSet dt = cd.cargarCBBX("TBL_Provedor", "Nombre");                                                       // CARGA DEL COMBOBOX CON LOS NOMBRES DE LOS PROVEEDORES
            Cbo_Proveedores.DataSource = dt.Tables[0].DefaultView;
            Cbo_Proveedores.ValueMember = "Nombre";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InterfazSeleccionDetalleOrdenDeDevolucion form = new InterfazSeleccionDetalleOrdenDeDevolucion();           // BOTON QUE ABRE EL DETALLE
            form.NOrden = Convert.ToInt32(Txt_NOrden.Text);                                                             // VARIABLE NORDEN QUE TOMA EL VALOR DE LA ORDEN
            form.Show();
        }

        private void Txt_NOrden_TextChanged(object sender, EventArgs e)
        {
            CapaDatosCompras cd = new CapaDatosCompras();                                                               // INSTANCIA A LA CAPA DE DATOS
            if (Txt_NOrden.Text != "")                                                                                  // HABILITACION O DESHABILITACION DEL BOTON PARA ABRIR EL DETALLE
            {
                button3.Enabled = true;                                                                                     

                DataSet ds;                                                                                             // SI EXISTE EL DETALLE, SUMAR EL TOTAL A TRAVES DEL SUBTOTAL
                ds = cd.ConsultarDatos1(Txt_NOrden.Text);
                Dgv_detalle.DataSource = ds.Tables[0];
                double suma = 0;
                foreach (DataGridViewRow row in Dgv_detalle.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        suma += Convert.ToDouble(row.Cells[4].Value);                                                   // SUMA DE SUBTOTALES
                    }
                }
                this.Txt_total.Text = Convert.ToString(suma);                                                           // CARGA DEL TEXTBOX DE TOTALES

            }
            else
            {
                button3.Enabled = false;                                                                                // DESHABILITACION DEL BOTON DE DETALLE
                DataTable dt = (DataTable)Dgv_detalle.DataSource;                                                       // CARGA DE DATOS AL DATAGRID
                dt.Clear();
                this.Txt_total.Text = "0";                                                                              // TOTAL IGUALADO A CERO
            }
        }

        private void Cbo_Proveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapaDatosCompras cd = new CapaDatosCompras();                                                                           // CARGA DE DATOS DLE PROVEEDORE
            Txt_CodigoProveedor.Text = cd.ExtraerCodigos(Cbo_Proveedores.Text, "PK_codProveedor", "TBL_Provedor", "Nombre");
        }

        private void Txt_CodigoProveedor_TextChanged(object sender, EventArgs e)
        {
            CapaDatosCompras cd = new CapaDatosCompras();
            Txt_CodigoProveedor.Text = cd.ExtraerCodigos(Cbo_Proveedores.Text, "PK_codProveedor", "TBL_Provedor", "Nombre");
        }

        private void Txt_Aprobacion_TextChanged(object sender, EventArgs e)
        {
            Txt_Aprobacion.Text = "0";                                                                                              // APROBACION DE LA ORDEN EN 0 = RECHAZADA, OTRO MODULO SE ENCARGARA DE ACEPTARLA O NO
        }
    }
}
