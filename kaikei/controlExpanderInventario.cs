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
using ControlsWPF_JMP.Controls;
using System.ComponentModel;

namespace Kaikei
{
	public class controlExpanderInventario : ExpanderJMP
	{
		public controlExpanderInventario()
		{
			// Insert code required on object creation below this point.
		}
		
		[Category("Contenido")]
		public string Cantidad {get; set;}
		[Category("Contenido")]
		public string Unidades {get; set;}
		[Category("Contenido")]
		public bool IsDisponibilidad {get; set;}

        [Category("Contenido")]
        public string IDMaterial
        {
            get {
                return (string)GetValue(IDMaterialProperty);
            }
            set
            {
                SetValue(IDMaterialProperty, value);
            }
        }
		
		public static DependencyProperty CantidadProperty = DependencyProperty.Register("Cantidad",typeof(string),typeof(controlExpanderInventario),null);
		public static DependencyProperty UnidadesProperty = DependencyProperty.Register("Unidades",typeof(string),typeof(controlExpanderInventario),null);
		public static DependencyProperty IsDisponibilidadProperty = DependencyProperty.Register("IsDisponibilidad",typeof(bool),typeof(controlExpanderInventario),new FrameworkPropertyMetadata(true));
        public static DependencyProperty IDMaterialProperty = DependencyProperty.Register("IDMaterial", typeof(string), typeof(controlExpanderInventario), null);
		
	}
}