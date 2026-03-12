using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantePeruSabor.Data.Entities
{
    [Table("Usuarios")]
    public class UsuarioEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string NombreUsuario { get; set; } = "";

        [Required, MaxLength(100)]
        public string Clave { get; set; } = "";

        [Required, MaxLength(20)]
        public string Rol { get; set; } = "";

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}