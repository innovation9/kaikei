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
using Kaikei.PlanillaDSTableAdapters;
//using 

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewEmpleadoNuevo.xaml
    /// </summary>
    public partial class viewEmpleadoNuevo : UserControl
    {

        CEmpleado empleado;
        AFPSTableAdapter afpTA;

        public viewEmpleadoNuevo()
        {
            InitializeComponent();
            txtSalario.Text = "0.0";
            
            //cargar AFP
            afpTA = new AFPSTableAdapter();
            PlanillaDS pDS = new PlanillaDS();
            afpTA.Fill(pDS.AFPS);

            //ingresamos valores a txtAFP
            txtAFP.DataContext = pDS.AFPS.DefaultView;
            txtAFP.DisplayMemberPath = "NOMBRE";
            txtAFP.SelectedValuePath = "ID_AFP";

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
            if (txtNombres.Text.Length == 0 && txtNombres.Text.Length >25)
            {
                txtNombres.Foreground = Brushes.Red;
                ok++;
            }
            if(txtApellidos.Text.Length == 0 && txtApellidos.Text.Length >25)
            {
                txtApellidos.Foreground = Brushes.Red;
                ok++;
            }
            if(txtDireccion.Text.Length == 0 && txtDireccion.Text.Length > 30)
            {
                txtDireccion.Foreground =Brushes.Red;
                ok++;
            }
            if (txtDUI.Text.Length == 0 && txtDUI.Text.Length > 9)
            {
                txtDUI.Foreground = Brushes.Red;
                ok++;
            }
            if (txtNIT.Text.Length == 0 && txtNIT.Text.Length > 14)
            {
                txtNIT.Foreground = Brushes.Red;
                ok++;
            }
            if (txtISSS.Text.Length == 0 && txtISSS.Text.Length > 8)
            {
                txtISSS.Foreground = Brushes.Red;
                ok++;
            }
            if (txtNUP.Text.Length == 0 && txtNUP.Text.Length > 12)
            {
                txtNUP.Foreground = Brushes.Red;
                ok++;
            }
            if (txtEmail.Text.Length == 0 && txtEmail.Text.Length > 25 && !validateBLX.Validar.Email(txtEmail.Text))
            {
                txtEmail.Foreground = Brushes.Red;
                ok++;
            }
            if(txtTelefonoFijo.Text.Length == 0 && txtTelefonoFijo.Text.Length > 8)
            {
                txtTelefonoFijo.Foreground = Brushes.Red;
                ok++;
            }
            if(txtTelefonoMovil.Text.Length == 0 && txtTelefonoMovil.Text.Length > 8)
            {
                txtTelefonoMovil.Foreground = Brushes.Red;
                ok++;
            }
            if (txtSalario.Text.Length > 0 && !Double.TryParse(txtSalario.Text,out p) &&
                !validateBLX.Validar.IsPositivo((int)p))
            {
                txtSalario.Foreground = Brushes.Red;
                ok++;
            }
            if (txtAFP.SelectedValue == null)
            {
                txtAFP.Foreground = Brushes.Red;
                ok++;
            }

            if (ok > 0)
            {
                MessageBox.Show("Verifique los datos.", "KaiKei System", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            empleado = new CEmpleado(new CConeccion(Kaikei.Properties.Settings.Default.KaikeiConnectionString));
            empleado.Nombres = txtNombres.Text;
            empleado.Apellidos = txtApellidos.Text;
            empleado.Direccion = txtDireccion.Text;
            empleado.AFP = new CAfp(new CConeccion(Kaikei.Properties.Settings.Default.KaikeiConnectionString),
                Int32.Parse(txtAFP.SelectedValue.ToString()));
            empleado.DUI = long.Parse(txtDUI.Text);
            empleado.NIT = long.Parse(txtNIT.Text);
            empleado.ISSS = long.Parse(txtISSS.Text);
            empleado.NUP = long.Parse(txtNUP.Text);
            empleado.TelefonoFijo = long.Parse(txtTelefonoFijo.Text);
            empleado.TelefonoMovil = long.Parse(txtTelefonoMovil.Text);
            empleado.Email = txtEmail.Text;
            empleado.Salario = p;
            
            //guardamos
            try
            {
                empleado.sqlInsert();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar. \n" + ex.Message);
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

        private void txtAFP_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAFP.IsDropDownOpen = true;
        }
    }
}
