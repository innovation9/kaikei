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
using System.Collections;
using Kaikei.VistasTabItem;
using kaikei;

namespace Kaikei
{
    /// <summary>
    /// Lógica de interacción para wndPrincipal.xaml
    /// </summary>
    public partial class wndPrincipal : Window
    {
        CConeccion coneccion = new CConeccion();
        Hashtable tableTabs = new Hashtable();

        public wndPrincipal()
        {
            InitializeComponent();

            //Tenemos que determinar el periodo contable en que nos encontramos, sabemos que el
            //periodo comprende en un mes asi que calculamos la Fecha de Inicio del Mes y la fecha de
            //finalizacion del mes
            Kaikei.Properties.Settings.Default.FechaIP = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            Kaikei.Properties.Settings.Default.FechaFP = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            Kaikei.Properties.Settings.Default.Save();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.VisualizarInventarioDisp);
        }

        private enum TipoTab
        {
            VisualizarInventarioDisp,
            EfectuarTransaccion,
            ComprasInventarios,
            BalanceGeneral,
            BalanceComprobacion,
            EstadoCapital,
            EstadoResultados,
            CatalogoCuentas,
            EmpleadoAgregar,
            EmpleadoGestionar,
            Planilla,
            Propiedades,
            OrdenFabricacion
        }

        private void CargarTabItem(TipoTab tipo)
        {
            //Primero buscamos en la lista de tab que no exista
            if (!tableTabs.ContainsKey(tipo))
            {
                CloseableTabItem newTab = new CloseableTabItem();
                this.AddHandler(CloseableTabItem.CloseTabEvent, new RoutedEventHandler(this.CloseTab));

                switch (tipo)
                {
                    case TipoTab.VisualizarInventarioDisp:
                        newTab.Name = "tabInventarioD";
                        newTab.Header = "Inventario KARDEX";
                        newTab.Content = new ViewInventarioD();
                        break;
                    case TipoTab.EfectuarTransaccion:
                        newTab.Name = "tabEfecTransacciones";
                        newTab.Header = "Transacciones";
                        newTab.Content = new viewTransacciones();
                        break;
					case TipoTab.ComprasInventarios:
                        newTab.Name = "tabComprasInv";
                        newTab.Header = "Compras";
                        newTab.Content = new viewComprasInv();
                        break;
                    case TipoTab.BalanceGeneral:
                        newTab.Name = "tabBalGral";
                        newTab.Header = "Balance General";
                        newTab.Content = new viewBalanceGeneral();
                        break;
                    case TipoTab.CatalogoCuentas:
                        newTab.Name = "tabCCuentas";
                        newTab.Header = "Catalogo Cuentas";
                        newTab.Content = new viewCatalogoCuentas();
                        break;
                    case TipoTab.BalanceComprobacion:
                        newTab.Name = "tabBalC";
                        newTab.Header = "Balance de Comprobacion";
                        newTab.Content = new viewBalanceComprobacion();
                        break;
                    case TipoTab.OrdenFabricacion:
                        newTab.Name = "tabOrdenFab";
                        newTab.Header = "Orden Fabricación";
                        newTab.Content = new viewOrdenFabricacion();
                        break;

                    case TipoTab.EstadoCapital:
                        newTab.Name = "tabEstCap";
                        newTab.Header = "Estado de Capital";
                        newTab.Content = new viewEstadoCapital();
                        break;

                    case TipoTab.EstadoResultados:
                        newTab.Name = "tabEstRes";
                        newTab.Header = "Estado de Resultados";
                        newTab.Content = new viewEstadoResultados();
                        break;

                    case TipoTab.EmpleadoAgregar:
                        newTab.Name = "tabNewEmp";
                        newTab.Header = "Agregar Empleado";
                        newTab.Content = new viewEmpleadoNuevo();
                        break;

                    case TipoTab.EmpleadoGestionar:
                        newTab.Name = "tabEmpGes";
                        newTab.Header = "Gestionar Empleados";
                        newTab.Content = new viewEmpleados();
                        break; 

                    case TipoTab.Propiedades:
                        newTab.Name = "tabPropiedades";
                        newTab.Header = "Propiedades";
                        newTab.Content = new viewSettings();
                        break; 
                }

                //Agregamos a la tabla Hash el nuevo control
                newTab.Tag = tipo;
                tableTabs.Add(tipo, newTab);
                tab_Ventanas.Items.Add(newTab);
                tab_Ventanas.SelectedIndex = tab_Ventanas.Items.Count - 1;
                newTab.Focus();
            }
        }

        private void CloseTab(object source, RoutedEventArgs args)
        {
            TabItem tabItem = args.Source as TabItem;
            if (tabItem != null)
            {
                TabControl tabControl = tabItem.Parent as TabControl;
                if (tabControl != null)
                    tabControl.Items.Remove(tabItem);

                //Tambien lo eliminamos de la tabla Hash
                tableTabs.Remove(tabItem.Tag);
            }
        }

        private void mnu_EfecTransac_Click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.EfectuarTransaccion);
        }
		
		private void mnu_ComprasInv_Click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.ComprasInventarios);
        }

        private void mnu_BalGral_click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.BalanceGeneral);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.CatalogoCuentas);
        }

        private void mnu_BalCompro_CLICK(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.BalanceComprobacion);
        }

        private void btnPropiedades_Click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.Propiedades);
        }

        private void btnOFabriacion_Click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.OrdenFabricacion);
        }

        private void btnNuevoEmpleado_Click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.EmpleadoAgregar);
        }

        private void btnAdminEmpleado_Click(object sender, RoutedEventArgs e)
        {
            CargarTabItem(TipoTab.EmpleadoGestionar);
        }

    }
}
