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
using IDE_AR.Datos;

namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for ConfiguracionProfesor.xaml
    /// </summary>
    public partial class ConfiguracionProfesor : Window
    {
        public ConfiguracionProfesor()
        {
            InitializeComponent();
            lbRuta.Text = VariablesGlobales.RutaPredeterminada;
            ObtenerDatos();
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
            string ruta = select.SelectedPath;
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
        public void ObtenerDatos()
        {
            tbUser.Text = VariablesGlobales.miusuario.NombreUsuario;
            tbRegistro.Text = VariablesGlobales.miusuario.Registro;
            tbCorreo.Text = VariablesGlobales.miusuario.Correo;


        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            btnCancelar.IsEnabled = true;
            btnCancelar.Visibility = Visibility.Visible;
            btnGuardar.IsEnabled = true;
            btnGuardar.Visibility = Visibility.Visible;
            btnEditar.IsEnabled = false;
            btnEditar.Visibility = Visibility.Hidden;
            Mostrar.Visibility = Visibility.Hidden;
            Editar.Visibility = Visibility.Visible;
            txtUser.Text = VariablesGlobales.miusuario.NombreUsuario;
            txtRegistro.Text = VariablesGlobales.miusuario.Registro;
            txtCorreo.Text = VariablesGlobales.miusuario.Correo;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnCancelar.IsEnabled = false;
            btnCancelar.Visibility = Visibility.Hidden;
            btnGuardar.IsEnabled = false;
            btnGuardar.Visibility = Visibility.Hidden;
            btnEditar.IsEnabled = true;
            btnEditar.Visibility = Visibility.Visible;
            Mostrar.Visibility = Visibility.Visible;
            Editar.Visibility = Visibility.Hidden;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            usuario x = VariablesGlobales.miusuario;
            x.NombreUsuario = txtUser.Text;
            x.Correo = txtCorreo.Text;
            x.Registro = txtRegistro.Text;
            if (InterfaceHttp.modificarUsuario(x))
            {
                string h = "Modificaciones exitosas";
                Mensaje(h);

            }
            else
            {
                string h = "Error";
                Mensaje(h);
            }
            btnCancelar.IsEnabled = false;
            btnCancelar.Visibility = Visibility.Hidden;
            btnGuardar.IsEnabled = false;
            btnGuardar.Visibility = Visibility.Hidden;
            btnEditar.IsEnabled = true;
            btnEditar.Visibility = Visibility.Visible;
            Mostrar.Visibility = Visibility.Visible;
            Editar.Visibility = Visibility.Hidden;
            ObtenerDatos();


        }
    }
}