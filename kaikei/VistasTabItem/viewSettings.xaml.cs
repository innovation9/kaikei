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
using kaikei.core;

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
            this.txtUsuario.Text = config.Usuario;
            this.txtPasword.Text = config.Password;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Validar

            config.EmpresaNombre = txtEmpresaNombre.Text;
            config.EmpresaDireccion = txtEmpresaDireccion.Text;
            config.EmpresaTelefono = txtEmpresaTel.Text;
            config.EmpresaNIT = txtEmpresaNIT.Text;
            config.IVA = Double.Parse(txtIVA.Text) / 100;
            config.EmpresaAdministrador = txtEmpresaAdministrador.Text;
            config.EmpresaContador = txtEmpresaContador.Text;
            config.Usuario = txtUsuario.Text;
            config.Password = txtPasword.Text;

            
            //No implementar
            //config.Usuario = CSeguro.GetMD5(txtUsuario.Text);
            //config.Password = CSeguro.GetMD5(txtPasword.Text);
            config.Save();
            MessageBox.Show("Propiedades Guardadas", "KaiKei System", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
