using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IDE_AR.Datos;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public enum delete { Materia, Grupo, Actividad };
    public partial class App : Application
    {
        private System.Threading.Mutex exmut;        

        private void Application_Startup(Object sender,StartupEventArgs e)
        {
            //Verificar si ya existe una instancia de la aplicación
            string nombre_exmut = "IDE-AR";
            bool nueva;
            exmut = new System.Threading.Mutex(true, nombre_exmut, out nueva);
            if (!nueva) Shutdown();
        }
    }
}
