using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string TipoRaiz { get; set; }
        public string IdRaiz { get; set; }
        public string Fecha { get; set; }
        public bool IsFolder { get; set; }
        public List<Fichero> Ficheros { get; set; }
    }
}
