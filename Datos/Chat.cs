using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    public class Chat
    {
        public usuario Host { get; set; }
        public usuario Guest  { get; set; }
        public List<mensaje> mensajes { get; set; }

        public void AsignarHost()
        {
            for(int cont=0;cont<mensajes.Count;cont++)
            {
                mensaje current = mensajes[cont];
                if (current.IdRemitente == Host.IdUsuario)
                {
                    current.IsHost = true;
                    current.NombreRemitente = Host.Nombre;
                }                   
                else
                {
                    current.IsHost = false;
                    current.NombreRemitente = Guest.Nombre;
                }
                    
            }
        }
    }
}
