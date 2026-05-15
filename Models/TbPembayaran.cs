using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAUNGJAJAN.Models
{
    [Table("tb_pembayaran")]
    public class TbPembayaran
    {
        [Key]
        [Column("id_pembayaran")]
        public int IdPembayaran { get; set; }

        [Column("id_pesanan")]
        public int IdPesanan { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("id_toko")]
        public int IdToko { get; set; }

        [Column("kode_kwitansi")]
        public string KodeKwitansi { get; set; } = string.Empty;

        [Column("metode_pembayaran")]
        public string MetodePembayaran { get; set; } = "Saldo";

        [Column("jumlah_bayar")]
        public decimal JumlahBayar { get; set; }

        [Column("status_pembayaran")]
        public string StatusPembayaran { get; set; } = "Ditahan";

        [Column("waktu_diteruskan")]
        public DateTime WaktuDiteruskan { get; set; }

        public TbPesanan? Pesanan { get; set; }

        public TbUser? User { get; set; }

        public TbToko? Toko { get; set; }
    }
}