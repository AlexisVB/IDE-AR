using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace IDE_AR.Analizadores
{
    public class RS
    {
        public List<token> tokens;
        const int directiva = 0;
        const int palabraReservada = 1;
        const int identificador = 2;
        const int digito = 3;
        const int operador = 4;
        const int simbolo = 5;
        const int indefinido = 6;
        const int grafico = 7;
        const int cadena = 8;
        const int error = 9;
        public RS(List<token> t)
        {
            tokens = t;
        }
        public void demoTokens()
        {
            string contenido="";
            token currentToken;
            for(int cont=0;cont<tokens.Count;cont++)
            {
                currentToken = tokens[cont];
                switch(currentToken.Tipo)
                {
                    case directiva:
                        contenido += "\r\n" + "Tipo: directiva\t\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case palabraReservada:
                        contenido += "\r\n" + "Tipo: reservada\t\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case identificador:
                        contenido += "\r\n" + "Tipo: identificador\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case digito:
                        contenido += "\r\n" + "Tipo: digito\t\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case operador:
                        contenido += "\r\n" + "Tipo: operador\t\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case simbolo:
                        contenido += "\r\n" + "Tipo: simbolo\t\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case indefinido:
                        contenido += "\r\n" + "Tipo: indefinido\t\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case grafico:                     
                        if (currentToken.Lexema.Length>1)
                            contenido += "\r\n" + "Tipo: comentario\t " + "-- \tLexema:\t "+ currentToken.Lexema;                       
                        break;
                    case cadena:
                        contenido += "\r\n" + "Tipo: cadena\t\t" + "-- \tLexema:\t " + currentToken.Lexema;
                        break;
                    case error:
                        contenido += "\r\n" + "Tipo: error\t\t" + "-- \tLexema:\t" + currentToken.Lexema;
                        break;                      
                }                
            }
            escribirTokens(contenido);
        }
        private void escribirTokens(string contenido)
        {           
           
            string ar = "tokens.show";
            //crea el archivo de texto    
            try
            {
                File.Delete(ar);
            }
            catch { }
            FileStream fs = new FileStream(ar, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.Write(contenido);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
