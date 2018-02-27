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
    /// Interaction logic for Agregar_Grupo.xaml
    /// </summary>
    public partial class Agregar_Grupo : Window
    {
        private List<grupo> listGroups = new List<grupo>();
        public grupo nuevoGrupo = new grupo();
        public materia materiaRaiz;
        public Agregar_Grupo(materia materiaraiz)
        {
            InitializeComponent();
            materiaRaiz = materiaraiz;
            nuevoGrupo.Color = "#444";
            btnColor.DataContext = nuevoGrupo;
            tbNombremateria.Text = materiaRaiz.Nombre+">";
            nuevoGrupo.Nombre = "";
            nuevoGrupo.Nick = "";            
        }
        private void lstGrupos_SelectionChanged(Object sender, RoutedEventArgs e)
        {
            lstGrupos.SelectedIndex = -1;
        }
        private void txtNombre_TextChanged(Object sender, RoutedEventArgs e)
        {
            lstGrupos.ItemsSource = null;
            nuevoGrupo.Nombre = txtNombre.Text;
            listGroups.Clear();
            listGroups.Add(nuevoGrupo);
            lstGrupos.ItemsSource = listGroups;

        }

        private void txtNick_TextChanged(Object sender, RoutedEventArgs e)
        {
            if (txtNick.Text.Length == 2)
            {
                lstGrupos.ItemsSource = null;
                nuevoGrupo.Nick = txtNick.Text.ToUpper();
                listGroups.Clear();
                listGroups.Add(nuevoGrupo);
                lstGrupos.ItemsSource = listGroups;
            }

        }
        private void btnColor_Click(Object sender, RoutedEventArgs e)
        {
            WindowPaleta nuevaPaleta = new WindowPaleta();
            if (nuevaPaleta.ShowDialog() == true)
            {
                lstGrupos.ItemsSource = null;
                btnColor.DataContext = null;
                nuevoGrupo.Color = nuevaPaleta.colorAgregar.ColorHex;
                listGroups.Clear();
                listGroups.Add(nuevoGrupo);
                lstGrupos.ItemsSource = listGroups;
                btnColor.DataContext = nuevoGrupo;
            }
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnAgregarClick(Object sender, RoutedEventArgs e)
        {
            string Error = "";
            if (nuevoGrupo.Nombre.Length > 20)
            {
                Error = Error + "\n-El nombre no debe\n exceder 20 caracteres.";
            }
            if (nuevoGrupo.Nombre.Length < 2)
            {
                Error = Error + "\n-El nombre no debe\n ser menor de 2 caracteres.";
            }          
            if (nuevoGrupo.Nick.Length != 2)
            {
                Error = Error + "\n-El nickname debe \n contener 2 caracteres";
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
            //codigo de agregar
            nuevoGrupo.IdMateria = materiaRaiz.IdMateria;
            nuevoGrupo=InterfaceHttp.insertarGrupo(nuevoGrupo);
            if (nuevoGrupo.IdGrupo>0)
            {
                DialogResult = true;
                Mensaje("Agregado Correctamente");
               

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
            this.Opacity = 1;
        }
       
    }
}
