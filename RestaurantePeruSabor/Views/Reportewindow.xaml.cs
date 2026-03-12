using System;
using System.Collections.Generic;
using System.Windows;
using RestaurantePeruSabor.Data.Repositories;
using RestaurantePeruSabor.Models;

namespace RestaurantePeruSabor.Views
{
    public partial class ReporteWindow : Window
    {
        private Usuario usuario;
        private VentaRepository ventaRepo = new VentaRepository();

        private class FilaVenta
        {
            public int Numero { get; set; }
            public string Cliente { get; set; } = "";
            public string Comprobante { get; set; } = "";
            public string Total { get; set; } = "";
        }

        public ReporteWindow(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            CargarReporte();
        }

        private void CargarReporte()
        {
            try
            {
                var ventas = ventaRepo.GetVentasHoy();
                var filas = new List<FilaVenta>();
                double total = 0;
                int num = 1;

                foreach (var v in ventas)
                {
                    string nombreCliente = v.Cliente != null ? v.Cliente.Nombre : "N/A";
                    filas.Add(new FilaVenta
                    {
                        Numero = num++,
                        Cliente = nombreCliente,
                        Comprobante = v.Comprobante,
                        Total = "S/ " + v.MontoFinal.ToString("F2")
                    });
                    total += (double)v.MontoFinal;
                }

                dgVentas.ItemsSource = filas;
                lblCantidad.Text = ventas.Count.ToString();
                lblTotalDia.Text = "S/ " + total.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reporte:\n" + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}