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
    /// Interaction logic for ModificarGrupo.xaml
    /// </summary>
    public partial class ModificarGrupo : Window
    {
        private List<grupo> listGroups = new List<grupo>();
        public grupo Grupo = new grupo();
        public materia materiaRaiz;
        public bool cargado;
        public ModificarGrupo(materia materiaraiz,grupo mod)
        {
            InitializeComponent();
            btnModificar.IsEnabled = false;
            materiaRaiz = materiaraiz;
            Grupo = mod;
            btnColor.DataContext = Grupo;
            tbNombremateria.Text = materiaRaiz.Nombre + ">";
            cargado = false;
            txtNombre.Text = Grupo.Nombre;
            txtNick.Text = Grupo.Nick;
            cargado = true;
            
        }
        private void lstGrupos_SelectionChanged(Object sender, RoutedEventArgs e)
        {
            lstGrupos.SelectedIndex = -1;
        }
        private void txtNombre_TextChanged(Object sender, RoutedEventArgs e)
        {
            if(cargado)
            {
                btnModificar.IsEnabled = true;
                lstGrupos.ItemsSource = null;
                Grupo.Nombre = txtNombre.Text;
                listGroups.Clear();
                listGroups.Add(Grupo);
                lstGrupos.ItemsSource = listGroups;
            } 
        }
        private void txtNick_TextChanged(Object sender, RoutedEventArgs e)
        {
            if (cargado&&txtNick.Text.Length == 2)
            {
                btnModificar.IsEnabled = true;
                lstGrupos.ItemsSource = null;
                Grupo.Nick = txtNick.Text.ToUpper();
                listGroups.Clear();
                listGroups.Add(Grupo);
                lstGrupos.ItemsSource = listGroups;
            }

        }
        private void btnColor_Click(Object sender, RoutedEventArgs e)
        {
            if(cargado)
            {
                btnModificar.IsEnabled = true;
                WindowPaleta nuevaPaleta = new WindowPaleta();
                if (nuevaPaleta.ShowDialog() == true)
                {
                    lstGrupos.ItemsSource = null;
                    btnColor.DataContext = null;
                    Grupo.Color = nuevaPaleta.colorAgregar.ColorHex;
                    listGroups.Clear();
                    listGroups.Add(Grupo);
                    lstGrupos.ItemsSource = listGroups;
                    btnColor.DataContext = Grupo;
                }
            }
          
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnModificarClick(Object sender, RoutedEventArgs e)
        {
            string Error = "";
            if (Grupo.Nombre.Length > 20)
            {
                Error = Error + "\n-El nombre no debe\n exceder 20 caracteres.";
            }
            if (Grupo.Nombre.Length < 2)
            {
                Error = Error + "\n-El nombre no debe\n ser menor de 2 caracteres.";
            }
            if (Grupo.Nick.Length != 2)
            {
                Error = Error + "\n-El nickname debe \n contener 2 caracteres";
            }
            if (Error.Length > 0)
            {
                Mensaje(Error);
            }
            else
            {
                modificar();
            }

        }
        private void modificar()
        {
            //codigo de agregar
            Grupo.IdMateria = materiaRaiz.IdMateria;
            if (Grupo.Modificar())
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
        private void btnCancelar_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public void Mensaje(string Text)
        {
            this.Opacity = 0.5;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 1;
        }
       
    }
}
