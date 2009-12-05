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
using System.Windows.Shapes;

namespace Kaikei.ConnectionDS
{
	public class InventariosDataProvider
	{
		private static InventariosDS invDS;
		
		public static InventariosDS GetInventariosDS {
			get {
				if (invDS == null) invDS = new InventariosDS();
				return invDS;
			}
			
		}
	}
}