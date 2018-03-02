using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using IDE_AR.DatosGlobales;
using IDE_AR.Datos;
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
        public SolucionProyecto miSolucion;
        string ruta = VariablesGlobales.RutaPredeterminada;
        private int subidos;//variable que cuenta cuantos archivos se subieron
        private int todos;//variable que cuenta cuantos archivos hay
        public adminSolucion()
        {

        }
        public adminSolucion(SolucionProyecto nva)
        {
            miSolucion = nva;
        }
    
        public bool ConstruirProyecto()
        {
            solucion = encabezado + "/" + miSolucion.Nombre + ":{}";
            string dir = miSolucion.RutaLocal;
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
        public ObservableCollection<Fichero> LeerArbol()
        {
                ObservableCollection<Fichero> lista = new ObservableCollection<Fichero>();             
                    if (pos<solucion.Length&&solucion[pos] == '{')
                    {
                        //comeinza en el primer identificador
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
                                                              
                            }
                            try { lista.Add(f); }
                            catch { }
                            if (pos < solucion.Length & solucion[pos] == '}')
                            {
                                pos++;
                                break;
                            }
                                
                            if (pos< solucion.Length&solucion[pos]==',')
                            {
                                //brincar coma
                                pos++;
                            }                  
                        }
                       
                    }                
            return lista;
        }
        private string ObtenerIdentificador()
        {
            string identificador = "";
            if (solucion[pos] == ',')
                pos++;
            while(solucion[pos]!=','&&solucion[pos]!=':'&&solucion[pos]!='{'&&solucion[pos]!='}')
            {
                identificador += solucion[pos];
                pos++;
            }
            return identificador;
        }
        public bool ActualizarSolucion()
        {
            miSolucion.RutaLocal= ruta;
            solucion = encabezado + "/" + miSolucion.Nombre + "{";
            ObtenerArbol(miSolucion.Ficheros);
            solucion += "}";              
            string ar = miSolucion.RutaLocal + "//" + miSolucion.Nombre +"//"+miSolucion.Nombre+".ar";
       
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
        public void ObtenerArbol(ObservableCollection<Fichero> lista)
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
                string ar = miSolucion.RutaLocal + "//" + miSolucion.Nombre + ".ar";
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
        public void AsignarPadres()
        {
            Fichero padre = new Fichero();
            padre.IdFichero = miSolucion.IdProyecto;
            padre.TipoRaiz = 0;
            padre.Nombre = miSolucion.Nombre;
            padre.RutaLocal = miSolucion.RutaLocal;
            Padrificar(padre,miSolucion.Ficheros);          
        }
        private void Padrificar(Fichero padre,ObservableCollection<Fichero> lista)
        {
            if (lista != null)
            {
                int cont;
                Fichero f;
                for (cont = 0; cont < lista.Count; cont++)
                {
                    f = lista[cont];
                    if (f.IsFolder)
                    {
                         f.Parent = padre;
                         f.Ruta = padre.Ruta + "/" + padre.Nombre;
                         f.RutaLocal = padre.RutaLocal + "//"+padre.Nombre;
                         Padrificar(f, f.Ficheros);     
                       
                    }
                    else
                    {
                        f.Parent = padre;
                        f.Ruta = padre.Ruta + "/" + padre.Nombre;
                        f.RutaLocal = padre.RutaLocal + "//" + padre.Nombre;
                    }                    

                }

            }
        }
        public int[] SubirANube()
        {
            int[] arreglo = new int[2]; 
            if(InterfaceHttp.InsertarSolucion(miSolucion))
            {
                subidos = 0;
                todos = 0;
                Fichero padre = new Fichero();                
                padre.IdFichero = miSolucion.IdProyecto;
                padre.TipoRaiz = 0;
                padre.Nombre = miSolucion.Nombre;
                padre.Ruta = miSolucion.Ruta;
                todos++;
                int response = Int32.Parse(InterfaceHttp.CrearDirectorio(miSolucion.Ruta));
                if (response == 1)
                    subidos++;
                subirFicheros(padre, miSolucion.Ficheros);
                arreglo[0] = todos;
                arreglo[1] = subidos;
            }
            else
            {
                arreglo[0]=-1;
                arreglo[1] =-1;
            } 
            return arreglo;
        }
        private void subirFicheros(Fichero padre,ObservableCollection<Fichero> lista)
        {
            if (lista != null)
            {
                int cont;
                Fichero f;
                for (cont = 0; cont < lista.Count; cont++)
                {
                    f = lista[cont];
                    string dir = padre.Ruta + "/" + f.Nombre;
                    f.Ruta = padre.Ruta + "/" + f.Nombre;
                    todos++;
                    if (f.IsFolder)
                    {
                       //mkdir
                        int response=Int32.Parse(InterfaceHttp.CrearDirectorio(dir));
                        if (response == 1)
                            subidos++;
                        subirFicheros(f, f.Ficheros);
                    }
                    else
                    {
                        //leerArchivo
                        f.LeerArchivo();
                        //subirArchivo                                                
                        int response = Int32.Parse(InterfaceHttp.EscribirArchivo(dir,f.ConvertirParaNube()));
                        if (response == 1)
                            subidos++;
                    }

                }

            }
        }
    }
}
