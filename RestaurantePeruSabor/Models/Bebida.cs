using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Bebida : Plato
    {
        public enum TipoBebida { GASEOSA, JUGO, AGUA, CERVEZA, VINO , OTRO }
        public  TipoBebida Tipo { get; private set; }       
        public bool EsAlcoholica { get; private set; }
        public int Mililitros { get; private set; }
        public Bebida(string nombre, double precio, TipoBebida tipo, bool esAlcoholica, int mililitros)
            : base(nombre, precio)
        {
            Tipo = tipo;
            EsAlcoholica = esAlcoholica;
            Mililitros = mililitros;
        }
        public void MostrarInfo()
        {
            System.Console.WriteLine($"Bebida: {Nombre} | Precio: S/ {Precio} | {Mililitros}ml");
        }
    }
}
