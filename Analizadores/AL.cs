using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Analizadores
{
    public class AL
    {
        List<token> tokens;
        //tipos de tokens
        //0=Directiva
        //1=Palabra Reservada
        //2=Identificador
        //3=digito
        //4=operador        
        //5=caracter especial
        //6=Indefinido
        //7=gráfico
        //8=cadena;
        //9=eror;
        int directiva = 0;
        int palabraReservada=1;
        int identificador=2;
        int digito=3;
        int operador=4;
        int simbolo=5;
        int indefinido=6;
        int grafico=7;
        int cadena = 8;
        int error = 9;

        public int lineaCont;
        public int contTokens = 0; 
        private int pos;
        public string entrada;
        string[] reservadas = new string[33];
        public AL(string s)
        {
            entrada = s;
            lineaCont = 0;
            pos = 0;
            int x = entrada.Length/3;
            tokens = new List<token>();
            cargarReservadas();
            
        }
        private void cargarReservadas()
        {
            reservadas[0]="auto";
            reservadas[1]="break";
            reservadas[2]="case";
            reservadas[3]="char";
            reservadas[4]="const";	
            reservadas[5]="continue";      	
            reservadas[6]="default";
            reservadas[7]="do";	
            reservadas[8]="double";	
            reservadas[9]="else";	
            reservadas[10]="enum";	
            reservadas[11]="extern";
            reservadas[12]="float";	
            reservadas[13]="for";	
            reservadas[14]="goto";	
            reservadas[15]="if";	
            reservadas[16]="int";	
            reservadas[17]="long";	
            reservadas[18]="register";	
            reservadas[19]="return";	
            reservadas[20]="short";	
            reservadas[21]="signed";	
            reservadas[22]="sizeof";	
            reservadas[23]="static";	
            reservadas[24]="struct";
            reservadas[25]="switch";
            reservadas[26]="typedef";	
            reservadas[27]="union";	
            reservadas[28]="unsigned";	
            reservadas[29]="void";	
            reservadas[30]="volatile";
            reservadas[31] ="while";
        }
        public List<token> ObtenerTokens()
        {
            //#<incude>stdio;
           
            string lexema="";
            int tipo=0;                              
            //analizar toda la cadena
            for (pos = 0; pos < entrada.Length; pos++)
            {
                char c = entrada[pos];
                int ascii = c;
                //es un numero               

                //es un identificador o palabra reservada
                if ((ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || ascii == 95 || (ascii >= 48 && ascii <= 57))
                {
                    lexema = ObtenerPalabra();
                    tipo = validarPalabra(lexema);
                }               
                //caracter no imprimible
                else if((ascii>=0&&ascii<=31)|ascii==127)
                {                    
                    //solo el caracter '\n' y '\t' son  imprimibles
                    if(c==10||c==09)
                    {
                        lexema = c.ToString();
                        if (c == 10)
                            lineaCont++;
                    }
                    else
                    {
                        c =(char) 178;
                        lexema = c.ToString();
                    }
                    tipo =grafico;
                }
                else if(c>127)
                {
                    lexema += c;
                    lexema = c.ToString();
                }
                else
                {
                    lexema = c.ToString();
                    switch (ascii)
                    {
                            
                        case 32:// espacio                          
                            tipo = grafico;
                            break;
                        case 33:// !                            
                            tipo = operador;
                            break;
                        case 34:// "
                            pos++;
                            lexema = '"'+ObtenerCadena();
                            tipo = cadena;
                            break;
                        case 35://#
                                //obtener directiva
                            lexema = ObtenerDirectiva();
                            tipo = directiva;
                            break;
                        case 37://%
                            tipo = operador;
                            break;
                        case 38://&
                            tipo = operador;
                            break;
                        case 39:// '
                            pos++;
                            lexema = "'"+ObtenerCaracter();
                            tipo = cadena;
                            break;
                        case 40:// (
                            tipo = simbolo;
                            break;
                        case 41:// )
                            tipo = simbolo;
                            break;
                        case 42:// *
                            tipo = operador;
                            break;
                        case 43:// +
                            tipo = operador;
                            break;
                        case 44:// ,
                            tipo = simbolo;
                            break;
                        case 45:// -
                            tipo = operador;
                            break;
                        case 46:// .
                            tipo = operador;
                            break;
                        case 47:// /
                            if (entrada[pos + 1] == 47)//es un comentario                                
                            {
                                //get comentario
                                lexema = ObtenerComentario();
                                tipo = grafico;
                            }                                
                            else
                            {
                                tipo = operador;
                            }
                                
                            break;
                        case 58:// :
                            tipo = simbolo;
                            break;
                        case 59:// ;
                            tipo = simbolo; 
                            break;
                        case 60:// <
                            tipo = operador;
                            break;
                        case 61:// =
                            tipo = operador;
                            break;
                        case 62:// >
                            tipo = operador;
                            break;
                        case 63:// ?
                            tipo = indefinido;
                            break;
                        case 64:// @
                            tipo = indefinido;
                            break;
                        case 91:// [
                            tipo = simbolo;
                            break;
                        case 92:// \
                            tipo = simbolo;
                            break;
                        case 93:// ]
                            tipo = simbolo;
                            break;
                        case 94:// ^
                            tipo = operador;
                            break;                       
                        case 96:// `
                            tipo = indefinido;
                            break;
                        case 123:// {
                            tipo = simbolo;
                            break;
                        case 124:// |
                            tipo = operador;
                            break;
                        case 125:// }
                            tipo = simbolo;
                            break;
                        case 126:// ~
                            tipo = indefinido;
                            break;
                    }
                }
                tokens.Add(new token(tipo,lexema));               
                contTokens++;
            }
            //return 
            return tokens;
        }//genera los tokens que ayudan al remarcador sintactico 
        public string [,] FiltrarTokens()//Limpia la lista de los tokens para el analizador sintactico
        {
            return null;
        }
        private string ObtenerDigito()
        {
            string temp="";
            char c='0';
            while(c>=48&&c<=57)
            {   
                c=entrada[pos];
                temp += c;
                pos++;
            }
            pos--;
            return temp;
        }
        private string ObtenerPalabra()
        {
            string temp = "";
            char c = '0';
            int final = 0;
            while (pos<entrada.Length&&final==0)
            {
                c = entrada[pos];              
                int ascii =(int)c;
                switch (ascii)
                {

                    case 32:// espacio                          
                        final = 1;
                        break;      
                   
                    case 37://%
                        final = 1;
                        break;
                    case 38://&
                        final = 1;
                        break;                   
                    case 40:// (
                        final = 1;
                        break;
                    case 41:// )
                        final = 1;
                        break;
                    case 42:// *
                        final = 1;
                        break;
                    case 43:// +
                        final = 1;
                        break;
                    case 44:// ,
                        final = 1;
                        break;
                    case 45:// -
                        final = 1;
                        break;                      
                    case 47:// /
                        final = 1;
                        break;
                    case 58:// :
                        final = 1;
                        break;
                    case 59:// ;
                        final = 1;
                        break;
                    case 60:// <
                        final = 1;
                        break;
                    case 61:// =
                        final = 1;
                        break;
                    case 62:// >
                        final = 1;
                        break;
                    case 63:// ?
                        final = 1;
                        break;
                    case 91:// [
                        final = 1;
                        break;                   
                    case 93:// ]
                        final = 1;
                        break;
                    default: break;                                    
                }
                if (final == 0)
                {
                    pos++;
                    temp += c;
                }   
            }
            pos--;//para regresar una posición
            return temp;
        }
        private string ObtenerComentario()
        {            
            string temp = "";
            char c='0';
            while(pos<entrada.Length&&c!=10)
            {
                c=entrada[pos];
                temp += c;
                pos++;
            }
            return temp;
        }

        private string ObtenerDirectiva()
        {
            string temp = "";
            char c=entrada[pos];
            while(pos<entrada.Length&&c!=10&&c!=13)
            {
                c=entrada[pos];
                temp += c;
                pos++;
            }
            pos--;
            return temp;
        }

        private string ObtenerCadena()
        {
            string temp = "";
            char c = '0';//solo inicializando           
            while (pos < entrada.Length && c!=34)
            {
                c = entrada[pos];
                temp += c;
                pos++;
            }
            pos--;
                    
            return temp;
        }
        private string ObtenerCaracter()
        {
            string temp = "";
            char c = '0';//solo inicializando           
            while (pos < entrada.Length && c != 39)
            {
                c = entrada[pos];
                temp += c;
                pos++;
            }
            pos--;

            return temp;
        }
        private int validarPalabra(string palabra)
        {
            int puntero = 0;
            char c = palabra[puntero];
            if(esCaracterRancio(c)&&c!=95&&c!=46)
            {
                return error;
            }
            else
            {
                if (palabra.Length == 1)
                {
                    if (esLetra(c))
                        return identificador;
                    else if (esNumero(c))
                        return digito;
                    else
                        return error;
                }
                else
                {

                    if (esLetra(c) || c == 95)//comienza con letra o _ es un identificador
                    {
                        if (ValidarIdentificador(palabra))
                        {
                            if (esReservada(palabra))
                                return palabraReservada;
                            else
                                return identificador;
                        }
                           
                        else
                            return error;
                    }                  
                    else
                    {
                        if (ValidarDigito(palabra))
                            return digito;
                        else
                            return error;
                    }
                }      
            }               
        }
        private bool esLetra(char c)
        {          
            int ascii = c;
            if ((ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122))
                return true;
            else
                return false;
        }
        private bool esNumero(char c)
        {
            int ascii = c;
            if (ascii >= 48 && ascii <= 57)
                return true;
            else
                return false;
        }

        private bool esCaracterRancio(char c)
        {
            int ascii = c;
            if(!esLetra(c)&&!esNumero(c))//no es numero ni letra
                return true;
            return false;
        }
        private bool esReservada(string palabra)
        {
            /*
             * auto
                break
                case	
                char	
                const	
                continue	
                default	
                do	
                double	
                else	
                enum	
                extern	
                float	
                for	
                goto	
                if	
                int	
                long	
                register	
                return	
                short	
                signed	
                sizeof	
                static	
                struct
                switch
                typedef	
                union	
                unsigned	
                void	
                volatile	
                while
             * */
            for (int cont = 0; cont < 32;cont++ )
            {
                string s = reservadas[cont];
                if (palabra.Equals(s))
                    return true;
            }

                return false;
        }
        private bool ValidarIdentificador(string palabra)
        {
            int puntero = 0;
            char c=palabra[0];
            if (esCaracterRancio(c)&& c != 95)//revisa que el primer caracter no sea numerico o rancio
                return false;
            puntero++;
            while(puntero<palabra.Length)
            {               
                c = palabra[puntero];
                if (esCaracterRancio(c) && c != 95)//caracter rancio
                    return false;
                puntero++;
            }
            return true;
        }

        private bool ValidarDigito(string palabra)
        {
            int puntero = 0;
            bool tienePunto = false;
            while(pos<palabra.Length)
            {
                char c=palabra[pos];
                if (!esNumero(c) && c != 46)//revisa que solo tenga numeros y puntos
                    return false;
                if(c==46)//revisa que solo haya un punto
                {
                    if (tienePunto)
                        return false;
                    else
                        tienePunto = true;
                }
                pos++;
            }
            return true;
        }
        public void EliminarCaracteres()
        {
            string temp ="";
            for(int cont=0;cont<entrada.Length;cont++)
            {
                
                
            }
        }       

    }
}
