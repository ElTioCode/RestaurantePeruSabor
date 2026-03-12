using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantePeruSabor.Data.Entities
{
    [Table("DetallePedidos")]
    public class DetallePedidoEntity
    {
        [Key]
        public int Id { get; set; }

        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public PedidoEntity Pedido { get; set; }

        [Required, MaxLength(100)]
        public string NombrePlato { get; set; } = "";

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }
    }
}