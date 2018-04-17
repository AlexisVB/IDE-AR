using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft;
namespace IDE_AR.Datos
{
    public class materia
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public int IdProfesor { get; set; }//id de profesor
        public string Color { get; set; }
        public string Nick { get; set; }

        public List<grupo> listaGrupos=new List<grupo>();       
        
    }
}
