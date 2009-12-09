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
        private FuncionesTableAdapter funcTA;
        private ORDEN_FABRICACIONTableAdapter ofTA;
        private DataTable tblReqMateriales;

        private int iMOD_NObreros, iMOD_Nhoras;
        private decimal dMOD_Salario;
        private decimal dGIF_Tasa, dGIF_Monto;

		public viewOrdenFabricacion()
		{
			this.InitializeComponent();
            mtTA = new ONLY_MATERIAL_DISPONIBLETableAdapter();
            funcTA = new FuncionesTableAdapter();
            ofTA = new ORDEN_FABRICACIONTableAdapter();
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
            //txtSalarioxHora.LostFocus += new RoutedEventHandler(HandlerMathJMP.NumeroDecimalMath);
            //txtMODNumeroHoras.LostFocus += new RoutedEventHandler(HandlerMathJMP.NumeroEnteroMath);
            //txtGIFMonto.LostFocus += new RoutedEventHandler(HandlerMathJMP.NumeroDecimalMath);
            //txtGIFTasa.LostFocus += new RoutedEventHandler(HandlerMathJMP.NumeroDecimalMath);

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

        private void btnAMaterial_Click(object sender, RoutedEventArgs e)
        {
            int iIDMaterial,iCantidadReq, iCantMax;
            if (!ValidacionesJMP.IsComboBoxValue(txtMaterial.SelectedValue)) {
                MessageBox.Show("ERROR! No a seleccionado un material de la lista");
                txtMaterial.Focus();
                return;
            }

            iIDMaterial = (int)txtMaterial.SelectedValue;

            //Validamos cantidad ahora
            if (!ValidacionesJMP.IsNumerico(txtCantidadReq.Text,out iCantidadReq))
            {
                MessageBox.Show("ERROR! El campo de Cantidad de Material no contiene un valor valido");
                txtCantidadReq.Focus();
                return;
            }

            //Por ultimo comprobamos que exista en el inventario la cantidad requerida
            iCantMax = funcTA.GET_MAXUNIT_MATERIAL(iIDMaterial).Value;
            if (iCantidadReq > iCantMax)
            {
                MessageBox.Show(string.Format("ERROR! Lo sentimos, la cantidad maxima del material {0} es de {1}",txtMaterial.Text.ToUpper(),iCantMax));
                txtCantidadReq.Text = iCantMax.ToString();
                txtCantidadReq.Focus();
                return;
            }

            //Una vez hemos validado todas las entradas, agregamos a la tabla el material requerido a la tabla
            tblReqMateriales.Rows.Add(iIDMaterial, txtMaterial.Text, iCantidadReq);

            //Limpiamos los campos
            LimpiarDatosReqMat();
            txtMaterial.Focus();

        }

        private void LimpiarDatosReqMat()
        {
            txtCantidadReq.Text = "0";
        }

        private void btnCrearOF_Click(object sender, RoutedEventArgs e)
        {
            DateTime dtFE, dtFRxC, dtFI, dtFT;

            //Validamos primero las fechas de los datos generales de la OF
            if ((!ValidacionesJMP.IsSelectedDate(txtFechaExp.Text, out dtFE)) || (!ValidacionesJMP.IsSelectedDate(txtFechaReq.Text,out dtFRxC))) 
            {
                MessageBox.Show("ERROR! Primero debe de seleccionar la Fecha de Expedicion y Fecha Requerida por el cliente", "ERROR! de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                txtFechaExp.Focus();
                return;
            }

            if (DateTime.Compare(dtFRxC,dtFE) < 0)
            {
                MessageBox.Show("ERROR! La fecha REQUERIDA POR EL CLIENTE no puede ser MENOR que la fecha de EXPEDICION", "ERROR! de Fechas", MessageBoxButton.OK, MessageBoxImage.Error);
                txtFechaReq.Focus();
                return;
            }

            //Ahora validamos los datos de Produccion
            int iCantidadProd;
            if ((ValidacionesJMP.IsNull(txtArticulo.Text)) || (!ValidacionesJMP.IsNumericoMayor(txtCantidad.Text, out iCantidadProd)) ||
                (!ValidacionesJMP.IsSelectedDate(txtFechaInicio.Text, out dtFI)) || (!ValidacionesJMP.IsSelectedDate(txtFechaFinalizacion.Text, out dtFT)) ||
                (ValidacionesJMP.IsNull(txtEspecificaciones.Text)))
            {
                MessageBox.Show("ERROR! Los campo de los datos de produccion contienen errores. Por favor, corrijalos y vuelva a intentarlo", "ERROR! de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                txtArticulo.Focus();
                return;
            }

            if (DateTime.Compare(dtFT, dtFI) < 0)
            {
                MessageBox.Show("ERROR! La fecha de FINALIZACION no puede ser MENOR que la fecha de INICIO", "ERROR! de Fechas", MessageBoxButton.OK, MessageBoxImage.Error);
                txtFechaReq.Focus();
                return;
            }

            //Ahora validamos datos del MOD
            if (lblTotalMOD.Text == "ERROR")
            {
                MessageBox.Show("ERROR! Los campos de los datos de MOD contiene errores", "ERROR! de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNoObreros.Focus();
                return;
            }

            //Por ultimo validamos los datos del CIF
            if (lblTotalGIF.Text == "ERROR")
            {
                MessageBox.Show("ERROR! Los campos de los datos de CIF contiene errores", "ERROR! de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                txtGIFTasa.Focus();
                return;
            }

            //Tambien validamos que exista requisicion de material en la orden
            if (tblReqMateriales.Rows.Count == 0)
            {
                MessageBox.Show("ERROR! No se puede continuar debido a que no a requerido de ningun material", "ERROR! No existe requisicion de material", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMaterial.Focus();
                return;
            }

            //Una vez tenemos validado todos los datos, podemos pasar primero agregar todo los datos
            //a la tabla de OF.
            ofTA.Insert(dtFE, dtFRxC, dtFI, dtFT, txtArticulo.Text,iCantidadProd, 
                        txtEspecificaciones.Text, iMOD_NObreros, iMOD_Nhoras, dMOD_Salario, 
                        dGIF_Monto, (float)dGIF_Tasa);

            //Recuperamos el numero de operacion que fue asignado a la Orden de Fabricacion
            int iIDOFab = funcTA.GET_NOLAST_ORDEN_FAB().Value;

            //Ahora tenemos que ingresa los materiales de manera de PEPS
            int iCTotal,iIDMat,iDifArt,iDRCantidad;
            InventariosDS iDS = new InventariosDS();
            INVENTARIO_CARDEXTableAdapter icxTA = new INVENTARIO_CARDEXTableAdapter();
            REQUISICION_MATERIALESTableAdapter reqmatTA = new REQUISICION_MATERIALESTableAdapter();

            foreach (DataRow dr in tblReqMateriales.Rows)
            {
                iIDMat = (int)dr["CODIGO"];
                iCTotal = (int)dr["CANTIDAD"];
                iDifArt = 0;
                
                //Cargamos la tabla con todos los productos disponibles ordenados en PEPS
                iDS.Clear();
                icxTA.FillByIDMaterial(iDS.INVENTARIO_CARDEX, iIDMat);
                foreach (DataRow drM in iDS.INVENTARIO_CARDEX.Rows)
                {
                    //Verificamos si la cantidad del primero articulo en PEPS alcanza para cubrir el material
                    iDRCantidad = (int)drM["DIFERENCIA"];
                    if (iDRCantidad >= iCTotal - iDifArt)
                    {
                        //Significa que la cnatidad de la compra alcanza para cubrirla requesicion
                        reqmatTA.Insert(iIDMat, iIDOFab, (int)drM["ID_COMPRA"], iCTotal - iDifArt);
                        break;
                    }
                    else
                    {
                        reqmatTA.Insert(iIDMat, iIDOFab, (int)drM["ID_COMPRA"], (int)drM["DIFERENCIA"]);
                        iDifArt += (int)drM["DIFERENCIA"];
                    }
                }

            }

            //Una vez terminado mostramos que la orden de fabriacion se realizo exitosamente
            MessageBox.Show(string.Format("La Orden de Fabriacion con No. {0}, se efectuo exitosamente en el sistema", iIDOFab), 
                                "La orden de fabricacion se ingreso correctamente en el sistema", 
                                MessageBoxButton.OK, MessageBoxImage.Information);


        }

        private void CalculoTasa_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxJMP txtTMP = sender as TextBoxJMP;
            if (txtTMP.Text.Length > 0 && txtTMP.Text.Substring(0, 1) == "=")
            {
                string result = MathEvalJMP.StringEval(txtTMP.Text.Substring(1));

                //Ahora tenemos q redondear el resultado a 2 cifras
                decimal dRes = decimal.Parse(result);
                txtTMP.Text = Math.Round(dRes, 2).ToString();
            }

            CalcularGIF();
        }

        private void CalculoMOD_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxJMP txtTMP = sender as TextBoxJMP;
            if (txtTMP.Text.Length > 0 && txtTMP.Text.Substring(0, 1) == "=")
            {
                string result = MathEvalJMP.StringEval(txtTMP.Text.Substring(1));

                //Ahora tenemos q redondear el resultado a 2 cifras
                decimal dRes = decimal.Parse(result);
                txtTMP.Text = Math.Round(dRes, 0).ToString();
            }

            CalcularMOD();
        }

        private void CalculoMODDec_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxJMP txtTMP = sender as TextBoxJMP;
            if (txtTMP.Text.Length > 0 && txtTMP.Text.Substring(0, 1) == "=")
            {
                string result = MathEvalJMP.StringEval(txtTMP.Text.Substring(1));

                //Ahora tenemos q redondear el resultado a 2 cifras
                decimal dRes = decimal.Parse(result);
                txtTMP.Text = Math.Round(dRes, 2).ToString();
            }

            CalcularMOD();
        }

        private void CalcularMOD()
        {
            if ((!ValidacionesJMP.IsNumericoMayor(txtNoObreros.Text, out iMOD_NObreros)) || (!ValidacionesJMP.IsNumericoMayor(txtMODNumeroHoras.Text, out iMOD_Nhoras)) ||
                (!ValidacionesJMP.IsNumericoMayor(txtSalarioxHora.Text, out dMOD_Salario)))
            {
                //MessageBox.Show("ERROR! Los campo de los datos de MANO DE OBRA DIRECTA contiene errores. Por favor, corrijalos y vuelva a intentarlo", "ERROR! de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                lblTotalMOD.Text = "ERROR";
                return;
            }

            lblTotalMOD.Text = string.Format("{0:C}", iMOD_NObreros * iMOD_Nhoras * dMOD_Salario);
        }

        private void CalcularGIF()
        {
            if (!ValidacionesJMP.IsNumericoMayor(txtGIFTasa.Text, out dGIF_Tasa, 100))
            {
                //MessageBox.Show("ERROR! La tasa tiene que ser un numero entre 1 y 100", "ERROR! de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                lblTotalGIF.Text = "ERROR";
                return;
            }

            if (!ValidacionesJMP.IsNumericoMayor(txtGIFMonto.Text, out dGIF_Monto))
            {
                //MessageBox.Show("ERROR! El monto de la tas del GIF tiene que ser mayor que cero", "ERROR! de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                lblTotalGIF.Text = "ERROR";
                return;
            }

            lblTotalGIF.Text = string.Format("{0:C}", (dGIF_Tasa/100) * dGIF_Monto);
        }

	}


}