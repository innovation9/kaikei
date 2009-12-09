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
        //DataTable Datos;

        public viewEstadoCapital()
        {
            InitializeComponent();

            bgDatos = new ESTADO_CAPITALTableAdapter();
            EstadosContables ec = new EstadosContables();
            DateTime t = DateTime.Today;
            FechaInicio = new DateTime(t.Year, t.Month, 1);
            FechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);
            bg.SetDataSource((DataTable)bgDatos.GetData());
            bg.SetParameterValue("EmpresaNombre", Kaikei.Properties.Settings.Default.EmpresaNombre);
            bg.SetParameterValue("txtRealizo", Kaikei.Properties.Settings.Default.EmpresaContador);
            bg.SetParameterValue("txtAutorizo", Kaikei.Properties.Settings.Default.EmpresaAdministrador);
            bg.SetParameterValue("FechaReporte", String.Format("DEL {0}/{1}/{2} AL {3}/{1}/{2}", FechaInicio.Day,
                FechaInicio.Month, FechaInicio.Year, FechaFin.Day));

            this.crvEstadoCapital.ReportSource = bg;
        }
    }
}
