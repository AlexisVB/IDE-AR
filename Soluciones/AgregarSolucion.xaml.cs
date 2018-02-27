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
using System.Collections.ObjectModel;
using IDE_AR.DatosGlobales;
namespace IDE_AR.Soluciones
{
    /// <summary>
    /// Interaction logic for AgregarSolucion.xaml
    /// </summary>
    public partial class AgregarSolucion : Window
    {
        string ruta = VariablesGlobales.RutaPredeterminada;
        string nombre;
        public SolucionProyecto nuevaSolucion = new SolucionProyecto();
        public AgregarSolucion()
        {
            InitializeComponent();
            nuevaSolucion.Ficheros = new ObservableCollection<Fichero>();
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            nombre = txtNombre.Text;
            lbRuta.Text = ruta + "//"+nombre;
            nuevaSolucion.Nombre = nombre;
            nuevaSolucion.RutaLocal = lbRuta.Text;
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            adminSolucion admin = new adminSolucion(nuevaSolucion);
            nuevaSolucion.Ficheros = new ObservableCollection<Fichero>();
            if (admin.ConstruirProyecto())
            {
                DialogResult = true;
                Mensaje("Proyecto Creado correctamente");
            }
            else
            {
                Mensaje("Error al crear el proyecto:\nEl directorio ya existe");
                DialogResult = false;
            }

        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
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
