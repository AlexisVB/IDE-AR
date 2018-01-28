using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    public class actividad
    {
        private int idActividad;
        private string nombreActividad;
        private grupo gpo;
        private string fechaInicial;
        private string fechaLimite;
        public int IdActividad
        {
            get { return idActividad; }
            set { idActividad = value; }
        }
        public string NombreActividad
        {
            get { return nombreActividad; }
            set { nombreActividad = value; }
        }
        public grupo Gpo
        {
            get { return gpo; }
            set{   gpo = value;}
        }
        public string FechaInicial
        {
            get { return fechaInicial; }
            set { fechaInicial = value; }
        }
        public string FechaLimite
        {
            get { return fechaLimite; }
            set { fechaLimite = value; }
        }
    }
}
