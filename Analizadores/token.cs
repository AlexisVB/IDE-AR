using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Analizadores
{
    public class token
    {
        public token() 
        { 
        }
        public token(int tipo,string lexema)
        {
            Tipo = tipo;
            Lexema = lexema;
        }
        public int Tipo{get;set;}
        public string Lexema{get;set;}

    }
}
