using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    public class grupo
    {
        private int idGrupo;
        private string nombre;
        private int idMateria;
        private string color;
        private string nick;
        public List<actividad> listaActividades = new List<actividad>();

        public int IdGrupo
        {
            get { return idGrupo; }
            set { idGrupo = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public int IdMateria
        {
            get { return idMateria; }
            set { idMateria = value; }
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
