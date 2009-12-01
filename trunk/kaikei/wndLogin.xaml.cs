using System;
using System.Collections.Generic;
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
using System.Security;
using kaikei.core;

namespace Kaikei
{
	/// <summary>
	/// Interaction logic for wndLogin.xaml
	/// </summary>
	public partial class wndLogin : WindowJMP
	{
		public wndLogin()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuario.Focus();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            //Comprobamos que usuario y pass no se encuentre vacios
            if (String.IsNullOrEmpty(txtUsuario.Text.Trim()))
            {
                MsjError("ERROR! El campo de 'USUARIO' no puede quedar vacio",
                    "Campo 'USUARIO' se encuentra vacio");
                txtUsuario.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Password))
            {
                MsjError("ERROR! El campo de 'CONTRASEÑA' no puede quedar vacio",
                    "Campo 'CONTRASEÑA' se encuentra vacio");
                txtPassword.Focus();
                return;
            }

            //Una vez determinamos que no esten vacios los campos pasamos a comprobar si la contraseña
            //es correcta
            if ((txtUsuario.Text == "admin") && (txtPassword.Password == "kaikei"))
            {
                wndPrincipal wnd = new wndPrincipal();
                wnd.Show();
                this.Close();
            }
            else
            {
                MsjError("Lo sentimos, pero el usuario o contraseña digitada es incorrecta, por favor vuelva a intentarlo",
                    "Usuario o Contraseña invalidas");
                txtPassword.SelectAll();
            }

        }

        private void MsjError(String MsjError, String MsjTitulo)
        {
            MessageBox.Show(MsjError, MsjTitulo, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
	}
}