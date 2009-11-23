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



public class ctlPlantilla : UserControl
{
    
    private string m_Titulo;
    private ImageSource m_Imagen;
    private ImageBrush m_FondoT;
    private ImageBrush m_FondoB;
    
    //Variables que almacenan las imagenes por defecto de las Ventanas que son el color de fondo de la barra de
    //titulo y el fondo de los barra de botones de las ventanas
    private static ImageSource m_imgFondoTitulo_Default = new BitmapImage(new Uri("pack://application:,,,/kaikei;component/Themes/imgPanelTitulos.jpg"));
    private static ImageSource m_imgFondoBotones_Default = new BitmapImage(new Uri("pack://application:,,,/kaikei;component/Themes/imgPanelBotones.png"));
    
    static ctlPlantilla()
    {
        //Esta llamada a OverrideMetadata indica al sistema que este elemento desea proporcionar un estilo diferente al de su clase base.
        //Este estilo se define en themes\generic.xaml
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ctlPlantilla), new FrameworkPropertyMetadata(typeof(ctlPlantilla)));
    }
    
    [Category("Contenido")]
    public string Titulo {
        get { return m_Titulo; }
        set { m_Titulo = value; }
    }
    
    [Category("Contenido")]
    public ImageSource Imagen {
        get { return m_Imagen; }
        set { m_Imagen = value; }
    }
    
    [Category("Contenido")]
    public ImageBrush FondoTitulo {
        get { return m_FondoT; }
        set { m_FondoT = value; }
    }
    
    [Category("Contenido")]
    public ImageBrush FondoBotones {
        get { return m_FondoB; }
        set { m_FondoB = value; }
    }
    
    /// <summary>
    /// Devuelve la imagen de fondo del Panel de titulo de la ventana en el Tema por defecto
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public static ImageSource GetFondoTituloDefault {
        get { return m_imgFondoTitulo_Default; }
    }
    
    /// <summary>
    /// Devuelve la imagen de fondo del panel de botones de la ventan en el tema por defecto
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public static ImageSource GetFondoBotonesDefault {
        get { return m_imgFondoBotones_Default; }
    }


    public static readonly DependencyProperty TituloProperty = DependencyProperty.Register("Titulo", typeof(string), typeof(ctlPlantilla), new PropertyMetadata("Plantilla"));

    public static readonly DependencyProperty ImagenProperty = DependencyProperty.Register("Imagen", typeof(ImageSource), typeof(ctlPlantilla), new PropertyMetadata(null));

    public static readonly DependencyProperty FondoTituloProperty = DependencyProperty.Register("FondoTitulo", typeof(Brush), typeof(ctlPlantilla), new PropertyMetadata(new ImageBrush(GetFondoTituloDefault)));

    public static readonly DependencyProperty FondoBotonesProperty = DependencyProperty.Register("FondoBotones", typeof(Brush), typeof(ctlPlantilla), new PropertyMetadata(new ImageBrush(GetFondoBotonesDefault)));
}



}
