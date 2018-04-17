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
namespace IDE_AR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private ConfiguracionProfesor ventanaConfiguracion;
        //*********************Variables para las listas************************************
        private List<materia> listAsignatures=new List<materia>();
        private List<grupo> listGroups = new List<grupo>();
        private List<actividad> listActivities = new List<actividad>();

        private List<usuario> listStudentsActives = new List<usuario>();
        private List<usuario> listStudentsInactives = new List<usuario>();
        private List<usuario> listStudentsNoActives = new List<usuario>();
        private List<usuario> listStudentsGroup = new List<usuario>();
        private List<mensaje> listMenssages=new List<mensaje>();

        private materia currentMateria;
        private grupo currentGrupo;
        private actividad currentActividad;
        private usuario currentAlumnoGrupo;

        public materia materiaContexto;//Variable usada solo para darle contexto a la lista materias
        public grupo grupoContexto;//Variable usada solo para darle contexto a la lista grupos
        public actividad actividadContexto;//Variable usada solo para darle contexto a la lista actividades
        public usuario usuarioContexto;//Variable usada solo para darle contexto a las listas de usuarios
        public mensaje mensajeContexto;
        //**********************************************************
        //*************************Variables para el editor*********************************
        private FontFamily familiaPre;
        private double sizePre;        
        public static RoutedUICommand RCGuardar = new RoutedUICommand();
        //**********************************************************************************        
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                //restaurar el estado desde los atributos del objeto
                //de la clase settings referenciado por default
                Rect restoreBounds = Properties.Settings.Default.MainRestoreBounds;
                Left = restoreBounds.Left;
                Top = restoreBounds.Top;
                if(restoreBounds.Width>0)
                Width = restoreBounds.Width;
                if (restoreBounds.Height > 0)
                Height = restoreBounds.Height;
                WindowState = Properties.Settings.Default.MainWindowState;
                
            }
            catch{}     
           btnAdd2.Visibility = System.Windows.Visibility.Hidden;
           btnAdd3.Visibility = System.Windows.Visibility.Hidden;
            
            busquedaDatosListas();
            InicializarListas();
            InicializacionDatos();
             
        }
        private void  MainWindow_Loaded(Object sender,RoutedEventArgs e)
        {
            familiaPre = ctEditor.FontFamily;
            sizePre = ctEditor.FontSize;
        }
        private void MainWindow_Closing(Object Sender,System.ComponentModel.CancelEventArgs e)
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
        private void ArchivoSalir_Click(Object sender,RoutedEventArgs e)
        {
            Close();//cerrrar la ventana principal
        }
        private void acercaDe_Click(Object sender,RoutedEventArgs e)
        {
            String creditos="Desarrolladores:\n"+
                             "-André Nicolás Ricoy Camacho\n"+
                             "-Alexis Daniel Villicaña Barrera";
            MessageBox.Show(creditos, "IDE-AR");
        }
        public void btConfiguracion_Click(Object sender, RoutedEventArgs e)
        {
            //mostar la ventana
            this.Opacity = 0.5;
            ventanaConfiguracion = new ConfiguracionProfesor();
            ventanaConfiguracion.Owner = this;
            //mostar la ventana
            ventanaConfiguracion.ShowDialog();
            tbUserName.Text = VariablesGlobales.miusuario.NombreUsuario;
            this.Opacity = 1;
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
        private void btnReduce_Click(Object sender,RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Normal;
            btnReduce.Visibility = System.Windows.Visibility.Hidden;
            btnExpand.Visibility = System.Windows.Visibility.Visible;
        }
        private void btnExpand_Click(Object sender,RoutedEventArgs e)
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
            lstGrupos.DataContext = grupoContexto;
            lstActividades.DataContext = actividadContexto;
            lstAlumnosGrupo.DataContext = usuarioContexto;
            lstChat.DataContext=mensajeContexto;
            //lstAlumnosActivos.DataContext = usuarioContexto;
            //lstAlumnosInactivos.DataContext = usuarioContexto;
            //lstAlumnosNoActivos.DataContext = usuarioContexto;

            //Asignación de listas
            lstMaterias.ItemsSource = listAsignatures;            
            lstGrupos.ItemsSource = listGroups;
            lstActividades.ItemsSource = listActivities;
            lstAlumnosGrupo.ItemsSource = listStudentsGroup;
            lstChat.ItemsSource=listMenssages;
            //lstAlumnosActivos.ItemsSource = listStudentsActives;
            //lstAlumnosInactivos.ItemsSource = listStudentsInactives;
            // lstAlumnosNoActivos.ItemsSource = listStudentsNoActives;
        }
        private void busquedaDatosListas()
        {
            //busqueda de datos inicial   
            //obtener todas las materias
            Materias mats= InterfaceHttp.GetMaterias(VariablesGlobales.miusuario.IdUsuario);
            if (mats.materias != null)
                for (int cont = 0; cont < mats.materias.Count; cont++)
                {
                    materia mat = mats.materias[cont];
                    Grupos grupos = InterfaceHttp.GetGrupos(mat.IdMateria);
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
                        mat.listaGrupos = grupos.grupos;
                    }
                    else
                    {
                        mat.listaGrupos = new List<grupo>();
                    }
                }
            else
                mats.materias = new List<materia>();                
            listAsignatures = mats.materias;
            lstMaterias.ItemsSource = null;
            lstMaterias.Items.Clear();
            lstMaterias.ItemsSource = listAsignatures;
                    
        }
        private void actualizarListaGrupos(List<grupo> lista)
        {
            lstGrupos.ItemsSource = null;
            listGroups = lista;
            lstGrupos.ItemsSource = listGroups;
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
            else
            {
                lstAlumnosGrupo.ItemsSource = null;
                lstAlumnosGrupo.Items.Clear();
            }
        }
        public void list1_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if(listAsignatures.Count>0&&lstMaterias.SelectedIndex>=0)
            {
                currentMateria = listAsignatures[lstMaterias.SelectedIndex];
                btnAdd2.Visibility = System.Windows.Visibility.Visible;                
                if(currentMateria.listaGrupos.Count>=0)
                {
                    actualizarListaGrupos(currentMateria.listaGrupos);
                    if (listGroups.Count > 0)
                    {
                        currentGrupo = listGroups[0];
                        lstGrupos.SelectedIndex = 0;
                        actualizarListaActividades(currentGrupo.listaActividades);
                        btnAdd3.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        btnAdd3.Visibility = System.Windows.Visibility.Hidden;
                        listGroups.Clear();
                        listActivities.Clear();
                        actualizarListaActividades(listActivities);
                    }                     
                }
            }
                
            //actualizar lista de grupos
            //actualizar lista de actividades
        }
      
        public void lstGrupo_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {

            if (listGroups.Count > 0 && lstGrupos.SelectedIndex >= 0)
            {
                currentGrupo = listGroups[lstGrupos.SelectedIndex];
                //actualizar lista de actividades
                actualizarListaActividades(currentGrupo.listaActividades);                
                btnAdd3.Visibility = System.Windows.Visibility.Visible;
                //actualizar lista de alumnosGrupo
                actualizarListaAlumnosGrupo();
            }
            else
            {                
                btnAdd3.Visibility = System.Windows.Visibility.Hidden;
            }
            
        }

        public void lstActividades_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (listActivities.Count > 0 && lstActividades.SelectedIndex >= 0)
            {
                currentActividad = listActivities[lstActividades.SelectedIndex];
            }          
        }
         private void lstAlumnosGrupo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listStudentsGroup.Count > 0 && lstAlumnosGrupo.SelectedIndex >= 0)
            {
                currentAlumnoGrupo = listStudentsGroup[lstAlumnosGrupo.SelectedIndex];
            }     
        }

      
        public void btAdd1_Click(Object sender,RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            AgregarMateria nuevaMateria = new AgregarMateria();
            nuevaMateria.Owner = this;            
            //mostar la ventana
           if( nuevaMateria.ShowDialog()==true)
           {
               //actualizar lista materias
               listAsignatures.Add(nuevaMateria.nuevaMateria);
               lstMaterias.ItemsSource = null;
               lstMaterias.Items.Clear();
               lstMaterias.ItemsSource = listAsignatures;
           }           
            this.Opacity = 1;            
            
        }
        public void btAdd2_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            Agregar_Grupo nuevoGrupo = new Agregar_Grupo(currentMateria);
            nuevoGrupo.Owner = this;                  
            //mostar la ventana
            if(nuevoGrupo.ShowDialog()==true)
            {
                //actualizar lista de grupos
                listGroups.Add(nuevoGrupo.nuevoGrupo);
                lstGrupos.ItemsSource = null;
                lstGrupos.Items.Clear();
                lstGrupos.ItemsSource = listGroups;
            }
            this.Opacity = 1;
        }
        public void btAdd3_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            AgregarActividad nuevaActividad = new AgregarActividad(currentMateria,currentGrupo);
            nuevaActividad.Owner = this;
            //mostar la ventana
            if(nuevaActividad.ShowDialog()==true)
            {
                //actualizar lista de actividades
                listActivities.Add(nuevaActividad.nuevaActividad);
                lstActividades.ItemsSource = null;
                lstActividades.Items.Clear();
                lstActividades.ItemsSource = listActivities;
            }
            this.Opacity = 1;
        }

        private void btnModificarMateria_Click(Object sender,RoutedEventArgs e)
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
            ModificarGrupo modificarGrupo = new ModificarGrupo(currentMateria,currentGrupo);
            modificarGrupo.Owner = this;
            //mostar la ventana
            if (modificarGrupo.ShowDialog() == true)
            {
                //refrescar lista de grupos 
                lstGrupos.Items.Refresh();
            }
            this.Opacity = 1;
        }
        private void btnModificarActividad_Click(Object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            ModificarActividad modificarActividad = new ModificarActividad(currentMateria, currentGrupo,currentActividad);
            modificarActividad.Owner = this;
            //mostar la ventana
            if (modificarActividad.ShowDialog() == true)
            {
                //refescar lista actividades     
                lstActividades.Items.Refresh();
            }
            this.Opacity = 1;
        }
        private void btnEliminarMateria_Click(Object sender,RoutedEventArgs e)
        {            
            this.Opacity = 0.5;
            VentanaEliminar deleteWindow = new VentanaEliminar(delete.Materia,currentMateria);
            if(deleteWindow.ShowDialog()==true)
            {
                //actualizar lista de materias
                busquedaDatosListas();
                //limpiar lista grupos
                lstGrupos.ItemsSource = null;
                lstGrupos.Items.Clear();
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
                lstGrupos.ItemsSource = null;
                lstGrupos.Items.Clear();
                lstGrupos.ItemsSource = listGroups;

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
        private void OpcionesFuenteCourier_Click(Object sender,RoutedEventArgs e)
        {
            ctEditor.FontFamily = new FontFamily("Courier new");
            OpcionesFuentePredeterminado.IsChecked = false;
            OpcionesFuenteArial.IsChecked = false;
            OpcionesFuenteCourier.IsChecked = true;
        }
        private void OpcionesFuenteArial_Click(Object sender, RoutedEventArgs e)
        {
            ctEditor.FontFamily = new FontFamily("Arial");
            OpcionesFuentePredeterminado.IsChecked =false;
            OpcionesFuenteArial.IsChecked = true;
            OpcionesFuenteCourier.IsChecked = false;
        }
        //**************************************************************************************************
        //Eventos para la asignación de tamaño
        private void OpcionesFuenteSize_Click(Object sender, RoutedEventArgs e)
        {
            MenuItem objeto=(MenuItem)e.Source;
            if(objeto.Name=="OpcionesPredeterminado")
            {
               ctEditor.FontSize=sizePre;
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
            if(nuevoChat.mensajes!=null)
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
            if(e.Key==Key.Enter)
            {
                sendMensaje();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            //Archivo de mantener sesión iniciada
            string nombre = "log.rup";
            File.Delete(nombre);
            Login x = new Login();
            x.Show();
            this.Close(); 
        }

        private void lstGrupos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            actualizarListaAlumnosGrupo();
        }
    }
}
