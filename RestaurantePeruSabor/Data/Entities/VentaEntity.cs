using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantePeruSabor.Data.Entities
{
    [Table("Ventas")]
    public class VentaEntity
    {
        [Key]
        public int Id { get; set; }

        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public PedidoEntity Pedido { get; set; }

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public ClienteEntity Cliente { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Descuento { get; set; } = 0;

        [Column(TypeName = "decimal(10,2)")]
        public decimal MontoFinal { get; set; }

        [Required, MaxLength(50)]
        public string Comprobante { get; set; } = "";

        public DateTime FechaVenta { get; set; } = DateTime.Now;
    }
}