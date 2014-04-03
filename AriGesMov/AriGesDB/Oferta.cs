using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Oferta
    {
        private int numOfert;

        public int NumOfert
        {
            get { return numOfert; }
            set { numOfert = value; }
        }
        private DateTime fecOfert;

        public DateTime FecOfert
        {
            get { return fecOfert; }
            set { fecOfert = value; }
        }
        private DateTime fecEntre;

        public DateTime FecEntre
        {
            get { return fecEntre; }
            set { fecEntre = value; }
        }
        private Decimal totalOfe;

        public Decimal TotalOfe
        {
            get { return totalOfe; }
            set { totalOfe = value; }
        }

        private IList<LinOferta> lineasOferta;

        public IList<LinOferta> LineasOferta
        {
            get { return lineasOferta; }
            set { lineasOferta = value; }
        }

        private bool aceptado;
        public bool Aceptado
        {
            get { return aceptado; }
            set { aceptado = value; }
        }

        public Oferta()
        {
            lineasOferta = new List<LinOferta>();
        }
    }
}
