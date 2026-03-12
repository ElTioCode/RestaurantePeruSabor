using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RestaurantePeruSabor.Data.Repositories;
using RestaurantePeruSabor.Models;
using RestaurantePeruSabor.Services;

namespace RestaurantePeruSabor.Views
{
    public partial class CobroWindow : Window
    {
        private Usuario usuario;
        private Cliente cliente;
        private Mesa mesa;
        private Pedido pedido;

        private VentaRepository ventaRepo = new VentaRepository();
        private GeneradorPDF generadorPdf = new GeneradorPDF();

        // Contador de serie (en producción vendría de la BD)
        private static int contadorSerie = 1;

        public CobroWindow(Usuario usuario, Cliente cliente, Mesa mesa, Pedido pedido)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.cliente = cliente;
            this.mesa = mesa;
            this.pedido = pedido;

            lblCliente.Text = "Cliente : " + cliente.Nombre;
            lblMesa.Text = "Mesa N  : " + mesa.Numero;
            lblSubtotal.Text = "Subtotal: S/ " + pedido.CalcularTotal().ToString("F2");
            lblTotal.Text = "S/ " + pedido.CalcularTotal().ToString("F2");

            txtDescuento.TextChanged += (s, e) => RecalcularTotal();
        }

        private void CmbComprobante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelFactura == null) return;
            var item = cmbComprobante.SelectedItem as ComboBoxItem;
            panelFactura.Visibility = (item?.Content?.ToString() == "Factura Electronica")
                                      ? Visibility.Visible
                                      : Visibility.Collapsed;
        }

        private void RecalcularTotal()
        {
            if (double.TryParse(txtDescuento.Text, out double desc))
            {
                double total = pedido.CalcularTotal();
                double montoFinal = total - (total * desc / 100);
                lblTotal.Text = "S/ " + montoFinal.ToString("F2");
            }
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtDescuento.Text, out double descPct))
                descPct = 0;

            string tipoComp = "Boleta Electronica";
            if (cmbComprobante.SelectedItem is ComboBoxItem item)
                tipoComp = item.Content.ToString();

            // Validar datos de factura
            if (tipoComp == "Factura Electronica")
            {
                if (string.IsNullOrWhiteSpace(txtRuc.Text) ||
                    string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MessageBox.Show("Ingrese RUC y Razon Social para la factura.",
                                    "Datos incompletos", MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                // 1. Guardar en base de datos
                ventaRepo.GuardarVenta(cliente, mesa, pedido, descPct / 100.0, tipoComp);

                // 2. Guardar en sesión
                var venta = new Venta(pedido, descPct / 100.0, tipoComp);
                SesionRestaurante.VentasDelDia.Add(venta);

                // 3. Generar PDF
                string serie = "001-" + contadorSerie.ToString("D6");
                string rutaPdf = "";

                if (tipoComp == "Factura Electronica")
                {
                    rutaPdf = generadorPdf.GenerarFactura(
                        cliente, pedido, descPct / 100.0,
                        serie, txtRuc.Text, txtRazonSocial.Text);
                }
                else
                {
                    rutaPdf = generadorPdf.GenerarBoleta(
                        cliente, pedido, descPct / 100.0, serie);
                }

                contadorSerie++;

                // 4. Liberar mesa
                mesa.LiberarMesa();
                SesionRestaurante.Mozo.EntregarPedido(pedido);

                // 5. Mostrar resultado y abrir PDF
                var resultado = MessageBox.Show(
                    "Cobro realizado exitosamente\n\n" +
                    "Cliente     : " + cliente.Nombre + "\n" +
                    "Total       : S/ " + venta.MontoFinal.ToString("F2") + "\n" +
                    "Comprobante : " + tipoComp + "\n" +
                    "Serie       : " + serie + "\n\n" +
                    "PDF guardado en Documentos/RestaurantePeruSabor/Comprobantes\n\n" +
                    "Desea abrir el PDF ahora?",
                    "Cobro Exitoso",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (resultado == MessageBoxResult.Yes && File.Exists(rutaPdf))
                    Process.Start(new ProcessStartInfo(rutaPdf) { UseShellExecute = true });

                var menu = new MenuPrincipalWindow(usuario);
                menu.Show();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}