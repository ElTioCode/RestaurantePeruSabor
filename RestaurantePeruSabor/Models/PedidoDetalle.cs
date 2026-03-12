using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class PedidoDetalle
    {
        public Plato Plato { get; set; }
        public int Cantidad { get; set; }
        public PedidoDetalle(Plato plato, int cantidad)
        {
            Plato = plato;
            Cantidad = cantidad;
        }
        public double CalcularSubtotal() => Plato.Precio * Cantidad;
    }
}
