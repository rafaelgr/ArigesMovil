using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriGesDB
{
    public class LinOferta
    {
        private int numOfert;

        public int NumOfert
        {
            get { return numOfert; }
            set { numOfert = value; }
        }
        private int numLinea;

        public int NumLinea
        {
            get { return numLinea; }
            set { numLinea = value; }
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
        private decimal precioAr;

        public decimal PrecioAr
        {
            get { return precioAr; }
            set { precioAr = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private decimal importel;

        public decimal Importel
        {
            get { return importel; }
            set { importel = value; }
        }

        private Oferta oferta;
        public Oferta Oferta
        {
            get
            {
                return oferta;
            }
            set
            {
                oferta = value;
            }
        }

        private decimal dtoLine1;

        public decimal DtoLine1
        {
            get { return dtoLine1; }
            set { dtoLine1 = value; }
        }
        private decimal dtoLine2;

        public decimal DtoLine2
        {
            get { return dtoLine2; }
            set { dtoLine2 = value; }
        }
    }
}
