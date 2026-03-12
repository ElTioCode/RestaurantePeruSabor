using RestaurantePeruSabor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Services
{
    //Guarda los pedidos en archivo TXT
    public static class RegistroPedidos
    {
        private static readonly string ARCHIVO = "pedidos.txt";
        public static void GuardarPedido(Pedido pedido, Mozo mozo, Mesa mesa, double total)
        {
            try
            {
                using (var sw = new StreamWriter(ARCHIVO, true))
                {
                    string fechaHora = DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");
                    sw.WriteLine("====================================");
                    sw.WriteLine($" PEDIDO REGISTRADO: {fechaHora}");
                    sw.WriteLine("====================================");
                    sw.WriteLine($"Mozo: {mozo.Nombre}");
                    sw.WriteLine($"Cliente : {pedido.ClienteNombre}");
                    sw.WriteLine($"Mesa N° : {mesa.Numero}");
                    sw.WriteLine("--------------------------------------");
                    sw.WriteLine(" DETALLE DEL PEDIDO:");

                    foreach (var linea in pedido.GetDetalleTexto())
                    {
                        sw.WriteLine($" {linea}");

                        sw.WriteLine("--------------------------------------");
                        sw.WriteLine($"TOTAL: S/. {total:F2}");
                        sw.WriteLine("====================================");
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al guardar el pedido: {ex.Message}");
            }
        }
    }
}
