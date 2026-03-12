using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantePeruSabor.Data.Entities
{
    [Table("Clientes")]
    public class ClienteEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = "";
        [Required, MaxLength(20)]
        public string TipoDocumento { get; set; } = "";
        [Required, MaxLength(20)]
        public string NumeroDocumento { get; set; } = "";
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

    }
}
