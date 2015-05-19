using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class DescuentoRapel
    {
        private int codfamia;

        public int Codfamia
        {
            get { return codfamia; }
            set { codfamia = value; }
        }
        private string nomfamia;

        public string Nomfamia
        {
            get { return nomfamia; }
            set { nomfamia = value; }
        }
        private int codmarca;

        public int Codmarca
        {
            get { return codmarca; }
            set { codmarca = value; }
        }
        private string nomarca;

        public string Nomarca
        {
            get { return nomarca; }
            set { nomarca = value; }
        }
        private DateTime fechadto;

        public DateTime Fechadto
        {
            get { return fechadto; }
            set { fechadto = value; }
        }
        private decimal dtoline1;

        public decimal Dtoline1
        {
            get { return dtoline1; }
            set { dtoline1 = value; }
        }
        private decimal dtoline2;

        public decimal Dtoline2
        {
            get { return dtoline2; }
            set { dtoline2 = value; }
        }
        private decimal rap1;

        public decimal Rap1
        {
            get { return rap1; }
            set { rap1 = value; }
        }
        private decimal rap2;

        public decimal Rap2
        {
            get { return rap2; }
            set { rap2 = value; }
        }
    }
}
