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
    /// Interaction logic for MessageBoxPersonalizado.xaml
    /// </summary>
    
    public partial class MessageBoxPersonalizado : Window
    {
        private Mensaje nuevoMsj = new Mensaje();
        private string texto;
        public MessageBoxPersonalizado()
        {
            InitializeComponent();            
            txtTexto.DataContext = nuevoMsj;            
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

        public void btnAceptar_Click(Object sender,RoutedEventArgs e)
        {
            this.Close();
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

        private void Button_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Close();

            }
               
        }
    }
   
}
