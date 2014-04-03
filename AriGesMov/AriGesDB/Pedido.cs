using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Pedido
    {
        private int numPedcl;

        public int NumPedcl
        {
            get { return numPedcl; }
            set { numPedcl = value; }
        }
        private DateTime fecPedcl;

        public DateTime FecPedcl
        {
            get { return fecPedcl; }
            set { fecPedcl = value; }
        }
        private DateTime fecEntre;

        public DateTime FecEntre
        {
            get { return fecEntre; }
            set { fecEntre = value; }
        }
        private Decimal totalPed;

        public Decimal TotalPed
        {
            get { return totalPed; }
            set { totalPed = value; }
        }

        private IList<LinPedido> lineasPedido;

        public IList<LinPedido> LineasPedido
        {
            get { return lineasPedido; }
            set { lineasPedido = value; }
        }

        public Pedido()
        {
            lineasPedido = new List<LinPedido>();
        }
    }
}
