using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    class Enigma
    {
        //"alexisAndre1998"
        //0bd7caeeb83897891a1470a7f9e73328
        string Secret = "0e894783";
        public string encriptar(string entrada)
        {
            int pos = 0;
            string salida = "";
            while (pos < entrada.Length)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (pos == entrada.Length)
                        break;
                    int c = ((int)entrada[pos]) + ((int)Secret[k]);
                    salida +=(char)c;
                    pos++;
                }
            }
            return salida;
        }
        public string desencriptar(string entrada)
        {
            int pos = 0;
            string salida = "";
            while (pos < entrada.Length)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (pos == entrada.Length)
                        break;
                    int c = ((int)entrada[pos]) - ((int)Secret[k]);
                    salida += (char)c;
                    pos++;
                }
            }
            return salida;
        }
    }
}
