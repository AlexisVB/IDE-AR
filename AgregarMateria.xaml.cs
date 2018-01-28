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
            nuevaMateria.Color = "#444";
            
        }
        private void lstMaterias_SelectionChanged(Object sender,RoutedEventArgs e)
        {
            lstMaterias.SelectedIndex = -1;
        }
        private void txtNombreMateria_TextChanged(Object sender,RoutedEventArgs e)
        {
            lstMaterias.ItemsSource = null;
            nuevaMateria.NombreMateria = txtNombreMateria.Text;
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

       
    }
}
