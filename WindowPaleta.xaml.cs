using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IDE_AR.Datos;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for WindowPaleta.xaml
    /// </summary>
    public partial class WindowPaleta : Window
    {
        private miColor colorRojo1=new miColor();
        private miColor colorRosa1 = new miColor();
        private miColor colorMorado1 = new miColor();
        private miColor colorMoradoProfundo1 = new miColor();
        private miColor colorAzulIndigo1 = new miColor();
        private miColor colorAzul1 = new miColor();
        private miColor colorAzulClaro1 = new miColor();
        private miColor colorCyan1 = new miColor();
        private miColor colorTeal1 = new miColor();
        private miColor colorVerde1 = new miColor();
        private miColor colorVerdeClaro1 = new miColor();
        private miColor colorLima1 = new miColor();
        private miColor colorAmarillo1 = new miColor();
        private miColor colorAmbar1 = new miColor();
        private miColor colorNaranja1 = new miColor();
        private miColor colorNaranjaOscuro1 = new miColor();
        private miColor colorCafe1 = new miColor();
        private miColor colorGris1 = new miColor();
        private miColor colorDefault = new miColor();
        public miColor colorAgregar = new miColor();
        public WindowPaleta()
        {
            InitializeComponent();
            InicializarColores();
            AsignarContextos();
        }
        private void InicializarColores()
        {
            colorRojo1.ColorName = "Color rojo";
            colorRojo1.ColorHex = "#d50000";

            colorRosa1.ColorName = "Color rosa";
            colorRosa1.ColorHex = "#f50057";

            colorMorado1.ColorName = "Color morado";
            colorMorado1.ColorHex = "#6a1b9a";

            colorMoradoProfundo1.ColorName = "Color morado profundo";
            colorMoradoProfundo1.ColorHex = "#651fff";

            colorAzulIndigo1.ColorName = "Color azul indigo";
            colorAzulIndigo1.ColorHex = "#303f9f";

            colorAzul1.ColorName = "Color azul";
            colorAzul1.ColorHex = "#2196f3";

            colorAzulClaro1.ColorName = "Color azul claro";
            colorAzulClaro1.ColorHex = "#03a9f4";

            colorCyan1.ColorName = "Color cyan";
            colorCyan1.ColorHex = "#00bcd4";

            colorTeal1.ColorName = "Color teal";
            colorTeal1.ColorHex = "#009688";

            colorVerde1.ColorName = "Color verde";
            colorVerde1.ColorHex = "#4caf50";

            colorVerdeClaro1.ColorName = "Color verde claro";
            colorVerdeClaro1.ColorHex = "#8bc34a";

            colorLima1.ColorName = "Color lima";
            colorLima1.ColorHex = "#cddc39";

            colorAmarillo1.ColorName = "Color amarillo";
            colorAmarillo1.ColorHex = "#ffeb3b";

            colorAmbar1.ColorName = "Color ambar";
            colorAmbar1.ColorHex = "#ffc107";

            colorNaranja1.ColorName = "Color naranja";
            colorNaranja1.ColorHex = "#ff9800";

            colorNaranjaOscuro1.ColorName = "Color naranja oscuro";
            colorNaranjaOscuro1.ColorHex = "#ff5722";

            colorCafe1.ColorName = "Color café";
            colorCafe1.ColorHex = "#4e342e";

            colorGris1.ColorName = "Color gris";
            colorGris1.ColorHex = "#616161";

            colorDefault.ColorName = "Color default";
            colorDefault.ColorHex = "#202225";

            colorAgregar.ColorName = "Color a agregar";
            colorAgregar.ColorHex = "#202225";
        }
        private void AsignarContextos()
        {
            btnRojo.DataContext = colorRojo1;
            btnRosa.DataContext = colorRosa1;
            btnMorado.DataContext = colorMorado1;
            btnMoradoProfundo.DataContext = colorMoradoProfundo1;
            btnAzulIndigo.DataContext = colorAzulIndigo1;
            btnAzul.DataContext = colorAzul1;
            btnAzulClaro.DataContext = colorAzulClaro1;
            btnCyan.DataContext = colorCyan1;
            btnTeal.DataContext = colorTeal1;
            btnVerde.DataContext = colorVerde1;
            btnVerdeClaro.DataContext = colorVerdeClaro1;
            btnLima.DataContext = colorLima1;
            btnAmarillo.DataContext = colorAmarillo1;
            btnAmbar.DataContext = colorAmbar1;
            btnNaranja.DataContext = colorNaranja1;
            btnNaranjaOscuro.DataContext = colorNaranjaOscuro1;
            btnCafe.DataContext = colorCafe1;
            btnGris.DataContext = colorGris1;
            btnDefault.DataContext = colorDefault;
        }
        private void btnRojo_click(Object sender,RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorRojo1.ColorHex;
            DialogResult = true;
        }
        private void btnRosa_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorRosa1.ColorHex;
            DialogResult = true;
        }
        private void btnMorado_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorMorado1.ColorHex;
            DialogResult = true;
        }
        private void btnMoradoProfundo_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorMoradoProfundo1.ColorHex;
            DialogResult = true;
        }
        private void btnAzulIndigo_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorAzulIndigo1.ColorHex;
            DialogResult = true;
        }
        private void btnAzul_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorAzul1.ColorHex;
            DialogResult = true;
        }
        private void btnAzulClaro_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorAzulClaro1.ColorHex;
            DialogResult = true;
        }
        private void btnCyan_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorCyan1.ColorHex;
            DialogResult = true;
        }
        private void btnTeal_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorTeal1.ColorHex;
            DialogResult = true;
        }
        private void btnVerde_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorVerde1.ColorHex;
            DialogResult = true;
        }
        private void btnVerdeClaro_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorVerdeClaro1.ColorHex;
            DialogResult = true;
        }
        private void btnLima_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorLima1.ColorHex;
            DialogResult = true;
        }
        private void btnAmarillo_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorAmarillo1.ColorHex;
            DialogResult = true;
        }
        private void btnAmbar_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorAmbar1.ColorHex;
            DialogResult = true;
        }
        private void btnNaranja_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorNaranja1.ColorHex;
            DialogResult = true;
        }
        private void btnNaranjaOscuro_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorNaranjaOscuro1.ColorHex;
            DialogResult = true;
        }
        private void btnCafe_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorCafe1.ColorHex;
            DialogResult = true;
        }
        private void btnGris_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorGris1.ColorHex;
            DialogResult = true;
        }
        private void btnDefault_click(Object sender, RoutedEventArgs e)
        {
            colorAgregar.ColorHex = colorDefault.ColorHex;
            DialogResult = true;
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
