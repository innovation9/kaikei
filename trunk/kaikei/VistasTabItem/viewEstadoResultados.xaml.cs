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
    /// Interaction logic for viewEstadoResultados.xaml
    /// </summary>
    public partial class viewEstadoResultados : UserControl
    {
        rEstadoResultados bg = new rEstadoResultados();
        DateTime FechaInicio;
        DateTime FechaFin;
        ESTADO_RESULTADOSTableAdapter bgDatos;

        public viewEstadoResultados()
        {
            InitializeComponent();

            bgDatos = new ESTADO_RESULTADOSTableAdapter();
            EstadosContables ecDS = new EstadosContables();
            DateTime t = DateTime.Today;
            FechaInicio = new DateTime(t.Year, t.Month, 1);
            FechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);
            DataTable Datos = new DataTable("ESTADO_RESULTADOS");
            DataTableReader origen = ecDS.ESTADO_RESULTADOS.CreateDataReader();

            Double Saldo = 0.0;

            bgDatos.Fill(ecDS.ESTADO_RESULTADOS, FechaInicio, FechaFin);
            Datos.Columns.Add("CODIGO");
            Datos.Columns.Add("NOMBRE");
            Datos.Columns.Add("DEBE", typeof(Double));
            Datos.Columns.Add("HABER", typeof(Double));
            Datos.Columns.Add("TIPO");

            while (origen.Read())
            {
                DataRow row = Datos.NewRow();

                row["CODIGO"] = origen["CODIGO"];
                row["NOMBRE"] = origen["NOMBRE"];

                Double s = Double.Parse(origen["DEBE"].ToString()) - Double.Parse(origen["HABER"].ToString());
 
                if (validateBLX.Validar.IsPositivo((int)s))
                {
                    row["TIPO"] = "Gastos";
                    row["DEBE"] = s;
                    Saldo += s;
                    row["HABER"] = 0.0;
                }
                else
                {
                    row["TIPO"] = "Ingresos";
                    row["HABER"] = -s;
                    Saldo += s;
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

            this.crvEstadoResultados.ReportSource = bg;
        }
    }
}
