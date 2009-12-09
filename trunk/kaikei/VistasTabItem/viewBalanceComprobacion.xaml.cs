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
using System.Data;
using Kaikei.ContaGeneralDSTableAdapters;
using validateBLX;

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for viewBalanceComprobacion.xaml
	/// </summary>
	public partial class viewBalanceComprobacion : UserControl
	{
        private GET_BALANCECOMPROBACIONTableAdapter bcTA;
        private FuncionesTableAdapter funcTA;
		public viewBalanceComprobacion()
		{
			this.InitializeComponent();
            
            //Cargamos en el encabezado las Fechas del periodo corresponediente
            lblEmpresaNombre.Text = Kaikei.Properties.Settings.Default.EmpresaNombre;
            lblPeriodo.Text = string.Format("DEL {0} AL {1} DE {2} DEL {3}",
                                            Kaikei.Properties.Settings.Default.FechaIP.Day,
                                            Kaikei.Properties.Settings.Default.FechaFP.Day,
                                            Kaikei.Properties.Settings.Default.FechaFP.ToString("MMMM").ToUpper(),
                                            Kaikei.Properties.Settings.Default.FechaIP.Year);

            //Una vez establecido el encabezado procedemos a cargar los datos d la tabla, ejecutamos la
            //funcion para rellenar los datos en la tabla y hacemos un filtro x las fechas definidas en el periodo
            bcTA = new GET_BALANCECOMPROBACIONTableAdapter();
            funcTA = new FuncionesTableAdapter();

            ContaGeneralDS cgDS = new ContaGeneralDS();
            bcTA.Fill(cgDS.GET_BALANCECOMPROBACION, Kaikei.Properties.Settings.Default.FechaIP, Kaikei.Properties.Settings.Default.FechaFP);
            dgBalComprobacion.ItemsSource = cgDS.GET_BALANCECOMPROBACION.DefaultView;

            //Por ultimo, llamamos a las funciones de la Base de datos para cargar el total del DEBE y
            //el total del haber al final de la pantalla
            decimal? dMontoD = funcTA.GET_TOTALDEBE(Kaikei.Properties.Settings.Default.FechaIP, Kaikei.Properties.Settings.Default.FechaFP);
            decimal? dMontoH = funcTA.GET_TOTALHABER(Kaikei.Properties.Settings.Default.FechaIP, Kaikei.Properties.Settings.Default.FechaFP);

            if (!dMontoD.HasValue || !dMontoH.HasValue)
            {
                dMontoD = 0m;
                dMontoH = 0m;
            }

            txtMontoD.Text = string.Format("{0:C}", dMontoD);
            txtMontoH.Text = string.Format("{0:C}", dMontoH);

            //Establecemos un mensaje para determinar si se cumple partida doble y si esta bueno el balance
            //para poder habilitar el siguiente estado
			if (dMontoD == dMontoH) {
				lblResultado.Text="LA SUMA DEL DEBE CON LA SUMA DEL HABER SON IGUALES, POR TANTO SE CUMPLE EL PRINCIPIO DE PARTIDA DOBLE";
				Kaikei.Properties.Settings.Default.IsValidoBC=true;
			}
			else{
				lblResultado.Text="NO SE PUEDE CONTINUAR DEBIDO A QUE NO SE A CUMPLIDO EL PRINCIPIO DE PARTIDA DOBLE EN LA SUMA DEL DEBE CON EL HABER";
				Kaikei.Properties.Settings.Default.IsValidoBC=false;
			}
			
			Kaikei.Properties.Settings.Default.Save();
		}
	}
}