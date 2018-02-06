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
            
        }
        private void btnCrearCuenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if(validarDatos())
            {
                if (solicitarDatos())
                {
                    Mensaje("Credenciales Correctas");
                    if(ChbMantenerSesion.IsChecked==true)
                    {
                        guardarDatos();
                    }
                    switch(VariablesGlobales.miusuario.Tipo)
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
                            break;
                    }
                }                    
                else
                    Mensaje("Las credenciales no\nson correctas");
            }
            else
               Mensaje("Las campos no pueden estar vacíos");       
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
        public void guardarDatos()
        {

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
          
    }
}
