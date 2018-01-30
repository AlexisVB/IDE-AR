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

namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for VentanaAdvertencia.xaml
    /// </summary>
    public partial class VentanaAdvertencia : Window
    {
        private Mensaje nuevoMsj = new Mensaje();
        private string texto;
        public VentanaAdvertencia()
        {
            InitializeComponent();
            tbConfirmación.DataContext = nuevoMsj;
        }
        public string Texto
        {
            get { return texto; }
            set
            {
                texto = value;
                nuevoMsj.Texto = texto;
            }
        }

        private void btnAceptar_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void btnCancelar_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private class Mensaje
        {
            private string texto;

            public string Texto
            {
                get { return texto; }
                set { texto = value; }
            }
        }
    }
}
