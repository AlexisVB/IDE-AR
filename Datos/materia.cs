using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    public class materia
    {
        private int idMateria;
        private string nombre;
        private string matriculaMateria;
        private int idUsuario;//id de profesor
        private string color;
        private string nick;
        public List<grupo> listaGrupos=new List<grupo>();
        public int IdMateria
        {
            get { return idMateria; }
            set { idMateria = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Matricula
        {
            get { return matriculaMateria; }
            set { matriculaMateria = value; }
        }
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
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
