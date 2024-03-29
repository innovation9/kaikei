﻿using System;
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
using System.Data;
using System.Data.SqlTypes;
using Kaikei.EstadosContablesTableAdapters;
using Kaikei.Properties;

namespace Kaikei.VistasTabItem
{
    /// <summary>
    /// Interaction logic for viewBalanceGeneral.xaml
    /// </summary>
    public partial class viewBalanceGeneral : UserControl
    {
        rBalanceGeneral bg = new rBalanceGeneral();
        BALANCE_GENERALTableAdapter bgDatos;
        //DataTable Datos;
        

        public viewBalanceGeneral()
        {
            InitializeComponent();
            bgDatos =new BALANCE_GENERALTableAdapter();
            EstadosContables ec =new EstadosContables();
            DateTime hoy = DateTime.Today;
            bg.SetDataSource((DataTable)bgDatos.GetData(hoy));
            bg.SetParameterValue("EmpresaNombre", Kaikei.Properties.Settings.Default.EmpresaNombre);
            bg.SetParameterValue("txtRealizo", Kaikei.Properties.Settings.Default.EmpresaContador);
            bg.SetParameterValue("txtAutorizo", Kaikei.Properties.Settings.Default.EmpresaAdministrador);
            bg.SetParameterValue("FechaReporte",String.Format("AL {0} de {1} de {2}",hoy.Day,
                hoy.ToString("MMMM").ToUpper(),hoy.Year));
            
            this.crvBalanceGeneral.ReportSource = bg;
        }
    }
}
