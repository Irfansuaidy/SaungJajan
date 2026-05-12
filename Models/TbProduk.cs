using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAUNGJAJAN.Models
{
    [Table("tb_produk")]
    public class TbProduk
    {
        [Key]
        [Column("id_produk")]
        public int IdProduk { get; set; }

        [Column("id_toko")]
        public int IdToko { get; set; }

        [Column("nama_makanan")]
        public string NamaMakanan { get; set; } = string.Empty;

        [Column("jenis")]
        public string Jenis { get; set; } = string.Empty;

        [Column("harga")]
        public decimal Harga { get; set; }

        [Column("stok")]
        public int Stok { get; set; }

        [ForeignKey(nameof(IdToko))]
        public TbToko? Toko { get; set; }

        public ICollection<DetailPesanan> DetailPesanan { get; set; } = new List<DetailPesanan>();
    }
}