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
    /// Interaction logic for AgregarActividad.xaml
    /// </summary>
    public partial class AgregarActividad : Window
    {
        private List<actividad> listActivities = new List<actividad>();
        public actividad nuevaActividad = new actividad();
        public materia materiaRaiz;
        public grupo grupoRaiz;
        public AgregarActividad(materia materiaraiz,grupo gruporaiz)
        {
            InitializeComponent();
            materiaRaiz = materiaraiz;
            grupoRaiz = gruporaiz;
            nuevaActividad.Color = "#444";
            btnColor.DataContext = nuevaActividad;
            tbRaiz.Text = materiaRaiz.Nombre+">"+gruporaiz.Nombre+">";
            nuevaActividad.Nombre = "";
            nuevaActividad.Nick = "";  
        }
        private void lstActividades_SelectionChanged(Object sender, RoutedEventArgs e)
        {
            lstActividades.SelectedIndex = -1;
        }
        private void txtNombreActividad_TextChanged(Object sender,RoutedEventArgs e)
        {
            lstActividades.ItemsSource = null;
            nuevaActividad.Nombre = txtNombreActividad.Text;
            listActivities.Clear();
            listActivities.Add(nuevaActividad);
            lstActividades.ItemsSource = listActivities;
        }
        private void dtFechaInicio_SelectedDateChanged(Object sender, RoutedEventArgs e)
        {
            DateTime fecha = new DateTime();
            fecha = DateTime.Parse(dtFechaInicio.SelectedDate.ToString());
            nuevaActividad.FechaInicial = fecha.ToShortDateString();            
        }
        private void dtFechaEntrega_SelectedDateChanged(Object sender, RoutedEventArgs e)
        {
            DateTime fecha = new DateTime();
            fecha = DateTime.Parse(dtFechaEntrega.SelectedDate.ToString());
            nuevaActividad.FechaLimite = fecha.ToShortDateString();            
        }
        private void txtNickMateria_TextChanged(Object sender, RoutedEventArgs e)
        {
            if (txtNickMateria.Text.Length == 2)
            {
                lstActividades.ItemsSource = null;
                nuevaActividad.Nick = txtNickMateria.Text.ToUpper();
                listActivities.Clear();
                listActivities.Add(nuevaActividad);
                lstActividades.ItemsSource = listActivities;
            }

        }
        private void btnColor_Click(Object sender, RoutedEventArgs e)
        {
            WindowPaleta nuevaPaleta = new WindowPaleta();
            if (nuevaPaleta.ShowDialog() == true)
            {
                lstActividades.ItemsSource = null;
                btnColor.DataContext = null;
                nuevaActividad.Color = nuevaPaleta.colorAgregar.ColorHex;
                listActivities.Clear();
                listActivities.Add(nuevaActividad);
                lstActividades.ItemsSource = listActivities;
                btnColor.DataContext = nuevaActividad;
            } 
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnAgregarClick(Object sender, RoutedEventArgs e)
        {
            string Error = "";
            if (nuevaActividad.Nombre.Length > 20)
            {
                Error = Error + "\n-El nombre no debe\n exceder 20 caracteres.";
            }
            if (nuevaActividad.Nombre.Length < 5)
            {
                Error = Error + "\n-El nombre no debe\n ser menor de 5 caracteres.";
            }
            if (dtFechaInicio.SelectedDate==null)
            {
                Error = Error + "\n-La fecha de inicio no debe\n de estar vacía.";
            }
            if (dtFechaEntrega.SelectedDate == null)
            {
                Error = Error + "\n-La fecha de entrega no debe\n de estar vacía.";
            }
            if (nuevaActividad.Nick.Length != 2)
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
            nuevaActividad.Gpo=grupoRaiz;
            //codigo de agregar            
            if (nuevaActividad.Insertar())
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
            this.Opacity = 1;
        }
    }
}
