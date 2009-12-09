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
using Kaikei.Properties;

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewSettings.xaml
    /// </summary>
    public partial class viewSettings : UserControl
    {
        Settings config;

        public viewSettings()
        {
            InitializeComponent();
            config = Settings.Default;
            this.txtEmpresaNombre.Text = config.EmpresaNombre;
            this.txtEmpresaDireccion.Text = config.EmpresaDireccion;
            this.txtEmpresaTel.Text = config.EmpresaTelefono; 
            this.txtEmpresaNIT.Text = config.EmpresaNIT;
            this.txtIVA.Text = (config.IVA * 100).ToString();
            this.txtEmpresaAdministrador.Text = config.EmpresaAdministrador;
            this.txtEmpresaContador.Text = config.EmpresaContador;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            //TODO: Validar y guardar
        }
    }
}
