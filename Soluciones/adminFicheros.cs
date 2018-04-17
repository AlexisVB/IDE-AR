using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace IDE_AR.Soluciones
{
    public class adminFicheros
    {
        Fichero currentFichero;
        public adminFicheros(Fichero cur)
        {
            currentFichero = cur;
        }

        public bool CrearArchivo()
        {
            if(currentFichero.IsFolder)
            {
                if (Directory.Exists(currentFichero.RutaLocal + "//" + currentFichero.Nombre))
                    return false;
                Directory.CreateDirectory(currentFichero.RutaLocal + "//" + currentFichero.Nombre);
                return true;
            }
            else
            {
                try
                {
                    string ar = currentFichero.RutaLocal + "//" + currentFichero.Nombre + ".cpp";
                    //crea el archivo de texto
                    FileStream fs = new FileStream(ar, FileMode.CreateNew, FileAccess.ReadWrite);
                    fs.Close();
                    return true;
                }
                catch
                {
                    return false;
                }               
            }
        }
        public void GuardarArchivo()
        {

        }
        public void RenombrarArchivo()
        {

        }
        public void EliminarArchivo()
        {

        }
    }
}
