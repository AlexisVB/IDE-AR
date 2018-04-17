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
using IDE_AR;
using IDE_AR.Datos;
using IDE_AR.DatosGlobales;
using IDE_AR.UsuariosForms;
using System.IO;
using IDE_AR.Settings;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        
        public Login()
        {
            InitializeComponent();
            Settings.Settings cargador = new Settings.Settings();
            ComprobarCredenciales();
            ObtenerConfiguracion();
            
        }
        public void ObtenerConfiguracion()
        {

        }
        private void btnCrearCuenta_Click(object sender, RoutedEventArgs e)
        {
            RegistroDeUsuario nuevoRegistro = new RegistroDeUsuario();
            this.Hide();
            nuevoRegistro.Show();
            this.Close();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            Ingresar();
        }
        private void Ingresar()
        {

            if (validarDatos())
            {
                if (solicitarDatos())
                {
                    Mensaje("Credenciales Correctas");
                    if (ChbMantenerSesion.IsChecked == true)
                    {
                        guardarDatos();
                    }
                    switch (VariablesGlobales.miusuario.Tipo)
                    {
                        case 0:
                            this.Hide();
                            administradorCuentas admin = new administradorCuentas();
                            admin.Show();
                            this.Close();
                            break;
                        case 1:
                            this.Hide();
                            MainWindow nueva = new MainWindow();
                            nueva.Show();
                            this.Close();
                            break;
                        case 2:
                            this.Hide();
                            InterfazAlumnos alumnos = new InterfazAlumnos();
                            alumnos.Show();
                            this.Close();
                            break;
                    }
                }
                else
                    Mensaje("Las credenciales no\nson correctas");
            }
            else
                Mensaje("Las campos no pueden estar vacíos");     
        }
        private void Ingresar(string user, string pass)
        {
                if (solicitarDatos(user, pass))
                {
                    switch (VariablesGlobales.miusuario.Tipo)
                    {
                        case 0:
                            this.Hide();
                            administradorCuentas admin = new administradorCuentas();
                            admin.Show();
                            this.Close();
                            break;
                        case 1:
                            this.Hide();
                            MainWindow nueva = new MainWindow();
                            nueva.Show();
                            this.Close();
                            break;
                        case 2:
                            this.Hide();
                            InterfazAlumnos alumnos = new InterfazAlumnos();
                            alumnos.Show();
                            this.Close();
                            break;
                    }
                }
                else
                    Mensaje("Las credenciales no\nson correctas");
        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();        
        }
       
        public bool validarDatos()
        {
            if (txtUsuario.Text.Length != 0 && txtPassword.Password.Length != 0)
                return true;
            else
                return false;
        }
        public bool solicitarDatos()
        {
            usuario solicitante = InterfaceHttp.GetUsuario(txtUsuario.Text,txtPassword.Password);
            if(solicitante.Nombre!=null)
            {
                VariablesGlobales.miusuario = solicitante;
                return true;
            }
            return false;
        }
        public bool solicitarDatos(string user, string pass)
        {
            usuario solicitante = InterfaceHttp.GetUsuario(user, pass);           
            if (solicitante.Nombre != null)
            {
                VariablesGlobales.miusuario = solicitante;
                return true;
            }
            return false;
        }
        public void guardarDatos()
        {
            string savepass;
            string saveuser;
            string concatenacion;
            string archivo;
            savepass = txtPassword.Password;
            saveuser = txtUsuario.Text;
            concatenacion = saveuser + "/" + savepass;
            Enigma x = new Enigma();
            archivo = x.encriptar(concatenacion);
            //Archivo de mantener sesión iniciada
            FileStream fs;
            string nombre = "log.rup";
            //crea el archivo de texto
            fs = new FileStream(nombre, FileMode.CreateNew, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.Write(archivo);
            sw.Flush();
            sw.Close();
            fs.Close();

        }
        public void Mensaje(string Text)
        {
            this.Opacity = 0.9;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 9;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void txtPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                Ingresar();
        }
        private void ComprobarCredenciales()
        {
            string file;
            file = "log.rup";   
            if (File.Exists(file)==true)
            {
                FileStream fs = new FileStream(file,FileMode.Open,FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin); //Ponemos el cursor de lectura en el caracter 0
                string lectura = sr.ReadToEnd();
                sr.Close();
                fs.Close();                
                Enigma x = new Enigma();
                lectura = x.desencriptar(lectura);
                
                string user = "";
                string password = "";
                int posicion = lectura.IndexOf("/");
                for (int cont=0;cont<posicion;cont++)
                    user += lectura[cont];

                for (int cont=posicion+1;cont<lectura.Length;cont++)
                    password += lectura[cont];
                Ingresar(user, password);

            }            
        }
          
    }
}
