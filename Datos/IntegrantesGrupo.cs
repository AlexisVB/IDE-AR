using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    class IntegrantesGrupo
    {
        private grupo gpo;
        private int[] idIntegrantes;
        private int numIntegrantes;
        List<usuario> integrantesLista=new List<usuario>();
       
        public IntegrantesGrupo(int numeroIntegrantes)
        {
            numIntegrantes = numeroIntegrantes;
            idIntegrantes = new int[numeroIntegrantes];
        }
        public grupo Gpo
        {
            get { return gpo; }
            set { gpo = value; }
        }
        public int[] IdIntegrantes
        {
            get{ return idIntegrantes;}
            set { idIntegrantes = value; }
        }
        public List<usuario> IntegrantesLista
        {
            get { return integrantesLista; }
            set { integrantesLista = value; }
        }
        //funcion que obtiene la lista de los usuarios en un grupo recibiendo los ids a buscar
        public int ObtenerIntegrantesGrupo(int[] idInte)
        {
            idIntegrantes = idInte;
            for (int i = 0; i < numIntegrantes; i++)
            {
                usuario nuevo = usuario.ObtenerUsuario(idIntegrantes[i]);
                if (nuevo != null)
                {
                    integrantesLista.Add(nuevo);
                }
            }
            return integrantesLista.Count;
        }
        //funcion que obtiene la lista de los usuarios en un grupo
        public int ObtenerIntegrantesGrupo()
        {
            for(int i=0;i<numIntegrantes;i++)
            {
                usuario nuevo=usuario.ObtenerUsuario(idIntegrantes[i]);
                if(nuevo!=null)
                {
                    integrantesLista.Add(nuevo);
                }
            }
            return integrantesLista.Count;
        }
    }
}
