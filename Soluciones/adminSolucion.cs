using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IDE_AR.DatosGlobales;
namespace IDE_AR.Soluciones
{
    public class adminSolucion
    {
        //archivo .ar
        //Encabezado
        //.arIDE-AR/NombreSolucion:{Archivos.cpp,funciones.cpp,datos:{}}}
        //encabezado/NombreSolucion:{archivos}
        string encabezado = ".arIDE-AR";
        string solucion="";     
        int pos = 10;
        SolucionProyecto nueva;
        string ruta = VariablesGlobales.RutaPredeterminada;
        public adminSolucion()
        {

        }
        public adminSolucion(SolucionProyecto nva)
        {
            nueva = nva;
        }
    
        public bool ConstruirProyecto()
        {
            solucion = encabezado + "/" + nueva.Nombre + ":{}";
            string dir = nueva.RutaLocal;
            try
            {
                if (Directory.Exists(dir))
                    return false;
                Directory.CreateDirectory(dir);
            }
            catch { }
           
            return EscribirSolucion(solucion);
        }
        public SolucionProyecto ObtenerProyecto(string fichero)
        {
            FileStream fs = new FileStream(fichero, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);            
            solucion=sr.ReadToEnd();           
            sr.Close();
            fs.Close();            
            string nombre = "";
            //obtener el nombre
            nombre = ObtenerIdentificador();
            SolucionProyecto nuevo=new SolucionProyecto();
            nuevo.Nombre = nombre;            
            nuevo.Ficheros = LeerArbol();
            return nuevo;
        }
        public List<Fichero> LeerArbol()
        {
                List<Fichero> lista = new List<Fichero>();             
                    if (solucion[pos] == '{')
                    {
                        pos++;
                        while (pos<solucion.Length&&solucion[pos] != '}')
                        {
                            Fichero f = new Fichero();
                            f.Nombre = ObtenerIdentificador();
                            if (solucion[pos] == '{')
                            {
                                f.IsFolder = true;                                
                                f.Ficheros = LeerArbol();
                            }
                            else
                            {
                                f.IsFolder = false;                               
                                pos++;
                            }
                            lista.Add(f);
                        }
                       if(pos+1<solucion.Length)
                       {
                           //brincar coma
                           pos++;
                       }
                    }                
            return lista;
        }
        private string ObtenerIdentificador()
        {
            string identificador = "";
            while(solucion[pos]!=','&&solucion[pos]!=':'&&solucion[pos]!='{'&&solucion[pos]!='}')
            {
                identificador += solucion[pos];
                pos++;
            }
            return identificador;
        }
        public bool ActualizarSolucion()
        {
            nueva.RutaLocal = ruta + "//" + nueva.Nombre;
            solucion = encabezado + "/" + nueva.Nombre + "{";
            ObtenerArbol(nueva.Ficheros);
            solucion += "}";
            string ar = nueva.RutaLocal + "//" + nueva.Nombre + ".ar";
            //Elimina el archivo de texto
            File.Delete(ar);
            //crea el archivo de texto
            FileStream fs = new FileStream(ar, FileMode.CreateNew, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.Write(solucion);
            sw.Flush();
            sw.Close();
            fs.Close();
            return true;
        }
        public void ObtenerArbol(List<Fichero> lista)
        {
            if(lista!=null)
            {
                int cont;
                Fichero f;
                for (cont = 0; cont < lista.Count; cont++)
                {
                    f = lista[cont];
                    if(cont==lista.Count-1)
                    {
                        if (f.IsFolder)
                        {
                            solucion += f.Nombre + "{";
                            ObtenerArbol(f.Ficheros);
                            solucion += "}";
                        }
                        else
                        {
                            solucion += f.Nombre;
                        } 
                    }
                    else
                    {
                        if (f.IsFolder)
                        {
                            solucion += f.Nombre + "{";
                            ObtenerArbol(f.Ficheros);
                            solucion += "},";
                        }
                        else
                        {
                            solucion += f.Nombre + ",";
                        } 
                    }
                   
                }                
              
            }
        }
        public bool EscribirSolucion(string solucion)
        {
            try
            {
                
                //log.bin
                //string ruta = nueva.RutaLocal + nueva.Nombre;
                string ar = nueva.RutaLocal + "//" + nueva.Nombre + ".ar";
                //crea el archivo de texto
                FileStream fs = new FileStream(ar, FileMode.CreateNew, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.Begin);                
                sw.Write(solucion);
                sw.Flush();
                sw.Close();
                fs.Close();
                return true;
            }catch (IOException ex)
            {
                return false;
            }
        }
    }
}
