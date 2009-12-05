using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kaikei.InventariosDSTableAdapters;
using Kaikei.ConnectionDS;
using System.Data;

namespace Kaikei.ConnectionDS
{
    class InventarioDisp_Provider
    {
		private INVENTARIO_DISPONIBLETableAdapter m_InventarioTA;
		
		public InventarioDisp_Provider() {
			m_InventarioTA = new INVENTARIO_DISPONIBLETableAdapter();
			m_InventarioTA.Fill(InventariosDataProvider.GetInventariosDS.INVENTARIO_DISPONIBLE);
		}
		
		public DataView GetView() {
			return InventariosDataProvider.GetInventariosDS.INVENTARIO_DISPONIBLE.DefaultView;
		}
		
		
    }
}
