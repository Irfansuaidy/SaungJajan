using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAUNGJAJAN.Models
{
    [Table("tb_toko")]
    public class TbToko
    {
        [Key]
        [Column("id_toko")]
        public int IdToko { get; set; }

        [Column("nama_toko")]
        public string NamaToko { get; set; } = string.Empty;

        [Column("pemasukan")]
        public decimal Pemasukan { get; set; }

        public ICollection<TbProduk> Produk { get; set; } = new List<TbProduk>();

        public ICollection<DetailPesanan> DetailPesanan { get; set; } = new List<DetailPesanan>();
    }
}