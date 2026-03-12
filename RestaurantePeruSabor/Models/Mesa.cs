using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Mesa
    {
        private int numero;
        private bool disponible;
        public int Numero { get { return numero; } }
        public bool Disponible { get { return disponible; } } 
        public Mesa(int numero)
        {
            this.numero = numero;
            this.disponible = true;
        }
        public void OcuparMesa() { disponible = false; }
        public void LiberarMesa() { disponible = true; }
    }
}
