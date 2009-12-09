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
using Kaikei.InventariosDSTableAdapters;
using System.Data;
using ControlsWPF_JMP.Controls;

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for viewOrdenFabricacion.xaml
	/// </summary>
	public partial class viewOrdenFabricacion : UserControl
	{
        private ONLY_MATERIAL_DISPONIBLETableAdapter mtTA;
        private DataTable tblReqMateriales;
		public viewOrdenFabricacion()
		{
			this.InitializeComponent();
            mtTA = new ONLY_MATERIAL_DISPONIBLETableAdapter();
            InventariosDS ivDS = new InventariosDS();

            //Cargamos fechas por defecto
            txtFechaExp.SelectedDate = DateTime.Today;
            txtFechaInicio.SelectedDate = DateTime.Today;

            //Cargamos la lista de materiales en el combobox
            mtTA.Fill(ivDS.ONLY_MATERIAL_DISPONIBLE);
            gridReqMateriales.DataContext = ivDS.ONLY_MATERIAL_DISPONIBLE.DefaultView;
            txtMaterial.DisplayMemberPath = "NOMBRE";
            txtMaterial.SelectedValuePath = "ID_MATERIAL";

            //Generamos la tabla que se conecta a la tabla de Req. Materiales
            GenerarTabla();
            dgReqMateriales.ItemsSource = tblReqMateriales.DefaultView;

            //Pasamos el handler a la clase manejadora de expresiones matematicas
            txtCantidad.LostFocus += new RoutedEventHandler(HandlerMathJMP.NumeroEnteroMath);
            txtCantidadReq.LostFocus += new RoutedEventHandler(HandlerMathJMP.NumeroEnteroMath);

            //Pasamos tambn el handler cuando entran al TextBox se seleccione el texto
            txtArticulo.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtEspecificaciones.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtCantidad.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtCantidadReq.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtNoObreros.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtSalarioxHora.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtMODNumeroHoras.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtGIFMonto.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
            txtGIFTasa.GotFocus += new RoutedEventHandler(HandlerMathJMP.TextSelect);
		}

        private void GenerarTabla()
        {
            tblReqMateriales = new DataTable("ReqMateriales");

            //Creamos las columnas de la tabla
            tblReqMateriales.Columns.Add("CODIGO", typeof(int));
            tblReqMateriales.Columns.Add("NOMBRE", typeof(string));
            tblReqMateriales.Columns.Add("CANTIDAD", typeof(int));
        }

        private void combo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtMaterial.IsDropDownOpen = true;
        }

	}


}