using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Venta
    {
        private Pedido pedido;
        private double descuento;

        public double MontoFinal { get; private set; }
        public string Comprobante { get; set; }
        public string ClienteNombre => pedido.ClienteNombre;
        public Pedido Pedido => pedido;

        public Venta(Pedido pedido , double descuento , string comprobante)
        {
            this.pedido = pedido;
            this.descuento = descuento;
            Comprobante = comprobante;
            CalcularMontoFinal();
        }
        public void CalcularMontoFinal()
        {
            double total = pedido.CalcularTotal();
            MontoFinal = total - (total * descuento);
        }
    }
}
