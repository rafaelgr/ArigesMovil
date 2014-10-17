using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class LineaComponente
    {
        private int numLinea;

        public int NumLinea
        {
            get { return numLinea; }
            set { numLinea = value; }
        }
        private string articulo;

        public string Articulo
        {
            get { return articulo; }
            set { articulo = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
