﻿using System;
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
    /// Interaction logic for WindowPaleta.xaml
    /// </summary>
    public partial class WindowPaleta : Window
    {
        public WindowPaleta()
        {
            InitializeComponent();
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
