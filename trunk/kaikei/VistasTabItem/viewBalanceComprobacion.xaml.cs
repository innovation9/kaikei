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
using Kaikei.ReportesDSTableAdapters;

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for viewBalanceComprobacion.xaml
	/// </summary>
	public partial class viewBalanceComprobacion : UserControl
	{
        private BALANCE_COMPROBACIONTableAdapter bcTA;
		public viewBalanceComprobacion()
		{
			this.InitializeComponent();

            bcTA = new BALANCE_COMPROBACIONTableAdapter();
            ReportesDS cgDS = new ReportesDS();
            bcTA.Fill(cgDS.BALANCE_COMPROBACION);
			dgBalComprobacion.ItemsSource=cgDS.BALANCE_COMPROBACION.DefaultView;
		}
	}
}