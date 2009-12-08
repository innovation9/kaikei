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
using Kaikei.EstadosContablesTableAdapters;
using validateBLX;

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for viewBalanceComprobacion.xaml
	/// </summary>
	public partial class viewBalanceComprobacion : UserControl
	{
        //private BALANCE_COMPROBACIONTableAdapter bcTA;
		public viewBalanceComprobacion()
		{
			this.InitializeComponent();
            //TODO: Ingresar datos de EstadosContables

            BALANCE_COMPROBACIONTableAdapter bcTA = new BALANCE_COMPROBACIONTableAdapter();
            EstadosContables ecDS = new EstadosContables();
            bcTA.Fill(ecDS.BALANCE_COMPROBACION,new DateTime(DateTime.Now.Year,DateTime.Now.Month,1),
                new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day).AddDays(-1));

            dgBalComprobacion.ItemsSource = ecDS.BALANCE_COMPROBACION.DefaultView;
		}
	}
}