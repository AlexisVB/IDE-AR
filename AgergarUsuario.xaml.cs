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
    /// Interaction logic for AgergarUsuario.xaml
    /// </summary>
    public partial class AgergarUsuario : Window
    {
       public usuario nuevoUsuario = new usuario();   
        public AgergarUsuario()
        {
            InitializeComponent();
        }

        private void txtNombreUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            nuevoUsuario.NombreUsuario = txtNombreUsuario.Text;
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            nuevoUsuario.Password = txtPassword.Text;
        }

        private void CbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nuevoUsuario.Tipo = CbTipo.SelectedIndex;
        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            nuevoUsuario.Nombre = txtNombre.Text;
        }
        private void txtRegistro_TextChanged(object sender, TextChangedEventArgs e)
        {
            nuevoUsuario.Registro = txtRegistro.Text;
        }

        private void txtGrupo_TextChanged(object sender, TextChangedEventArgs e)
        {
            nuevoUsuario.Grupo = txtGrupo.Text;
        }

        private void txtCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {
            nuevoUsuario.Correo=txtCorreo.Text;
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnAgregarClick(Object sender, RoutedEventArgs e)
        {
            string Error = "";
            if (nuevoUsuario.NombreUsuario.Length > 40)
            {
                Error = Error + "\n-El nombre de usuario no debe\n exceder 40 caracteres.";
            }
            if (nuevoUsuario.NombreUsuario.Length < 10)
            {
                Error = Error + "\n-El nombre de usuario no debe\n ser menor de 10 caracteres.";
            }
            if (nuevoUsuario.Password.Length > 16)
            {
                Error = Error + "\n-El password no debe\n exceder 16 caracteres.";
            }
            if (nuevoUsuario.Password.Length < 6)
            {
                Error = Error + "\n-El password no debe\n ser menor de 6 caracteres.";
            }
            if (nuevoUsuario.Nombre.Length > 40)
            {
                Error = Error + "\n-El nombre no debe \n exceder 40 caracteres.";
            }
            if (nuevoUsuario.Nombre.Length < 10)
            {
                Error = Error + "\n-El nombre  no debe\n ser menor de 10 caracteres.";
            }
            if (nuevoUsuario.Registro.Length > 20)
            {
                Error = Error + "\n-El registro no debe \n exceder 20 caracteres.";
            }
            if (nuevoUsuario.Registro.Length < 6)
            {
                Error = Error + "\n-El registro no debe\n ser menor de 6 caracteres.";
            }
            if (nuevoUsuario.Grupo.Length > 10)
            {
                Error = Error + "\n-El grupo no debe \n exceder 10 caracteres.";
            }
            if (nuevoUsuario.Grupo.Length < 2)
            {
                Error = Error + "\n-El grupo no debe\n ser menor de 2 caracteres.";
            }
            if (nuevoUsuario.Correo.Length > 30)
            {
                Error = Error + "\n-El correo no debe \n exceder 30 caracteres";
            }
            if (nuevoUsuario.Correo.Length < 10)
            {
                Error = Error + "\n-El correo no debe\n ser menor de 10 caracteres."; 
            }
            if (Error.Length > 0)
            {
                Mensaje(Error);
            }
            else
            {
                agregar();
            }

        }
        private void agregar()
        {
            
            usuario nuevoUsuario2 = InterfaceHttp.insertarUsuario(nuevoUsuario);
            //codigo de agregar            
            if (nuevoUsuario2.IdUsuario > 0)
            {
                nuevoUsuario = nuevoUsuario2;
                Mensaje("Agregado Correctamente");
                DialogResult = true;
            }
            else
            {
                Mensaje("Error al agregar");
            }
        }

        public void Mensaje(string Text)
        {
            this.Opacity = 0.5;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 5;
        }

      
       
    }
}
