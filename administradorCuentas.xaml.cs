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
    /// Interaction logic for administradorCuentas.xaml
    /// </summary>
    public partial class administradorCuentas : Window
    {
        List<usuario> listUsers=new List<usuario>();
        usuario currentUsuario = new usuario();
        public administradorCuentas()
        {
            InitializeComponent();
            lstUsuario.DataContext = currentUsuario;
            lstUsuario.ItemsSource = listUsers;
            BuscarDatos();
        }
        private void BuscarDatos()
        {
            Usuarios users = InterfaceHttp.GetUsuarios();
            listUsers = users.usuarios;            
            lstUsuario.ItemsSource = null;
            lstUsuario.Items.Clear();
            lstUsuario.ItemsSource = listUsers;
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            AgergarUsuario nuevoUsuario = new AgergarUsuario();
            nuevoUsuario.Owner = this;
            //mostar la ventana
            if (nuevoUsuario.ShowDialog() == true)
            {
                //actualizar lista usuarios
                listUsers.Add(nuevoUsuario.nuevoUsuario);
                lstUsuario.ItemsSource = null;
                lstUsuario.Items.Clear();
                lstUsuario.ItemsSource = listUsers;
            }
            this.Opacity = 1;   
        }

        private void ModificarUsuario_Click(object sender, RoutedEventArgs e)
        {

            this.Opacity = 0.5;
            ModificarUsuario modificarUsuario = new ModificarUsuario(currentUsuario);
            modificarUsuario.Owner = this;
            //mostar la ventana
            if (modificarUsuario.ShowDialog() == true)
            {
                //refrescar lista materias   
                lstUsuario.Items.Refresh();
            }
            this.Opacity = 1;          
            
        }

        private void EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            VentanaEliminar deleteWindow = new VentanaEliminar(delete.Usuario, currentUsuario);
            if (deleteWindow.ShowDialog() == true)
            {
                BuscarDatos();
            }
            this.Opacity = 1;
        }
        public void list1_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if(lstUsuario.SelectedIndex>=0)
                currentUsuario = listUsers[lstUsuario.SelectedIndex];
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void btnReduce_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
