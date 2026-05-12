using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAUNGJAJAN.Models
{
    [Table("tb_pesanan")]
    public class TbPesanan
    {
        [Key]
        [Column("id_pesanan")]
        public int IdPesanan { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("waktu_pesan")]
        public DateTime WaktuPesan { get; set; }

        [Column("total_harga")]
        public decimal TotalHarga { get; set; }

        public ICollection<DetailPesanan> DetailPesanan { get; set; } = new List<DetailPesanan>();
    }
}