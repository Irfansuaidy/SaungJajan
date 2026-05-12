using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAUNGJAJAN.Models
{
    [Table("detail_pesanan")]
    public class DetailPesanan
    {
        [Key]
        [Column("id_detail")]
        public int IdDetail { get; set; }

        [Column("id_pesanan")]
        public int IdPesanan { get; set; }

        [Column("id_produk")]
        public int IdProduk { get; set; }

        [Column("id_toko")]
        public int IdToko { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [ForeignKey(nameof(IdPesanan))]
        public TbPesanan? Pesanan { get; set; }

        [ForeignKey(nameof(IdProduk))]
        public TbProduk? Produk { get; set; }

        [ForeignKey(nameof(IdToko))]
        public TbToko? Toko { get; set; }
    }
}