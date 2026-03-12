using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Anfitrion : Empleado
    {
        public string turno;
        public string Turno {get { return turno; } }
        public Anfitrion(string nombre, string tipoDocumento, string numeroDocumento, int codigoEmpleado, string turnoParam)
            : base(nombre, tipoDocumento, numeroDocumento, codigoEmpleado)
        {
            turno = turnoParam;
        }

        public void RecibirCliente(Cliente cliente)
        {
            System.Console.WriteLine("Anfitrion" + nombre + "recibe a: " + cliente.Nombre);

        }
        public void AsignarMesa(Cliente cliente, Mesa mesa)
        {
            if (mesa.Disponible)
            {
                mesa.OcuparMesa();
                System.Console.WriteLine("Mesa" +  mesa.Numero + " asignada a " + cliente.Nombre);
            }
            else
            {
                System.Console.WriteLine("la mesa no esta disponible");
            }
        }

        public override void MostrarInfo()
        {
            System.Console.WriteLine("Anfitrion:" + nombre +" | Turno:" + turno);
        }

    }
}
