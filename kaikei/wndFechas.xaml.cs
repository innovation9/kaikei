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
using System.Data;

namespace Kaikei
{
    /// <summary>
    /// Interaction logic for wndFechas.xaml
    /// </summary>
    public partial class wndFechas : Window
    {
        DateTime fInicio;
        DateTime fFin;

        public wndFechas()
        {
            InitializeComponent();
        }

        public void Obtener(ref DateTime inicio, ref DateTime fin)
        {
            inicio = this.fInicio;
            fin = this.fFin;            
        }

        private void txtFechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            fInicio = (DateTime)txtFechaInicio.SelectedDate;
        }

        private void txtFechaFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            fFin = (DateTime)txtFechaFin.SelectedDate;
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

       
    }
}
