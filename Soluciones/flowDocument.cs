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
        public FlowDocument ConstruirFlowDocument(string content)
        {
            position = 0;
            contenido = content;
            contenido=quitarCaracteres(contenido);            
            documentFlow = new FlowDocument();
            //construir
            //#Include<stdio.h\c/
            /*
             * <Paragrahp>
             *  <Run>#</Run>
             * </Paragrahp>
             * */
            Paragraph nuevoParrafo = new Paragraph();
            while(position<this.contenido.Length)
            {
                char c = contenido[position];
                Run caracter = new Run();
                caracter.Text = "";
                switch (c)
                {
                    case '►':
                            if (position + 1 < contenido.Length && contenido[position+1] == '◄')
                            {
                                //agregar run especial
                                position ++;
                                caracter.Text = "│";
                                caracter.FontSize = 16;
                                caracter.Focus();
                                caracter.FontWeight = System.Windows.FontWeights.Light;
                                caracter.Foreground = System.Windows.Media.Brushes.Purple;
                                nuevoParrafo.Inlines.Add(caracter);
                            }                           
                            break;
                    case '\n':
                        nuevoParrafo.Inlines.Add(new LineBreak());
                        break;
                    case '\r':
                        position++;
                        break;
                    
                    default:
                        //crear run con el puro caracter
                        caracter.Text += contenido[position];
                        nuevoParrafo.Inlines.Add(caracter);
                        break;
                }
                position++;
            }
            documentFlow.Blocks.Add(nuevoParrafo);
            return documentFlow;            
        }
        private string quitarCaracteres(string content)
        {
            string temp = "";
            position=0;
            while(position<content.Length)
            {
                if (content[position] != '\r')
                    temp += content[position];
                position++;
            }            
            position = 0;
            return temp;
        }        
    }
}
