using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IDE_AR.Datos;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for VentanaEliminar.xaml
    /// </summary>
    public partial class VentanaEliminar : Window
    {
        private delete del;
        private materia Materia;
        private grupo Grupo;
        private actividad Actividad;
        private usuario Usuario;
        List<actividad> listaActividades;
        public VentanaEliminar(delete d,Object objeto)
        {
            InitializeComponent();
            del = d;            
           switch(del)
           {
               case delete.Materia:
                   Materia = (materia)objeto;
                   tbDatosAEliminar.Text = "Eliminar materia: " + Materia.Nombre;
                   break;
               case delete.Grupo:
                   Grupo = (grupo)objeto;
                   tbDatosAEliminar.Text = "Eliminar grupo: " + Grupo.Nombre;
                   break;
               case delete.Actividad:
                   Actividad = (actividad)objeto;
                   tbDatosAEliminar.Text = "Eliminar actividad: " + Actividad.Nombre;
                   break;
               case delete.Usuario:
                   Usuario = (usuario)objeto;
                   tbDatosAEliminar.Text = "Usuario: " + Usuario.Nombre;
                   break;
           }            
        }
        private void btnAceptar_Click(Object sender, RoutedEventArgs e)
        {
            string Text = "¿Esta seguro que desea eliminar: ";
            this.Opacity = 0.5;
            VentanaAdvertencia mostrar = new VentanaAdvertencia();
            switch (del)
            {
                case delete.Materia:
                    Text = Text + Materia.Nombre;
                    break;
                case delete.Grupo:
                    Text = Text + Grupo.Nombre;
                    break;
                case delete.Actividad:
                    Text = Text + Actividad.Nombre;                    
                    break;
                case delete.Usuario:
                    Text = Text + Usuario.Nombre;
                    break;
            }
            Text = Text + "?\n Se eliminara todo su contenido y\n los datos ligados al objeto.";
            mostrar.Texto = Text;
            if(mostrar.ShowDialog()==true)
            {
                //eliminar objeto
                bool eliminado=false;
                switch (del)
                {
                    case delete.Materia:
                        //eliminado=Materia.Eliminar();
                        eliminado=InterfaceHttp.eliminarMateria(Materia);
                        //eliminar grupos
                        List<grupo> listaGrupos = Materia.listaGrupos;
                        if(listaGrupos!=null)
                        {
                            for(int i=0;i<listaGrupos.Count;i++)
                            {
                                grupo gpo = listaGrupos[i];
                                listaActividades = gpo.listaActividades;
                                if (listaActividades != null)
                                {
                                    for (int cont = 0; cont < listaActividades.Count; cont++)
                                    {
                                        actividad act = listaActividades[cont];
                                        InterfaceHttp.eliminarActividad(act);
                                    }
                                }       
                                InterfaceHttp.eliminarGrupo(gpo);
                            }
                          
                        }
                        //y  actividades
                        break;
                    case delete.Grupo:
                       // eliminado=Grupo.Eliminar();
                        eliminado = InterfaceHttp.eliminarGrupo(Grupo);
                        //eliminar ACtividades
                        //actualizar lista de grupos                        
                        listaActividades=Grupo.listaActividades;
                        if(listaActividades!=null)
                        {
                            for(int cont=0;cont<listaActividades.Count;cont++)
                            {
                                actividad act = listaActividades[cont];
                                InterfaceHttp.eliminarActividad(act);
                            }
                        }                      
                        break;
                    case delete.Actividad:
                        //eliminado=Actividad.Eliminar();
                        eliminado = InterfaceHttp.eliminarActividad(Actividad);
                        break;
                    case delete.Usuario:
                        //eliminado=Actividad.Eliminar();
                        eliminado = InterfaceHttp.eliminarUsuario(Usuario);
                        break;
                }
                if(eliminado==true)
                {
                    Mensaje("Eliminado Correctamente");
                    DialogResult = true;
                }
                else
                {
                    Mensaje("Error al eliminar");
                    DialogResult = false;
                }
                
            }
            this.Opacity = 1;           
        }
        private void btnCancelar_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void Mensaje(string Text)
        {
            this.Opacity = 0.5;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 1;
        }
        
    }
}
