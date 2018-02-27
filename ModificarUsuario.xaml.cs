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
    /// Interaction logic for ModificarUsuario.xaml
    /// </summary>
    public partial class ModificarUsuario : Window
    {
        public usuario Usuario = new usuario();
        public bool cargado;
        public ModificarUsuario(usuario user)
        {
            InitializeComponent();
            Usuario = user;
            cargado = false;
        }
        private void ModificarWindow_Loaded(Object sender, RoutedEventArgs e)
        {
            txtNombreUsuario.Text = Usuario.NombreUsuario;
            txtPassword.Text = Usuario.Password;
            txtNombre.Text = Usuario.Nombre;
            CbTipo.SelectedIndex = 0;
            txtRegistro.Text = Usuario.Registro;
            txtGrupo.Text = Usuario.Grupo;
            txtCorreo.Text = Usuario.Correo;
            cargado = true;
        }
        private void txtNombreUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(cargado)
                Usuario.NombreUsuario = txtNombreUsuario.Text;
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cargado)
                Usuario.Password = txtPassword.Text;
        }

        private void CbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cargado)
                Usuario.Tipo = CbTipo.SelectedIndex;
        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cargado)
                Usuario.Nombre = txtNombre.Text;
        }
        private void txtRegistro_TextChanged(object sender, TextChangedEventArgs e)
        {
           if(cargado)
            Usuario.Registro = txtRegistro.Text;
        }

        private void txtGrupo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(cargado)
                Usuario.Grupo = txtGrupo.Text;
        }

        private void txtCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(cargado)
                Usuario.Correo = txtCorreo.Text;
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnAgregarClick(Object sender, RoutedEventArgs e)
        {
            string Error = "";
            if (Usuario.NombreUsuario.Length > 40)
            {
                Error = Error + "\n-El nombre de usuario no debe\n exceder 40 caracteres.";
            }
            if (Usuario.NombreUsuario.Length < 10)
            {
                Error = Error + "\n-El nombre de usuario no debe\n ser menor de 10 caracteres.";
            }
            if (Usuario.Password.Length > 16)
            {
                Error = Error + "\n-El password no debe\n exceder 16 caracteres.";
            }
            if (Usuario.Password.Length < 6)
            {
                Error = Error + "\n-El password no debe\n ser menor de 6 caracteres.";
            }
            if (Usuario.Nombre.Length > 40)
            {
                Error = Error + "\n-El nombre no debe \n exceder 40 caracteres.";
            }
            if (Usuario.Nombre.Length < 10)
            {
                Error = Error + "\n-El nombre  no debe\n ser menor de 10 caracteres.";
            }
            if (Usuario.Registro.Length > 20)
            {
                Error = Error + "\n-El registro no debe \n exceder 20 caracteres.";
            }
            if (Usuario.Registro.Length < 6)
            {
                Error = Error + "\n-El registro no debe\n ser menor de 6 caracteres.";
            }
            if (Usuario.Grupo.Length > 10)
            {
                Error = Error + "\n-El grupo no debe \n exceder 10 caracteres.";
            }
            if (Usuario.Grupo.Length < 2)
            {
                Error = Error + "\n-El grupo no debe\n ser menor de 2 caracteres.";
            }
            if (Usuario.Correo.Length > 30)
            {
                Error = Error + "\n-El correo no debe \n exceder 30 caracteres";
            }
            if (Usuario.Correo.Length < 10)
            {
                Error = Error + "\n-El correo no debe\n ser menor de 10 caracteres.";
            }
            if (Error.Length > 0)
            {
                Mensaje(Error);
            }
            else
            {
                Modificar();
            }

        }
        private void Modificar()
        {

            MessageBox.Show(Usuario.IdUsuario + "");
            //codigo de agregar            
            if (InterfaceHttp.modificarUsuario(Usuario))
            {                
                Mensaje("Modificado Correctamente");
                DialogResult = true;
            }
            else
            {
                Mensaje("Error al modificar");
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
        private void btnCancelar_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
       
    }
}
