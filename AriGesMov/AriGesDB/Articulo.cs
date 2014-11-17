using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class Articulo
    {

        public Articulo()
        {
            componentes = new List<LineaComponente>();
            stocks = new List<LineaStock>();
        }

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

        private Familia familia;
        public Familia Familia
        {
            get
            {
                return familia;
            }
            set
            {
                familia = value;
            }
        }

        private Proveedor proveedor;
        public Proveedor Proveedor
        {
            get
            {
                return proveedor;
            }
            set
            {
                proveedor = value;
            }
        }

        private bool rotacion;

        public bool Rotacion
        {
            get { return rotacion; }
            set { rotacion = value; }
        }
        private decimal reservas;

        public decimal Reservas
        {
            get { return reservas; }
            set { reservas = value; }
        }

        private string referprov;

        public string Referprov
        {
            get { return referprov; }
            set { referprov = value; }
        }

        private IList<LineaComponente> componentes;
        public IList<LineaComponente> Componentes
        {
            get { return componentes; }
            set { componentes = value; }
        }

        private IList<LineaStock> stocks;
        public IList<LineaStock> Stocks
        {
            get { return stocks; }
            set { stocks = value; }
        }

        private decimal pedido;

        public decimal Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

    }
}
