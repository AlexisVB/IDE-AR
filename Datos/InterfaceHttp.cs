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
    
    public static class InterfaceHttp
    {
        //**************************************Direccion fija del servidor****************************************
        static string baseURL = "http://proyectosinformaticatnl.ceti.mx/asistencia-automatizada/IDE-AR/";
        //static string baseURL = "http://localhost/IDE-AR/";//url para trabajar offline

        //**************************************Funciones necesarias para las demás funciones****************************************
        private static string HacerRequest(string query)
        {
            string json = "";
            try
            {
                 //se crea el objeto para hacer el request
            WebRequest request = WebRequest.Create(query);
            request = WebRequest.Create(query);
            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;
            request.Proxy = WebProxy.GetDefaultProxy();

            //se obtiene la respuesta en un flujo de datos
            Stream objStream = request.GetResponse().GetResponseStream();

            //se convierte el flujo de datos en cadena
            StreamReader objReader = new StreamReader(objStream);
            json= objReader.ReadLine();
            }
            catch { }
            return json;
        }
        private static string PrepararColor(string color)
        {
            string colorPre = "";
            for (int cont = 1; cont < color.Length; cont++)
            {
                colorPre = colorPre + color[cont];
            }
            return colorPre;
        }
        private static string ConvertirColor(string color)
        {
            return "#" + color;
        }
        //**************************************Funciones para el login****************************************
        public static usuario GetUsuario(string user,string password)
        {
            //Se crea la cadena para hacer el request
            string query = baseURL + "login.php?user=" + user + "&password=" +password;
            
            //se hace el request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            usuario x= new usuario();
            Newtonsoft.Json.JsonConvert.PopulateObject(json,x);
            return x;
        }
        //revisada
        //**************************************Funciones para las listas****************************************
        public static Materias GetMaterias(int idProfesor)
        {
            string scriptname = "ObtenerMaterias.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query+="idProfesor=" + idProfesor;
            
            //se hace el request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            Materias x = new Materias();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir colores
            if(x.materias!=null)
                for (int cont = 0; cont < x.materias.Count;cont++ )
                {
                    materia obj = x.materias[cont];
                    obj.Color = ConvertirColor(obj.Color);
                }
            return x;
        }
        
        public static Grupos GetGrupos(int idMateria)
        {
            string scriptname = "ObtenerGrupos.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idMateria=" + idMateria;

            //se hace el request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            Grupos x = new Grupos();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir colores
            if(x.grupos!=null)
                for (int cont = 0; cont < x.grupos.Count; cont++)
                {
                    grupo obj = x.grupos[cont];
                    obj.Color = ConvertirColor(obj.Color);
                }
            return x;
        }
        public static Actividades GetActividades(int idGrupo)
        {
            string scriptname = "ObtenerActividades.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idGrupo=" + idGrupo;

            //se hace el request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            Actividades x = new Actividades();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir colores
            if(x.actividades!=null)
                for (int cont = 0; cont < x.actividades.Count; cont++)
                {
                    actividad obj = x.actividades[cont];
                    obj.Color = ConvertirColor(obj.Color);
                }
            return x;
        }
        public static Usuarios GetUsuarios()
        {
            string scriptname = "ObtenerUsuarios.php";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;   
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            Usuarios x = new Usuarios();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);          
            return x;
        }

        //**************************************Funciones para las materias****************************************
        public static materia InsertarMateria(materia mat)
        {
            //insertarMateria.php?nombre=Sistemas%20embebidos&matricula=818323&idProfesor=2&color=%23009688&nick=SE
            string scriptname = "insertarMateria.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "nombre=" + mat.Nombre;
            query += "&matricula=" + mat.Matricula;
            query += "&idProfesor=" + mat.IdProfesor;
            query += "&color=" + PrepararColor(mat.Color);
            query += "&nick=" + mat.Nick;
            Console.WriteLine(query);

            //se hace el request
            string json = HacerRequest(query);

            //deserialización de json a c# object                   
            materia x = new materia();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            return x;
        }
    
        public static bool modificarMateria(materia mat)
        {
            string scriptname = "modificarMateria.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idMateria=" + mat.IdMateria;
            query += "&nombre=" + mat.Nombre;
            query += "&matricula=" + mat.Matricula;
            query += "&idProfesor=" + mat.IdProfesor;
            query += "&color=" + PrepararColor(mat.Color);
            query += "&nick=" + mat.Nick;

            //se hace el request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            materia x = new materia();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            if (x.Nombre.Length > 0)
            {
                return true;
            }
            return false;
        }
        public static materia consultarMateria(materia mat)
        {
            string scriptname = "consultarMateria.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idMateria=" + mat.IdMateria;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            materia x = new materia();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            return x;
        }     
        public static bool eliminarMateria(materia mat)
        {
            string scriptname = "eliminarMateria.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idMateria=" + mat.IdMateria;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            materia x = new materia();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            if (x.Nombre!=null)
                return false;
            return true;
        }
        //**************************************Funciones para los grupos****************************************
        public static grupo insertarGrupo(grupo gpo)
        {            
            string scriptname = "insertarGrupo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "nombre=" + gpo.Nombre;
            query += "&idMateria=" + gpo.IdMateria;            
            query += "&color=" + PrepararColor(gpo.Color);
            query += "&nick=" + gpo.Nick;
            Console.WriteLine(query);
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                   
            grupo x = new grupo();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            return x;
        }
        public static bool modificarGrupo(grupo  gpo)
        {
            string scriptname = "modificarGrupo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idGrupo=" + gpo.IdGrupo;
            query += "&nombre=" + gpo.Nombre;            
            query += "&color=" + PrepararColor(gpo.Color);
            query += "&nick=" + gpo.Nick;
            //se crea el objeto para hacer el request
            WebRequest request = WebRequest.Create(query);
            request = WebRequest.Create(query);
            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;
            request.Proxy = WebProxy.GetDefaultProxy();

            //se obtiene la respuesta en un flujo de datos
            Stream objStream = request.GetResponse().GetResponseStream();

            //se convierte el flujo de datos en cadena
            StreamReader objReader = new StreamReader(objStream);
            string json = objReader.ReadLine();

            //deserialización de json a c# object
            grupo x = new grupo();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            if (x.Nombre.Length > 0)
            {
                return true;
            }
            return false;
        }
        public static materia consultarGrupo(grupo gpo)
        {
            string scriptname = "consultarGrupo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idGrupo=" + gpo.IdGrupo;
            //hacer request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            materia x = new materia();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            return x;
        }
        public static bool eliminarGrupo(grupo gpo)
        {
            string scriptname = "eliminarGrupo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idGrupo=" + gpo.IdGrupo;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            grupo x = new grupo();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            if (x.Nombre != null)
                return false;
            return true;
        }
        //**************************************Funciones para las actividades****************************************
        public static actividad insertarActividad(actividad act)
        {
            string scriptname = "insertarActividad.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "nombre=" + act.Nombre;
            query += "&idGrupo=" + act.IdGrupo;
            query += "&fechaIni=" + act.FechaInicial;
            query += "&fechaLim=" + act.FechaLimite;
            query += "&color=" + PrepararColor(act.Color);
            query += "&nick=" + act.Nick;
            Console.WriteLine(query);
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                   
            actividad x = new actividad();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            return x;
        }
        public static bool modificarActividad(actividad act)
        {
            string scriptname = "modificarActividad.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idActividad=" + act.IdActividad;
            query += "&nombre=" + act.Nombre;            
            query += "&idFechaIni=" + act.FechaInicial;
            query += "&idFechaLim=" + act.FechaLimite;
            query += "&color=" + PrepararColor(act.Color);
            query += "&nick=" + act.Nick;
            //se crea el objeto para hacer el request
            WebRequest request = WebRequest.Create(query);
            request = WebRequest.Create(query);
            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;
            request.Proxy = WebProxy.GetDefaultProxy();

            //se obtiene la respuesta en un flujo de datos
            Stream objStream = request.GetResponse().GetResponseStream();

            //se convierte el flujo de datos en cadena
            StreamReader objReader = new StreamReader(objStream);
            string json = objReader.ReadLine();

            //deserialización de json a c# object
            actividad x = new actividad();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            if (x.Nombre.Length > 0)
            {
                return true;
            }
            return false;
        }
        public static actividad consultarActividad(actividad act)
        {
            string scriptname = "consultarActividad.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idActividad=" + act.IdActividad;
            //hacer request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            actividad x = new actividad();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            return x;
        }
        public static bool eliminarActividad(actividad act)
        {
            string scriptname = "eliminarActividad.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idActividad=" + act.IdActividad;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            actividad x = new actividad();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            if (x.Nombre != null)
                return false;
            return true;
        }
        //**************************************Funciones para los Usuarios****************************************
        public static usuario insertarUsuario(usuario user)
        {
            string scriptname = "insertarUsuario.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "nombreUsuario=" + user.NombreUsuario;
            query += "&password=" + user.Password;
            query += "&tipo=" + user.Tipo;
            query += "&nombre=" + user.Nombre;
            query += "&registro=" + user.Registro;
            query += "&grupo=" + user.Grupo;
            query += "&correo=" + user.Correo;            
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                   
            usuario x = new usuario();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }        
            return x;
        }
        public static bool modificarUsuario(usuario user)
        {
            string scriptname = "modificarUsuario.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idUsuario=" + user.IdUsuario;
            query += "&nombreUsuario=" + user.NombreUsuario;
            query += "&password=" + user.Password;
            query += "&tipo=" + user.Tipo;
            query += "&nombre=" + user.Nombre;
            query += "&registro=" + user.Registro;
            query += "&grupo=" + user.Grupo;
            query += "&correo=" + user.Correo;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                   
            usuario x = new usuario();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }            
            if (x.Nombre!= null)
            {
                return true;
            }
            return false;
        }
        /*
        public static user consultarUsuario(usuario user)
        {
            string scriptname = "consultarActividad.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idActividad=" + act.IdActividad;
            //hacer request
            string json = HacerRequest(query);

            //deserialización de json a c# object
            actividad x = new actividad();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            //reconvertir color
            x.Color = ConvertirColor(x.Color);
            return x;
        }*/
        public static bool eliminarUsuario(usuario user)
        {
            string scriptname = "eliminarUsuario.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idUsuario=" + user.IdUsuario;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            actividad x = new actividad();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            if (x.Nombre != null)
                return false;
            return true;
        }
        public static Usuarios BuscarAlumnosGrupo(string gpo)
        {
            string scriptname = "buscarAlumnosGrupo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "grupo=" + gpo;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            Usuarios x = new Usuarios();
            try
            {
                Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            }
            catch { }            
            return x;
        }
        public static Usuarios ObtenerAlumnosGrupo(grupo gpo)
        {
            string scriptname = "ObtenerAlumnosGrupo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idGrupo=" + gpo.IdGrupo;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            Usuarios x = new Usuarios();
            try
            {
                Newtonsoft.Json.JsonConvert.PopulateObject(json, x);
            }
            catch { }
            return x;
        }
        public static bool InsertarIntegranteGrupo(int idGrupo,int idUsuario)
        {
            string scriptname = "insertarIntegranteGrupo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idGrupo=" + idGrupo;
            query += "&idUsuario=" + idUsuario;
            //se hace el request
            string json = HacerRequest(query);           
            if (json.Equals("Insertado"))
                return true;
            else
                return false;
        }
        //****************************Funciones par email****************************
        public static string VerficarCorreo(usuario user)
        {
            string scriptname = "validacionEmail.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "usuario=" + user.NombreUsuario;
            query += "&password=" + user.Password;
            query += "&correo=" + user.Correo;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            string x = "";
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            return x;  
        }
        //****************************Funciones de chat****************************
        public static bool EnviarMensaje(mensaje mess)
        {
            string scriptname = "enviarMensaje.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idRemitente=" + mess.IdRemitente;
            query += "&idDestinatario=" + mess.IdDestinatario;
            query += "&fechaEnvio=" + mess.FechaEnvio;
            query += "&mensaje=" + mess.Mensaje;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object
            mensaje x = new mensaje();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            if(x.Mensaje!=null)
                return true;
            return false;
        }
        public static void ObtenerChat(Chat nuevoChat)
        {
            string scriptname = "obtenerChat.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "idusuario1=" + nuevoChat.Host.IdUsuario;
            query += "&idusuario2=" + nuevoChat.Guest.IdUsuario;            
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object            
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, nuevoChat); }
            catch { }            
        }
        /*
         * ********************************Funciones de las listas del alumno**************************************
         */

        public static void ObtenerGruposAlumno(Grupos gpo)
        {
            string scriptname = "ObtenerGruposAlumno.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "IdUsuario=" + DatosGlobales.VariablesGlobales.miusuario.IdUsuario;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object            
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, gpo); }
            catch { }
        }
        public static materia ObtenerMateriasAlumno(grupo gpo)
        {
            string scriptname = "ObtenerMateriasAlumno.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "IdGrupo=" + gpo.IdGrupo;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                   
            materia x = new materia();
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }
            //reconvertir colores
            if (x.Color != null)
                x.Color = ConvertirColor(x.Color);
            return x;
        }
        public static Actividades ObtenerActividadesAlumno(grupo gpo)
        {
            string scriptname = "ObtenerActividadesAlumno.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "IdGrupo=" + gpo.IdGrupo;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                   
            Actividades x = new Actividades();
            x.IdGrupo = 2;
            try { Newtonsoft.Json.JsonConvert.PopulateObject(json, x); }
            catch { }


            //reconvertir colores
            if (x.actividades != null)
                for (int cont = 0; cont < x.actividades.Count; cont++)
                {
                    actividad obj = x.actividades[cont];
                    obj.Color = ConvertirColor(obj.Color);
                }
            return x;
        }

        /*
        * ********************************Funciones de las soluciones alumno**************************************
        */

        /*
        * ********************************Funciones de las soluciones profesor**************************************
        */

        /*
        * ********************************Funciones de los directorios en el servidor**************************************
        */
        public static string CrearDirectorio(string carpeta)
        {
            string scriptname = "CrearDirectorio.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "carpeta=" + carpeta;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                  
            return json;          
        }
        public static string EliminarDirectorio(string carpeta)
        {
            string scriptname = "EliminarDirectorio.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "carpeta=" + carpeta;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                  
            return json;
        }
        public static string RenombrarFichero(string ficheroOrigen,string ficheroDestino)
        {
            string scriptname = "RenombrarFichero.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "origen=" + ficheroOrigen;
            query += "&destino=" + ficheroDestino;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                  
            return json;
        }
        /*
      * ********************************Funciones de los archivos en el servidor**************************************
      */
        public static string CrearArchivo(string fichero)
        {
            string scriptname = "CrearArchivo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "fichero=" + fichero;            
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                  
            return json;
        }
        public static string EliminarArchivo(string fichero)
        {
            string scriptname = "EliminarArchivo.php?";
            //Se crea la cadena para hacer el request
            string query = baseURL + scriptname;
            query += "fichero=" + fichero;
            //se hace el request
            string json = HacerRequest(query);
            //deserialización de json a c# object                  
            return json;
        }


    }
}
