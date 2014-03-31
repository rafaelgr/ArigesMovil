using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Trabajador
    {
        private int codTraba;

        public int CodTraba
        {
            get { return codTraba; }
            set { codTraba = value; }
        }
        private string nomTraba;

        public string NomTraba
        {
            get { return nomTraba; }
            set { nomTraba = value; }
        }
        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public Agente Agente
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
