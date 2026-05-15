using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAUNGJAJAN.Models
{
    [Table("tb_keranjang")]
    public class TbKeranjang
    {
        [Key]
        [Column("id_keranjang")]
        public int IdKeranjang { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("id_toko")]
        public int IdToko { get; set; }

        [Column("id_produk")]
        public int IdProduk { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("waktu_ditambahkan")]
        public DateTime WaktuDitambahkan { get; set; }

        public TbUser? User { get; set; }

        public TbToko? Toko { get; set; }

        public TbProduk? Produk { get; set; }
        
    }
}