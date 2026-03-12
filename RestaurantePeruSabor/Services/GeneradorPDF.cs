using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RestaurantePeruSabor.Models;
using System.IO;

namespace RestaurantePeruSabor.Services
{
    public class GeneradorPDF
    {
        private const string NOMBRE_RESTAURANTE = "Restaurante Peru Sabor";
        private const string RUC = "20123456789";
        private const string DIRECCION = "Av. La Marina 123, Lima, Peru";
        private const string TELEFONO = "01-234-5678";
        private const string EMAIL = "contacto@perusabor.com";

        public GeneradorPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public string GenerarBoleta(Cliente cliente, Pedido pedido,
                                    double descuento, string numeroSerie)
        {
            string rutaSalida = ObtenerRuta("Boleta", numeroSerie);

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                    page.Header().Element(c => Encabezado(c, "BOLETA DE VENTA", numeroSerie));
                    page.Content().Element(c => Contenido(c, cliente, pedido, descuento, null, null));
                    page.Footer().Element(Pie);
                });
            }).GeneratePdf(rutaSalida);

            return rutaSalida;
        }

        public string GenerarFactura(Cliente cliente, Pedido pedido,
                                     double descuento, string numeroSerie,
                                     string rucEmpresa, string razonSocial)
        {
            string rutaSalida = ObtenerRuta("Factura", numeroSerie);

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                    page.Header().Element(c => Encabezado(c, "FACTURA ELECTRONICA", numeroSerie));
                    page.Content().Element(c => Contenido(c, cliente, pedido, descuento, rucEmpresa, razonSocial));
                    page.Footer().Element(Pie);
                });
            }).GeneratePdf(rutaSalida);

            return rutaSalida;
        }

        private void Encabezado(IContainer container, string tipoDoc, string serie)
        {
            container.Column(col =>
            {
                col.Item().Row(row =>
                {
                    string logoPath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "Assets", "logo.png");

                    if (File.Exists(logoPath))
                        row.ConstantItem(70).Image(logoPath);
                    else
                        row.ConstantItem(70).Background(Colors.Purple.Medium)
                           .AlignCenter().AlignMiddle()
                           .Text("RPS").Bold().FontColor(Colors.White);

                    row.RelativeItem().PaddingLeft(10).Column(info =>
                    {
                        info.Item().Text(NOMBRE_RESTAURANTE).FontSize(14).Bold();
                        info.Item().Text("RUC: " + RUC).FontSize(9).FontColor(Colors.Grey.Medium);
                        info.Item().Text(DIRECCION).FontSize(9).FontColor(Colors.Grey.Medium);
                        info.Item().Text("Tel: " + TELEFONO).FontSize(9).FontColor(Colors.Grey.Medium);
                        info.Item().Text(EMAIL).FontSize(9).FontColor(Colors.Grey.Medium);
                    });
                });

                col.Item().PaddingTop(10).BorderBottom(2).BorderColor(Colors.Purple.Medium);

                col.Item().PaddingTop(8).Background(Colors.Black)
                   .Padding(8).Column(c =>
                   {
                       c.Item().AlignCenter().Text(tipoDoc)
                        .FontSize(13).Bold().FontColor(Colors.Purple.Lighten2);
                       c.Item().AlignCenter().Text("N " + serie)
                        .FontSize(11).FontColor(Colors.Grey.Lighten2);
                   });

                col.Item().PaddingTop(6)
                   .Text("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                   .FontSize(9).FontColor(Colors.Grey.Medium);
            });
        }

        private void Contenido(IContainer container, Cliente cliente, Pedido pedido,
                                double descuento, string rucEmpresa, string razonSocial)
        {
            container.PaddingTop(15).Column(col =>
            {
                // Datos cliente
                col.Item().Background(Colors.Grey.Lighten4).Padding(8).Column(c =>
                {
                    c.Item().Text("DATOS DEL CLIENTE").Bold().FontSize(9)
                     .FontColor(Colors.Grey.Medium);
                    c.Item().PaddingTop(4).Text("Nombre: " + cliente.Nombre).FontSize(10);
                    c.Item().Text("Doc: " + cliente.TipoDocumento + " " +
                                  cliente.NumeroDocumento).FontSize(10);

                    if (!string.IsNullOrEmpty(rucEmpresa))
                    {
                        c.Item().Text("RUC Empresa: " + rucEmpresa).FontSize(10);
                        c.Item().Text("Razon Social: " + razonSocial).FontSize(10);
                    }
                });

                col.Item().PaddingTop(12).Text("DETALLE").Bold().FontSize(10);

                // Tabla de items
                col.Item().PaddingTop(6).Table(table =>
                {
                    table.ColumnsDefinition(cols =>
                    {
                        cols.RelativeColumn(4);
                        cols.RelativeColumn(1);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Background(Colors.Black).Padding(5)
                              .Text("Descripcion").FontColor(Colors.White).Bold().FontSize(9);
                        header.Cell().Background(Colors.Black).Padding(5).AlignCenter()
                              .Text("Cant.").FontColor(Colors.White).Bold().FontSize(9);
                        header.Cell().Background(Colors.Black).Padding(5).AlignRight()
                              .Text("P.Unit").FontColor(Colors.White).Bold().FontSize(9);
                        header.Cell().Background(Colors.Black).Padding(5).AlignRight()
                              .Text("Subtotal").FontColor(Colors.White).Bold().FontSize(9);
                    });

                    bool alt = false;
                    foreach (var d in pedido.Detalles)
                    {
                        string bg = alt ? Colors.Grey.Lighten4 : Colors.White;
                        alt = !alt;

                        table.Cell().Background(bg).Padding(5)
                             .Text(d.Plato.Nombre).FontSize(9);
                        table.Cell().Background(bg).Padding(5).AlignCenter()
                             .Text(d.Cantidad.ToString()).FontSize(9);
                        table.Cell().Background(bg).Padding(5).AlignRight()
                             .Text("S/ " + d.Plato.Precio.ToString("F2")).FontSize(9);
                        table.Cell().Background(bg).Padding(5).AlignRight()
                             .Text("S/ " + d.CalcularSubtotal().ToString("F2")).FontSize(9);
                    }
                });

                // Totales
                double subtotal = pedido.CalcularTotal();
                double descMonto = subtotal * descuento;
                double igv = (subtotal - descMonto) * 0.18;
                double total = subtotal - descMonto;

                col.Item().PaddingTop(10).AlignRight().Column(totales =>
                {
                    totales.Item().Row(r =>
                    {
                        r.ConstantItem(120).AlignRight().Text("Subtotal:").FontSize(10);
                        r.ConstantItem(80).AlignRight()
                         .Text("S/ " + subtotal.ToString("F2")).FontSize(10);
                    });

                    if (descuento > 0)
                    {
                        totales.Item().Row(r =>
                        {
                            r.ConstantItem(120).AlignRight()
                             .Text("Descuento (" + (descuento * 100).ToString("F0") + "%):").FontSize(10);
                            r.ConstantItem(80).AlignRight()
                             .Text("- S/ " + descMonto.ToString("F2"))
                             .FontSize(10).FontColor(Colors.Red.Medium);
                        });
                    }

                    totales.Item().Row(r =>
                    {
                        r.ConstantItem(120).AlignRight()
                         .Text("IGV (18%):").FontSize(10).FontColor(Colors.Grey.Medium);
                        r.ConstantItem(80).AlignRight()
                         .Text("S/ " + igv.ToString("F2")).FontSize(10).FontColor(Colors.Grey.Medium);
                    });

                    totales.Item().BorderTop(1).BorderColor(Colors.Purple.Medium)
                           .PaddingTop(4).Row(r =>
                           {
                               r.ConstantItem(120).AlignRight().Text("TOTAL:").Bold().FontSize(13);
                               r.ConstantItem(80).AlignRight()
                                .Text("S/ " + total.ToString("F2")).Bold().FontSize(13);
                           });
                });

                col.Item().PaddingTop(20).AlignCenter()
                   .Text("Gracias por su preferencia")
                   .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
            });
        }

        private void Pie(IContainer container)
        {
            container.BorderTop(1).BorderColor(Colors.Grey.Lighten2)
                     .PaddingTop(5).Row(row =>
                     {
                         row.RelativeItem()
                            .Text("Documento generado electronicamente")
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                         row.ConstantItem(80).AlignRight()
                            .Text(DateTime.Now.ToString("dd/MM/yyyy"))
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                     });
        }

        private string ObtenerRuta(string tipo, string serie)
        {
            string carpeta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "RestaurantePeruSabor", "Comprobantes");

            Directory.CreateDirectory(carpeta);

            return Path.Combine(carpeta,
                tipo + "_" + serie.Replace("-", "") + "_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
        }
    }
}