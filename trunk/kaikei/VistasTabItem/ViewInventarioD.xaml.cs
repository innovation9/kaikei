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
using Kaikei;
using Kaikei.InventariosDSTableAdapters;
using Microsoft.Windows.Controls;

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Lógica de interacción para ViewInventarioD.xaml
    /// </summary>
    public partial class ViewInventarioD : UserControl
    {
        private controlExpanderInventario ctlExpander;
        private INVENTARIO_CARDEXTableAdapter iTA;

        public ViewInventarioD()
        {
            InitializeComponent();
            iTA = new INVENTARIO_CARDEXTableAdapter();
        }

        void ctlExpander_Expanded(object sender, RoutedEventArgs e)
        {
            //Primero Interceptamos el Expander que provoco el evento
            ctlExpander = sender as controlExpanderInventario;

            //Ahora recuperamos el valor del ID_MATERIAL para la consulta de su detalle
            string IDM = ctlExpander.IDMaterial;

            //Instaciamos el TA y cargamos la Consulta por el ID_Material
            ListBox dg = (ListBox)ctlExpander.Content;
			InventariosDS iDS = new InventariosDS();
            //dg.Items.Clear();
            iTA.FillByID(iDS.INVENTARIO_CARDEX, Int32.Parse(IDM));
            dg.ItemsSource= iDS.INVENTARIO_CARDEX;

            
        }

        void ctlExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            //Primero Interceptamos el Expander que provoco el evento
            ctlExpander = sender as controlExpanderInventario;

            //Limpiamos el grid y desconectamos
            ListBox dg = (ListBox)ctlExpander.Content;
            dg.ItemsSource = null;
        }

    }
}
