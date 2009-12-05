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
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Interop;

namespace kaikei {



public class ctlPlantillaTabItem : UserControl
{
    
    private string m_Titulo;
    private ImageBrush m_FondoB;
    
    //Variables que almacenan las imagenes por defecto de las Ventanas que son el color de fondo de la barra de
    //titulo y el fondo de los barra de botones de las ventanas
    private static ImageSource m_imgFondoBotones_Default = new BitmapImage(new Uri("pack://application:,,,/kaikei;component/Themes/imgPanelBotones.png"));
    
    static ctlPlantillaTabItem()
    {
        //Esta llamada a OverrideMetadata indica al sistema que este elemento desea proporcionar un estilo diferente al de su clase base.
        //Este estilo se define en themes\generic.xaml
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ctlPlantillaTabItem), new FrameworkPropertyMetadata(typeof(ctlPlantillaTabItem)));
    }
    
    [Category("Contenido")]
    public string SubTitulo {
        get { return m_Titulo; }
        set { m_Titulo = value; }
    }
    
    [Category("Contenido")]
    public ImageBrush FondoBoton {
        get { return m_FondoB; }
        set { m_FondoB = value; }
    }
	
	[Category("Contenido")]
	public string Descripcion { get; set;}
    
    /// <summary>
    /// Devuelve la imagen de fondo del panel de botones de la ventan en el tema por defecto
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public static ImageSource GetFondoBotonesDefault {
        get { return m_imgFondoBotones_Default; }
    }


    public static readonly DependencyProperty SubTituloProperty = DependencyProperty.Register("SubTitulo", typeof(string), typeof(ctlPlantillaTabItem), new PropertyMetadata("Plantilla"));

	public static readonly DependencyProperty DescripcionProperty = DependencyProperty.Register("Descripcion", typeof(string), typeof(ctlPlantillaTabItem), new PropertyMetadata("Una pequeña descripcion sobre la ventana"));

    public static readonly DependencyProperty FondoBotonProperty = DependencyProperty.Register("FondoBoton", typeof(Brush), typeof(ctlPlantillaTabItem), new PropertyMetadata(new ImageBrush(GetFondoBotonesDefault)));
}



}
