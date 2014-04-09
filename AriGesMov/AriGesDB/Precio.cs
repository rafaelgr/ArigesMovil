using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Precio
    {
        private decimal pvp;

        public decimal Pvp
        {
            get { return pvp; }
            set { pvp = value; }
        }
        private decimal dto1;

        public decimal Dto1
        {
            get { return dto1; }
            set { dto1 = value; }
        }
        private decimal dto2;

        public decimal Dto2
        {
            get { return dto2; }
            set { dto2 = value; }
        }
        private decimal importe;

        public decimal Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        private string origen;

        public string Origen
        {
            get { return origen; }
            set { origen = value; }
        }
    }
}
