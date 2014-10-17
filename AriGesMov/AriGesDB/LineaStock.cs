using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class LineaStock
    {
        private int numLinea;

        public int NumLinea
        {
            get { return numLinea; }
            set { numLinea = value; }
        }
        private string almacen;

        public string Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }
        private decimal stock;

        public decimal Stock
        {
            get { return stock; }
            set { stock = value; }
        }
    }
}
