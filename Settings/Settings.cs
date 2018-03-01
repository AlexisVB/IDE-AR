using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IDE_AR.DatosGlobales;
namespace IDE_AR.Settings
{
    public class Settings
    {
        public Settings()
        {
            CargarLocacionPredeterminada();
        }
        public void CargarLocacionPredeterminada()
        {
             string dir = "Settings";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string ar = "Settings//defaultLocation.bin";                        
            //crea el archivo de texto            
            FileStream fs = new FileStream(ar, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string ruta=sr.ReadToEnd();            
            sr.Close();
            fs.Close();

            VariablesGlobales.RutaPredeterminada = ruta;
        }
    }
}
