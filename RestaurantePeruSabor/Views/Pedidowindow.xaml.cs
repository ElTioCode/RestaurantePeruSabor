using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using RestaurantePeruSabor.Models;
using RestaurantePeruSabor.Services;

namespace RestaurantePeruSabor.Views
{
    public partial class PedidoWindow : Window
    {
        private Usuario usuario;
        private Cliente cliente;
        private Mesa mesa;
        private Pedido pedido;

        private class FilaPedido
        {
            public string Nombre { get; set; } = "";
            public int Cantidad { get; set; }
            public string Subtotal { get; set; } = "";
        }

        public PedidoWindow(Usuario usuario, Cliente cliente, Mesa mesa, Pedido pedido)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.cliente = cliente;
            this.mesa = mesa;
            this.pedido = pedido;

            lblCliente.Text = "Cliente: " + cliente.Nombre;
            lblMesa.Text = "Mesa N: " + mesa.Numero;

            ActualizarTabla();
        }

        private void BtnAgregarItem_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.Tag == null) return;

            string[] partes = btn.Tag.ToString().Split('|');
            if (partes.Length < 2) return;

            string nombre = partes[0];
            if (!double.TryParse(partes[1], out double precio)) return;

            var plato = new Plato(nombre, precio);
            pedido.AgregarPlato(plato, 1);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            var filas = new List<FilaPedido>();
            foreach (var d in pedido.Detalles)
            {
                filas.Add(new FilaPedido
                {
                    Nombre = d.Plato.Nombre,
                    Cantidad = d.Cantidad,
                    Subtotal = "S/ " + d.CalcularSubtotal().ToString("F2")
                });
            }
            dgPedido.ItemsSource = null;
            dgPedido.ItemsSource = filas;
            lblTotal.Text = "S/ " + pedido.CalcularTotal().ToString("F2");
        }

        private void BtnCobrar_Click(object sender, RoutedEventArgs e)
        {
            if (pedido.Detalles.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un item al pedido.",
                                "Pedido vacio", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RegistroPedidos.GuardarPedido(pedido, SesionRestaurante.Mozo, mesa, pedido.CalcularTotal());

            var ventana = new CobroWindow(usuario, cliente, mesa, pedido);
            ventana.Show();
            this.Close();
        }
    }
}