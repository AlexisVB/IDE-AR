using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace IDE_AR.Soluciones
{
    public class SolucionProyecto
    {
        //idprofesor>materia>grupo>actividad>alumno>solucion
        //tipo raiz
        //0=Es solo un archivo
        //1=Solucion Proyecto
        //2=carpeta
        public int IdProyecto { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string RutaLocal { get; set; }
        public int IdPropietario { get; set; }
        public int IdActividad { get; set; }
        public string Fecha { get; set; }
        public string NombrePropietario { get; set; }
        public ObservableCollection<Fichero> Ficheros { get; set; }
        
    }
}
