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
using System.Windows.Forms;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for configuracion.xaml
    /// </summary>
    public partial class configuracion : Window
    {
        public configuracion()
        {
            InitializeComponent();
        }

        private void btnSelectRuta_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog select = new FolderBrowserDialog();
            if(select.ShowDialog()==System.Windows.Forms.DialogResult.Yes)
            {
                
            }
            Mensaje(select.SelectedPath);
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
