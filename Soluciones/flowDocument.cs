using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Documents;
namespace IDE_AR.Soluciones
{
    public class flowDocument
    {
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
        public string contenidoFlow { get; set; }

        public FlowDocument documentFlow { get; set; }
        public ObservableCollection<Fichero> Ficheros { get; set; }

        private int position;
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
        public FlowDocument ConstruirFlowDocument(string contenido)
        {
            position = 0;
            this.contenido = contenido;
            quitarCaracteres();            
            documentFlow = new FlowDocument();
            //construir
            Paragraph nuevoParrafo = new Paragraph();
            while(position<this.contenido.Length)
            {
                Run linea = new Run();
                linea.Text = getLine();
                if (linea.Text.Length > 0)
                {
                    nuevoParrafo.Inlines.Add(linea);
                    nuevoParrafo.Inlines.Add(new LineBreak()); 
                }                
            }
            documentFlow.Blocks.Add(nuevoParrafo);
            return documentFlow;
            
        }
        private void quitarCaracteres()
        {
            string temp = "";
            position=0;
            while(position<contenido.Length)
            {
                if (contenido[position] != '\r')
                    temp += contenido[position];
                position++;
            }
            contenido = temp;
            position = 0;
        }
        private string getLine()
        {
            string line = "";

            while (position < contenido.Length &&contenido[position] != '\n')
            {
                line += contenido[position];
                position++;
            }
            position++;            
            
            return line;
        }
    }
}
