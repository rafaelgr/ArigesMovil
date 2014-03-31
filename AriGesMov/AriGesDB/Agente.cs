using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Agente
    {
        private int codAgent;

        public int CodAgent
        {
            get { return codAgent; }
            set { codAgent = value; }
        }
        private string nomAgent;

        public string NomAgent
        {
            get { return nomAgent; }
            set { nomAgent = value; }
        }
    }
}
