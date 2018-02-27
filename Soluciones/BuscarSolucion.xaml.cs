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
using Microsoft.Win32;
using System.IO;
using IDE_AR.DatosGlobales;
namespace IDE_AR.Soluciones
{
    /// <summary>
    /// Interaction logic for BuscarSolucion.xaml
    /// </summary>
    public partial class BuscarSolucion : Window
    {
        public SolucionProyecto solucion;
        string ruta = VariablesGlobales.RutaPredeterminada;
        public BuscarSolucion()
        {
            InitializeComponent();
            btnAceptar.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog seleccionador= new OpenFileDialog();
            seleccionador.Filter="ficheros ar(*.ar)|*.ar|Todos (*.*)|*.*";
            seleccionador.FilterIndex=1;
            seleccionador.CheckFileExists = false;
            seleccionador.CheckPathExists = false;            
            
            if(seleccionador.ShowDialog()==true)
            {
                
                string filename = seleccionador.FileName;
                lbRuta.Text = filename;
                adminSolucion admin =new adminSolucion();
                if(validarArchivo(seleccionador.FileName))
                {
                    btnAceptar.IsEnabled = true;
                    solucion = admin.ObtenerProyecto(filename);
                    //crear el arbol de navegacion a la inversa
                    //es decir de hijos a padres

                    admin.miSolucion = solucion;
                    admin.AsignarPadres();
                }                    
                else
                {
                    Mensaje("Archivo inválido");
                    btnAceptar.IsEnabled = false;
                }

            }
        }
        private bool validarArchivo(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            char[] buffer = new char[9];
            sr.Read(buffer,0,9);
            string encabezado = "";
            for(int cont=0;cont<9;cont++)
            {
                encabezado += buffer[cont];
            }            
            sr.Close();
            fs.Close();
            if(encabezado==".arIDE-AR")
                return true;
            return false;
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            
            DialogResult = true;
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
