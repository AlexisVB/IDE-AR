using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    public class mensaje
    {        
        public int IdMensaje { get; set; }
        public int IdRemitente { get; set; }
        public string NombreRemitente { get; set; }
        public int IdDestinatario { get; set; }
        public string FechaEnvio { get; set; }
        public string Mensaje { get; set; }
        public bool IsHost { get; set; }
    }
}
