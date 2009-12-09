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
//using 

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewEmpleadoNuevo.xaml
    /// </summary>
    public partial class viewEmpleadoNuevo : UserControl
    {


        public viewEmpleadoNuevo()
        {
            InitializeComponent();
            txtDUI.Text = "        - ";
            txtNIT.Text = "   -      -   - ";
            txtSalario.Text = "0.0";
            
            //cargar AFP


        }

        private void txtNombres_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //validar
            Double p = new Double();
            int ok = 0;

            //if(!validateBLX.Validar.DUI(txtDUI.Text))
            if ((txtNombres.Text.Length + txtApellidos.Text.Length + txtDireccion.Text.Length +
                txtNUP.Text.Length + txtISSS.Text.Length) > 0)
            {
                ok++;
            }

            if (txtEmail.Text.Length > 0 && !validateBLX.Validar.Email(txtEmail.Text))
            {
                txtEmail.Foreground = Brushes.Red;
                ok++;
            }
            if (txtSalario.Text.Length > 0 && !Double.TryParse(txtSalario.Text,out p) &&
                !validateBLX.Validar.IsPositivo((int)p))
            {
                txtSalario.Foreground = Brushes.Red;
                ok++;
            }

            //guardamos
            if (ok > 0)
            {
                MessageBox.Show("Verifique los datos.", "KaiKei System", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("El Registro de a guardado correctamente.", "KaiKei System",
                MessageBoxButton.OK, MessageBoxImage.Information);
            //limpiamos
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDireccion.Text = "";
            txtAFP.SelectedIndex = -1;
            txtDUI.Text = "";
            txtNIT.Text = "";
            txtISSS.Text = "";
            txtNUP.Text = "";
            txtTelefonoFijo.Text = "";
            txtTelefonoMovil.Text = "";
            txtEmail.Text = "";
            txtSalario.Text = "0.0";
        }
    }
}
