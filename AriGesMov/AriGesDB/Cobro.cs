using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Cobro
    {
        private DateTime fechaVenci;

        public DateTime FechaVenci
        {
            get { return fechaVenci; }
            set { fechaVenci = value; }
        }
        private DateTime fechaFact;

        public DateTime FechaFact
        {
            get { return fechaFact; }
            set { fechaFact = value; }
        }
        private string numFact;

        public string NumFact
        {
            get { return numFact; }
            set { numFact = value; }
        }
        private string nomForpa;

        public string NomForpa
        {
            get { return nomForpa; }
            set { nomForpa = value; }
        }
        private decimal total;

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
