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

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for viewInventarioKardex.xaml
	/// </summary>
	public partial class viewInventarioKardex : UserControl
	{
		public viewInventarioKardex()
		{
			this.InitializeComponent();
            INVENTARIO_CARDEXTableAdapter ita = new INVENTARIO_CARDEXTableAdapter();
            InventariosDS ids = new InventariosDS();
            ita.Fill(ids.INVENTARIO_CARDEX);
            this.DataContext = ids.INVENTARIO_CARDEX;
		}
	}
}