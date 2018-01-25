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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FontFamily familiaPre;
        private double sizePre;
        public static RoutedUICommand RCGuardar = new RoutedUICommand();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                //restaurar el estado desde los atributos del objeto
                //de la clase settings referenciado por default
                Rect restoreBounds = Properties.Settings.Default.MainRestoreBounds;
                Left = restoreBounds.Left;
                Top = restoreBounds.Top;
                Width = restoreBounds.Width;
                Height = restoreBounds.Height;
                WindowState = Properties.Settings.Default.MainWindowState;
                txtEstado.Text = "Listo";
            }
            catch{}         
        }
        private void  MainWindow_Loaded(Object sender,RoutedEventArgs e)
        {
            familiaPre = ctEditor.FontFamily;
            sizePre = ctEditor.FontSize;
        }
        private void MainWindow_Closing(Object Sender,System.ComponentModel.CancelEventArgs e)
        {
            //Guardar el estado desde los atributos
            Properties.Settings.Default.MainRestoreBounds = RestoreBounds;
            Properties.Settings.Default.MainWindowState = WindowState;
            Properties.Settings.Default.Save();
        }
      
        private void ExecutedRCGuardar(Object sender, ExecutedRoutedEventArgs e)
        {
            //implementar aqui logica para la orden guardar
            MessageBox.Show("orden guardar SEMI-personalizada");
        }
        private void CanExecuteRCGuardar(Object sender, CanExecuteRoutedEventArgs e)
        {
            //validar cuando haya cambios en algun archivo
            e.CanExecute = true;
        }
        private void ArchivoSalir_Click(Object sender,RoutedEventArgs e)
        {
            Close();//cerrrar la ventana principal
        }
        private void acercaDe_Click(Object sender,RoutedEventArgs e)
        {
            String creditos="Desarrolladores:\n"+
                             "-André Nicolás Ricoy Camacho\n"+
                             "-Alexis Daniel Villicaña Barrera";
            MessageBox.Show(creditos, "IDE-AR");
        }
        public void list1_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Elemento seleccionado:" + lstMaterias.SelectedIndex);
        }
      
        public void lstGrupo_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Elemento seleccionado:" + lstGrupos.SelectedIndex);
        }

        public void lstActividades_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Elemento seleccionado:" + lstActividades.SelectedIndex);
        }

        //**************************************************************************************************
        //Eventos para la asignación de fuentes
        private void OpcionesFuentePredeterminada_Click(Object sender, RoutedEventArgs e)
        {
            ctEditor.FontFamily = familiaPre;
            OpcionesFuentePredeterminado.IsChecked = true;
            OpcionesFuenteArial.IsChecked = false;
            OpcionesFuenteCourier.IsChecked = false;
        }
        private void OpcionesFuenteCourier_Click(Object sender,RoutedEventArgs e)
        {
            ctEditor.FontFamily = new FontFamily("Courier new");
            OpcionesFuentePredeterminado.IsChecked = false;
            OpcionesFuenteArial.IsChecked = false;
            OpcionesFuenteCourier.IsChecked = true;
        }
        private void OpcionesFuenteArial_Click(Object sender, RoutedEventArgs e)
        {
            ctEditor.FontFamily = new FontFamily("Arial");
            OpcionesFuentePredeterminado.IsChecked =false;
            OpcionesFuenteArial.IsChecked = true;
            OpcionesFuenteCourier.IsChecked = false;
        }
        //**************************************************************************************************
        //Eventos para la asignación de tamaño
        private void OpcionesFuenteSize_Click(Object sender, RoutedEventArgs e)
        {
            MenuItem objeto=(MenuItem)e.Source;
            if(objeto.Name=="OpcionesPredeterminado")
            {
               ctEditor.FontSize=sizePre;
               OpcionesPredeterminado.IsChecked = true;
               OpcionesTamano10.IsChecked = false;
               OpcionesTamano12.IsChecked = false;
               OpcionesTamano14.IsChecked = false;
               OpcionesTamano16.IsChecked = false;
               OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano10")
            {
                ctEditor.FontSize = 10.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = true;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano12")
            {
                ctEditor.FontSize = 12.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = true;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano14")
            {
                ctEditor.FontSize = 14.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = true;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano16")
            {
                ctEditor.FontSize = 16.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = true;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano18")
            {
                ctEditor.FontSize = 18.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = true;
            }
        }
    }
}
