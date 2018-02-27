using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    class EntregaActividad
    {
        private int idActividad;
        private string fechaEntregada;
        private List<string> direccionesArchivos=new List<string>();
        private int calificacion;
        private string comentarios;

        public int Idactividad
        {
            get{return idActividad;}
            set{idActividad=value;}
        }
        public string FechaEntergada
        {
            get{return fechaEntregada;}
            set{fechaEntregada=value;}
        }
        public List<string> DireccionesArchivos
        {
            get{return direccionesArchivos;}
            set{direccionesArchivos=value;}
        }
        private int Calificacion
        {
            get{return calificacion;}
            set{calificacion=value;}
        }
        private string Comentarios
        {
            get{return comentarios;}
            set{comentarios=value;}
        }
    }
}
