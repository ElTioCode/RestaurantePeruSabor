using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Mozo : Empleado
    {
        private int mesasAtendidas;
        private List<Pedido> pedidosActivos;

        public Mozo(string nombre, string tipoDocumento, string numeroDocumento, int codigoEmpleado)
            : base(nombre, tipoDocumento, numeroDocumento, codigoEmpleado)
        {
            mesasAtendidas = 0;
            pedidosActivos = new List<Pedido>();
        }

        public int MesasAtendidas => mesasAtendidas;

        public Pedido TomarPedido(Cliente cliente)
        {
            var pedido = new Pedido(cliente);
            pedidosActivos.Add(pedido);
            mesasAtendidas++;
            return pedido;
        }
        public void EntregarPedido(Pedido pedido)
        {
            pedidosActivos.Remove(pedido);
        }
        public override void MostrarInfo()
        {
            System.Console.WriteLine($"Mozo: {nombre} | Mesas atendidas: {mesasAtendidas}");
        }
    }
}
