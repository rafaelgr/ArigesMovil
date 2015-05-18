using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class PrecioEspecial
    {
        private int codartic;

        public int Codartic
        {
            get { return codartic; }
            set { codartic = value; }
        }
        private string nomartic;

        public string Nomartic
        {
            get { return nomartic; }
            set { nomartic = value; }
        }
        private decimal precioac;

        public decimal Precioac
        {
            get { return precioac; }
            set { precioac = value; }
        }
        private decimal dtoespe;

        public decimal Dtoespe
        {
            get { return dtoespe; }
            set { dtoespe = value; }
        }
    }
}
