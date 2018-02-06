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
    /// Interaction logic for ModificarActividad.xaml
    /// </summary>
    public partial class ModificarActividad : Window
    {
        private List<actividad> listActivities = new List<actividad>();
        public actividad Actividad;
        public materia materiaRaiz;
        public grupo grupoRaiz;
        public bool cargado;
        public ModificarActividad(materia mat,grupo gpo,actividad mod)
        {
            InitializeComponent();
            materiaRaiz = mat;
            grupoRaiz = gpo;
            Actividad = mod;
            btnModificar.IsEnabled = false;
            btnColor.DataContext = Actividad;
            tbRaiz.Text = materiaRaiz.Nombre + ">" + grupoRaiz.Nombre + ">";
            cargado = false;
            txtNombreActividad.Text = Actividad.Nombre;
            txtNickMateria.Text = Actividad.Nick;            
            dtFechaInicio.SelectedDate = DateTime.Parse(Actividad.FechaInicial);
            dtFechaEntrega.SelectedDate = DateTime.Parse(Actividad.FechaLimite);
            cargado=true;

        }
        private void lstActividades_SelectionChanged(Object sender, RoutedEventArgs e)
        {
            lstActividades.SelectedIndex = -1;
        }
        private void txtNombreActividad_TextChanged(Object sender, RoutedEventArgs e)
        {
            if(cargado)
            {
                btnModificar.IsEnabled = true;
                lstActividades.ItemsSource = null;
                Actividad.Nombre = txtNombreActividad.Text;
                listActivities.Clear();
                listActivities.Add(Actividad);
                lstActividades.ItemsSource = listActivities;
            }
           
        }
        private void dtFechaInicio_SelectedDateChanged(Object sender,RoutedEventArgs e)
        {
            if(cargado)
            {
                btnModificar.IsEnabled = true;
                DateTime fecha = new DateTime();
                fecha = DateTime.Parse(dtFechaInicio.SelectedDate.ToString());
                Actividad.FechaInicial = fecha.ToShortDateString();
            }           
        }
        private void dtFechaEntrega_SelectedDateChanged(Object sender, RoutedEventArgs e)
        {
            if(cargado)
            {
                btnModificar.IsEnabled = true;
                DateTime fecha = new DateTime();
                fecha = DateTime.Parse(dtFechaEntrega.SelectedDate.ToString());
                Actividad.FechaLimite = fecha.ToShortDateString();
            }            
        }
        private void txtNickMateria_TextChanged(Object sender, RoutedEventArgs e)
        {
            if (cargado&&txtNickMateria.Text.Length == 2)
            {
                btnModificar.IsEnabled = true;
                lstActividades.ItemsSource = null;
                Actividad.Nick = txtNickMateria.Text.ToUpper();
                listActivities.Clear();
                listActivities.Add(Actividad);
                lstActividades.ItemsSource = listActivities;
            }

        }
        private void btnColor_Click(Object sender, RoutedEventArgs e)
        {
            if(cargado)
            {
                WindowPaleta nuevaPaleta = new WindowPaleta();
                if (nuevaPaleta.ShowDialog() == true)
                {
                    btnModificar.IsEnabled = true;
                    lstActividades.ItemsSource = null;
                    btnColor.DataContext = null;
                    Actividad.Color = nuevaPaleta.colorAgregar.ColorHex;
                    listActivities.Clear();
                    listActivities.Add(Actividad);
                    lstActividades.ItemsSource = listActivities;
                    btnColor.DataContext = Actividad;
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
            if (Actividad.Nombre.Length > 20)
            {
                Error = Error + "\n-El nombre no debe\n exceder 20 caracteres.";
            }
            if (Actividad.Nombre.Length < 5)
            {
                Error = Error + "\n-El nombre no debe\n ser menor de 5 caracteres.";
            }
            if (dtFechaInicio.SelectedDate == null)
            {
                Error = Error + "\n-La fecha de inicio no debe\n de estar vacía.";
            }
            if (dtFechaEntrega.SelectedDate == null)
            {
                Error = Error + "\n-La fecha de entrega no debe\n de estar vacía.";
            }
            if (Actividad.Nick.Length != 2)
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
            Actividad.IdGrupo = grupoRaiz.IdGrupo;
            //codigo de agregar            
            if (InterfaceHttp.modificarActividad(Actividad))
            {
                Mensaje("Modificado Correctamente");
                DialogResult = true;
            }
            else
            {
                Mensaje("Error al Modificar :(");
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
