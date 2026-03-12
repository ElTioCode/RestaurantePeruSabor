using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Cliente : Persona
    {
        public Cliente(string nombre, string tipoDocumento, string numeroDocumento)
            :base(nombre, tipoDocumento, numeroDocumento) { }
        public override void MostrarInfo()
        {
            System.Console.WriteLine("Cliente:" + nombre + " | " + tipoDocumento + ": " + numeroDocumento);
        }
        
    }
    
    
}
