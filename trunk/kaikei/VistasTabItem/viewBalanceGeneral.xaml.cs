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

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewBalanceGeneral.xaml
    /// </summary>
    public partial class viewBalanceGeneral : UserControl
    {
        rBalanceGeneral bg = new rBalanceGeneral();
        DateTime FechaInicio;
        DateTime FechaFin;
        BALANCE_GENERALTableAdapter bgDatos;
        //DataTable Datos;
        

        public viewBalanceGeneral()
        {
            InitializeComponent();
            bgDatos =new BALANCE_GENERALTableAdapter();

            EstadosContables ec =new EstadosContables();
            wndFechas f = new wndFechas();
            f.Show();
            f.Obtener(ref FechaInicio,ref FechaFin);
            f.Close();


            bg.SetDataSource((DataTable)bgDatos.GetData(FechaInicio,FechaFin));
            this.crvBalanceGeneral.ReportSource = bg;
        }
    }
}
