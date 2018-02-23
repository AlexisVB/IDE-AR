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

namespace IDE_AR.Soluciones
{
    /// <summary>
    /// Interaction logic for AgregarFichero.xaml
    /// </summary>
    public partial class AgregarFichero : Window
    {
        public Fichero FicheroRaiz = new Fichero();
        public Fichero fichero = new Fichero();       
        public AgregarFichero()
        {
            InitializeComponent();
            cbTipo.SelectedIndex = 0;
        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            fichero.Nombre = txtNombre.Text;
            fichero.RutaLocal = FicheroRaiz.RutaLocal + "//" + FicheroRaiz.Nombre;
            if(fichero.IsFolder)
            {
                lbRuta.Text = FicheroRaiz.Nombre + "//" + fichero.Nombre;
            }
            else
            {
                lbRuta.Text = FicheroRaiz.Nombre + "//" + fichero.Nombre+".cpp";
            }
            

        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(FicheroRaiz.IsFolder)
                fichero.RutaLocal = FicheroRaiz.RutaLocal+"//"+FicheroRaiz.Nombre;
            else
                fichero.RutaLocal = FicheroRaiz.RutaLocal;
            adminFicheros admin = new adminFicheros(fichero);

            if (admin.CrearArchivo())
            {
                Mensaje("Fichero Creado correctamente");
                DialogResult = true;                
            }
            else
            {
                Mensaje("Error al crear el fichero:\nEl fichero ya existe");
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

        private void cbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbTipo.SelectedIndex==0)
            {
                fichero.IsFolder = false;
            }
            if(cbTipo.SelectedIndex==1)
            {
                fichero.IsFolder = true;
            }
        }
    }
}
