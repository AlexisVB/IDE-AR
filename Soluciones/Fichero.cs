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
        bool cursorFinLinea;
        int posx;
        int posy;
        int[] col;
        int countFilas;
        int currentFila;
        private char[,] matriz =new char[500,2000];
        public ObservableCollection<Fichero> Ficheros { get; set; }
        public Fichero()
        {
            cursorFinLinea = false;
            contenido = "";
            posx = 0;
            posy = 0;
            currentFila = 0;
        }
        private void LimpiarMatriz()
        {
            for(int cont=0;cont<2000;cont++)
            {
                for (int cont2 = 0; cont2 < 500; cont2++)
                {
                    matriz[cont2, cont]=' ';
                }
            }
        }
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
                    PasarMatrizAContenido();
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
                PasarMatrizAContenido();
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
                pasarContenidoAMatriz();
                return contenido;
            }
            catch
            {
                return "";
            }
                        
        }
        public string ObtenerContenido()
        {
            PasarMatrizAContenido();
            return contenido;
        }
        private void PasarMatrizAContenido()
        {
            contenido = "";            
            for(int cont=0;cont<countFilas;cont++)
            {
                for(int cont2=0;cont2<=col[cont];cont2++)
                {
                    contenido += matriz[cont2, cont];
                    if (cont2 == posx && cont == posy)
                        contenido += "►◄";
                }
            }
        }
        private void pasarContenidoAMatriz()
        {
            int cabezal=0;
            col=new int[1000];
            countFilas = 0;            
            while(cabezal<contenido.Length)
            {
                char c = contenido[cabezal];
                inicializarCols();
                while(cabezal<contenido.Length&&contenido[cabezal]!='\r')
                {
                    
                    matriz[col[countFilas], countFilas]=c;
                    col[countFilas]++;
                    cabezal++;
                }
                matriz[col[countFilas], countFilas] = c;//agregar \r\n
                matriz[col[countFilas] + 1, countFilas] ='\n';
                countFilas++;
                cabezal++;
            }
        }
        private void inicializarCols()
        {
            for(int cont=0;cont<1000;cont++)
            {
                col[cont] = 0;
            }
        }
        public string ConvertirParaNube()
        {
            string temp="";
           for(int cont=0;cont<contenido.Length;cont++)
           {
               string c = contenido[cont].ToString();
               if(c=="#")
                   c="\\23";
               temp+=c;
           }
            return temp;
        }

        public void EscribirCaracter(char c)
        {
            if(cursorFinLinea)
            {
                //el cursor esta al final de la linea
                if (c == '\r')
                {
                    matriz[posx, posy] = '\r';
                    matriz[++posx, posy] = '\n';
                    col[currentFila]++;
                    col[currentFila]++;
                    currentFila++;
                    MoverCursor(-col[currentFila], 1);//se regresa al inicio de la fila y avanza una fila
                                                      //hay que recorrer todas las filas hacia abajo
                }
                else
                {
                    matriz[posx, posy] = c;
                    col[currentFila]++;
                    MoverCursor(1, 0);//avanza una casilla en la misma fila 
                }     
            }
            else
            {
                //no esta al final por lo tanto hay que recorrer la matriz al escribir
                //el cursor esta en medio de la linea hay que brincar el resto de la fila hacia abajo
                if (c == '\r')
                {
                    matriz[posx, posy] = '\r';
                    matriz[++posx, posy] = '\n';
                    col[currentFila]++;
                    col[currentFila]++;
                    currentFila++;
                    MoverCursor(-col[currentFila], 1);//se regresa al inicio de la fila y avanza una fila
                }
                else
                {
                    RecorrerColumna(posx, col[currentFila]);
                    matriz[posx, posy] = c;
                    col[currentFila]++;
                    MoverCursor(1, 0);//avanza una casilla en la misma fila 
                }     
            }            
        }
        private void RecorrerColumna(int b,int e)
        {
            for(int cont=e;cont>=b;cont--)
            {
                matriz[cont + 1, currentFila] = matriz[cont, currentFila];
            }
        }

        public void MoverCursorIzquierda()
        {
            if(posx>0)
                MoverCursor(-1, 0);
            cursorFinLinea = false;
        }
        public void MoverCursorDerecha()
        {
            if (posx < 500)
                MoverCursor(1, 0);
        }
        public void MoverCursorArriba()
        {
            if (posy > 0)
                MoverCursor(0, -1);
        }
        public void MoverCursorAbajo()
        {
            if(posy<2000)
                MoverCursor(0, 1);
        }
        public void MoverCursor(int x,int y)
        {
            posx += x;
            posy += y;
        }

        public void Enter()
        {
            EscribirCaracter('\r');
        }
    }
}
