using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public class Usuario
    {
        public enum Rol
        {  ADMINISTRADOR,
           CAJERO
        }
        public string NombreUsuario { get; set; }
        public string clave;
        public Rol RolUsuario { get; private set; }
        public bool Activo { get; private set; }
        public Usuario (string nombreUsuario, string clave, Rol rol)
        {
            NombreUsuario = nombreUsuario;
            this.clave = clave;
            RolUsuario = rol;
            Activo = true;
        }
        public bool Autenticar(string claveIngresada) => clave == claveIngresada;
    }
}
