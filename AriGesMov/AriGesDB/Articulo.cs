using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Articulo
    {
        private string codArtic;

        public string CodArtic
        {
            get { return codArtic; }
            set { codArtic = value; }
        }
        private string nomArtic;

        public string NomArtic
        {
            get { return nomArtic; }
            set { nomArtic = value; }
        }
        private decimal preciove;

        public decimal Preciove
        {
            get { return preciove; }
            set { preciove = value; }
        }
        private int codFamia;

        public int CodFamia
        {
            get { return codFamia; }
            set { codFamia = value; }
        }
        private int codMarca;

        public int CodMarca
        {
            get { return codMarca; }
            set { codMarca = value; }
        }

        private Precio precio;
        public Precio Precio
        {
            get
            {
                return precio;
            }
            set
            {
                precio = value;
            }
        }

        private decimal stock;

        public decimal Stock
        {
            get { return stock; }
            set { stock = value; }
        }
    }
}
