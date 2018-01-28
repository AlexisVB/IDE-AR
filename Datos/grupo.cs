using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_AR.Datos
{
    public class grupo
    {
        private int idGrupo;
        private string nombreGrupo;
        private int idMateria;

        public int IdGrupo
        {
            get { return idGrupo; }
            set { idGrupo = value; }
        }
        public string NombreGrupo
        {
            get { return nombreGrupo; }
            set { nombreGrupo = value; }
        }
        public int IdMateria
        {
            get { return idMateria; }
            set { idMateria = value; }
        }

    }
}
