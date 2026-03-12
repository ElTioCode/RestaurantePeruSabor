using System.Linq;
using RestaurantePeruSabor.Data;
using RestaurantePeruSabor.Data.Entities;

namespace RestaurantePeruSabor.Data.Repositories
{
    public class UsuarioRepository
    {
        public UsuarioEntity Autenticar(string nombreUsuario, string clave)
        {
            using (var db = new RestauranteContext())
            {
                return db.Usuarios
                         .FirstOrDefault(u => u.NombreUsuario == nombreUsuario
                                           && u.Clave == clave
                                           && u.Activo == true);
            }
        }
    }
}