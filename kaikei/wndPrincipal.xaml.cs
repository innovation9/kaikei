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
using System.Windows.Shapes;
using ControlsWPF_JMP.Ventanas;
using kaikei.core;

namespace Kaikei
{
    /// <summary>
    /// Lógica de interacción para wndPrincipal.xaml
    /// </summary>
    public partial class wndPrincipal : Window
    {
        CConeccion coneccion = new CConeccion();

        public wndPrincipal()
        {
            InitializeComponent();
            this.tabNuevoEmpleado.Visibility = Visibility.Hidden;
        }

        private void btnAdminEmpleado_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNuevoEmpleado_Click(object sender, RoutedEventArgs e)
        {
            TabItem tb = new TabItem();
            this.tabNuevoEmpleado.Visibility = Visibility.Visible;
            this.tabNuevoEmpleado.Focus();
        }
    }
}
