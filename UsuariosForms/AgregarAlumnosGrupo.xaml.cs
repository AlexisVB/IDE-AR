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
namespace IDE_AR.UsuariosForms
{
    /// <summary>
    /// Interaction logic for AgregarAlumnosGrupo.xaml
    /// </summary>
    public partial class AgregarAlumnosGrupo : Window
    {
        //Lista de alumnos que contiene todos los alumnos que coinciden con el criterio de búsqueda actual
        Usuarios listAlumnosBuscados;
        usuario usuarioContext;
        grupo currentGrupo;
        public AgregarAlumnosGrupo()
        {
            InitializeComponent();
        }
        public AgregarAlumnosGrupo(grupo gpo)
        {
            InitializeComponent();
            currentGrupo = gpo;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBuscar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void txtBuscar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                actualizarListaAlumnos();
            }
        }
        public void actualizarListaAlumnos()
        {
            listAlumnosBuscados = new Usuarios();
            listAlumnosBuscados = InterfaceHttp.BuscarAlumnosGrupo(txtBuscar.Text);
            if (listAlumnosBuscados.usuarios != null)
                //refrescarListas
                RefrescarLista();
            else
            {
                Mensaje("No hay ningun alumno en el grupo buscado");
                LimpiarLista();
            }
                
        }    
        private void RefrescarLista()
        {
            lstAlumnos.DataContext = usuarioContext;
            lstAlumnos.ItemsSource = null;
            lstAlumnos.ItemsSource = listAlumnosBuscados.usuarios;
        }
        private void LimpiarLista()
        {
            lstAlumnos.ItemsSource = null;
            lstAlumnos.Items.Clear();

        }

        private void btnAgregarAlumnos_Click(object sender, RoutedEventArgs e)
        {
            //obtener los indices seleccionados y agregar los alumnos a la tabla Integrantes grupo
            //idGrupo
            //idUsuario
            //insertar en la tabla
            //dialogResult=true;
           
            if(lstAlumnos.SelectedItems!=null)
            {
                int flag = 0;
                bool flag2 = false;
                foreach(usuario al in lstAlumnos.SelectedItems)
                {
                    flag = 1;
                    flag2 = InterfaceHttp.InsertarIntegranteGrupo(currentGrupo.IdGrupo, al.IdUsuario);
                }
                if(flag==0)
                    Mensaje("Ningún alumno seleccionado");
                if(flag2)
                   Mensaje("Alumnos agregados correctamente");                
                else
                    Mensaje("Erros al agregar alguno de los alumnos");

            }
            else
                Mensaje("Ningún alumno seleccionado");
        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
        public void Mensaje(string Text)
        {
            this.Opacity = 0.9;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 9;
        }
    }
}
