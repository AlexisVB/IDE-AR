using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDE_AR.Datos;
namespace IDE_AR.DatosGlobales
{
    public class VariablesGlobales
    {
        public static usuario miusuario = new usuario();//variable usada para almacenar los datos de la cuenta actual
        public static string RutaPredeterminada = "";//variable que guarda los proyectos en una carpeta/se lee del archivo defaultLocation.bin
    }
}
