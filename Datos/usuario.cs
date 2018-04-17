using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    public class usuario
    {
        private int idUsuario;
        private string nombreUsuario;
        private string password;
        private int tipo;
        private string nombre;
        private string registro;
        private string grupo;
        private string correo;
        private int estado;
        

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Registro
        {
            get { return registro; }
            set { registro = value; }
        }
        public string Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }
        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        public static usuario ObtenerUsuario(int id)//funcion que obtiene un usuario pasandole un id
        {
            bool encontrado=false;
            //consulta en la bd
            if(encontrado)
            {
                usuario nuevo=new usuario();
                //asignacion de datos
                return nuevo;
            }
            return null;            
        }
        public static usuario ObtenerUsuario(string name,string pass)//funcion que obtiene un usuario recibiendo su nombre y contraseña
        {
            bool encontrado = false;
            //consulta en la bd
            if (encontrado)
            {
                usuario nuevo = new usuario();
                //asignacion de datos
                return nuevo;
            }
            return null;    
        }
    }
}
