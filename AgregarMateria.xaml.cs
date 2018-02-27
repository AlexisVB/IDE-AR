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
    /// Interaction logic for AgregarMateria.xaml
    /// </summary>
    public partial class AgregarMateria : Window
    {
        private List<materia> listAsignatures = new List<materia>();
        public materia nuevaMateria = new materia();
        
        public AgregarMateria()
        {
            InitializeComponent();
            btnColor.DataContext = nuevaMateria;
            lstMaterias.DataContext = nuevaMateria;
            nuevaMateria.Nombre = "";
            nuevaMateria.Matricula = "";
            nuevaMateria.Nick = "";
            nuevaMateria.Color = "#444";            
        }
        private void lstMaterias_SelectionChanged(Object sender,RoutedEventArgs e)
        {
            lstMaterias.SelectedIndex = -1;
        }
        private void txtNombreMateria_TextChanged(Object sender,RoutedEventArgs e)
        {
            lstMaterias.ItemsSource = null;
            nuevaMateria.Nombre = txtNombreMateria.Text;
            listAsignatures.Clear();
            listAsignatures.Add(nuevaMateria);
            lstMaterias.ItemsSource = listAsignatures;
            
        }
        private void txtMatriculaMateria_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstMaterias.ItemsSource = null;
            nuevaMateria.Matricula = txtMatriculaMateria.Text;
            listAsignatures.Clear();
            listAsignatures.Add(nuevaMateria);
            lstMaterias.ItemsSource = listAsignatures;
        }
      
        private void txtNickMateria_TextChanged(Object sender, RoutedEventArgs e)
        {            
            if(txtNickMateria.Text.Length==2)
            {
                lstMaterias.ItemsSource = null;
                nuevaMateria.Nick = txtNickMateria.Text.ToUpper();
                listAsignatures.Clear();
                listAsignatures.Add(nuevaMateria);
                lstMaterias.ItemsSource = listAsignatures;          
            }
            
        }
        private void btnColor_Click(Object sender,RoutedEventArgs e)
        {
            WindowPaleta nuevaPaleta = new WindowPaleta();
            if (nuevaPaleta.ShowDialog() == true)
            {
                lstMaterias.ItemsSource = null;
                btnColor.DataContext = null;
                nuevaMateria.Color = nuevaPaleta.colorAgregar.ColorHex;
                listAsignatures.Clear();
                listAsignatures.Add(nuevaMateria);
                lstMaterias.ItemsSource = listAsignatures;
                btnColor.DataContext = nuevaMateria;
            }
            
           
            
        }
        private void btnCerrar_Click(Object sender,RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnAgregarClick(Object sender,RoutedEventArgs e)
        {
            string Error = "";
           if(nuevaMateria.Nombre.Length>30)
           {
               Error = Error + "\n-El nombre no debe\n exceder 30 caracteres.";
           }
           if (nuevaMateria.Nombre.Length <5)
           {
               Error = Error + "\n-El nombre no debe\n ser menor de 5 caracteres.";
           }
           if (nuevaMateria.Matricula.Length > 20)
           {
               Error = Error + "\n-La matrícula no debe\n exceder 20 caracteres.";
           }
           if (nuevaMateria.Matricula.Length < 5)
           {
               Error = Error + "\n-La matrícula no debe\n ser menor de 10 caracteres.";
           }
           if (nuevaMateria.Nick.Length != 2)
           {
               Error = Error + "\n-El nickname debe \n contener 2 caracteres";
           }
           if(Error.Length>0)
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
            nuevaMateria.IdProfesor = VariablesGlobales.miusuario.IdUsuario;
            nuevaMateria = InterfaceHttp.InsertarMateria(nuevaMateria);
            //codigo de agregar            
            if(nuevaMateria.IdMateria>0)
            {
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
