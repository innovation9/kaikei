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
using Kaikei.PlanillaDSTableAdapters;
using System.Data;

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewEmpleados.xaml
    /// </summary>
    public partial class viewEmpleados : UserControl
    {

        EMPLEADOS_AFPTableAdapter eaTA;
        public viewEmpleados()
        {
            InitializeComponent();
            eaTA = new EMPLEADOS_AFPTableAdapter();
            PlanillaDS cgDS = new PlanillaDS();
            eaTA.Fill(cgDS.EMPLEADOS_AFP);
            this.dgEmpleadosAFP.ItemsSource = cgDS.EMPLEADOS_AFP.DefaultView;
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
