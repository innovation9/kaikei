using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.SqlTypes;
using Kaikei.EstadosContablesTableAdapters;
using Kaikei.Properties;

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewEstadoCapital.xaml
    /// </summary>
    public partial class viewEstadoCapital : UserControl
    {
        rEstadoCapital bg = new rEstadoCapital();
        DateTime FechaInicio;
        DateTime FechaFin;
        ESTADO_CAPITALTableAdapter bgDatos;

        public viewEstadoCapital()
        {
            InitializeComponent();

            bgDatos = new ESTADO_CAPITALTableAdapter();
            EstadosContables ecDS = new EstadosContables();
            DateTime t = DateTime.Today;
            FechaInicio = new DateTime(t.Year, t.Month, 1);
            FechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);
            DataTable Datos = new DataTable("ESTADO_CAPITAL");
            DataTableReader origen = ecDS.ESTADO_CAPITAL.CreateDataReader();

            Double Saldo = 0.0;

            bgDatos.Fill(ecDS.ESTADO_CAPITAL, FechaInicio, FechaFin);
            Datos.Columns.Add("CODIGO");
            Datos.Columns.Add("NOMBRE");
            Datos.Columns.Add("DEBE",typeof(Double));
            Datos.Columns.Add("HABER", typeof(Double));
            Datos.Columns.Add("SALDO");
            Datos.Columns.Add("TIPO");

            while (origen.Read())
            {
                DataRow row = Datos.NewRow();

                row["CODIGO"] = origen["CODIGO"];
                row["NOMBRE"] = origen["NOMBRE"];
                row["SALDO"] = "";

                if (validateBLX.Validar.IsPositivo((int)Double.Parse(origen["SALDO"].ToString())))
                {
                    row["TIPO"] = "Desinversion";
                    row["DEBE"] = Double.Parse(origen["SALDO"].ToString());
                    Saldo += Double.Parse(origen["SALDO"].ToString());
                    row["HABER"] = 0.0;
                }
                else
                {
                    row["TIPO"] = "Inversion";
                    row["HABER"] = -Double.Parse(origen["SALDO"].ToString());
                    Saldo += Double.Parse(origen["SALDO"].ToString());
                    row["DEBE"] = 0.0;
                }
                Datos.Rows.Add(row);
            }

            bg.SetDataSource(Datos.DefaultView);
            bg.SetParameterValue("EmpresaNombre", Kaikei.Properties.Settings.Default.EmpresaNombre);
            bg.SetParameterValue("txtRealizo", Kaikei.Properties.Settings.Default.EmpresaContador);
            bg.SetParameterValue("txtAutorizo", Kaikei.Properties.Settings.Default.EmpresaAdministrador);
            bg.SetParameterValue("FechaReporte", String.Format("DEL {0} AL {1} DE {2} De {3}", FechaInicio.Day,
                FechaFin.Day, FechaInicio.ToString("MMMM").ToUpper(), FechaInicio.Year));
            bg.SetParameterValue("txtSALDO", Saldo);

            this.crvEstadoCapital.ReportSource = bg;
        }
    }
}
