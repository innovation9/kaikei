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
using ControlsWPF_JMP.Ventanas;
using System.Windows.Interop;


namespace kaikei
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class frmPlantilla : Window
    {
        public frmPlantilla()
        {
            InitializeComponent();
        }

        private void Plantilla_Loaded(object sender, RoutedEventArgs e)
        {
            GlassVentana wndGlass = new GlassVentana(15, 15, 15, 15, new WindowInteropHelper(this).Handle);
            wndGlass.AplicarGlass();
        }
    }
}
