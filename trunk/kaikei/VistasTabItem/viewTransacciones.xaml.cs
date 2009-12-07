using System;
using System.Collections.Generic;
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
using Kaikei.ContaGeneralDSTableAdapters;
using System.Data;

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for viewTransacciones.xaml
	/// </summary>
	public partial class viewTransacciones : UserControl
	{
		private OPERACIONESTableAdapter iTA;
		private CATALOGO_CUENTASTableAdapter ccTA;
		private DataTable tblDetalle;
		public viewTransacciones()
		{
			this.InitializeComponent();
			iTA = new OPERACIONESTableAdapter();
			ccTA = new CATALOGO_CUENTASTableAdapter();
			
			ContaGeneralDS cDS = new ContaGeneralDS();
			iTA.Fill(cDS.OPERACIONES);
			ccTA.Fill(cDS.CATALOGO_CUENTAS);
			
			//Conectamos con las operaciones del Sistemas
			txtOperaciones.DataContext = cDS.OPERACIONES.DefaultView;
            txtOperaciones.DisplayMemberPath = "DESCRIPCION";
            txtOperaciones.SelectedValuePath = "ID_OPERACION";
			
			//Despues Conectamos con el Catalogo de Cuentas del Sistema
			txtCuentas.DataContext=cDS.CATALOGO_CUENTAS.DefaultView;
			txtCuentas.DisplayMemberPath="DESCRIPCION";
			txtCuentas.SelectedValuePath="ID_CUENTA";
			
			//Generamos la Tabla para el detalle de la Transacciones y la conectamos al Grid
			GenerarTabla();
			dgDetalleT.ItemsSource=tblDetalle.DefaultView;
		}

        private void txtOperaciones_GotFocus(object sender, RoutedEventArgs e)
        {
            txtOperaciones.IsDropDownOpen = true;
        }
		
		private void txtCuentas_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCuentas.IsDropDownOpen = true;
        }
		
		private void GenerarTabla() {
			tblDetalle = new DataTable("DetalleT");
			
			//Creamos las columnas de la tabla
			tblDetalle.Columns.Add("ID",typeof(int));
			tblDetalle.Columns.Add("NOMBRE",typeof(string));
			tblDetalle.Columns.Add("DEBE",typeof(decimal));
			tblDetalle.Columns.Add("HABER",typeof(decimal));
		}
	}
}