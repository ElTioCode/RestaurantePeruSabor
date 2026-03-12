using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Pedido
    {
        private Cliente cliente;
        private List<PedidoDetalle> detalles;
        public Pedido(Cliente cliente)
        {
            this.cliente = cliente;
            detalles = new List<PedidoDetalle>();

        }

        public string ClienteNombre => cliente.Nombre;
        public List<PedidoDetalle> Detalles => detalles;
        public void AgregarPlato(Plato plato, int cantidad)
        {
            detalles.Add(new PedidoDetalle(plato, cantidad));

        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (var d in detalles)
                total += d.CalcularSubtotal();
            return total;
        }
        public List<string> GetDetalleTexto()
        {
            var lineas = new List<string>();
            foreach (var d in detalles)
                lineas.Add($"{d.Plato.Nombre} x{d.Cantidad} = S/ {d.CalcularSubtotal():F2}");
            return lineas;
        }
    }
}
