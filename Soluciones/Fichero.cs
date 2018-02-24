using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace IDE_AR.Soluciones
{
    public class Fichero
    {
        //tipo raiz
        //0=Es solo un archivo
        //1=Solucion Proyecto
        //2=carpeta
        public int IdFichero { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string RutaLocal { get; set; }
        public int TipoRaiz { get; set; }
        public int IdRaiz { get; set; }
        public string Fecha { get; set; }
        public bool IsFolder { get; set; }
        public Fichero Parent { get; set; }
        public ObservableCollection<Fichero> Ficheros { get; set; }
    }
}
