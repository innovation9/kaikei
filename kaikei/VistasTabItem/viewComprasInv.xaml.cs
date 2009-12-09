using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kaikei.InventariosDSTableAdapters;
using Kaikei.ContaGeneralDSTableAdapters;
using System.Data;
using ControlsWPF_JMP.Controls;
using Microsoft.Windows.Controls;

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Lógica de interacción para viewComprasInv.xaml
    /// </summary>
    public partial class viewComprasInv : UserControl
    {
		private PROVEEDORESTableAdapter proTA;
		private MATERIALES_UNIDADESTableAdapter matTA;
        private COMPRASTableAdapter comTA;
        private DETALLE_COMPRATableAdapter comdetTA;
		private DataTable tblDetalle;
        public viewComprasInv()
        {
            InitializeComponent();
			
			proTA = new PROVEEDORESTableAdapter();
			matTA = new MATERIALES_UNIDADESTableAdapter();
            comTA = new COMPRASTableAdapter();
            comdetTA = new DETALLE_COMPRATableAdapter();
			
			//Cargamos los proveedores al ComboBox
			InventariosDS iDS = new InventariosDS();
			proTA.Fill(iDS.PROVEEDORES);
			txtProveedor.ItemsSource=iDS.PROVEEDORES.DefaultView;
			txtProveedor.DisplayMemberPath="NOMBRE";
			txtProveedor.SelectedValuePath="ID_PROVEEDOR";
			
			//Cargamos los materiales
			matTA.Fill(iDS.MATERIALES_UNIDADES);
			gridDetalle.DataContext = iDS.MATERIALES_UNIDADES.DefaultView;
			txtMaterial.DisplayMemberPath="NOMBRE";
			txtMaterial.SelectedValuePath="ID_MATERIAL";
			
			//Creamos la tabla detalle de Factura
			GenerarTabla();
			dgDetalleF.ItemsSource=tblDetalle.DefaultView;

            //Cargamos la fecha actual del sistema
            txtFecha.Text = DateTime.Today.ToString();
            txtProveedor.Focus();
        }
		
	private void txtProveedor_GotFocus(object sender, RoutedEventArgs e)
        {
            txtProveedor.IsDropDownOpen = true;
        }
	private void txtMaterial_GotFocus(object sender, RoutedEventArgs e)
        {
            txtMaterial.IsDropDownOpen = true;
        }
		
	private void GenerarTabla() {
			tblDetalle = new DataTable("DetalleF");
			
			//Creamos las columnas de la tabla
			tblDetalle.Columns.Add("CODIGO",typeof(int));
			tblDetalle.Columns.Add("NOMBRE",typeof(string));
			tblDetalle.Columns.Add("CANTIDAD",typeof(int));
			tblDetalle.Columns.Add("PRECIO",typeof(decimal));
			tblDetalle.Columns.Add("TOTAL",typeof(decimal));
		}

    private void btnAMaterial_Click(object sender, RoutedEventArgs e)
    {
        int iIDMaterial, iCantidad = 0;
        decimal dPrecio;
        try
        {
            iIDMaterial = (int)txtMaterial.SelectedValue;
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            return;
        }

        if (!int.TryParse(txtTUnit.Text, out iCantidad))
        {
            MessageBox.Show("El datos no es valido en la cantidad de Unidades");
            txtTUnit.Focus();
            txtTUnit.SelectAll();
            return;
        }

        if (!decimal.TryParse(txtPrecioU.Text, out dPrecio))
        {
            MessageBox.Show("El datos no es valido en la Precio Unitario");
            txtPrecioU.Focus();
            txtPrecioU.SelectAll();
            return;
        }

        if ((iCantidad <= 0) || (dPrecio <= 0))
        {
            MessageBox.Show("Los campos de Precio unitario y Cantidad no pueden ser menor o iguales a cero");
            return;
        }

        //Una vez validado, agreagamos el material a la lista
        tblDetalle.Rows.Add(iIDMaterial, txtMaterial.Text, iCantidad, Math.Round(dPrecio,2), Math.Round(dPrecio * iCantidad, 2));

        //Limpiamos los datos
        txtMaterial.Focus();
        txtTUnit.Text = "0";
        txtPrecioU.Text = "0";
        CalcularTotales();
    }

    private void txt_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBoxJMP txtTMP = sender as TextBoxJMP;
        txtTMP.SelectAll();
    }

    private void dgDetalleF_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
    {
        CalcularTotales();
    }

    private void CalcularTotales()
    {
        decimal dPTotal=0;
        int iCTotal=0;

        foreach (DataRow dr in tblDetalle.Rows)
        {
            dr["TOTAL"] = Math.Round((decimal)dr["PRECIO"] * (int)dr["CANTIDAD"], 2);
            dPTotal = dPTotal + (decimal)dr["TOTAL"];
            iCTotal++;
        }
		
		lblTotalM.Text = string.Format("{0} Materiales",iCTotal);
		lblTotalC.Text = string.Format("{0:C}",dPTotal);
    }

    private void btnCompra_Click(object sender, RoutedEventArgs e)
    {
        DateTime dt;
        int iIDProv;
        try {
            dt = DateTime.Parse(txtFecha.Text);
        } catch (Exception ex) {
            ex.Data.Clear();
            MessageBox.Show("Por favor, seleccione la fecha de la operacion");
            txtFecha.Focus();
            return;
        }

        try
        {
            iIDProv = (int)txtProveedor.SelectedValue;
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            MessageBox.Show("Por favor, seleccione un proveedor para la compra");
            txtProveedor.Focus();
            return;
        }

        //Agregamos la transaccion a la tabla COMPRAS, que es la info general
        comTA.Insert(iIDProv, txtFecha.SelectedDate);

        int iIDCompra;

        //Recalculamos totales por cualquier cosa
        CalcularTotales();

        //Una vez agregada tenemos que recuperar el ID_COMPRA
        InventariosDS iDSTMP = new InventariosDS();
        comTA.Fill(iDSTMP.COMPRAS);
        iIDCompra = (int)iDSTMP.COMPRAS[iDSTMP.COMPRAS.Rows.Count-1]["ID_COMPRA"];

        //Una vez recuperado empezamos agregar material por material a la tabla detalle de compras
        foreach (DataRow dr in tblDetalle.Rows)
        {
            comdetTA.Insert(iIDCompra, (int)dr["CODIGO"], (int)dr["CANTIDAD"], (decimal)dr["PRECIO"]);
        }

        //Una vez con la compra de materieles tenemos que realizar la transaccion de Inventario Mercaderia
        //Calculamos el IVA y a que cuenta vamos abonar
        decimal dMontoT = decimal.Parse(lblTotalC.Text.Substring(1));
        decimal dIVA = Math.Round(dMontoT * 0.13m, 2);
        int iIDTransaccion;
        TRANSACCIONESTableAdapter transTA = new TRANSACCIONESTableAdapter();
        ContaGeneralDS contaDS = new ContaGeneralDS();
        DETALLE_TRANSACCIONESTableAdapter detTransTA = new DETALLE_TRANSACCIONESTableAdapter();

        //Hacemos primero la insercion en la tabla transacciones
        transTA.Insert(12, DateTime.Today, string.Format("F{0}", iIDCompra), dMontoT, "Compra de Materiales");
        transTA.FillByCodigo(contaDS.TRANSACCIONES, string.Format("F{0}", iIDCompra));
        iIDTransaccion = (int)contaDS.TRANSACCIONES[0]["ID_TRANSACCION"];

        //Ahora afectamos las cuentas de la opoeracions d compra de materiales
        detTransTA.Insert(iIDTransaccion, 1, 39, dIVA, 0);
        detTransTA.Insert(iIDTransaccion, 2, 80, dMontoT-dIVA, 0);
        if (optEfectivo.IsChecked == true)
            detTransTA.Insert(iIDTransaccion, 3, 1, 0, dMontoT);     //EFECTIVO
        else
            detTransTA.Insert(iIDTransaccion, 3, 31, 0, dMontoT);    //CxP


        MessageBox.Show("La transaccion de Compra de Mercaderia se realizo con exito", "Transaccion Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
        tblDetalle.Clear();
        txtPrecioU.Text = "0";
        txtTUnit.Text = "0";
        txtFecha.Focus();
        
    }

    private void txtProveedor_LostFocus(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(txtProveedor.Text.Trim()))
        {
            if (txtProveedor.SelectedValue == null)
            {
                if (MessageBox.Show(string.Format("El proveedor '{0}' no existe en el sistema. ¿Desea crearlo?", txtProveedor.Text), "¿Desea crear el proveedor?",
                                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {

                }
                else
                    try
                    {
                        txtProveedor.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Clear();
                        txtProveedor.Text = "";
                    }
                    finally
                    {
                        txtProveedor.Focus();
                    }
            }
        }
    }
    }
	

	

}
