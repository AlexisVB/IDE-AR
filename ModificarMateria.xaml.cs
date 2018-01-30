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
using IDE_AR.DatosGlobales;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for ModificarMateria.xaml
    /// </summary>
    public partial class ModificarMateria : Window
    {
        private List<materia> listAsignatures = new List<materia>();
        public materia Materia = new materia();
        private bool cargado;
        public ModificarMateria(materia mat)
        {
            InitializeComponent();
            Materia = mat;
            btnColor.DataContext = Materia;
            lstMaterias.DataContext = Materia;
            btnModificar.IsEnabled = false;
            cargado = false;
        }
        private void ModificarWindow_Loaded(Object sender,RoutedEventArgs e)
        {
            txtNombreMateria.Text = Materia.Nombre;
            txtMatriculaMateria.Text = Materia.Matricula;
            txtNickMateria.Text = Materia.Nick;
            cargado = true;
        }
        private void lstMaterias_SelectionChanged(Object sender, RoutedEventArgs e)
        {
            lstMaterias.SelectedIndex = -1;
        }
        private void txtNombreMateria_TextChanged(Object sender, RoutedEventArgs e)
        {
            if(cargado)
            {
                btnModificar.IsEnabled = true;
                lstMaterias.ItemsSource = null;
                Materia.Nombre = txtNombreMateria.Text;
                listAsignatures.Clear();
                listAsignatures.Add(Materia);
                lstMaterias.ItemsSource = listAsignatures;
            }         
        }
        private void txtMatriculaMateria_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(cargado)
            {
                btnModificar.IsEnabled = true;
                lstMaterias.ItemsSource = null;
                Materia.Matricula = txtMatriculaMateria.Text;
                listAsignatures.Clear();
                listAsignatures.Add(Materia);
                lstMaterias.ItemsSource = listAsignatures;
            }           
        }

        private void txtNickMateria_TextChanged(Object sender, RoutedEventArgs e)
        {
            if (cargado&&txtNickMateria.Text.Length == 2)
            {
                btnModificar.IsEnabled = true;
                lstMaterias.ItemsSource = null;
                Materia.Nick = txtNickMateria.Text.ToUpper();
                listAsignatures.Clear();
                listAsignatures.Add(Materia);
                lstMaterias.ItemsSource = listAsignatures;
            }

        }
        private void btnColor_Click(Object sender, RoutedEventArgs e)
        {           
            WindowPaleta nuevaPaleta = new WindowPaleta();
            if (nuevaPaleta.ShowDialog() == true)
            {
                btnModificar.IsEnabled = true;
                lstMaterias.ItemsSource = null;
                btnColor.DataContext = null;
                Materia.Color = nuevaPaleta.colorAgregar.ColorHex;
                listAsignatures.Clear();
                listAsignatures.Add(Materia);
                lstMaterias.ItemsSource = listAsignatures;
                btnColor.DataContext = Materia;
            }



        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnModificarClick(Object sender, RoutedEventArgs e)
        {
            string Error = "";
            if (Materia.Nombre.Length > 30)
            {
                Error = Error + "\n-El nombre no debe\n exceder 30 caracteres.";
            }
            if (Materia.Nombre.Length < 5)
            {
                Error = Error + "\n-El nombre no debe\n ser menor de 5 caracteres.";
            }
            if (Materia.Matricula.Length > 20)
            {
                Error = Error + "\n-La matrícula no debe\n exceder 20 caracteres.";
            }
            if (Materia.Matricula.Length < 5)
            {
                Error = Error + "\n-La matrícula no debe\n ser menor de 10 caracteres.";
            }
            if (Materia.Nick.Length != 2)
            {
                Error = Error + "\n-El nickname debe \n contener 2 caracteres";
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
            Materia.IdUsuario = VariablesGlobales.miusuario.IdUsuario;
            //codigo de agregar            
            if (Materia.Modificar())
            {
                Mensaje("Modificado Correctamente");
                DialogResult = true;
            }
            else
            {
                Mensaje("Error al modificar");
                DialogResult = false;
            }
        }

        public void Mensaje(string Text)
        {
            this.Opacity = 0.5;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 1;
        }
        private void btnCancelar_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
       
    }
}
