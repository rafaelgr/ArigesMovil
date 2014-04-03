using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Albaran
    {
        private string codtipom;

        public string CodTipom
        {
            get { return codtipom; }
            set { codtipom = value; }
        }


        private int numAlbar;

        public int NumAlbar
        {
            get { return numAlbar; }
            set { numAlbar = value; }
        }
        private DateTime fechaAlb;

        public DateTime FechaAlb
        {
            get { return fechaAlb; }
            set { fechaAlb = value; }
        }


        private Decimal totalAlb;

        public Decimal TotalAlb
        {
            get { return totalAlb; }
            set { totalAlb = value; }
        }

        private IList<LinAlbaran> lineasAlbaran;

        public IList<LinAlbaran> LineasAlbaran
        {
            get { return lineasAlbaran; }
            set { lineasAlbaran = value; }
        }

        public Albaran()
        {
            lineasAlbaran = new List<LinAlbaran>();
        }
    }
}
