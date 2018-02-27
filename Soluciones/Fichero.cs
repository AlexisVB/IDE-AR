using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
namespace IDE_AR.Soluciones
{
    public class Fichero
    {
        //tipo raiz
        //0=Es solo un archivo
        //1=Solucion Proyecto
        //2=carpeta
        public int IdFichero { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string RutaLocal { get; set; }
        public int TipoRaiz { get; set; }
        public int IdRaiz { get; set; }
        public string Fecha { get; set; }
        public bool IsFolder { get; set; }
        public Fichero Parent { get; set; }
        public string contenido { get; set; }
        public ObservableCollection<Fichero> Ficheros { get; set; }
        public bool GuardarArchivo()
        {            
                try
                {
                    string ar = this.RutaLocal + "//" + this.Nombre + ".cpp";
                    //Elimina el archivo
                    File.Delete(ar);
                    //crea el archivo de texto y escribe su contenido
                    FileStream fs = new FileStream(ar, FileMode.Open, FileAccess.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.Begin);
                    sw.Write(this.contenido);
                    fs.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
           
        }
        public bool RenombrarArchivo(string nuevoNombre)
        {
            try
            {
                string origen = this.RutaLocal + "//" + this.Nombre + ".cpp";
                string destino = this.RutaLocal + "//" + nuevoNombre + ".cpp";
                //Elimina el archivo
                File.Delete(origen);
                //crea el archivo de texto y escribe su contenido
                FileStream fs = new FileStream(destino, FileMode.CreateNew, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.Begin);
                sw.Write(this.contenido);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EliminarArchivo()
        {
            try
            {
                string ar = this.RutaLocal + "//" + this.Nombre + ".cpp";
                //Elimina el archivo
                File.Delete(ar);               
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        public string LeerArchivo()
        {
            try
            {
                string ar = this.RutaLocal + "//" + this.Nombre + ".cpp";
                FileStream fs = new FileStream(ar, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin); //Ponemos el cursor de lectura en el caracter 0
                contenido = sr.ReadToEnd();
                sr.Close();
                fs.Close();                
                return contenido;
            }
            catch
            {
                return "";
            }
                        
        }
    }
}
