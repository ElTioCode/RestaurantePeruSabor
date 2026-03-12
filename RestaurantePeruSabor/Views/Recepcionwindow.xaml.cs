using System.Windows;
using System.Windows.Controls;
using RestaurantePeruSabor.Models;
using RestaurantePeruSabor.Services;

namespace RestaurantePeruSabor.Views
{
    public partial class RecepcionWindow : Window
    {
        private Usuario usuario;

        public RecepcionWindow(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void BtnContinuar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string numDoc = txtNumDoc.Text.Trim();
            string tipoDoc = (cmbTipoDoc.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "DNI";
            string mesaTxt = txtMesa.Text.Trim();

            // Validaciones
            if (string.IsNullOrEmpty(nombre) || nombre.Length < 3)
            {
                lblMensaje.Text = "⚠ Ingrese un nombre válido (mínimo 3 letras)";
                return;
            }
            if (string.IsNullOrEmpty(numDoc) || numDoc.Length < 8)
            {
                lblMensaje.Text = "⚠ Ingrese un número de documento válido (mínimo 8 dígitos)";
                return;
            }
            if (!int.TryParse(mesaTxt, out int numMesa) || numMesa <= 0)
            {
                lblMensaje.Text = "⚠ Ingrese un número de mesa válido";
                return;
            }

            // Crear cliente y mesa
            var cliente = new Cliente(nombre, tipoDoc, numDoc);
            var mesa = new Mesa(numMesa);

            // Anfitrión recibe y asigna mesa
            SesionRestaurante.Anfitrion.RecibirCliente(cliente);
            SesionRestaurante.Anfitrion.AsignarMesa(cliente, mesa);

            // Mozo toma el pedido
            var pedido = SesionRestaurante.Mozo.TomarPedido(cliente);

            // Abrir ventana de pedido
            var ventana = new PedidoWindow(usuario, cliente, mesa, pedido);
            ventana.Show();
            this.Close();
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            var menu = new MenuPrincipalWindow(usuario);
            menu.Show();
            this.Close();
        }
    }
}