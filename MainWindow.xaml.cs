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
            }
            catch{}            
        }
        private void MainWindow_Closing(Object Sender,System.ComponentModel.CancelEventArgs e)
        {
            //Guardar el estado desde los atributos
            Properties.Settings.Default.MainRestoreBounds = RestoreBounds;
            Properties.Settings.Default.MainWindowState = WindowState;
            Properties.Settings.Default.Save();
        }
    }
}
