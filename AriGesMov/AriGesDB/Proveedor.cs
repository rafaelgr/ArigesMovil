using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Proveedor
    {
        private int codProve;

        public int CodProve
        {
            get { return codProve; }
            set { codProve = value; }
        }
        private string nomProve;

        public string NomProve
        {
            get { return nomProve; }
            set { nomProve = value; }
        }
    }
}
