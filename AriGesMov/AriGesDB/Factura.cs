using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Factura
    {
        private string codtipom;

        public string CodTipom
        {
            get { return codtipom; }
            set { codtipom = value; }
        }


        private int numFactu;

        public int NumFactu
        {
            get { return numFactu; }
            set { numFactu = value; }
        }
        private DateTime fecFactu;

        public DateTime FecFactu
        {
            get { return fecFactu; }
            set { fecFactu = value; }
        }

        private decimal bases;
        public decimal Bases
        {
            get { return bases; }
            set { bases = value; }
        }

        private decimal cuotas;
        public decimal Cuotas
        {
            get { return cuotas; }
            set { cuotas = value; }
        }

        private Decimal totalFac;

        public Decimal TotalFac
        {
            get { return totalFac; }
            set { totalFac = value; }
        }

        private IList<LinFactura> lineasFactura;

        public IList<LinFactura> LineasFactura
        {
            get { return lineasFactura; }
            set { lineasFactura = value; }
        }

        public Factura()
        {
            lineasFactura = new List<LinFactura>();
        }
    }
}
