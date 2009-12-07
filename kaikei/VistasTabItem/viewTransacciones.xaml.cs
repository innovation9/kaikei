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
using ControlsWPF_JMP.Controls;

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
            btnCargar.IsDefault = true;
        }
		
		private void txtCuentas_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCuentas.IsDropDownOpen = true;
            btnACuenta.IsDefault = true;
        }
		
		private void GenerarTabla() {
			tblDetalle = new DataTable("DetalleT");
			
			//Creamos las columnas de la tabla
			tblDetalle.Columns.Add("ID",typeof(int));
			tblDetalle.Columns.Add("NOMBRE",typeof(string));
			tblDetalle.Columns.Add("DEBE",typeof(decimal));
			tblDetalle.Columns.Add("HABER",typeof(decimal));
		}

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            //Obtenemos los datos de Operaciones ingresados por el usuario
            int iIDOper;
            Decimal dMonto;

            try
            {
                iIDOper = (int)txtOperaciones.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                return;
            }

            if (iIDOper == 11 || (iIDOper == 12))
            {
                txtCuentas.Focus();
                return;
            }

            //Validamos los datos ingresados
            if (!Decimal.TryParse(txtMonto.Text,out dMonto)) {
                MessageBox.Show("Por favor ingrese un monto numerico valido","Monto no valido",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            
            if (dMonto <= 0)
            {
                MessageBox.Show("Por favor, ingrese un monton arriba de $0", "Monto no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ContaGeneralDS tmpDS = new ContaGeneralDS();
            int iDebe, iHaber,iIVA;
            string sCDebe, sCHaber;

            //Hacemos primero el filtro para obtener los valores del DEBE, HABER y si tiene tratamiento IVA
            iTA.FillByIDO(tmpDS.OPERACIONES,iIDOper);
            iDebe = (int)tmpDS.OPERACIONES.Rows[0]["COD_DEBE"];
            iHaber = (int)tmpDS.OPERACIONES.Rows[0]["COD_HABER"];
            iIVA = (int)tmpDS.OPERACIONES.Rows[0]["IVA"];

            //Ahora determinamos el nombre de las Cuentas del DEBE y HABER
            ccTA.FillByIDC(tmpDS.CATALOGO_CUENTAS, iDebe);
            sCDebe = (string)tmpDS.CATALOGO_CUENTAS[0]["DESCRIPCION"];
            ccTA.FillByIDC(tmpDS.CATALOGO_CUENTAS, iHaber);
            sCHaber = (string)tmpDS.CATALOGO_CUENTAS[0]["DESCRIPCION"];

            //Como ya tenemos todos los datos necesarios cargados, ya podemos agregar a la tabla detalle de
            //transacciones con los campos incluidos
            //Tambien verificamos si la cuenta posse IVA
            decimal dMontoIVA = Math.Round(dMonto*0.13m,2);
            dMonto = Math.Round(dMonto, 2);
            tblDetalle.Clear();
            switch (iIVA)
            {
                case 0: //SIN IVA
                    tblDetalle.Rows.Add(iDebe, sCDebe, dMonto, 0);
                    tblDetalle.Rows.Add(iHaber, sCHaber, 0, dMonto);
                    break;
                case 1: //IVA en el DEBE
                    tblDetalle.Rows.Add(39, "IVA Acreditable", dMontoIVA, 0);
                    tblDetalle.Rows.Add(iDebe, sCDebe, dMonto - dMontoIVA, 0);
                    tblDetalle.Rows.Add(iHaber, sCHaber, 0, dMonto);
                    break;
                case 2: //IVA en el HABER
                    tblDetalle.Rows.Add(iDebe, sCDebe, dMonto, 0);
                    tblDetalle.Rows.Add(38, "IVA por PAGAR", 0, dMontoIVA);
                    tblDetalle.Rows.Add(iHaber, sCHaber, 0, dMonto - dMontoIVA);
                    break;
            }
            tmpDS.Dispose();
            EstablecerTotales();
            txtDescripcion.Focus();
        }

        private void dgDetalleT_CellEditEnding(object sender, Microsoft.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            EstablecerTotales();
        }

        private void EstablecerTotales()
        {
            //Determinamos la Suma del DEBE y la suma del HABER
            decimal dTDebe = 0, dTHaber = 0;
            foreach (DataRow dr in tblDetalle.Rows)
            {
                dTDebe = dTDebe + (decimal)dr["DEBE"];
                dTHaber = dTHaber + (decimal)dr["HABER"];
            }

            lblTotalDebe.Text = string.Format("{0:C}", dTDebe);
            lblTotalHaber.Text = string.Format("{0:C}", dTHaber);
        }

        private void txtDebe_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBoxJMP)sender).SelectAll();
        }

        private void txtMonto_GotFocus(object sender, RoutedEventArgs e)
        {
            btnCargar.IsDefault = true;
        }

        private void btnACuenta_Click(object sender, RoutedEventArgs e)
        {
            //Agregamos la cuenta a la tabla de Detalle de transacciones
            int iIDCuenta;
            string sCuenta;
            decimal dDebe, dHaber;

            //Realizamos las validaciones
            try
            {
                iIDCuenta = (int)txtCuentas.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                return;
            }
            sCuenta = (string)txtCuentas.Text;

            if (!decimal.TryParse(txtDebe.Text, out dDebe))
            {
                MessageBox.Show("Por favor ingrese un monto para el debe valido", "Monto del DEBE no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDebe.Focus();
                return;
            }

            if (!decimal.TryParse(txtHaber.Text, out dHaber))
            {
                MessageBox.Show("Por favor ingrese un monto para el HABER valido", "Monto del HABER no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                txtHaber.Focus();
                return;
            }

            //Comprobamos que los valores sean mayores o iguales a CERO
            if ((dHaber < 0) || (dHaber < 0) || ((dDebe < 0) && (dHaber < 0)))
            {
                MessageBox.Show("El valos minimo de los campos del DEBE y HABER es cero", "Datos no validos", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Una vez validados, pasamos a meter los datos a la tabla
            tblDetalle.Rows.Add(iIDCuenta, sCuenta, dDebe, dHaber);

            EstablecerTotales();
            txtDebe.Text = "0";
            txtHaber.Text = "0";
            txtCuentas.Focus();
        }

        private void btnTransaccion_Click(object sender, RoutedEventArgs e)
        {
            //Primero validmos que el DEBE es igual al HABER
            //Actualizamos los totales
            EstablecerTotales();
            decimal dTDebe, dTHaber;
            int iIDOper;

            try
            {
                iIDOper = (int)txtOperaciones.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                iIDOper = 11; //Transacciones Personalizada
            }

            if (tblDetalle.Rows.Count == 0) return;

            dTDebe = decimal.Parse(lblTotalDebe.Text.Substring(1));
            dTHaber = decimal.Parse(lblTotalHaber.Text.Substring(1));
            

            if (dTDebe != dTHaber)
            {
                MessageBox.Show("Lo sentimos, la transaccion no se puede realizar debido a que no se cumple el principio de partida doble en las cuentas. Por favor corrija y vuelva a intentarlo");
                return;
            }

            //PRIMERO - Hacemos la insercion en la tabla TRANSACCIONES para luego especificar el detalle
            //de las transacciones
            TRANSACCIONESTableAdapter tranTA = new TRANSACCIONESTableAdapter();
            tranTA.Insert(iIDOper, DateTime.Today, string.Format("OP{0}", iIDOper), dTDebe, txtDescripcion.Text);

            //SEGUNDO - Determinamos la transaccion efectuada el ID que fue asignado
            ContaGeneralDS contaDS = new ContaGeneralDS();
            tranTA.Fill(contaDS.TRANSACCIONES);
            int iIDTrans = (int)contaDS.TRANSACCIONES[contaDS.TRANSACCIONES.Rows.Count-1]["ID_TRANSACCION"];

            //TERCERO - Una vez guardado en la tabla de TRANSACCIONES nos toca guardar el detalle de las cuentas
            //afectas en la tranasccion en DETALLE_TRANSACCION
            DETALLE_TRANSACCIONESTableAdapter detTA = new DETALLE_TRANSACCIONESTableAdapter();
            int partida = 1;
            foreach (DataRow dr in tblDetalle.Rows)
            {
                detTA.Insert(iIDTrans, partida, (int)dr["ID"], (decimal)dr["DEBE"], (decimal)dr["HABER"]);
                partida++;
            }

            MessageBox.Show("La transaccion se realizo con exito en el sistema", "Transaccion Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
            tblDetalle.Clear();
            txtDescripcion.Text = "";
            txtDebe.Text = "0";
            txtHaber.Text = "0";
            txtCuentas.Text = "";
            txtMonto.Text = "";
            EstablecerTotales();
            txtOperaciones.Focus();
        }

        private void dgDetalleT_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            EstablecerTotales();
        }

	}
}