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
using System.Windows.Forms;
using System.IO;
using IDE_AR.DatosGlobales;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for configuracion.xaml
    /// </summary>
    public partial class configuracion : Window
    {
        public configuracion()
        {
            InitializeComponent();
            lbRuta.Text = VariablesGlobales.RutaPredeterminada;
        }

        private void btnSelectRuta_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog select = new FolderBrowserDialog();            
            if (select.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {

            }
            string ruta=select.SelectedPath;
            lbRuta.Text = ruta;
            if (ruta.Length < 0)
                Mensaje("La ruta seleccionada no existe");
            else
            {
                GuardarLocacionPredeterminada(ruta);
            }
        }
        public void GuardarLocacionPredeterminada(string ruta)
        {
            string dir = "Settings";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string ar = "Settings//defaultLocation.bin";                        
            //crea el archivo de texto   
            VariablesGlobales.RutaPredeterminada = ruta;
            FileStream fs = new FileStream(ar, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.Write(ruta);
            sw.Flush();
            sw.Close();
            fs.Close();
            
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void Mensaje(string Text)
        {
            this.Opacity = 0.5;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 1;
        }
    }
}
