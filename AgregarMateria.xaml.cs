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
        public materia nuevaMateria = new materia();
        public AgregarMateria()
        {
            InitializeComponent();
            btnColor.DataContext = nuevaMateria;
            btnPreview.DataContext = nuevaMateria;
            nuevaMateria.Color = "#444";
            
        }
        private void txtNombreMateria_TextChanged(Object sender,RoutedEventArgs e)
        {
            btnPreview.DataContext = null;
            nuevaMateria.NombreMateria = txtNombreMateria.Text;            
            btnPreview.DataContext = nuevaMateria;
            
        }
        private void txtMatriculaMateria_TextChanged(object sender, TextChangedEventArgs e)
        {            
            nuevaMateria.Matricula = txtMatriculaMateria.Text;            
        }
      
        private void txtNickMateria_TextChanged(Object sender, RoutedEventArgs e)
        {

            
            if(txtNickMateria.Text.Length==2)
            {
                btnPreview.DataContext = null;
                nuevaMateria.Nick = txtNickMateria.Text.ToUpper();
                btnPreview.DataContext = nuevaMateria;            
            }
            
        }
        private void btnColor_Click(Object sender,RoutedEventArgs e)
        {
            btnPreview.DataContext = null;
            btnColor.DataContext = null;
            nuevaMateria.Color = "#2979ff";
            btnPreview.DataContext = nuevaMateria;
            btnColor.DataContext = nuevaMateria;
        }
        private void btnCerrar_Click(Object sender,RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
