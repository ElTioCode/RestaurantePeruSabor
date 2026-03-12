using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantePeruSabor.Data.Entities
{
    [Table("Pedidos")]
    public class PedidoEntity
    {
        [Key]
        public int Id { get; set; }

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public ClienteEntity Cliente { get; set; }

        public int NumeroMesa { get; set; }

        [Required, MaxLength(100)]
        public string NombreMozo { get; set; } = "";

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.Now;

        public List<DetallePedidoEntity> Detalles { get; set; } = new List<DetallePedidoEntity>();
    }
}