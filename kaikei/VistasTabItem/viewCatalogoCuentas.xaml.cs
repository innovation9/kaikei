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

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for viewCatalogoCuentas.xaml
	/// </summary>
	public partial class viewCatalogoCuentas : UserControl
	{
        private CLASIFICACIONCUENTASTableAdapter ccTA;
		public viewCatalogoCuentas()
		{
			this.InitializeComponent();
            ccTA = new CLASIFICACIONCUENTASTableAdapter();
            ContaGeneralDS cgDS = new ContaGeneralDS();
            ccTA.Fill(cgDS.CLASIFICACIONCUENTAS);
            dgCCuentas.ItemsSource = cgDS.CLASIFICACIONCUENTAS.DefaultView;
		}
	}
}