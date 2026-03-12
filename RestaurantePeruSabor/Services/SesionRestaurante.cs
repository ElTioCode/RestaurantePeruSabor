using RestaurantePeruSabor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Services
{
    //Clase estática - almacena datos compartidos entre las ventanas
    public static class SesionRestaurante
    {
        public static List<Venta> VentasDelDia { get; } = new List<Venta>();
        //Personal fijo del restaurante
        public static Anfitrion Anfitrion { get; } =
            new Anfitrion("Ana Torres", "DNI", "45678901", 101, "Tarde");
        public static Mozo Mozo { get; } =
            new Mozo("Carlos Ramirez", "DNI", "56789012", 202);
    }
}
