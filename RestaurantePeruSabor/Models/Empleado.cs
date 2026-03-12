using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantePeruSabor.Models
{
    public abstract class Empleado : Persona
    {
        protected int codigoEmpleado;

        public Empleado(String nombre, string tipoDocumento, string numeroDocumento, int codigoEmpleado)
            : base(nombre, tipoDocumento, numeroDocumento)
        {
            this.codigoEmpleado = (codigoEmpleado > 0) ? codigoEmpleado : 0;
        }
        public int CodigoEmpleado => codigoEmpleado;
    }
}
