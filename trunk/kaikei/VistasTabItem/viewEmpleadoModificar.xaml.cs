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
using kaikei.core;
using Kaikei.Properties;

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewEmpleadoNuevo.xaml
    /// </summary>
    public partial class viewEmpleadoModificar : UserControl
    {
        CEmpleado empleado;

        public viewEmpleadoModificar(int Id)
        {
            InitializeComponent();

            //obtener datos
            empleado = new CEmpleado(new CConeccion(Settings.Default.KaikeiConnectionString), Id);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //validar y guardar

            TabItem tabItem = e.Source as TabItem;
            if (tabItem != null)
            {
                TabControl tabControl = tabItem.Parent as TabControl;
                if (tabControl != null)
                    tabControl.Items.Remove(tabItem);

                //Tambien lo eliminamos de la tabla Hash
                //tableTabs.Remove(tabItem.Tag);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //eliminar
            //if(
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //modificar
            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = true;

        }
    }
}
