using Microsoft.EntityFrameworkCore;
using SAUNGJAJAN.Models;

namespace SAUNGJAJAN.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TbKeranjang> TbKeranjang { get; set; }
        public DbSet<TbUser> TbUser { get; set; }
        public DbSet<TbToko> TbToko { get; set; }
        public DbSet<TbProduk> TbProduk { get; set; }
        public DbSet<TbPesanan> TbPesanan { get; set; }
        public DbSet<DetailPesanan> DetailPesanan { get; set; }
        public DbSet<TbPembayaran> TbPembayaran { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.ToTable("tb_user");
                entity.HasKey(e => e.IdUser);
        
                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .ValueGeneratedOnAdd();
        
                entity.Property(e => e.Saldo)
                    .HasPrecision(18, 2);
            });
        
            modelBuilder.Entity<TbToko>(entity =>
            {
                entity.ToTable("tb_toko");
                entity.HasKey(e => e.IdToko);
        
                entity.Property(e => e.IdToko)
                    .HasColumnName("id_toko")
                    .ValueGeneratedOnAdd();
        
                entity.Property(e => e.Pemasukan)
                    .HasPrecision(18, 2);
            });
        
            modelBuilder.Entity<TbProduk>(entity =>
            {
                entity.ToTable("tb_produk");
                entity.HasKey(e => e.IdProduk);
        
                entity.Property(e => e.IdProduk)
                    .HasColumnName("id_produk")
                    .ValueGeneratedOnAdd();
        
                entity.Property(e => e.Harga)
                    .HasPrecision(18, 2);
        
                entity.HasOne(e => e.Toko)
                    .WithMany(e => e.Produk)
                    .HasForeignKey(e => e.IdToko);
            });
        
            modelBuilder.Entity<TbPesanan>(entity =>
            {
                entity.ToTable("tb_pesanan");
                entity.HasKey(e => e.IdPesanan);
        
                entity.Property(e => e.IdPesanan)
                    .HasColumnName("id_pesanan")
                    .ValueGeneratedOnAdd();
        
                entity.Property(e => e.TotalHarga)
                    .HasPrecision(18, 2);
            });
        
            modelBuilder.Entity<DetailPesanan>(entity =>
            {
                entity.ToTable("detail_pesanan");
                entity.HasKey(e => e.IdDetail);
        
                entity.Property(e => e.IdDetail)
                    .HasColumnName("id_detail")
                    .ValueGeneratedOnAdd();
        
                entity.Property(e => e.Subtotal)
                    .HasPrecision(18, 2);
        
                entity.HasOne(e => e.Pesanan)
                    .WithMany(e => e.DetailPesanan)
                    .HasForeignKey(e => e.IdPesanan);
        
                entity.HasOne(e => e.Produk)
                    .WithMany(e => e.DetailPesanan)
                    .HasForeignKey(e => e.IdProduk);
        
                entity.HasOne(e => e.Toko)
                    .WithMany(e => e.DetailPesanan)
                    .HasForeignKey(e => e.IdToko);
            });

            modelBuilder.Entity<TbPembayaran>(entity =>
            {
                entity.ToTable("tb_pembayaran");
                entity.HasKey(e => e.IdPembayaran);
            
                entity.Property(e => e.IdPembayaran)
                    .HasColumnName("id_pembayaran")
                    .ValueGeneratedOnAdd();
            
                entity.Property(e => e.IdPesanan)
                    .HasColumnName("id_pesanan");
            
                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user");
            
                entity.Property(e => e.IdToko)
                    .HasColumnName("id_toko");
            
                entity.Property(e => e.KodeKwitansi)
                    .HasColumnName("kode_kwitansi");
            
                entity.Property(e => e.MetodePembayaran)
                    .HasColumnName("metode_pembayaran");
            
                entity.Property(e => e.JumlahBayar)
                    .HasColumnName("jumlah_bayar")
                    .HasPrecision(18, 2);
            
                entity.Property(e => e.StatusPembayaran)
                    .HasColumnName("status_pembayaran");
            
                entity.Property(e => e.WaktuDiteruskan)
                    .HasColumnName("waktu_diteruskan");
            
                entity.HasOne(e => e.Pesanan)
                    .WithMany()
                    .HasForeignKey(e => e.IdPesanan);
            
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.IdUser);
            
                entity.HasOne(e => e.Toko)
                    .WithMany()
                    .HasForeignKey(e => e.IdToko);
            });

            modelBuilder.Entity<TbKeranjang>(entity =>
            {
                entity.ToTable("tb_keranjang");

                entity.HasKey(e => e.IdKeranjang);

                entity.Property(e => e.IdKeranjang)
                    .HasColumnName("id_keranjang")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user");

                entity.Property(e => e.IdToko)
                    .HasColumnName("id_toko");

                entity.Property(e => e.IdProduk)
                    .HasColumnName("id_produk");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity");

                entity.Property(e => e.WaktuDitambahkan)
                    .HasColumnName("waktu_ditambahkan");

                entity.HasIndex(e => new { e.IdUser, e.IdToko, e.IdProduk })
                    .IsUnique();

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.IdUser);

                entity.HasOne(e => e.Toko)
                    .WithMany()
                    .HasForeignKey(e => e.IdToko);

                entity.HasOne(e => e.Produk)
                    .WithMany()
                    .HasForeignKey(e => e.IdProduk);
            });
        }
    }
}