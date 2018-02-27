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
        private string nombre;
        private int gpo;
        private string fechaInicial;
        private string fechaLimite;
        private string color;
        private string nick;

        public int IdActividad
        {
            get { return idActividad; }
            set { idActividad = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public int IdGrupo
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
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
        public bool Insertar()
        {
            //codigo para insertar
            return true;
        }
        public bool Modificar()
        {
            //codigo para modificar
            return true;
        }
        public bool Cosultar()
        {
            //codigo para consultar
            return true;
        }
        public bool Eliminar()
        {
            //codigo para eliminar
            return false;
        }
    }
}
