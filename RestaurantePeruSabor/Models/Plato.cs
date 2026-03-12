using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Plato
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public Plato(string nombre, double precio)
        {
            Nombre = nombre;
            Precio = precio;
        }
    }
}
