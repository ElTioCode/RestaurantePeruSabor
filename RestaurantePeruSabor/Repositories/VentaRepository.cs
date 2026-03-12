using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestaurantePeruSabor.Data;
using RestaurantePeruSabor.Data.Entities;
using RestaurantePeruSabor.Models;

namespace RestaurantePeruSabor.Data.Repositories
{
    public class VentaRepository
    {
        public void GuardarVenta(Cliente cliente, Mesa mesa, Pedido pedido,
                                 double descuento, string comprobante)
        {
            using (var db = new RestauranteContext())
            {
                var clienteEntity = new ClienteEntity
                {
                    Nombre = cliente.Nombre,
                    TipoDocumento = cliente.TipoDocumento,
                    NumeroDocumento = cliente.NumeroDocumento
                };
                db.Clientes.Add(clienteEntity);
                db.SaveChanges();

                double total = pedido.CalcularTotal();
                var pedidoEntity = new PedidoEntity
                {
                    ClienteId = clienteEntity.Id,
                    NumeroMesa = mesa.Numero,
                    NombreMozo = "Carlos Rios",
                    Total = (decimal)total
                };
                db.Pedidos.Add(pedidoEntity);
                db.SaveChanges();

                foreach (var d in pedido.Detalles)
                {
                    db.DetallePedidos.Add(new DetallePedidoEntity
                    {
                        PedidoId = pedidoEntity.Id,
                        NombrePlato = d.Plato.Nombre,
                        Precio = (decimal)d.Plato.Precio,
                        Cantidad = d.Cantidad,
                        Subtotal = (decimal)d.CalcularSubtotal()
                    });
                }
                db.SaveChanges();

                double montoFinal = total - (total * descuento);
                var ventaEntity = new VentaEntity
                {
                    PedidoId = pedidoEntity.Id,
                    ClienteId = clienteEntity.Id,
                    Descuento = (decimal)(descuento * 100),
                    MontoFinal = (decimal)montoFinal,
                    Comprobante = comprobante
                };
                db.Ventas.Add(ventaEntity);
                db.SaveChanges();
            }
        }

        public List<VentaEntity> GetVentasHoy()
        {
            using (var db = new RestauranteContext())
            {
                DateTime hoy = DateTime.Today;
                return db.Ventas
                         .Include(v => v.Cliente)
                         .Include(v => v.Pedido)
                         .Where(v => v.FechaVenta.Date == hoy)
                         .ToList();
            }
        }
    }
}