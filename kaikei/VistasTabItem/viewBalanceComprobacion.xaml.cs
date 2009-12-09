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
            DataTable datos = new DataTable("BALANCE_COMPROBACION");
            DataTableReader origen = ecDS.BALANCE_COMPROBACION.CreateDataReader();
            Double tHaber = 0.0;
            Double tDebe = 0.0;

            bcTA.Fill(ecDS.BALANCE_COMPROBACION,new DateTime(DateTime.Now.Year,DateTime.Now.Month,1),
                new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day).AddDays(-1));

            datos.Columns.Add("NOMBRE");
            datos.Columns.Add("DESCRIPCION");
            datos.Columns.Add("DEBE");
            datos.Columns.Add("HABER");

            while (origen.Read())
            {
                DataRow row = datos.NewRow();

                row["NOMBRE"] = origen["NOMBRE"].ToString();
                row["DESCRIPCION"] = origen["DESCRIPCION"].ToString();
                if (Int32.Parse(origen["TIPO"].ToString()) >= 8)
                {
                    //activos y pasibos
                    if (Validar.IsPositivo((Int32)Double.Parse(origen["SALDO"].ToString())))
                    {
                        row["HABER"] = "";
                        row["DEBE"] = origen["SALDO"].ToString();
                        tDebe += Double.Parse(origen["SALDO"].ToString());
                    }
                    else
                    {
                        row["DEBE"] = "";
                        row["HABER"] = (-(Double.Parse(origen["SALDO"].ToString()))).ToString();
                        tHaber += -(Double.Parse(origen["SALDO"].ToString()));
                    }
                }
                else
                {
                    //capital
                    if (Validar.IsPositivo((Int32)Double.Parse(origen["SALDO"].ToString())))
                    {
                        row["DEBE"] = "";
                        row["HABER"] = origen["SALDO"].ToString();
                        tHaber += Double.Parse(origen["SALDO"].ToString());
                    }
                    else
                    {
                        row["HABER"] = "";
                        row["DEBE"] = (-(Double.Parse(origen["SALDO"].ToString()))).ToString();
                        tDebe += -(Double.Parse(origen["SALDO"].ToString()));
                    }
                }
                datos.Rows.Add(row);
            }

            datos.Rows.Add(datos.NewRow());
            DataRow Total = datos.NewRow();
            Total["NOMBRE"] = "";
            Total["DESCRIPCION"] = "TOTALES";
            Total["DEBE"] = tDebe.ToString();
            Total["HABER"] = tHaber.ToString();
            datos.Rows.Add(Total);

            dgBalComprobacion.ItemsSource = datos.DefaultView;
		}
	}
}