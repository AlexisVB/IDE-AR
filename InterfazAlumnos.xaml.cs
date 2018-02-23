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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IDE_AR.Datos;
using IDE_AR.DatosGlobales;
using IDE_AR.UsuariosForms;
using System.IO;
using IDE_AR.Soluciones;
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for InterfazAlumnos.xaml
    /// </summary>
    public partial class InterfazAlumnos : Window
    {
      
        private configuracion ventanaConfiguracion;
        //*********************Variables para las listas************************************
        private List<materia> listAsignatures = new List<materia>();
        private List<grupo> listGroups = new List<grupo>();
        private List<actividad> listActivities = new List<actividad>();

        private List<usuario> listStudentsActives = new List<usuario>();
        private List<usuario> listStudentsInactives = new List<usuario>();
        private List<usuario> listStudentsNoActives = new List<usuario>();
        private List<usuario> listStudentsGroup = new List<usuario>();
        private List<mensaje> listMenssages = new List<mensaje>();
        private List<Fichero> listFiles = new List<Fichero>();

        private materia currentMateria;
        private grupo currentGrupo;
        private actividad currentActividad;
        private usuario currentAlumnoGrupo;
        private Fichero currentFichero;

        public materia materiaContexto;//Variable usada solo para darle contexto a la lista materias
        public grupo grupoContexto;//Variable usada solo para darle contexto a la lista grupos
        public actividad actividadContexto;//Variable usada solo para darle contexto a la lista actividades
        public usuario usuarioContexto;//Variable usada solo para darle contexto a las listas de usuarios
        public mensaje mensajeContexto;
        public Fichero ficheroContexto;
        //**********************************************************
        //*************************Variables para el editor*********************************
        private FontFamily familiaPre;
        private double sizePre;
        public static RoutedUICommand RCGuardar = new RoutedUICommand();
        SolucionProyecto misolucion;
        //**********************************************************************************        
        public InterfazAlumnos()
        {
            InitializeComponent();
            try
            {
                //restaurar el estado desde los atributos del objeto
                //de la clase settings referenciado por default
                Rect restoreBounds = Properties.Settings.Default.MainRestoreBounds;
                Left = restoreBounds.Left;
                Top = restoreBounds.Top;
                if (restoreBounds.Width > 0)
                    Width = restoreBounds.Width;
                if (restoreBounds.Height > 0)
                    Height = restoreBounds.Height;
                WindowState = Properties.Settings.Default.MainWindowState;

            }
            catch { }        

            busquedaDatosListas();
            InicializarListas();
            InicializacionDatos();

        }
        private void InterfazAlumnos_Loaded(Object sender, RoutedEventArgs e)
        {
            familiaPre = ctEditor.FontFamily;
            sizePre = ctEditor.FontSize;
        }
        private void InterfazAlumnos_Closing(Object Sender, System.ComponentModel.CancelEventArgs e)
        {
            //Guardar el estado desde los atributos
            Properties.Settings.Default.MainRestoreBounds = RestoreBounds;
            Properties.Settings.Default.MainWindowState = WindowState;
            Properties.Settings.Default.Save();
        }

        private void InicializacionDatos()
        {
            txtEstado.Text = "Listo";
            tbUserName.Text = VariablesGlobales.miusuario.NombreUsuario;
        }
        private void ExecutedRCGuardar(Object sender, ExecutedRoutedEventArgs e)
        {
            //implementar aqui logica para la orden guardar
            MessageBox.Show("orden guardar SEMI-personalizada");
        }
        private void CanExecuteRCGuardar(Object sender, CanExecuteRoutedEventArgs e)
        {
            //validar cuando haya cambios en algun archivo
            e.CanExecute = true;
        }
        private void ArchivoSalir_Click(Object sender, RoutedEventArgs e)
        {
            Close();//cerrrar la ventana principal
        }
        private void acercaDe_Click(Object sender, RoutedEventArgs e)
        {
            String creditos = "Desarrolladores:\n" +
                             "-André Nicolás Ricoy Camacho\n" +
                             "-Alexis Daniel Villicaña Barrera";
            MessageBox.Show(creditos, "IDE-AR");
        }
        public void btConfiguracion_Click(Object sender, RoutedEventArgs e)
        {
            ventanaConfiguracion = new configuracion();
            //mostar la ventana
            ventanaConfiguracion.ShowDialog();
        }
        private void btnMin_Click(Object sender, RoutedEventArgs e)
        {
            //minimizar
            WindowState = System.Windows.WindowState.Minimized;
        }
        private void btnCerrar_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnReduce_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Normal;
            btnReduce.Visibility = System.Windows.Visibility.Hidden;
            btnExpand.Visibility = System.Windows.Visibility.Visible;
        }
        private void btnExpand_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Maximized;

            btnReduce.Visibility = System.Windows.Visibility.Visible;
            btnExpand.Visibility = System.Windows.Visibility.Hidden;
        }
        //************************Funciones y eventos de las listas***********************************************
        private void InicializarListas()
        {
            //Asignación de contextos
            lstMaterias.DataContext = materiaContexto;        
            lstActividades.DataContext = actividadContexto;
            lstAlumnosGrupo.DataContext = usuarioContexto;
            lstChat.DataContext = mensajeContexto;
            solucionP.DataContext = ficheroContexto;
            //lstAlumnosActivos.DataContext = usuarioContexto;
            //lstAlumnosInactivos.DataContext = usuarioContexto;
            //lstAlumnosNoActivos.DataContext = usuarioContexto;

            //Asignación de listas
            lstMaterias.ItemsSource = listAsignatures;
            lstActividades.ItemsSource = listActivities;
            lstAlumnosGrupo.ItemsSource = listStudentsGroup;
            lstChat.ItemsSource = listMenssages;
            solucionP.ItemsSource = listFiles;
            //lstAlumnosActivos.ItemsSource = listStudentsActives;
            //lstAlumnosInactivos.ItemsSource = listStudentsInactives;
            // lstAlumnosNoActivos.ItemsSource = listStudentsNoActives;
        }
        private void busquedaDatosListas()
        {
            ObtenerGrupos();
            ObtenerMaterias();
            ObtenerActividades();
            //busqueda de datos inicial   
            //obtener todas las materias
            Materias mats = InterfaceHttp.GetMaterias(VariablesGlobales.miusuario.IdUsuario);
        }
        private void actualizarListaGrupos(List<grupo> lista)
        {
            listGroups = lista;
        }
        private void actualizarListaActividades(List<actividad> lista)
        {
            lstActividades.ItemsSource = null;
            listActivities = lista;
            lstActividades.ItemsSource = listActivities;
        }
        private void actualizarListaAlumnosGrupo()
        {
            Usuarios estudiantes = InterfaceHttp.ObtenerAlumnosGrupo(currentGrupo);
            if (estudiantes != null && estudiantes.usuarios != null)
            {
                listStudentsGroup = estudiantes.usuarios;
                lstAlumnosGrupo.ItemsSource = null;
                lstAlumnosGrupo.Items.Clear();
                lstAlumnosGrupo.ItemsSource = listStudentsGroup;
            }
        }
        public void list1_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (listAsignatures.Count > 0 && lstMaterias.SelectedIndex >= 0)
            {
                currentMateria = listAsignatures[lstMaterias.SelectedIndex];
                currentGrupo = currentMateria.listaGrupos[0];
                if (currentGrupo.listaActividades.Count > 0)
                {
                    actualizarListaActividades(currentGrupo.listaActividades);
                }
                else
                {
                    listActivities.Clear();
                    actualizarListaActividades(listActivities);
                }
                actualizarListaAlumnosGrupo();
            }

            //actualizar lista de grupos
            //actualizar lista de actividades
        }

        public void lstActividades_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (listActivities.Count > 0 && lstActividades.SelectedIndex >= 0)
            {
                currentActividad = listActivities[lstActividades.SelectedIndex];
            }
        }


        public void btAdd1_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            AgregarMateria nuevaMateria = new AgregarMateria();
            nuevaMateria.Owner = this;
            //mostar la ventana
            if (nuevaMateria.ShowDialog() == true)
            {
                //actualizar lista materias
                listAsignatures.Add(nuevaMateria.nuevaMateria);
                lstMaterias.ItemsSource = null;
                lstMaterias.Items.Clear();
                lstMaterias.ItemsSource = listAsignatures;
            }
            this.Opacity = 1;

        }
        public void btAdd3_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            AgregarActividad nuevaActividad = new AgregarActividad(currentMateria, currentGrupo);
            nuevaActividad.Owner = this;
            //mostar la ventana
            if (nuevaActividad.ShowDialog() == true)
            {
                //actualizar lista de actividades
                listActivities.Add(nuevaActividad.nuevaActividad);
                lstActividades.ItemsSource = null;
                lstActividades.Items.Clear();
                lstActividades.ItemsSource = listActivities;
            }
            this.Opacity = 1;
        }

        private void btnModificarMateria_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            ModificarMateria modificarMateria = new ModificarMateria(currentMateria);
            modificarMateria.Owner = this;
            //mostar la ventana
            if (modificarMateria.ShowDialog() == true)
            {
                //refrescar lista materias   
                lstMaterias.Items.Refresh();
            }
            this.Opacity = 1;

        }
        private void btnModificarGrupo_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            ModificarGrupo modificarGrupo = new ModificarGrupo(currentMateria, currentGrupo);
            modificarGrupo.Owner = this;
            //mostar la ventana
            this.Opacity = 1;
        }
        private void btnModificarActividad_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            ModificarActividad modificarActividad = new ModificarActividad(currentMateria, currentGrupo, currentActividad);
            modificarActividad.Owner = this;
            //mostar la ventana
            if (modificarActividad.ShowDialog() == true)
            {
                //refescar lista actividades     
                lstActividades.Items.Refresh();
            }
            this.Opacity = 1;
        }
        private void btnEliminarMateria_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            VentanaEliminar deleteWindow = new VentanaEliminar(delete.Materia, currentMateria);
            if (deleteWindow.ShowDialog() == true)
            {
                //actualizar lista de materias
                busquedaDatosListas();
                //limpiar lista grupos
                //limpiar lista actividades
                lstActividades.ItemsSource = null;
                lstActividades.Items.Clear();
            }
            this.Opacity = 1;
        }
        private void btnEliminarGrupo_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            VentanaEliminar deleteWindow = new VentanaEliminar(delete.Grupo, currentGrupo);
            if (deleteWindow.ShowDialog() == true)
            {
                //actualizar lista de grupos
                Grupos grupos = InterfaceHttp.GetGrupos(currentMateria.IdMateria);
                if (grupos.grupos != null)
                {
                    for (int cont2 = 0; cont2 < grupos.grupos.Count; cont2++)
                    {
                        grupo gpo = grupos.grupos[cont2];
                        Actividades acts = InterfaceHttp.GetActividades(gpo.IdGrupo);
                        if (acts.actividades != null)
                            gpo.listaActividades = acts.actividades;
                        else
                            gpo.listaActividades = new List<actividad>();
                    }
                    currentMateria.listaGrupos = grupos.grupos;
                }
                else
                {
                    currentMateria.listaGrupos = new List<grupo>();
                }
                listGroups = currentMateria.listaGrupos;
                lstActividades.ItemsSource = null;
                lstActividades.Items.Clear();
            }
            this.Opacity = 1;
        }
        private void btnEliminarActividad_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            VentanaEliminar deleteWindow = new VentanaEliminar(delete.Actividad, currentActividad);
            if (deleteWindow.ShowDialog() == true)
            {
                Actividades acts = InterfaceHttp.GetActividades(currentGrupo.IdGrupo);
                if (acts.actividades != null)
                    currentGrupo.listaActividades = acts.actividades;
                else
                    currentGrupo.listaActividades = new List<actividad>();
                listActivities = currentGrupo.listaActividades;
                lstActividades.ItemsSource = null;
                lstActividades.Items.Clear();
                lstActividades.ItemsSource = listActivities;
            }
            this.Opacity = 1;
        }
        private void AgregarAlumnos_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            AgregarAlumnosGrupo agregarNuevos = new AgregarAlumnosGrupo(currentGrupo);
            agregarNuevos.Owner = this;
            //mostar la ventana
            if (agregarNuevos.ShowDialog() == true)
            {
                //actualizar lista alumnoGrupo

            }
            this.Opacity = 1;
        }
        private void EnviarMensaje_Click(object sender, RoutedEventArgs e)
        {
            gridChat.Visibility = System.Windows.Visibility.Visible;
            lbChat.Text = currentAlumnoGrupo.Nombre;
            ActualizarChat();
        }
        private void VerPerfil_Click(object sender, RoutedEventArgs e)
        {

        }

        //**************************************************************************************************
        //Eventos para la asignación de fuentes
        private void OpcionesFuentePredeterminada_Click(Object sender, RoutedEventArgs e)
        {
            ctEditor.FontFamily = familiaPre;
            OpcionesFuentePredeterminado.IsChecked = true;
            OpcionesFuenteArial.IsChecked = false;
            OpcionesFuenteCourier.IsChecked = false;
        }
        private void OpcionesFuenteCourier_Click(Object sender, RoutedEventArgs e)
        {
            ctEditor.FontFamily = new FontFamily("Courier new");
            OpcionesFuentePredeterminado.IsChecked = false;
            OpcionesFuenteArial.IsChecked = false;
            OpcionesFuenteCourier.IsChecked = true;
        }
        private void OpcionesFuenteArial_Click(Object sender, RoutedEventArgs e)
        {
            ctEditor.FontFamily = new FontFamily("Arial");
            OpcionesFuentePredeterminado.IsChecked = false;
            OpcionesFuenteArial.IsChecked = true;
            OpcionesFuenteCourier.IsChecked = false;
        }
        //**************************************************************************************************
        //Eventos para la asignación de tamaño
        private void OpcionesFuenteSize_Click(Object sender, RoutedEventArgs e)
        {
            MenuItem objeto = (MenuItem)e.Source;
            if (objeto.Name == "OpcionesPredeterminado")
            {
                ctEditor.FontSize = sizePre;
                OpcionesPredeterminado.IsChecked = true;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano10")
            {
                ctEditor.FontSize = 10.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = true;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano12")
            {
                ctEditor.FontSize = 12.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = true;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano14")
            {
                ctEditor.FontSize = 14.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = true;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano16")
            {
                ctEditor.FontSize = 16.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = true;
                OpcionesTamano18.IsChecked = false;
            }
            if (objeto.Name == "OpcionesTamano18")
            {
                ctEditor.FontSize = 18.0;
                OpcionesPredeterminado.IsChecked = false;
                OpcionesTamano10.IsChecked = false;
                OpcionesTamano12.IsChecked = false;
                OpcionesTamano14.IsChecked = false;
                OpcionesTamano16.IsChecked = false;
                OpcionesTamano18.IsChecked = true;
            }
        }
        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            sendMensaje();
        }
        private void sendMensaje()
        {
            if (txtMensaje.Text.Length > 0)
            {
                mensaje nuevoMensaje = new mensaje();
                nuevoMensaje.IdRemitente = VariablesGlobales.miusuario.IdUsuario;
                nuevoMensaje.NombreRemitente = VariablesGlobales.miusuario.Nombre;
                nuevoMensaje.IdDestinatario = currentAlumnoGrupo.IdUsuario;
                nuevoMensaje.FechaEnvio = DateTime.Now.ToShortDateString();
                nuevoMensaje.Mensaje = txtMensaje.Text;
                nuevoMensaje.IsHost = true;
                if (InterfaceHttp.EnviarMensaje(nuevoMensaje))
                {
                    //enviado correctamente
                    //actualizar chat
                    txtMensaje.Text = "";
                    listMenssages.Add(nuevoMensaje);
                    lstChat.ItemsSource = null;
                    lstChat.Items.Clear();
                    lstChat.ItemsSource = listMenssages;
                }
                else
                {
                    //mensaje de error al enviar
                    Mensaje("Error al enviar el mensaje");
                }
            }
            else
            {
                //mensaje esta vacío
                Mensaje("El mensaje esta vacío");

            }
        }
        private void ActualizarChat()
        {
            Chat nuevoChat = new Chat();
            nuevoChat.Host = VariablesGlobales.miusuario;
            nuevoChat.Guest = currentAlumnoGrupo;
            InterfaceHttp.ObtenerChat(nuevoChat);
            nuevoChat.Host = VariablesGlobales.miusuario;
            nuevoChat.Guest = currentAlumnoGrupo;
            if (nuevoChat.mensajes != null)
            {
                nuevoChat.AsignarHost();
                listMenssages = nuevoChat.mensajes;
                lstChat.ItemsSource = null;
                lstChat.Items.Clear();
                lstChat.ItemsSource = listMenssages;
            }
            else
            {
                Mensaje("Error al cargar el chat");
            }
        }
        public void Mensaje(string Text)
        {
            this.Opacity = 0.9;
            MessageBoxPersonalizado mostrar = new MessageBoxPersonalizado();
            mostrar.Texto = Text;
            mostrar.ShowDialog();
            this.Opacity = 9;
        }
        private void txtMensaje_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sendMensaje();
            }
        }
        private void ObtenerGrupos()
        {
            Grupos MisGrupos = new Grupos();
            InterfaceHttp.ObtenerGruposAlumno(MisGrupos);
            if (MisGrupos.grupos!=null)
            {
                listGroups = MisGrupos.grupos;
            }
        }
        private void ObtenerMaterias()
        {
            listAsignatures = new List<materia>();
            for (int cont = 0; cont < listGroups.Count; cont++)
            {
                currentGrupo = listGroups[cont];
                materia NuevaMateria = InterfaceHttp.ObtenerMateriasAlumno(currentGrupo);
                NuevaMateria.listaGrupos = new List<grupo>();
                NuevaMateria.listaGrupos.Add(currentGrupo);
                listAsignatures.Add(NuevaMateria);                

            }   
        }
        private void ObtenerActividades()
        {
            listActivities = new List<actividad>(); 
                       
            for (int cont = 0; cont < listGroups.Count; cont++)
            {
                currentGrupo = listGroups[cont];
                Actividades NuevaActividad = InterfaceHttp.ObtenerActividadesAlumno(currentGrupo);
                if (NuevaActividad.actividades!=null)
                {
                    currentGrupo.listaActividades = NuevaActividad.actividades;
                }
                else
                {
                    currentGrupo.listaActividades = new List<actividad>();
                }    
            }

        }
        private void lstAlumnosGrupo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listStudentsGroup.Count > 0 && lstAlumnosGrupo.SelectedIndex >= 0)
            {
                currentAlumnoGrupo = listStudentsGroup[lstAlumnosGrupo.SelectedIndex];
                
            }
        }

        private void VerDetalles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EntregarActividad_Click(object sender, RoutedEventArgs e)
        {

        }
     //************************************funciones de archivos y soluciones***********************************
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            string nombre = "log.rup";
            File.Delete(nombre);
            Login x = new Login();
            x.Show();
            this.Close();
            
        }
        private void crearSolucion_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.9;
            AgregarSolucion nueva = new AgregarSolucion();
            if (nueva.ShowDialog() == true)
            {
                misolucion = nueva.nuevaSolucion;
                solucionP.ItemsSource = misolucion.Ficheros;
                NombreP.Text = "-- "+misolucion.Nombre+" --";
                misolucion.IdActividad = currentActividad.IdActividad;
                misolucion.IdPropietario = VariablesGlobales.miusuario.IdUsuario;
                misolucion.Fecha = DateTime.Now.ToShortDateString().ToString();
                misolucion.NombrePropietario = VariablesGlobales.miusuario.Nombre;
                //obtener la ruta en el servidor
                //misolucion.Ruta
            }
            this.Opacity = 9;
        }

        private void buscarSolución_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.9;
            BuscarSolucion buscar = new BuscarSolucion();            
            if (buscar.ShowDialog() == true)
            {

                misolucion = buscar.solucion;                
                actualizarSolucion();
            }
            else
            {
                Mensaje("Problemas añadiendo al proyecto");
            }
            this.Opacity = 9;
        }
        private void agregarArchivo_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.9;
            AgregarFichero nuevo = new AgregarFichero();
            Fichero fr=new Fichero();
            fr.RutaLocal=misolucion.RutaLocal;
            fr.IsFolder=false;
            fr.Nombre = misolucion.Nombre;
            nuevo.FicheroRaiz = fr;
            if (nuevo.ShowDialog()==true)
            {
                listFiles.Add(nuevo.fichero);
                misolucion.Ficheros = listFiles;
                actualizarSolucion();
            }
            else
            {
                Mensaje("Problemas añadiendo al proyecto");
            }
            this.Opacity = 9;
        }
        public void actualizarSolucion()
        {
            NombreP.Text = misolucion.Nombre;
            solucionP.ItemsSource = null;
            solucionP.Items.Clear();
            listFiles = misolucion.Ficheros;
            solucionP.ItemsSource=listFiles;
            adminSolucion admin = new adminSolucion(misolucion);
            admin.ActualizarSolucion();
        }

     
    }
}
