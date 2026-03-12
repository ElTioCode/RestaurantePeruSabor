using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public abstract class Persona
    {
        protected string nombre;
        protected string tipoDocumento;
        protected string numeroDocumento;

        public Persona(string nombre, string tipoDocumento, string numeroDocumento)
        {
            this.nombre = (nombre != null && nombre.Length > 2) ? nombre : "Sin nombre";
            this.tipoDocumento = tipoDocumento;
            this.numeroDocumento = (numeroDocumento != null && numeroDocumento.Length >= 8)
                ? numeroDocumento : "00000000";
        }

        public string Nombre  { get { return nombre; } }
        public string TipoDocumento {get {return tipoDocumento;} }
        public string NumeroDocumento { get { return numeroDocumento; } }

        public abstract void MostrarInfo();
    }
}
